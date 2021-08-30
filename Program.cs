using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace files_module
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string storesDirectory = Path.Combine(currentDirectory, "stores");

            string salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            Directory.CreateDirectory(salesTotalDir);

            var salesFiles = FindFiles(storesDirectory);

            var salesTotal = CalculateSalesTotal(salesFiles);

            File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");
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

        static double CalculateSalesTotal(IEnumerable<string> salesFiles)
        {
            double salesTotal = 0;

            foreach(var file in salesFiles)
            {
                string salesJson = File.ReadAllText(file);

                SalesData data = JsonConvert.DeserializeObject<SalesData>(salesJson);

                salesTotal+=data.Total;
            }

            return salesTotal;
        }

        class SalesData
        {
            public double Total { get; set; }
        }

    }
}
