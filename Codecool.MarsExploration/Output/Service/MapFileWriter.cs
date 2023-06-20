using Codecool.MarsExploration.MapElements.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.Output.Service
{
    
    public class MapFileWriter : IMapFileWriter
    {
        public void WriteMapFile(Map map, string file)
        {
            try
            {
                using StreamWriter writer = File.CreateText(file);
                writer.WriteLine(map);
                Console.WriteLine($"File succesfully written to file: {file}\n");
            }
            catch (IOException exception)
            {
                Console.WriteLine($"Error - Couldn't write to file {file}: ");
                Console.WriteLine($"{exception.Message}\n");
            }
           
        }
    }
}

