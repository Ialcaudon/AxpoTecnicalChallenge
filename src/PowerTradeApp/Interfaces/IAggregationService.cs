using PowerTradeApp.Models;

namespace PowerTradeApp.Interfaces;

public interface IAggregationService
{
    IEnumerable<PowerPosition> AggregatePowerTradesByHour(IEnumerable<PowerTrade> trades);
}