# QuickBite Food Delivery App

ASP.NET Core MVC prototype for ONT401 Assignment C: Food Ordering and Delivery App using Design Patterns.

## Tech Stack

- C# / .NET 8
- ASP.NET Core MVC
- Razor Views
- Local CSS

## Run Instructions

From the project folder:

```powershell
dotnet build
dotnet run
```

Open the localhost URL shown in the terminal, then select **New Order**.

## Implemented Design Patterns

| Pattern | Implementation | Purpose |
| --- | --- | --- |
| Factory Method | `MealFactory`, `FastFoodMealFactory`, `PremiumMealFactory`, `HealthyMealFactory` | Creates meals from the selected restaurant's own menu. |
| Abstract Factory | `RestaurantFactory` and concrete restaurant factories | Produces related restaurant family details: menu, packaging, and branding. |
| Decorator | `MealDecorator` and customization decorators | Adds only meal-appropriate extras such as cheese, bacon, double patty, large portion, sauce, and gluten-free base at runtime. |
| Strategy | `IPaymentStrategy` implementations | Switches between credit card, EFT, PayFast, promo, loyalty, and surge pricing. |
| Observer | `IOrderObserver` implementations | Notifies customer, restaurant, driver, and kitchen staff when order status changes. |
| Singleton | `OrderLogger` | Provides one shared in-memory order log for the running app. |

Runtime object creation is centralized in `CreationProviders.cs`. The MVC controller requests restaurants, payment strategies, decorators, and observers by key, while provider/factory classes create the concrete objects.

## Demo Workflow

1. Select a restaurant: QuickBite Fast Food, Urban Table Premium, or GreenBowl Healthy.
2. Select a meal from that restaurant's menu.
3. Add optional customizations shown for that selected meal.
4. Select a payment strategy.
5. Submit the order.
6. Review the generated notifications and singleton log on the details page.

## Key Files

- `Controllers/OrderController.cs`
- `Models/Order.cs`
- `Models/Patterns/*.cs`
- `Models/Patterns/CreationProviders.cs`
- `Views/Order/Create.cshtml`
- `Views/Order/Details.cshtml`
- `wwwroot/css/site.css`
