namespace FoodDeliveryApp.Models.Patterns;

public abstract class RestaurantFactory
{
    protected abstract MealFactory MealFactory { get; }

    public Meal CreateMeal(string mealType) => MealFactory.CreateMeal(mealType);
    public IReadOnlyList<MealOption> GetMenuItems() => MealFactory.GetMenuItems();
    public abstract string GetRestaurantName();
    public abstract string GetPackagingType();
    public abstract string GetBranding();
    public abstract string GetMenuPositioning();
}

public class FastFoodRestaurantFactory : RestaurantFactory
{
    protected override MealFactory MealFactory { get; } = new FastFoodMealFactory();
    public override string GetRestaurantName() => "BITE Fast Food";
    public override string GetPackagingType() => "Branded paper clamshell and cup carrier";
    public override string GetBranding() => "Red, yellow, and black quick-service branding";
    public override string GetMenuPositioning() => "Affordable meals prepared for fast turnaround";
}

public class PremiumRestaurantFactory : RestaurantFactory
{
    protected override MealFactory MealFactory { get; } = new PremiumMealFactory();
    public override string GetRestaurantName() => "Urban Table Premium";
    public override string GetPackagingType() => "Rigid black delivery box with tamper seal";
    public override string GetBranding() => "Minimal gold and charcoal premium branding";
    public override string GetMenuPositioning() => "Restaurant-quality meals with careful presentation";
}

public class HealthyFoodRestaurantFactory : RestaurantFactory
{
    protected override MealFactory MealFactory { get; } = new HealthyMealFactory();
    public override string GetRestaurantName() => "GreenBowl Healthy";
    public override string GetPackagingType() => "Compostable bowl, paper sleeve, and recycled bag";
    public override string GetBranding() => "Fresh green and white wellness branding";
    public override string GetMenuPositioning() => "Balanced meals with lighter ingredients";
}
