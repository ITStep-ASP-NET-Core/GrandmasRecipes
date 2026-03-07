namespace GrandmasRecipes.WebApi.Common
{
	public static class ImagePicker
	{
		public static ICollection<string> DefaultAvatarPaths { get; set; }

		private static readonly string _folderPath;

		static ImagePicker ( )
		{
			_folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "avatars");

			if(!Directory.Exists(_folderPath))
			{
				DefaultAvatarPaths = [];
			}
			else
			{
				DefaultAvatarPaths = Directory.GetFiles(_folderPath, "defaultAvatar_*.png").Select(Path.GetFileName).ToList();
			}
		}

		public static async Task<string> SaveImage ( IFormFile avatar )
		{
			if(!Directory.Exists(_folderPath))
				Directory.CreateDirectory(_folderPath);

			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatar.FileName)}";
			using(var stream = new FileStream(Path.Combine(_folderPath, fileName), FileMode.Create))
			{
				await avatar.CopyToAsync(stream);
			}
			return fileName;
		}

		public static string? GetRandomDefaultAvatar ( )
		{
			if (DefaultAvatarPaths.Count == 0)
				return null;

			var random = new Random();
			return DefaultAvatarPaths.ToList()[random.Next(0, DefaultAvatarPaths.Count)];
		}
	}
}
