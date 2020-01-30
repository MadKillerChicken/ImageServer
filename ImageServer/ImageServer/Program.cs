using MediaHub;
using MediaHub.Models;
using MediaHub.Models.Containers;
using System;
using System.Linq;
using System.Threading;

namespace ImageServer
{

    class Program
    {
        private static MediaManager _mm;

        static void Main(string[] args)
        {
            Console.WriteLine("HELLO");
            Console.ReadLine();

            _mm = new MediaManager();
            _mm.Test();
            /*
            if (!_mm.GetCategories().Any()) {
                _mm.AddCategory(new EntryCategory() { Name = "Category_1" });
                _mm.AddCategory(new EntryCategory() { Name = "Category_2" });
            }

            if (!_mm.Scanner.GetMediaContainers().Any()) {
                FileSystemMediaContainer container = new FileSystemMediaContainer(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\Camera Roll",
                    "*.jpg;*.jpeg;*.png");
                _mm.Scanner.AddMediaContainer(container);
                Console.WriteLine("Container added.");
            }

            Console.WriteLine("[Containers]");
            _mm.Scanner.GetMediaContainers().ToList().ForEach(c => Console.WriteLine(c.Url + "\t" + c.Filter));

            Console.WriteLine("\r\n[Sets]");
            foreach(var mediaSet in _mm.GetSets().ToList()) {
                Console.WriteLine("[" + mediaSet.Name + "]" + "\t <" + mediaSet.IsFsObject + ">\t" + mediaSet.Location);

                if (mediaSet.Children != null) {
                    foreach (var entry in _mm.GetEntries(e => mediaSet.Children.Select(c => c.Id).Contains(e.Id))) {
                        Console.WriteLine(entry.Name + "\t" + entry.Location);
                    }
                }
            }

            Console.WriteLine("\r\n[Entries]");
            foreach (var entry in _mm.GetEntries()) {
                Console.WriteLine(entry.Name + "\t" + entry.Location);
            }
            */

            Console.WriteLine();
            Console.WriteLine("Waiting for data...");
            Console.ReadLine();

        }
    }

}
