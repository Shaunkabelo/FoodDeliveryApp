using FoodDeliveryApp.Models;
using FoodDeliveryApp.Models.Patterns;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApp.Controllers;

public class OrderController : Controller
{
    private static Order? _currentOrder;

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.RestaurantMenus = RestaurantFactoryProvider.GetMenus();
        ViewBag.CustomizationOptions = CustomizationCatalog.Options;
        return View();
    }

    [HttpPost]
    public IActionResult Create(string restaurantType, string mealType, string[] extras, string paymentType, string promoCode)
    {
        RestaurantFactory restaurantFactory = RestaurantFactoryProvider.Create(restaurantType);

        var selectedMeal = restaurantFactory.GetMenuItems().First(item =>
            item.Value.Equals(mealType, StringComparison.OrdinalIgnoreCase));

        Meal meal = restaurantFactory.CreateMeal(mealType);
        meal = MealCustomizationFactory.ApplyCustomizations(
            meal,
            extras ?? Array.Empty<string>(),
            selectedMeal.CustomizationKeys);

        IPaymentStrategy paymentStrategy = PaymentStrategyFactory.Create(paymentType, promoCode);

        var order = new Order
        {
            Meal = meal,
            RestaurantFactory = restaurantFactory,
            PaymentStrategy = paymentStrategy
        };

        foreach (var observer in OrderObserverProvider.CreateDefaultObservers())
        {
            order.Attach(observer);
        }

        _currentOrder = order;
        order.Checkout();

        TempData["Success"] = "Order placed successfully.";
        return RedirectToAction(nameof(Details));
    }

    public IActionResult Details()
    {
        if (_currentOrder == null)
        {
            return RedirectToAction(nameof(Create));
        }

        ViewBag.Order = _currentOrder;
        ViewBag.Logger = OrderLogger.Instance.GetLogs();
        return View();
    }
}
