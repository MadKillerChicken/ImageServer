namespace ImageServer.Models
{
    /// <summary>
    /// Basic main content of a media entry.
    /// </summary>
    internal abstract class MediaSubject
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

    }
}
