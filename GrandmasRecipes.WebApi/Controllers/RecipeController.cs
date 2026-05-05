using Microsoft.AspNetCore.Mvc;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        // 1. Получить все рецепты
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _recipeRepository.GetAll().ToListAsync();
            return Ok(recipes);
        }

        // 2. Получить один рецепт по ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null) return NotFound("Рецепт не найден");
            return Ok(recipe);
        }

        // 3. Создать новый рецепт
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Recipe recipe)
        {
            if (recipe == null) return BadRequest();

            await _recipeRepository.AddAsync(recipe);
            await _recipeRepository.SaveChangesAsync(); // Сохраняем в БД

            return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
        }

        // 4. Обновить существующий рецепт
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Recipe updatedRecipe)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null) return NotFound();

            recipe.Title = updatedRecipe.Title;
            recipe.Description = updatedRecipe.Description;
            recipe.LikesCount = updatedRecipe.LikesCount;

            _recipeRepository.Update(recipe);
            await _recipeRepository.SaveChangesAsync();

            return NoContent();
        }

        // 5. Удалить рецепт
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null) return NotFound();

            _recipeRepository.Delete(recipe);
            await _recipeRepository.SaveChangesAsync();

            return Ok("Рецепт удален");
        }


        // Метод для загрузки фото к рецепту
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(Guid id, IFormFile file)
        {
            // Формируем путь к папке
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            // --- ДОБАВЬ ЭТУ СТРОКУ (Заметка: создаем папку, если её нет) ---
            if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);


            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null) return NotFound("Рецепт не найден");

            if (file == null || file.Length == 0) return BadRequest("Файл не выбран");

            // Формируем путь: wwwroot/images/название_файла
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            // Сохраняем файл на диск
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Записываем путь в базу (чтобы потом найти картинку)
            recipe.ImagePath = "/images/" + fileName;
            await _recipeRepository.SaveChangesAsync();

            return Ok(new { path = recipe.ImagePath });
        }
    }
}