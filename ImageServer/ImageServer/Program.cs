using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HELLO");
            Console.ReadLine();
        }
    }

    internal class ImageEntry : MediaEntry
    {
        public override IEnumerable<MediaSubject> Subjects { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
    }

    internal abstract class MediaEntry
    {
        public abstract IEnumerable<MediaSubject> Subjects { get; protected set; }

        public FileInfo Location { get; protected set; } 
    }

    /// <summary>
    /// Subject displayed in an image
    /// </summary>
    internal class ImageSubject : MediaSubject
    {

    }

    /// <summary>
    /// Basic main content of a media entry.
    /// </summary>
    internal abstract class MediaSubject
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

    }
}
