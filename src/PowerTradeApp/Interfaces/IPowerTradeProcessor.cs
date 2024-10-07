namespace PowerTradeApp.Services;

public interface IPowerTradeProcessor
{
    Task ProcessDayAheadTradesAsync(DateTime dayAheadDate, string csvDirectory);
}