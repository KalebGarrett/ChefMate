using System.Text.Json;
using ChefMate.Models;

namespace ChefMate.App.Services;

public class RecipeService
{
    private readonly HttpClient _httpClient;
    
    public RecipeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Recipe>> GetAll()
    {
        var result = await _httpClient.GetAsync("recipes");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Recipe>>(json, new JsonSerializerOptions()
            {
               PropertyNameCaseInsensitive = true
            });
        }

        return new List<Recipe>();
    }
}