namespace ImageServer.Models
{
    internal abstract class MediaCategory
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }
    }
}
