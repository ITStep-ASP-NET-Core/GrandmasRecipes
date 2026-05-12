using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління акаунтами користувачів.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Отримати публічну інформацію про акаунт.
        /// </summary>
        /// <remarks>
        /// Повертає публічний профіль користувача включно з кількістю лайків та опублікованих рецептів.
        /// Не потребує авторизації — доступно для всіх.
        /// </remarks>
        /// <param name="id">Ідентифікатор акаунту.</param>
        /// <returns>Публічний профіль акаунту <see cref="AccountDto"/>.</returns>
        /// <response code="200">Акаунт знайдений.</response>
        /// <response code="404">Акаунт з вказаним ідентифікатором не існує.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Редагувати акаунт.
        /// </summary>
        /// <remarks>
        /// Часткове оновлення — передавай тільки поля що змінюються, решта залишається без змін.
        /// Якщо передано новий пароль, він буде захешований через Argon2id перед збереженням.
        /// </remarks>
        /// <param name="dto">Дані для оновлення. Обов'язковий лише <c>Id</c>.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Акаунт успішно оновлений.</response>
        /// <response code="400">Акаунт з вказаним <c>Id</c> не знайдений.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAccount([FromBody] AccountEditDto dto)
        {
            var result = await _accountService.EditAccountAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }

        /// <summary>
        /// Видалити акаунт.
        /// </summary>
        /// <remarks>
        /// Каскадне видалення: разом з акаунтом видаляються всі його рецепти,
        /// інгредієнти, кроки, відгуки та лайки.
        /// Операція незворотня.
        /// </remarks>
        /// <param name="id">Ідентифікатор акаунту для видалення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Акаунт та всі пов'язані дані успішно видалені.</response>
        /// <response code="400">Акаунт з вказаним ідентифікатором не знайдений.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}