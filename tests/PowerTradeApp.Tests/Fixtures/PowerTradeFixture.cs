using PowerTradeApp.Models;

namespace PowerTradeApp.Tests.Fixtures;

public static class PowerTradeFixture
{
    public static PowerTrade GenerateSinglePowerTradeWithMultiplePowerPeriod(DateTime date, List<double> volumes)
    {
        var powerPeriod = new List<PowerPeriod>();
        int period = 0;
        foreach (var volume in volumes)
        {
            period++;
            powerPeriod.Add(new PowerPeriod(period,volume));
        }
        return new PowerTrade(date, powerPeriod);
    }

    public static List<PowerPosition> GeneratePowerPositions(DateTime date,int[] positions)
    {
        var positionList = new List<PowerPosition>();
        foreach (var position in positions)
        {
            positionList.Add(new PowerPosition(date, position));
            date = date.AddHours(1);
        }
        return positionList;
    }

    public static List<PowerTrade> GenerateMultiplePowerTrades(DateTime date)
    {
        return new List<PowerTrade>
        {
            GenerateSinglePowerTradeWithMultiplePowerPeriod(date,[100,50]),
            GenerateSinglePowerTradeWithMultiplePowerPeriod(date,[200,25])
        };
    }
}