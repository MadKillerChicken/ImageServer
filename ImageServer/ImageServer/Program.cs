using ImageServer.EF;
using ImageServer.Models;
using System;
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

            using (var ctx = new MediaContext()) {

                Console.WriteLine("Dirs:");
                ctx.Entries
                    .Where(e => e is ContainerEntry)
                    .ToList()
                    .ForEach(e => Console.WriteLine(e.Name));

                Console.WriteLine();
                Console.WriteLine("Entries:");
                ctx.Entries
                    .Where(e => !(e is ContainerEntry))
                    .ToList()
                    .ForEach(e => Console.WriteLine(e.Name));

                var asd = ctx.Entries.FirstOrDefault(e => e.Parent == null);

            }
            Console.WriteLine("Demo completed.");
            Console.ReadLine();

        }

    }

    internal class MediaManager
    {

        //private void LoadDirectoryToMedia

    }
}
