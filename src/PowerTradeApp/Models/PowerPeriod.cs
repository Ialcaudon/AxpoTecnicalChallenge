namespace PowerTradeApp.Models;

public class PowerPeriod
{
    public int Period { get; set; } // Periodo horario (1, 2, 3, ...)
    public double Volume { get; set; } // Volumen de energía para ese periodo

    public PowerPeriod(int period, double volume)
    {
        Period = period;
        Volume = volume;
    }
}