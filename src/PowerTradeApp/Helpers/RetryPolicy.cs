using PowerTradeApp.Services;

namespace PowerTradeApp.Helpers;

public class RetryPolicy(int maxRetries, TimeSpan retryDelay) : IRetryPolicy
{
    public async Task ExecuteWithRetryAsync(Func<Task> operation)
    {
        int retryCount = 0;

        while (true)
        {
            try
            {
                await operation();
                break; 
            }
            catch (Exception ex)
            {
                retryCount++;

                if (retryCount > maxRetries)
                {
                    Console.WriteLine("Máximo número de reintentos alcanzado. Lanzando excepción.");
                    throw; 
                }

                Console.WriteLine($"Error: {ex.Message}. Reintentando en {retryDelay.TotalMilliseconds} ms... (Intento {retryCount}/{maxRetries})");
                await Task.Delay(retryDelay);
            }
        }
    }
}