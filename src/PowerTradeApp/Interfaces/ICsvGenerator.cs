using PowerTradeApp.Models;

namespace PowerTradeApp.Services;

public interface ICsvGenerator
{
    void GenerateCsv(string filePath, IEnumerable<PowerPosition> powerPositions);
    string GenerateCsvFileName(DateTime dayAheadDate);
}