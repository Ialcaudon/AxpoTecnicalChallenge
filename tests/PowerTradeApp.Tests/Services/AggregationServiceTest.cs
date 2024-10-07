using FluentAssertions;
using PowerTradeApp.Models;
using PowerTradeApp.Services;
using PowerTradeApp.Tests.Fixtures;

namespace PowerTradeApp.Tests.Services;

[TestFixture]
[TestOf(typeof(AggregationService))]
public class AggregationServiceTest
{

    private readonly AggregationService _aggregationService = new();
    private readonly DateTime _date = DateTime.UtcNow.Date;

    [Test]
    public void AggregatePowerTradesByHour_SingleTrade_SinglePeriod_ReturnsCorrectAggregation()
    {
        var trades = new List<PowerTrade>
        {
            PowerTradeFixture.GenerateSinglePowerTradeWithMultiplePowerPeriod(_date,[100])
        };
        
        var result = _aggregationService.AggregatePowerTradesByHour(trades);

        result.Should().HaveCount(1);
        result.First().DateTimeUtc.Date.Should().Be(_date.Date);
        result.First().Volume.Should().Be(100);
    }

    [Test]
    public void AggregatePowerTradesByHour_MultipleTrade_SamePeriods_ReturnsCorrectAggregation()
    {
        var trades = PowerTradeFixture.GenerateMultiplePowerTrades(_date);
        
        var result = _aggregationService.AggregatePowerTradesByHour(trades);
        
        result.Should().HaveCount(2);
        result.First().DateTimeUtc.Should().Be(_date);
        result.First(x => x.DateTimeUtc.Hour == 0).Volume.Should().Be(300);
        result.First(x => x.DateTimeUtc.Hour == 1).Volume.Should().Be(75);
    }
}