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

    public async Task OnSubmit()
    {
        await RecipeService.Create(Recipe);
    }

    public async Task OnSubmitComplete()
    {
        NavigationManager.NavigateTo("/");
    }
}