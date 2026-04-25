using Microsoft.AspNetCore.Mvc;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _repository;

        public IngredientsController(IIngredientRepository repository)
        {
            _repository = repository;
        }

        // Получить список продуктов для конкретного рецепта
        [HttpGet("recipe/{recipeId}")]
        public async Task<IActionResult> GetByRecipe(Guid recipeId)
        {
            var ingredients = await _repository.GetByRecipeIdAsync(recipeId);
            return Ok(ingredients);
        }

        // Добавить ингредиент в рецепт
        [HttpPost]
        public async Task<IActionResult> Add(Ingredient ingredient) // Убери [FromBody], если он мешает
        {
            // Небольшая проверка, чтобы не упасть, если ID рецепта не пришел
            if (ingredient.RecipeId == Guid.Empty) return BadRequest("Нужен реальный ID рецепта");

            await _repository.AddAsync(ingredient);
            await _repository.SaveChangesAsync();

            return Ok(ingredient);
        }
    }
}