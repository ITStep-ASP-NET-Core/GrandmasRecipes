using GrandmasRecipes.Application.DTO.Auth;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Авторизація та автентифікація користувачів.
    /// </summary>
    /// <remarks>
    /// Використовує схему JWT + Refresh токен.
    /// JWT діє 15 хвилин, refresh токен — 7 днів.
    /// Паролі зберігаються у вигляді Argon2id хешу.
    /// </remarks>
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        /// <summary>
        /// Зареєструвати нового користувача.
        /// </summary>
        /// <remarks>
        /// Створює новий акаунт типу <c>User</c> та повертає токени для негайного входу.
        /// Email повинен бути унікальним — якщо акаунт з таким email вже існує, повертається 409.
        /// Пароль хешується через Argon2id перед збереженням.
        /// </remarks>
        /// <param name="model">Дані реєстрації: нікнейм, email, пароль.</param>
        /// <returns>JWT та refresh токен для негайної авторизації.</returns>
        /// <exception cref="InvalidOperationException">
        /// Виникає якщо акаунт з таким email вже зареєстрований.
        /// </exception>
        /// <response code="200">Успішна реєстрація. Повертає <see cref="AuthResponseDto"/>.</response>
        /// <response code="400">Невалідні дані (не пройшла валідація моделі).</response>
        /// <response code="409">Акаунт з таким email вже існує.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await authService.RegisterAsync(new RegisterDto
                {
                    Nickname = model.Nickname,
                    Email = model.Email,
                    Password = model.Password
                });

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Увійти в систему за email та паролем.
        /// </summary>
        /// <remarks>
        /// Верифікує пароль через Argon2id та повертає нову пару токенів.
        /// Попередній refresh токен при цьому не інвалідується — користувач може мати кілька активних сесій.
        /// </remarks>
        /// <param name="model">Email та пароль у відкритому вигляді.</param>
        /// <returns>JWT та refresh токен.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Виникає якщо email не знайдений або пароль невірний.
        /// </exception>
        /// <response code="200">Успішний вхід. Повертає <see cref="AuthResponseDto"/>.</response>
        /// <response code="400">Невалідні дані (не пройшла валідація моделі).</response>
        /// <response code="401">Невірний email або пароль.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await authService.LoginAsync(new LoginDto
                {
                    Email = model.Email,
                    Password = model.Password
                });

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Оновити JWT за допомогою refresh токену.
        /// </summary>
        /// <remarks>
        /// Refresh токен є одноразовим — після використання старий токен видаляється
        /// і видається нова пара (access + refresh).
        /// Якщо токен прострочений або не існує в БД — повертається 401.
        /// </remarks>
        /// <param name="model">Діючий refresh токен.</param>
        /// <returns>Нова пара JWT та refresh токен.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Виникає якщо refresh токен не знайдений, прострочений або вже використаний.
        /// </exception>
        /// <response code="200">Токени успішно оновлені. Повертає <see cref="AuthResponseDto"/>.</response>
        /// <response code="400">Невалідні дані (не пройшла валідація моделі).</response>
        /// <response code="401">Refresh токен невалідний або прострочений.</response>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await authService.RefreshAsync(model.RefreshToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Вийти з системи та відкликати refresh токен.
        /// </summary>
        /// <remarks>
        /// Видаляє refresh токен з бази даних — подальші спроби оновити JWT з цим токеном
        /// повертатимуть 401. JWT при цьому залишається дійсним до закінчення свого терміну (15 хв).
        /// </remarks>
        /// <param name="model">Refresh токен поточної сесії.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Вихід успішний, токен відкликаний.</response>
        /// <response code="400">Невалідні дані (не пройшла валідація моделі).</response>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] RefreshViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await authService.LogoutAsync(model.RefreshToken);
            return NoContent();
        }
    }
}