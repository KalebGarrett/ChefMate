using ChefMate.API.Repositories.Interfaces;
using ChefMate.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate.API.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    private readonly IMongoRepository<Recipe> _recipeRepository;
    
    public RecipeController(IMongoRepository<Recipe> recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
        var recipes = await _recipeRepository.GetAll();
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var recipe = await _recipeRepository.GetById(id);
        return Ok(recipe);
    }

    [HttpPost("")]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        recipe = await _recipeRepository.Create(recipe);
        return Created(recipe.Id, recipe);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Recipe recipe)
    {
        recipe = await _recipeRepository.Update(id, recipe);
        return Ok(recipe);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _recipeRepository.Delete(id);
        return NoContent();
    }
}