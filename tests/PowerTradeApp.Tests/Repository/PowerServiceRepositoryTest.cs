using Axpo;
using FluentAssertions;
using Moq;
using PowerTradeApp.Repository;

namespace PowerTradeApp.Tests.Repository;

[TestFixture]
[TestOf(typeof(PowerServiceRepository))]
public class PowerServiceRepositoryTest
{
    private readonly Mock<IPowerService> _powerServiceMock = new();

    [Test]
    public async Task RepositoryShouldCallDllAndReturnMapObjects()
    {
        var date = DateTime.Now;
        var sut = new PowerServiceRepository(_powerServiceMock.Object);
        _powerServiceMock.Setup(service => service.GetTradesAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<PowerTrade>
            {
                PowerTrade.Create(date,8)
            });
        
        var result = await sut.GetDayAheadTradesAsync(date);

        var powerTrades = result.ToList();
        powerTrades.Should().NotBeNull();
        powerTrades.First().Date.Should().Be(date);
    }
}