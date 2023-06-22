using Codecool.MarsExploration.MapElements.Model;
using Codecool.MarsExploration.Output.Service;
using System.IO;

namespace MarsExplorationTests;
[TestFixture]

public class MapFileWriterTest
{
    private IMapFileWriter fileWriter;
    private readonly string?[,] representation = new string?[2, 2] { { "A", "B" }, { "C", "D" } };
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    private void SetFileAsReadOnly(string path)
    {
        File.SetAttributes(path, FileAttributes.ReadOnly);
    }
    
    [SetUp]
    public void SetUp()
    {
        fileWriter = new MapFileWriter();
        
    }

    [Test]
    public void TestFileWriter_ValidMap()
    {
        Map map = new Map(representation);
        string filePath = "TestMaps/testMap1.txt";
        fileWriter.WriteMapFile(map, filePath);
        
        Assert.That(File.Exists(filePath));
    }

    
    [Test]
    public void WriteMapFile_IOException_CorrectErrorMessagePrinted()
    {
        string file = "TestMaps/testMap2.txt";

        Map map = null;

        // Act and Assert
        Assert.Throws<IOException>(() => fileWriter.WriteMapFile(map, file));
    }
}
