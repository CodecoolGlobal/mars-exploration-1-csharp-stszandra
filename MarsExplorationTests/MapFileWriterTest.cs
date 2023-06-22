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
    public void TestFileWriter_Catch()
    {
        string filePath = "ReadOnly.txt";
        string?[,] representation2 = new string?[2, 2] { { "A", "B" }, { "C", "D" } };;
        Map map = new Map(representation2);
        SetFileAsReadOnly(filePath);

        Assert.That(() => fileWriter.WriteMapFile(map, filePath), Throws.TypeOf<IOException>().With.Message.Contains("Couldn't write to file"));
    }
}