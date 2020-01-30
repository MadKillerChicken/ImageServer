using MediaHub.Models.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.EventArguments
{
    public class MediaContainerEventArgs : EventArgs
    {
        public const string ContainersChangeAdded = "ContainerAdded";
        public const string ContainersChangeRemoved = "ContainerRemoved";

        public readonly IEnumerable<MediaContainer> containers;

        public readonly string change;

        public MediaContainerEventArgs(IEnumerable<MediaContainer> containers, string change)
        {
            this.containers = containers;
            this.change = change;
        }

    }
}
