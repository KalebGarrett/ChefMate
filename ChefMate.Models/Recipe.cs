namespace ChefMate.Models;

public class Recipe : BaseResource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
}

public class Ingredient
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public string Unit { get; set; }
}

public class RecipeStep
{
    public string Description { get; set; }
}