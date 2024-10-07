using System.Net;
using FluentAssertions;
using PowerTradeApp.Helpers;
using PowerTradeApp.Services;
using PowerTradeApp.Tests.Fixtures;

namespace PowerTradeApp.Tests.Services;

[TestFixture]
[TestOf(typeof(CsvGenerator))]
public class CsvGeneratorTest
{
    private readonly string testPath = "../../../TestData/";

    [Test]
    public void GenerateCsv_CreatesCorrectFileAndContent()
    {
        var csvGenerator = new CsvGenerator();
        var powerPositions = PowerTradeFixture.GeneratePowerPositions(DateTime.Now, [10, 150, 88, 66, 99]);
        string tempFilePath = Path.Combine(testPath, "TestCsvFile.csv");
        csvGenerator.GenerateCsv(tempFilePath,powerPositions);
        
        File.Exists(tempFilePath).Should().BeTrue();
        var content = File.ReadAllLines(tempFilePath);
        content.Length.Should().Be(6);
        content[0].Should().Be("datetime;Volume");
        File.Delete(tempFilePath);
    }
    
    [Test]
    public void GenerateCsv_FilePathDirectoryIsCreated()
    {
        var csvGenerator = new CsvGenerator();
        var powerPositions = PowerTradeFixture.GeneratePowerPositions(DateTime.Now, [10, 150, 88, 66, 99]);
        string directoryPath = Path.Combine(Path.GetTempPath(), "TestDirectory");
        string tempFilePath = Path.Combine(directoryPath, "TestCsvFile.csv");


        csvGenerator.GenerateCsv(tempFilePath, powerPositions);
        
        Directory.Exists(directoryPath).Should().BeTrue();
        Directory.Delete(directoryPath, true);
    }

    [Test]
    public void GenerateCsv_GivenEmptyPositions_DoesNotThrowException()
    {
        var csvGenerator = new CsvGenerator();
        var powerPositions = PowerTradeFixture.GeneratePowerPositions(DateTime.Now, []);
        string tempFilePath = Path.Combine(Path.GetTempPath(), "EmptyCsvFile.csv");

        Action  act = ()=> csvGenerator.GenerateCsv(tempFilePath, powerPositions);
        act.Should().NotThrow();
        Assert.True(File.Exists(tempFilePath), "El archivo CSV no fue creado.");
        
        File.Delete(tempFilePath);
    }
}