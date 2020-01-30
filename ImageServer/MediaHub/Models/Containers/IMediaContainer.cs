using System.Collections.Generic;

namespace MediaHub.Models.Containers
{
    public interface IMediaContainer
    {
        IReadOnlyCollection<FileTypes> Filter { get; }
        int Id { get; }
        string Url { get; }

        bool GetIsScanning();
        void SetIsScanning();
    }
}