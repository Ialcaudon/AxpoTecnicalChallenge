using PowerTradeApp.Interfaces;
using PowerTradeApp.Models;
using PowerTradeApp.Repository;

namespace PowerTradeApp.Services
{
    public class PowerTradeProcessor(
        IPowerServiceRepository powerService,
        ICsvGenerator csvGenerator,
        IRetryPolicy retryPolicy,
        IAggregationService aggregationService)
        : IPowerTradeProcessor
    {
        public async Task ProcessDayAheadTradesAsync(DateTime dayAheadDate, string csvDirectory)
        {
            await retryPolicy.ExecuteWithRetryAsync(async () =>
            {
                var trades = await powerService.GetDayAheadTradesAsync(dayAheadDate);
                var powerPositions = aggregationService.AggregatePowerTradesByHour(trades);
                string fileName = csvGenerator.GenerateCsvFileName(dayAheadDate);
                string filePath = Path.Combine(csvDirectory, fileName);
                csvGenerator.GenerateCsv(filePath, powerPositions);
            });
        }
    }
}
