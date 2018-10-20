using Lab08_Linq.Classes;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Lab08_Linq
{
    public class Program
    {
        /// <summary>
        /// Read and parse json file into the defined object Feature Collection
        /// </summary>
        /// <param name="jsonFile">File name to read</param>
        /// <returns>FeatureCollection object with data parsed according its structure</returns>
        public static FeatureCollection ParseFeaturesFile(string jsonFile)
        {
            string buffer;
            using (StreamReader sr = File.OpenText(jsonFile))
            {
                buffer = sr.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<FeatureCollection>(buffer);
        }

        public static void Main(string[] args)
        {

            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight + Console.WindowHeight / 2);
            string filePath = "../../../../data.json";
            var data = ParseFeaturesFile(filePath);

            // Get all features from an array
            var features = data.Features.ToList<Feature>();

            // Get all Neighborhoods in data
            var allNeighborhoods = features.Select(x => x.Properties.Neighborhood);

            // Group, count and order neighborhoods by count desc (thus get rid of duplicates)
            var allNeighborhoodsGrouped = allNeighborhoods
                .GroupBy(x => x, (key, val) => new { Neighborhood = key, Cnt = val.Count() })
                .OrderByDescending(x => x.Cnt);
            Console.WriteLine("All neighborhoods:");

            // Output neighborhoods to console
            allNeighborhoodsGrouped
                .ToList()
                .ForEach(elm => Console.WriteLine(elm.Neighborhood.PadRight(40, ' ') + elm.Cnt));
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("All neighborhoods whithout blank data:");

            // Filter out all blank neighborhood names
            allNeighborhoodsGrouped
                .Where(x => x.Neighborhood != string.Empty)
                .ToList()
                .ForEach(elm => Console.WriteLine(elm.Neighborhood));
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("All in one line");

            // One-liner
            features
                .Where(x => x.Properties.Neighborhood != string.Empty)
                .Select(x => x.Properties.Neighborhood)
                .GroupBy(x => x, (key, val) => new { Neighborhood = key })
                .ToList()
                .ForEach(x => Console.WriteLine(x.Neighborhood));
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("All in one line again but with LINQ");

            // One-liner as LINQ without lambda
            var neighborhoods = from feature in features
                                where feature.Properties.Neighborhood != string.Empty
                                group feature by feature.Properties.Neighborhood into newGroup
                                orderby newGroup.Key
                                select newGroup.Key;
            neighborhoods.ToList().ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
        }
    }
}
