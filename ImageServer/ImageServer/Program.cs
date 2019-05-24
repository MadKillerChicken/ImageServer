using System;
using System.Collections.Generic;
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



    internal abstract class MediaSet
    {

        public abstract IEnumerable<MediaSubject> Subjects { get; protected set; }

    }

    internal class MediaManager
    {

        //private void LoadDirectoryToMedia

    }
}
