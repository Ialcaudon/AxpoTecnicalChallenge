using System.Globalization;
using System.Text;
using PowerTradeApp.Models;
using PowerTradeApp.Services;

namespace PowerTradeApp.Helpers;

public class CsvGenerator : ICsvGenerator
{

    private readonly string _separator = ";";
    private readonly CultureInfo _csvCulture = CultureInfo.InvariantCulture; 

    public void GenerateCsv(string filePath, IEnumerable<PowerPosition> powerPositions)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var csvContent = new StringBuilder();
        
        csvContent.AppendLine("datetime" + _separator + "Volume");
        
        foreach (var position in powerPositions)
        {
            string line = $"{position.DateTimeUtc.ToString("yyyy-MM-ddTHH:mm:ssZ", _csvCulture)}{_separator}{position.Volume.ToString("F", _csvCulture)}";
            csvContent.AppendLine(line);
        }
        
        File.WriteAllText(filePath, csvContent.ToString());
    }
    
    public string GenerateCsvFileName(DateTime dayAheadDate)
    {
        string dayAheadStr = dayAheadDate.ToString("yyyyMMdd");
        string extractionTimeStr = DateTime.UtcNow.ToString("yyyyMMddHHmm");
        return $"PowerPosition_{dayAheadStr}_{extractionTimeStr}.csv";
    }
}