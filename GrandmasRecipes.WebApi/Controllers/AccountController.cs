using Microsoft.AspNetCore.Mvc;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }

        // Получаем список всех пользователей
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _repository.GetAllAsync();
            return Ok(accounts);
        }

        // Регистрация нового аккаунта
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Account account)
        {
            if (account.Id == Guid.Empty) account.Id = Guid.NewGuid();

            await _repository.AddAsync(account);
            await _repository.SaveChangesAsync();

            return Ok(account);
        }

        // Удаление профиля по ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) return NotFound("Пользователь не найден");

            _repository.Delete(account);
            await _repository.SaveChangesAsync();

            return Ok("Аккаунт удален");
        }
    }
}