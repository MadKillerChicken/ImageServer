namespace ImageServer
{
    /// <summary>
    /// Basic main content of a media entry.
    /// </summary>
    internal abstract class MediaSubject
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

    }
}
