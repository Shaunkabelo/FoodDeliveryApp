namespace FoodDeliveryApp.Models.Patterns;

public static class RestaurantFactoryProvider
{
    private static readonly IReadOnlyDictionary<string, Func<RestaurantFactory>> Factories =
        new Dictionary<string, Func<RestaurantFactory>>(StringComparer.OrdinalIgnoreCase)
        {
            ["FastFood"] = () => new FastFoodRestaurantFactory(),
            ["Premium"] = () => new PremiumRestaurantFactory(),
            ["Healthy"] = () => new HealthyFoodRestaurantFactory()
        };

    public static RestaurantFactory Create(string restaurantType)
    {
        return Factories.TryGetValue(restaurantType, out var factory)
            ? factory()
            : new FastFoodRestaurantFactory();
    }

    public static IReadOnlyDictionary<string, IReadOnlyList<MealOption>> GetMenus()
    {
        return Factories.ToDictionary(
            item => item.Key,
            item => item.Value().GetMenuItems(),
            StringComparer.OrdinalIgnoreCase);
    }
}

public static class MealCustomizationFactory
{
    private static readonly IReadOnlyDictionary<string, Func<Meal, Meal>> Decorators =
        new Dictionary<string, Func<Meal, Meal>>(StringComparer.OrdinalIgnoreCase)
        {
            ["cheese"] = meal => new ExtraCheeseDecorator(meal),
            ["bacon"] = meal => new BaconDecorator(meal),
            ["doublepatty"] = meal => new DoublePattyDecorator(meal),
            ["largeportion"] = meal => new LargePortionDecorator(meal),
            ["extrasauce"] = meal => new ExtraSauceDecorator(meal),
            ["glutenfree"] = meal => new GlutenFreeDecorator(meal)
        };

    public static Meal ApplyCustomizations(Meal meal, IEnumerable<string> requestedExtras, IEnumerable<string> allowedExtras)
    {
        var allowed = allowedExtras.ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var extra in requestedExtras)
        {
            if (allowed.Contains(extra) && Decorators.TryGetValue(extra, out var decorate))
            {
                meal = decorate(meal);
            }
        }

        return meal;
    }
}

public static class PaymentStrategyFactory
{
    private static readonly IReadOnlyDictionary<string, Func<string, IPaymentStrategy>> Strategies =
        new Dictionary<string, Func<string, IPaymentStrategy>>(StringComparer.OrdinalIgnoreCase)
        {
            ["credit"] = _ => new CreditCardStrategy(),
            ["eft"] = _ => new EftStrategy(),
            ["payfast"] = _ => new PayFastStrategy(),
            ["promo"] = promoCode => new PromoCodeStrategy(promoCode),
            ["loyalty"] = _ => new LoyaltyDiscountStrategy(),
            ["surge"] = _ => new SurgeDeliveryFeeStrategy()
        };

    public static IPaymentStrategy Create(string paymentType, string promoCode)
    {
        return Strategies.TryGetValue(paymentType, out var strategy)
            ? strategy(promoCode)
            : new CreditCardStrategy();
    }
}

public static class OrderObserverProvider
{
    private static readonly IReadOnlyList<Func<IOrderObserver>> ObserverFactories =
    [
        () => new CustomerNotifier(),
        () => new RestaurantNotifier(),
        () => new DriverNotifier(),
        () => new KitchenStaffNotifier()
    ];

    public static IReadOnlyList<IOrderObserver> CreateDefaultObservers()
    {
        return ObserverFactories.Select(createObserver => createObserver()).ToList();
    }
}
