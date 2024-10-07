using PowerTradeApp.Interfaces;
using PowerTradeApp.Models;

namespace PowerTradeApp.Services;

public class AggregationService : IAggregationService 
{
    public IEnumerable<PowerPosition> AggregatePowerTradesByHour(IEnumerable<PowerTrade> trades)
    {
        var aggregatedPositions = new Dictionary<DateTime, double>();

        foreach (var trade in trades)
        {
            foreach (var period in trade.Periods)
            {
                var dateTimeUtc = ConvertToUtc(trade.Date, period.Period);

                if (!aggregatedPositions.ContainsKey(dateTimeUtc))
                {
                    aggregatedPositions[dateTimeUtc] = 0;
                }

                aggregatedPositions[dateTimeUtc] += period.Volume;
            }
        }
        
        return aggregatedPositions.Select(pos => new PowerPosition(pos.Key, pos.Value)).ToList();
    }
    private DateTime ConvertToUtc(DateTime tradeDate, int period)
    {
        return tradeDate.AddHours(period - 1).ToUniversalTime();
    }
}