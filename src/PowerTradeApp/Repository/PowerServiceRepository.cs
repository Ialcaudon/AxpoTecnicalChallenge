using Axpo;

namespace PowerTradeApp.Repository;

using PowerTradeModel = PowerTradeApp.Models.PowerTrade;
using PowerPeriodModel = PowerTradeApp.Models.PowerPeriod;

public class PowerServiceRepository(IPowerService powerService) : IPowerServiceRepository
{
    public async Task<IList<PowerTradeModel>> GetDayAheadTradesAsync(DateTime dayAheadDate)
    {
        var externalTrades = await powerService.GetTradesAsync(dayAheadDate);
        return externalTrades.Select(trade => new PowerTradeModel(
            trade.Date,
            trade.Periods.Select(p => new PowerPeriodModel(p.Period, p.Volume)).ToList())).ToList();
    }
}