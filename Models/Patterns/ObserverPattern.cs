using FoodDeliveryApp.Models;

namespace FoodDeliveryApp.Models.Patterns;

public interface IOrderObserver
{
    OrderNotification Update(Order order, string message);
}

public record OrderNotification(string Stakeholder, string Message, DateTime TimeStamp);

public class CustomerNotifier : IOrderObserver
{
    public OrderNotification Update(Order order, string message)
    {
        return new OrderNotification("Customer", message, DateTime.Now);
    }
}

public class RestaurantNotifier : IOrderObserver
{
    public OrderNotification Update(Order order, string message)
    {
        return new OrderNotification("Restaurant", $"New order received: {order.Meal?.GetDescription()}", DateTime.Now);
    }
}

public class DriverNotifier : IOrderObserver
{
    public OrderNotification Update(Order order, string message)
    {
        return new OrderNotification("Driver", "Pickup assigned and route added to delivery queue", DateTime.Now);
    }
}

public class KitchenStaffNotifier : IOrderObserver
{
    public OrderNotification Update(Order order, string message)
    {
        return new OrderNotification("Kitchen Staff", "Meal preparation started", DateTime.Now);
    }
}
