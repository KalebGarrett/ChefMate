using ChefMate.App.Services;
using ChefMate.Models;
using Microsoft.AspNetCore.Components;

namespace ChefMate.App.Pages;

public partial class CreateRecipe : ComponentBase
{
    [Inject]
    public RecipeService RecipeService { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public Recipe Recipe { get; set; } = new Recipe();

    private bool IsSubmitting { get; set; }
    
    private async Task HandleSubmit()
    {
        IsSubmitting = true;
        await RecipeService.Create(Recipe);
        IsSubmitting = false;
        NavigationManager.NavigateTo("/");
    }

    private void AddIngredient()
    {
        Recipe.Ingredients.Add(new Ingredient());
    }

    private void DeleteIngredient(Ingredient ingredient)
    {
        Recipe.Ingredients.Remove(ingredient);
    }

    private void AddStep()
    {
        Recipe.Steps.Add(new RecipeStep());
    }
    
    private void DeleteStep(RecipeStep step)
    {
        Recipe.Steps.Remove(step);
    }
}