namespace FoodDeliveryApp.Models.Patterns;

public interface IPaymentStrategy
{
    PaymentResult ProcessPayment(decimal amount);
    string GetPaymentDetails();
}

public record PaymentResult(decimal OriginalAmount, decimal FinalAmount, string Message);

public class CreditCardStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(decimal amount)
    {
        return new PaymentResult(amount, amount, $"Credit card payment approved for R{amount:0.00}");
    }

    public string GetPaymentDetails() => "Paid securely with Credit Card";
}

public class EftStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(decimal amount)
    {
        return new PaymentResult(amount, amount, $"EFT reference generated for R{amount:0.00}");
    }

    public string GetPaymentDetails() => "Paid by EFT bank transfer";
}

public class PayFastStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(decimal amount)
    {
        return new PaymentResult(amount, amount, $"PayFast checkout completed for R{amount:0.00}");
    }

    public string GetPaymentDetails() => "Paid using PayFast";
}

public class PromoCodeStrategy : IPaymentStrategy
{
    private readonly string _code;

    public PromoCodeStrategy(string code) => _code = string.IsNullOrWhiteSpace(code) ? "WELCOME15" : code.Trim();

    public PaymentResult ProcessPayment(decimal amount)
    {
        var finalAmount = decimal.Round(amount * 0.85m, 2);
        return new PaymentResult(amount, finalAmount, $"Promo code {_code} applied: 15% discount");
    }

    public string GetPaymentDetails() => $"Paid using Promo Code: {_code}";
}

public class LoyaltyDiscountStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(decimal amount)
    {
        var finalAmount = decimal.Round(amount * 0.90m, 2);
        return new PaymentResult(amount, finalAmount, "Loyalty discount applied: 10% off");
    }

    public string GetPaymentDetails() => "Paid with loyalty member discount";
}

public class SurgeDeliveryFeeStrategy : IPaymentStrategy
{
    public PaymentResult ProcessPayment(decimal amount)
    {
        var finalAmount = amount + 18.00m;
        return new PaymentResult(amount, finalAmount, "Surge delivery fee added: R18.00");
    }

    public string GetPaymentDetails() => "Paid with surge delivery fee pricing";
}
