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

                Console.WriteLine("Sets:");
                ctx.MediaEntrySets
                    .ToList()
                    .ForEach(e => Console.WriteLine(e.Name));

                Console.WriteLine();

                Console.WriteLine("Entries:");
                ctx.MediaEntries
                    .ToList()
                    .ForEach(e => Console.WriteLine(e.Name));

                Console.WriteLine();

                var testSet = ctx.MediaEntrySets.OrderBy(e => e.Name).Skip(1).FirstOrDefault();
                Console.WriteLine("Test-Set-Children [" + testSet.Name + "]:");
                testSet?.Children?.ToList().ForEach(e => Console.WriteLine(e.Name));

            }
            Console.WriteLine();
            Console.WriteLine("Demo completed.");
            Console.ReadLine();

        }

    }

    internal class MediaManager
    {

        //private void LoadDirectoryToMedia

    }
}
