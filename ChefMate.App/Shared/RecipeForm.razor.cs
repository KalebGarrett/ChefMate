using ChefMate.App.Services;
using ChefMate.Models;
using Microsoft.AspNetCore.Components;

namespace ChefMate.App.Shared;

public partial class RecipeForm
{
    [Parameter] 
    public Recipe Recipe { get; set; } = new Recipe();
    
    [Parameter]
    public Func<Task> OnSubmit { get; set; }
    
    [Parameter]
    public Func<Task> OnSubmitComplete { get; set; }

    private bool IsSubmitting { get; set; }

    private async Task HandleSubmit()
    {
        IsSubmitting = true;
        if (OnSubmit != null)
        {
            await OnSubmit.Invoke();
        }
        IsSubmitting = false;
        if (OnSubmitComplete != null)
        {
            await OnSubmitComplete.Invoke();
        }
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