using ChefMate.App.Services;
using ChefMate.Models;
using Microsoft.AspNetCore.Components;

namespace ChefMate.App.Pages;

public partial class EditRecipe : ComponentBase
{
    [Inject] 
    public RecipeService RecipeService { get; set; }
    
    [Inject] 
    public NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public string Id { get; set; }

    public Recipe Recipe { get; set; } = new Recipe();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        Recipe = await RecipeService.Get(Id);
        if (Recipe == null)
        {
            NavigationManager.NavigateTo("/");
        }
    }

    public async Task OnSubmit()
    {
        Recipe = await RecipeService.Update(Id, Recipe);
    }
}