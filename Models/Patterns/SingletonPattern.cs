namespace FoodDeliveryApp.Models.Patterns;

public sealed class OrderLogger
{
    private static OrderLogger? _instance;
    private static readonly object Lock = new();
    private readonly List<string> _logs = new();

    private OrderLogger() { }

    public static OrderLogger Instance
    {
        get
        {
            lock (Lock)
            {
                _instance ??= new OrderLogger();
                return _instance;
            }
        }
    }

    public void Log(string message)
    {
        var entry = $"{DateTime.Now:HH:mm:ss} - {message}";
        _logs.Add(entry);
    }

    public IReadOnlyList<string> GetLogs() => _logs.AsReadOnly();
}
