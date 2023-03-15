using ChefMate.Models;
using Microsoft.AspNetCore.Mvc;

namespace FishWatch.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var recipes = new List<Recipe>();
        recipes.Add(new Recipe());
        recipes.Add(new Recipe());
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(new Recipe());
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        return Created("", recipe);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Recipe recipe)
    {
        return Ok(recipe);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return NoContent();
    }
}