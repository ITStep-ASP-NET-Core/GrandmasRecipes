using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Recipe;

namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс для роботи з рецептами.
    /// Підтримує пагінацію, фільтрацію та повне управління рецептами.
    /// </summary>
    public interface IRecipeService
    {
        /// <summary>
        /// Отримати всі рецепти посортовані за популярністю з пагінацією.
        /// </summary>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="userId">Ідентифікатор поточного користувача для визначення IsLiked.</param>
        Task<PagedResult<RecipePreviewDto>> GetRecipesAsync(int page, Guid? userId = null);

        /// <summary>
        /// Отримати рецепти з фільтрацією за категоріями, кухнями, складністю та продуктами.
        /// Можна передавати декілька фільтрів одночасно.
        /// </summary>
        /// <param name="filter">Параметри фільтрації.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="userId">Ідентифікатор поточного користувача для визначення IsLiked.</param>
        Task<PagedResult<RecipePreviewDto>> GetRecipesByFiltersAsync(RecipeFilterDto filter, int page, Guid? userId = null);

        /// <summary>
        /// Отримати рецепти конкретного автора з пагінацією.
        /// </summary>
        /// <param name="authorId">Ідентифікатор автора.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="userId">Ідентифікатор поточного користувача для визначення IsLiked.</param>
        Task<PagedResult<RecipePreviewDto>> GetRecipesByAuthorAsync(Guid authorId, int page, Guid? userId = null);

        /// <summary>
        /// Отримати рецепти які лайкнув користувач, посортовані за датою лайку.
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        Task<PagedResult<RecipePreviewDto>> GetRecipesByLikedAsync(Guid userId, int page);

        /// <summary>
        /// Отримати повні дані рецепту включаючи інгредієнти, кроки та автора.
        /// </summary>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="userId">Ідентифікатор поточного користувача для визначення IsLiked.</param>
        Task<RecipeDetailsDto?> GetRecipeByIdAsync(Guid recipeId, Guid? userId = null);

        /// <summary>Створити новий рецепт.</summary>
        /// <param name="recipeDto">Дані для створення рецепту.</param>
        Task<Result> CreateRecipeAsync(RecipeCreateDto recipeDto);

        /// <summary>
        /// Редагувати існуючий рецепт.
        /// Всі поля окрім RecipeId опціональні — передавай тільки те що змінюєш.
        /// </summary>
        /// <param name="recipeDto">Дані для редагування рецепту.</param>
        Task<Result> EditRecipeAsync(RecipeEditDto recipeDto);

        /// <summary>Видалити рецепт разом з інгредієнтами, кроками та відгуками.</summary>
        /// <param name="recipeId">Ідентифікатор рецепту для видалення.</param>
        Task<Result> DeleteRecipeAsync(Guid recipeId);
    }
}