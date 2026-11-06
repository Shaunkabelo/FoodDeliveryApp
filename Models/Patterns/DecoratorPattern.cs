namespace FoodDeliveryApp.Models.Patterns;

public abstract class MealDecorator : Meal
{
    protected readonly Meal Meal;

    protected MealDecorator(Meal meal) => Meal = meal;

    public override string GetDescription() => Meal.GetDescription();
    public override decimal GetPrice() => Meal.GetPrice();
}

public class ExtraCheeseDecorator : MealDecorator
{
    public ExtraCheeseDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Extra Cheese";
    public override decimal GetPrice() => base.GetPrice() + 15.00m;
}

public class BaconDecorator : MealDecorator
{
    public BaconDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Bacon";
    public override decimal GetPrice() => base.GetPrice() + 25.00m;
}

public class DoublePattyDecorator : MealDecorator
{
    public DoublePattyDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Double Patty";
    public override decimal GetPrice() => base.GetPrice() + 32.00m;
}

public class LargePortionDecorator : MealDecorator
{
    public LargePortionDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Large Portion";
    public override decimal GetPrice() => base.GetPrice() + 22.00m;
}

public class ExtraSauceDecorator : MealDecorator
{
    public ExtraSauceDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Extra Sauce";
    public override decimal GetPrice() => base.GetPrice() + 9.00m;
}

public class GlutenFreeDecorator : MealDecorator
{
    public GlutenFreeDecorator(Meal meal) : base(meal) { }
    public override string GetDescription() => base.GetDescription() + " + Gluten-Free Base";
    public override decimal GetPrice() => base.GetPrice() + 18.00m;
}
