using System.Text;
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
    
    public async Task<Recipe> Get(string id)
    {
        var result = await _httpClient.GetAsync($"recipes/{id}");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Recipe>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }

    public async Task<Recipe> Create(Recipe recipe)
    {
        var json = JsonSerializer.Serialize(recipe);
        var result = await _httpClient.PostAsync("recipes", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }
        return recipe;
    }
    
    public async Task<Recipe> Update(string id, Recipe recipe)
    {
        var json = JsonSerializer.Serialize(recipe);
        var result = await _httpClient.PutAsync($"recipes/{id}", new StringContent(json, Encoding.UTF8, "application/json"));
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }
        return recipe;
    }

    public async Task<bool> Delete(string id)
    {
        var result = await _httpClient.DeleteAsync($"recipes/{id}");
        return result.IsSuccessStatusCode;
    }
 }