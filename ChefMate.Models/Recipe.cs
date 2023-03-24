using System.Text.Json.Serialization;

namespace ChefMate.Models;

public class Recipe : BaseResource
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    public int PrepTime { get; set; } 
    public int CookTime { get; set; }
    [JsonIgnore]
    public int TotalTime => PrepTime + CookTime;
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