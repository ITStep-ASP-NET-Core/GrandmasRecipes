using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Account;

namespace GrandmasRecipes.Application.Interfaces
{
	public interface IAccountService
	{
		Task<AccountDto> GetAccountAsync ( Guid accountId );
		Task<Result> EditAccountAsync ( AccountEditDto accountDto );
		Task<Result> DeleteAccountAsync ( Guid accountId );
	}
}