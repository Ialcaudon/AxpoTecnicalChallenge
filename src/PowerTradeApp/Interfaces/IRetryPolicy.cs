namespace PowerTradeApp.Services;

public interface IRetryPolicy
{
    Task ExecuteWithRetryAsync(Func<Task> operation);
}