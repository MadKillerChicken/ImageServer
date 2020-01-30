using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaHub.Models.Containers
{
    /// <summary>
    /// References an abstract container of media source objects (usually a FileSystem object like a dir)
    /// </summary>
    public abstract class MediaContainer : IMediaContainer
    {
        private readonly object _lock = new object();

        private bool _isScanning = false;

        public int Id { get; set; }

        public string Url { get; set; }

        // FILTER IS NOT SAVED TO DB!!!!

        public HashSet<FileTypes> Filter { get; set; }
        IReadOnlyCollection<FileTypes> IMediaContainer.Filter => Filter;

        public bool GetIsScanning()
        {
            lock (_lock) {
                return _isScanning;
            }
        }

        public void SetIsScanning()
        {
            lock (_lock) {
                _isScanning = false;
            }
        }

        public MediaContainer(string url, params FileTypes[] filters)
        {
            Url = (!string.IsNullOrEmpty(url))
                ? new Uri(url).AbsoluteUri
                : throw new ArgumentNullException(nameof(url));

            Filter = new HashSet<FileTypes>(filters ?? new FileTypes[0]);
        }

    }
}
