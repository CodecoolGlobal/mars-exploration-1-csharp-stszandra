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
                if (map == null)
                {
                    throw new IOException("Invalid map provided.");
                }
                using StreamWriter writer = File.CreateText(file);
                writer.WriteLine(map);
                Console.WriteLine($"File successfully written to file: {file}\n");
            }
            catch (IOException exception)
            {
                throw; // Re-throw the IOException
            }

        }
    }
}

