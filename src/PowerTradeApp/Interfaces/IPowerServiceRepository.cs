using PowerTradeApp.Models;

namespace PowerTradeApp.Repository;

public interface IPowerServiceRepository
{
    Task<IList<PowerTrade>> GetDayAheadTradesAsync(DateTime date);
}