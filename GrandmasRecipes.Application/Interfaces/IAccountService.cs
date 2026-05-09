using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Account;

namespace GrandmasRecipes.Application.Interfaces
{
	public interface IAccountService
	{
		Task<PagedResult<AccountDetailsDto>> GetAccountsAsync ( int page );
		Task<AccountDto> GetAccountAsync ( Guid accountId );

		Task<Result> EditAccountAsync ( AccountEditDto accountDto );
		Task<Result> DeleteAccountAsync ( Guid accountId );
	}
}