using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
	public class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly ApplicationContext _context;

		public RefreshTokenRepository ( ApplicationContext context )
		{
			_context = context;
		}

		public async Task<RefreshToken?> GetByTokenAsync ( string token )
		{
			return await _context.RefreshTokens.Include(rt => rt.Account).FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
		}

		public async Task CreateAsync ( RefreshToken refreshToken )
		{
			await _context.RefreshTokens.AddAsync(refreshToken);
		}

		public async Task RevokeAsync ( RefreshToken refreshToken )
		{
			refreshToken.IsRevoked = true;
			_context.RefreshTokens.Update(refreshToken);
		}
	}
}
