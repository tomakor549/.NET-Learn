using System;
using System.IO;
using System.Collections.Generic;

namespace files_module
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var storesDirectory = Path.Combine(currentDirectory, "stores");

            var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            Directory.CreateDirectory(salesTotalDir);

            var salesFiles = FindFiles(storesDirectory);

            File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);
        }

        static IEnumerable<string> FindFiles(string dirName)
        {
            List<string> salesFile = new List<string>();
            var foundFiles = Directory.EnumerateFiles(dirName, "*", SearchOption.AllDirectories);

            foreach (var file in foundFiles)
            {
                if (file.EndsWith("sales.json"))
                    salesFile.Add(file);
            }
            return salesFile;
        }

    }
}
