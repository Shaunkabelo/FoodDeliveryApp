using FoodDeliveryApp.Models.Patterns;

namespace FoodDeliveryApp.Models;

public class Order
{
    public Meal? Meal { get; set; }
    public RestaurantFactory? RestaurantFactory { get; set; }
    public IPaymentStrategy? PaymentStrategy { get; set; }
    public PaymentResult? PaymentResult { get; private set; }
    public string Status { get; private set; } = "Draft";
    public List<IOrderObserver> Observers { get; } = new();
    public List<OrderNotification> Notifications { get; } = new();

    public void Attach(IOrderObserver observer) => Observers.Add(observer);

    public void Notify(string message)
    {
        foreach (var observer in Observers)
        {
            Notifications.Add(observer.Update(this, message));
        }
    }

    public void Checkout()
    {
        Status = "Confirmed";
        OrderLogger.Instance.Log($"Order placed for {Meal?.GetDescription()}");
        PaymentResult = PaymentStrategy?.ProcessPayment(Meal?.GetPrice() ?? 0);

        if (PaymentResult != null)
        {
            OrderLogger.Instance.Log(PaymentResult.Message);
        }

        Notify("Order confirmed and live tracking started");
        Status = "Preparing";
        Notify("Order is being prepared by the restaurant");
        Status = "Out for Delivery";
        Notify("Order is out for delivery");
    }
}
