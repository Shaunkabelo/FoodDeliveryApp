namespace FoodDeliveryApp.Models.Patterns;

public record MealOption(string Value, string Name, string Description, decimal BasePrice, IReadOnlyList<string> CustomizationKeys);

public record CustomizationOption(string Value, string Name, decimal Price);

public static class CustomizationCatalog
{
    public static IReadOnlyDictionary<string, CustomizationOption> Options { get; } =
        new Dictionary<string, CustomizationOption>
        {
            ["cheese"] = new("cheese", "Extra Cheese", 15.00m),
            ["bacon"] = new("bacon", "Bacon", 25.00m),
            ["doublepatty"] = new("doublepatty", "Double Patty", 32.00m),
            ["largeportion"] = new("largeportion", "Large Portion", 22.00m),
            ["extrasauce"] = new("extrasauce", "Extra Sauce", 9.00m),
            ["glutenfree"] = new("glutenfree", "Gluten-Free Base", 18.00m)
        };
}

public abstract class Meal
{
    public string Name { get; protected set; } = string.Empty;
    public string RestaurantName { get; protected set; } = string.Empty;
    public decimal BasePrice { get; protected set; }
    public abstract string GetDescription();
    public abstract decimal GetPrice();
}

public class RestaurantMeal : Meal
{
    private readonly string _description;

    public RestaurantMeal(string restaurantName, MealOption option)
    {
        RestaurantName = restaurantName;
        Name = option.Name;
        BasePrice = option.BasePrice;
        _description = option.Description;
    }

    public override string GetDescription() => $"{Name} from {RestaurantName} - {_description}";
    public override decimal GetPrice() => BasePrice;
}

public abstract class MealFactory
{
    protected abstract string RestaurantName { get; }
    public abstract IReadOnlyList<MealOption> GetMenuItems();

    public Meal CreateMeal(string mealType)
    {
        var menuItem = GetMenuItems().FirstOrDefault(item =>
            item.Value.Equals(mealType, StringComparison.OrdinalIgnoreCase));

        if (menuItem == null)
        {
            throw new ArgumentException($"'{mealType}' is not available from {RestaurantName}.");
        }

        return new RestaurantMeal(RestaurantName, menuItem);
    }
}

public class FastFoodMealFactory : MealFactory
{
    protected override string RestaurantName => "QuickBite Fast Food";

    public override IReadOnlyList<MealOption> GetMenuItems() =>
    [
        new("smash-burger-combo", "Smash Burger Combo", "double beef smash patty, fries, and a soda", 109.90m, ["cheese", "bacon", "doublepatty", "extrasauce"]),
        new("crispy-chicken-burger", "Crispy Chicken Burger", "southern-style chicken, slaw, and chilli mayo", 94.90m, ["cheese", "bacon", "extrasauce"]),
        new("pepperoni-pizza", "Pepperoni Pizza", "thin base pizza with mozzarella and pepperoni", 129.90m, ["cheese", "extrasauce", "glutenfree"]),
        new("loaded-fries", "Loaded Fries", "fries topped with cheese sauce, jalapenos, and crispy onions", 69.90m, ["cheese", "bacon", "extrasauce", "largeportion"]),
        new("oreo-milkshake", "Oreo Milkshake", "thick vanilla shake blended with cookie pieces", 44.90m, ["largeportion"])
    ];
}

public class PremiumMealFactory : MealFactory
{
    protected override string RestaurantName => "Urban Table Premium";

    public override IReadOnlyList<MealOption> GetMenuItems() =>
    [
        new("sirloin-steak", "Grilled Sirloin Steak", "250g steak with herb butter and potato gratin", 219.90m, ["extrasauce", "largeportion"]),
        new("salmon-pasta", "Salmon Linguine", "linguine with seared salmon, lemon cream, and capers", 184.90m, ["extrasauce", "largeportion", "glutenfree"]),
        new("truffle-mushroom-risotto", "Truffle Mushroom Risotto", "arborio rice, wild mushrooms, parmesan, and truffle oil", 169.90m, ["largeportion", "extrasauce"]),
        new("lamb-flatbread", "Lamb Flatbread", "slow-cooked lamb, feta, pickled onion, and yoghurt dressing", 154.90m, ["extrasauce", "largeportion", "glutenfree"]),
        new("tiramisu", "Classic Tiramisu", "espresso-soaked sponge, mascarpone, and cocoa", 79.90m, ["largeportion"])
    ];
}

public class HealthyMealFactory : MealFactory
{
    protected override string RestaurantName => "GreenBowl Healthy";

    public override IReadOnlyList<MealOption> GetMenuItems() =>
    [
        new("grilled-chicken-bowl", "Grilled Chicken Power Bowl", "brown rice, greens, grilled chicken, avo, and sesame dressing", 119.90m, ["extrasauce", "largeportion"]),
        new("vegan-buddha-bowl", "Vegan Buddha Bowl", "quinoa, chickpeas, roasted veg, hummus, and seeds", 112.90m, ["extrasauce", "largeportion", "glutenfree"]),
        new("salmon-poke", "Salmon Poke Bowl", "sushi rice, salmon, edamame, cucumber, and ponzu", 149.90m, ["extrasauce", "largeportion"]),
        new("turkey-wrap", "Lean Turkey Wrap", "wholewheat wrap with turkey, greens, tomato, and yoghurt sauce", 89.90m, ["extrasauce", "glutenfree"]),
        new("green-smoothie", "Green Recovery Smoothie", "spinach, banana, apple, ginger, and protein", 54.90m, ["largeportion"])
    ];
}
