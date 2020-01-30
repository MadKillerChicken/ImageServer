using MediaHub.Models;
using System.Collections;
using System.Collections.Generic;

namespace MediaHub
{
    public class ScannerQueueEntry
    {
        public string Url { get; }

        public IEnumerable<FileTypes> Filter { get; }

        public bool Recurse { get; }

        public ScannerQueueEntry(string url, IEnumerable<FileTypes> filter, bool recurse = false)
        {
            Url = url;
            Filter = filter;
            Recurse = recurse;
        }
    }

}
