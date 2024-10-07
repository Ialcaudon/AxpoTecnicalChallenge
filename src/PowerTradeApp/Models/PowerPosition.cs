namespace PowerTradeApp.Models;

public class PowerPosition
{
    public DateTime DateTimeUtc { get; set; }
    public double Volume { get; set; }

    public PowerPosition(DateTime dateTimeUtc, double volume)
    {
        DateTimeUtc = dateTimeUtc;
        Volume = volume;
    }
}