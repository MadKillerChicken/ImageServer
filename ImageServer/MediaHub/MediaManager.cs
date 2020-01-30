using MediaHub.EF;
using MediaHub.EventArguments;
using MediaHub.Models;
using MediaHub.Models.Containers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MediaHub
{
    public class MediaManager
    {

        public MediaScanner Scanner { get; private set; } = new MediaScanner();
        
        public MediaManager() 
        {
            Scanner.ContainersChanged += Scanner_ContainersChanged;
        }

        #region Events

        private void Scanner_ContainersChanged(object sender, MediaContainerEventArgs e)
        {
            if (e.change == MediaContainerEventArgs.ContainersChangeAdded) {
                MediaContainer container = e.containers.FirstOrDefault();
                if (container != null) {
                    Scanner.QueueScanEntry(new ScannerQueueEntry(container.Url, container.Filter, true));
                }
            } else if (e.change == MediaContainerEventArgs.ContainersChangeRemoved) {
                // Do something here?
            }
        }

        #endregion Events

        public void Test()
        {
            if (!Scanner.GetMediaContainers().Any()) {
                FileSystemMediaContainer container = new FileSystemMediaContainer(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Camera Roll"); //,FileTypes.Image
                Scanner.AddMediaContainer(container);
                Console.WriteLine("Container added.");
                Scanner.ScanCompleted += Scanner_ScanCompleted;
            } else {
                Scanner_ScanCompleted(this, new EventArgs());
            }
        }

        private void Scanner_ScanCompleted(object sender, EventArgs e)
        {
            void ListChildren(int depth, DirectoryItem parentDir)
            {
                if (parentDir.Children.Any()) {
                    foreach(var child in parentDir.Children) {
                        Console.WriteLine(new string('\t', depth) + child.Name + " [" + child.Id + "]");
                        if (child is DirectoryItem dir) {
                            ListChildren(depth + 1, dir);
                        }
                    }
                }
            }
            
            Console.WriteLine("[Containers]");
            Scanner.GetMediaContainers().ToList().ForEach(c => Console.WriteLine(c.Url + "\t" + c.Filter));
            Console.WriteLine();

            using (var ctx = new MediaContext())
            {
                var root = ctx.Directories.FirstOrDefault(d => !d.Parents.Any());
                ListChildren(0, root);
                Console.WriteLine();

                var entries = ctx.Items.ToList();
                foreach (var entry in entries)
                {
                    if (entry is DirectoryItem dir) {

                        Console.WriteLine("[Directory: " + dir.Name + "]");
                        Console.WriteLine("Location: " + dir.Location);
                        Console.WriteLine("Parents: " + dir.Parents.Count);
                        if (dir.Parents.Any()) {
                            dir.Parents.ToList().ForEach(p => Console.WriteLine("\t" + p.Name));
                        }
                        Console.WriteLine("Children: " + dir.Children.Count);
                        if (dir.Children.Any()) {
                            dir.Children.ToList().ForEach(c => Console.WriteLine("\t" + c.Name));
                        }

                    } else if (entry is FileItem file) {

                        Console.WriteLine("[File: " + file.Name + "]");
                        Console.WriteLine("Location: " + file.Location);
                        Console.WriteLine("Parents: " + file.Parents.Count);
                        if (file.Parents.Any()) {
                            file.Parents.ToList().ForEach(p => Console.WriteLine("\t" + p.Name));
                        }

                    }

                    Console.WriteLine();
                }



                // +++++++++++++++++++++++++++++++++++++++++++
                /*Console.WriteLine("\r\n[Sets]");
                foreach (DirectoryItem directory in ctx.Entries.Where(e => e is DirectoryItem).Select(e => e).ToList())
                {
                    //Console.WriteLine("[" + directory.Name + "]" + "\t <" + directory.IsContainer + ">\t" + directory.Location);

                    if (true) // directory.HasChildren
                    {
                        foreach (var entry in directory.Children)
                        {
                            Console.WriteLine(entry.Name + "\t" + entry.Location);
                        }
                    }

                    if (!directory.Parents.Any())
                    {
                        // DOESNT WORK
                    }
                }*/
            }

            /*Console.WriteLine("\r\n[Entries]");
            foreach (var entry in GetEntries())
            {
                Console.WriteLine(entry.Name + "\t" + entry.Location);
            }*/

        }

        public IEnumerable<IFileSystemItem> GetEntries(Func<FileSystemItem, bool> predicate = null)
        {
            using (var ctx = new MediaContext()) {
                IEnumerable<FileSystemItem> dataSet =
                    predicate == null
                        ? ctx.Items
                            .Select(e => e)
                        : ctx.Items
                            .Where(predicate)
                            .Select(e => e);
                foreach (var entry in dataSet) {
                    yield return entry;
                }
            }
        }

        public IEnumerable<IDirectoryItem> GetSets(Func<DirectoryItem, bool> predicate = null)
        {
            using (var ctx = new MediaContext()) {
                IEnumerable<DirectoryItem> dataSet =
                    predicate == null
                        ? ctx.Directories
                            .Select(e => e)
                        : ctx.Directories
                            .Where(predicate)
                            .Select(e => e);
                foreach (var entry in dataSet) {
                    yield return entry;
                }
            }
        }

        #region Categories

        public Category AddCategory(Category newCategory)
        {
            using (var ctx = new MediaContext()) {
                var result = ctx.Categories.Add(newCategory);
                ctx.SaveChanges();
                return result;
            }
        }

        public void RemoveCategory(Category categoryToRemove)
        {
            using (var ctx = new MediaContext()) {
                ctx.Categories.Remove(categoryToRemove);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<ICategory> GetCategories()
        {
            using (var ctx = new MediaContext()) {
                foreach (var category in ctx.Categories) {
                    yield return category;
                }
            }
        }

        public void AddCategoryToItem(IFileSystemItem item, ICategory category) =>
            AddCategoriesToItem(item.Id, new ICategory[] { category });

        public void AddCategoriesToItem(IFileSystemItem item, IEnumerable<ICategory> categories) => 
            AddCategoriesToItem(item.Id, categories);

        public void AddCategoryToItem(string itemId, ICategory category) =>
            AddCategoriesToItem(itemId, new ICategory[] { category });

        public void AddCategoriesToItem(string itemId, IEnumerable<ICategory> categories)
        {
            using (var ctx = new MediaContext()) {
                FileSystemItem item = ctx.Items.FirstOrDefault(e => e.Id == itemId);
                if (item != null) {
                    foreach(var categoryId in categories.Select(e => e.Id)) {
                        Category categoryToAdd = ctx.Categories.FirstOrDefault(c => c.Id == categoryId);
                        if (categoryToAdd != null) {
                            item.Categories.Add(categoryToAdd);
                        }
                    }
                }
            }
        }

        public void RemoveCategoryToItem(IFileSystemItem item, ICategory category) =>
            RemoveCategoriesToItem(item.Id, new ICategory[] { category });

        public void RemoveCategoriesToItem(IFileSystemItem item, IEnumerable<ICategory> categories) =>
            RemoveCategoriesToItem(item.Id, categories);

        public void RemoveCategoryToItem(string itemId, ICategory category) =>
            RemoveCategoriesToItem(itemId, new ICategory[] { category });

        public void RemoveCategoriesToItem(string itemId, IEnumerable<ICategory> categories)
        {
            using (var ctx = new MediaContext()) {
                FileSystemItem item = ctx.Items.FirstOrDefault(e => e.Id == itemId);
                if (item != null) {
                    foreach(var categoryId in categories.Select(e => e.Id)) {
                        Category categoryToRemove = ctx.Categories.FirstOrDefault(c => c.Id == categoryId);
                        if (categoryToRemove != null) {
                            item.Categories.Remove(categoryToRemove);
                        }
                    }
                }
            }
        }

        #endregion Categories

        #region Subjects

        public Subject AddSubject(Subject newSubject)
        {
            using (var ctx = new MediaContext()) {
                var result = ctx.Subjects.Add(newSubject);
                ctx.SaveChanges();
                return result;
            }
        }

        public void RemoveSubject(Subject subjectToRemove)
        {
            using (var ctx = new MediaContext()) {
                ctx.Subjects.Remove(subjectToRemove);
                ctx.SaveChanges();
            }
        }

        public IEnumerable<ISubject> GetSubjects()
        {
            using (var ctx = new MediaContext()) {
                foreach (var subject in ctx.Subjects) {
                    yield return subject;
                }
            }
        }

        public void AddSubjectToItem(IFileSystemItem item, ISubject subject) =>
            AddSubjectsToItem(item.Id, new ISubject[] { subject });

        public void AddSubjectsToItem(IFileSystemItem item, IEnumerable<ISubject> subjects) =>
            AddSubjectsToItem(item.Id, subjects);

        public void AddSubjectToItem(string itemId, ISubject subject) =>
            AddSubjectsToItem(itemId, new ISubject[] { subject });

        public void AddSubjectsToItem(string itemId, IEnumerable<ISubject> subjects)
        {
            using (var ctx = new MediaContext()) {
                FileSystemItem item = ctx.Items.FirstOrDefault(e => e.Id == itemId);
                if (item != null) {
                    foreach(var subjectId in subjects.Select(e => e.Id)) {
                        Subject subjectToAdd = ctx.Subjects.FirstOrDefault(c => c.Id == subjectId);
                        if (subjectToAdd != null) {
                            item.Subjects.Add(subjectToAdd);
                        }
                    }
                }
            }
        }

        public void RemoveSubjectToItem(IFileSystemItem item, ISubject subject) =>
            RemoveSubjectsToItem(item.Id, new ISubject[] { subject });

        public void RemoveSubjectsToItem(IFileSystemItem item, IEnumerable<ISubject> subjects) =>
            RemoveSubjectsToItem(item.Id, subjects);

        public void RemoveSubjectToItem(string itemId, ISubject subject) =>
            RemoveSubjectsToItem(itemId, new ISubject[] { subject });

        public void RemoveSubjectsToItem(string itemId, IEnumerable<ISubject> subjects)
        {
            using (var ctx = new MediaContext()) {
                FileSystemItem item = ctx.Items.FirstOrDefault(e => e.Id == itemId);
                if (item != null) {
                    foreach(var subjectId in subjects.Select(e => e.Id)) {
                        Subject subjectToRemove = ctx.Subjects.FirstOrDefault(c => c.Id == subjectId);
                        if (subjectToRemove != null) {
                            item.Subjects.Remove(subjectToRemove);
                        }
                    }
                }
            }
        }

        #endregion Subjects

    }

}
