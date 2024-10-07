namespace PowerTradeApp.Models;

public class PowerTrade
{
    public DateTime Date { get; set; }
    public List<PowerPeriod> Periods { get; set; }

    public PowerTrade(DateTime date, List<PowerPeriod> periods)
    {
        Date = date;
        Periods = periods;
    }
}