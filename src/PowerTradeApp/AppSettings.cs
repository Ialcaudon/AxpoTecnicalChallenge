namespace PowerTradeApp;

public class AppSettings
{
    public string CsvDirectory { get; set; } = String.Empty;
    public int ExtractionIntervalMinutes { get; set; }
    public int MaxRetryAttempts { get; set; }
    public int RetryDelayMilliseconds { get; set; }
}