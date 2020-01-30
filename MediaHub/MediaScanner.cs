using MediaHub.EF;
using MediaHub.EventArguments;
using MediaHub.Models;
using MediaHub.Models.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace MediaHub
{
    public class MediaScanner : IDisposable
    {
        private Thread _scannerThread;
        private readonly AutoResetEvent _scannerEvent = new AutoResetEvent(false);
        private readonly object _queueLock = new object();
        private readonly List<ScannerQueueEntry> _queue = new List<ScannerQueueEntry>();

        #region EventHandlers

        /// <summary>
        /// Media Container was added or removed
        /// </summary>
        public event EventHandler<MediaContainerEventArgs> ContainersChanged;

        public event EventHandler<EventArgs> ScanCompleted;

        #endregion EventHandlers

        public MediaScanner() 
        {
            _scannerThread = new Thread(ScannerThread) {
                Name = "Scanner",
                IsBackground = true
            };
            _scannerThread.Start();
        }

        public void QueueScanEntry(ScannerQueueEntry entry, bool urgent = false)
        {
            lock (_queueLock) {
                if (!urgent) {
                    _queue.Add(entry);
                } else {
                    _queue.Insert(0, entry);
                }
            }
            _scannerEvent.Set();
        }

        private void ScannerThread()
        {
            try {
                bool firstRun = true;
                WeakReference _hostRef = new WeakReference(this);
                while (_hostRef.IsAlive) {

                    // Notify that a scan cycle completed
                    bool scanCompleted = false;
                    lock (_queueLock) {
                        scanCompleted = !firstRun && _queue.Count < 1;
                    }
                    if (scanCompleted) {
                        scanCompleted = false;
                        var eh = ScanCompleted;
                        eh?.Invoke(this, new EventArgs());
                    }
                    firstRun = false;

                    _scannerEvent.WaitOne(); // Wait for signal to process data
                    ScannerQueueEntry entry = null;
                    lock (_queueLock) { entry = _queue[0]; _queue.RemoveAt(0); }
                    if (entry != null) {

                        // Scan entry
                        CreateEntriesFromFileSystem(new DirectoryInfo(new Uri(entry.Url).LocalPath), entry.Filter, entry.Recurse);

                    }

                }
            } catch (Exception ex) {

            }
        }

        // Needs performance improvements
        private void CreateEntriesFromFileSystem(DirectoryInfo dir, IEnumerable<FileTypes> filter, bool recurse = false)
        {
            try {

                // Get all entries
                IEnumerable<FileSystemInfo> fsEntries = dir.GetFileSystemInfos();
                // Create filtering hashset
                HashSet<string> filterExtensions = FileExtension.GetFsExtensionFilter(filter?.ToArray());

                using (var ctx = new MediaContext()) {
                    // Find a possible parent entry for the current dir
                    string currentParentId = FileSystemItem.GenerateId(dir.Parent.FullName);

                    // Create the currently scanned dir as object
                    var currentDirObj = ctx.Items.Add(FileSystemItem.CreateFromFileSystemInfo(dir)) as DirectoryItem;
                    // if we have a current parent, add it
                    if (ctx.Items.FirstOrDefault(e => e.Id == currentParentId) is DirectoryItem currentDirParent) {
                        currentDirObj.Parents.Add(currentDirParent);
                    }

                    IEnumerable<FileSystemInfo> fsFileEntries =
                        fsEntries.Where(e =>
                            !File.GetAttributes(e.FullName).HasFlag(FileAttributes.Directory) &&
                            (!filterExtensions.Any() ||
                            filterExtensions.Contains(e.Extension)));
                    if (fsFileEntries.Any()) {
                        foreach (var fsFileEntry in fsFileEntries) {
                            FileSystemItem currentEntryObj = ctx.Items.Add(
                                FileSystemItem.CreateFromFileSystemInfo(fsFileEntry));
                            currentEntryObj.Parents.Add(currentDirObj);
                            currentDirObj.Children.Add(currentEntryObj);
                        }
                    }

                    ctx.SaveChanges();
                }

                // Recurse sub-dirs
                if (recurse) {
                    foreach (var fsDir in fsEntries.Where(e => File.GetAttributes(e.FullName).HasFlag(FileAttributes.Directory))) {
                        CreateEntriesFromFileSystem(new DirectoryInfo(fsDir.FullName), filter, recurse);
                    }
                }

            } catch (Exception ex) {

            }
        }



        #region Events

        /// <summary>
        /// Called when containers are added
        /// </summary>
        private void OnContainersAdded(IEnumerable<MediaContainer> containersAdded)
        {
            var eventHandler = ContainersChanged;
            eventHandler?.Invoke(this, new MediaContainerEventArgs(containersAdded, MediaContainerEventArgs.ContainersChangeAdded));
        }

        /// <summary>
        /// Called when containers are removed
        /// </summary>
        private void OnContainersRemoved(IEnumerable<MediaContainer> containersRemoved)
        {
            var eventHandler = ContainersChanged;
            eventHandler?.Invoke(this, new MediaContainerEventArgs(containersRemoved, MediaContainerEventArgs.ContainersChangeRemoved));
        }

        #endregion Events

        #region Containers

        public void AddMediaContainer(MediaContainer containerToAdd)
        {
            using (var ctx = new MediaContext())
            {
                ctx.Containers.Add(containerToAdd);
                OnContainersAdded(new MediaContainer[] { containerToAdd });
                ctx.SaveChanges();
            }
        }

        public void RemoveMediaContainer(int idOfContainerToRemove)
        {
            using (var ctx = new MediaContext())
            {
                var container = ctx.Containers.Find(idOfContainerToRemove);
                if (container != null) {
                    ctx.Containers.Remove(container);
                    OnContainersRemoved(new MediaContainer[] { container });
                    ctx.SaveChanges();
                }
            }
        }

        public IEnumerable<IMediaContainer> GetMediaContainers()
        {
            using (var ctx = new MediaContext()) {
                foreach(var container in ctx.Containers) {
                    yield return container;
                }
            }
        }

        #endregion Containers

        public void Dispose()
        {
            _scannerEvent.Set();

            _scannerEvent.Dispose();
        }


    }

}
