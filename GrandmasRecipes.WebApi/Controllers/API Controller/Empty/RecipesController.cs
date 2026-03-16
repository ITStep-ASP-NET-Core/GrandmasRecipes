using Microsoft.AspNetCore.Mvc;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // Проверка: Получить все рецепты
        [HttpGet]
        public IActionResult GetRecipes()
        {
            var recipes = _recipeRepository.GetAll().ToList();
            return Ok(recipes);
        }
    }
}