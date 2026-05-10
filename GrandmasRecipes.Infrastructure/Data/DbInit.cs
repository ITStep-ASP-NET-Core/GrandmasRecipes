using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Domain.Enums;

namespace GrandmasRecipes.Infrastructure.Data
{
	public static class DbInitializer
	{
		public static void Initialize ( ApplicationContext context )
		{
			if(context.Recipes.Any())
				return;

			var user1 = new User
			{
				Id = Guid.NewGuid(),
				Nickname = "grandma_rose",
				Email = "rose@recipes.com",
				PasswordHash = "hashed_password_1"
			};

			var user2 = new User
			{
				Id = Guid.NewGuid(),
				Nickname = "chef_mario",
				Email = "mario@recipes.com",
				PasswordHash = "hashed_password_2"
			};

			context.Users.AddRange(user1, user2);
			context.SaveChanges();

			var productFlour = new Product { Name = "Мука" };
			var productEgg = new Product { Name = "Яйцо" };
			var productMilk = new Product { Name = "Молоко" };
			var productSugar = new Product { Name = "Сахар" };
			var productButter = new Product { Name = "Сливочное масло" };
			var productTomato = new Product { Name = "Помидор" };
			var productPasta = new Product { Name = "Паста" };
			var productGarlic = new Product { Name = "Чеснок" };
			var productOliveOil = new Product { Name = "Оливковое масло" };

			context.Products.AddRange(
				productFlour, productEgg, productMilk, productSugar,
				productButter, productTomato, productPasta, productGarlic, productOliveOil
			);
			context.SaveChanges();

			var difficultyEasy = new Difficulty { Name = "Легко", Level = DifficultyLevel.Easy };
			var difficultyNormal = new Difficulty { Name = "Нормально", Level = DifficultyLevel.Normal };

			context.Dificulties.AddRange(difficultyEasy, difficultyNormal);
			context.SaveChanges();

			var categoryDessert = new Category { Name = "Десерты", Description = "Сладкие блюда" };
			var categoryMain = new Category { Name = "Основные блюда", Description = "Горячие блюда" };

			context.Categories.AddRange(categoryDessert, categoryMain);
			context.SaveChanges();

			var cuisineItalian = new Cuisine { Name = "Итальянская", Description = "Кухня Италии" };
			var cuisineEuropean = new Cuisine { Name = "Европейская", Description = "Европейская кухня" };

			context.Cuisines.AddRange(cuisineItalian, cuisineEuropean);
			context.SaveChanges();

			var recipe1Id = Guid.NewGuid();
			var recipe1 = new Recipe
			{
				Id = recipe1Id,
				Title = "Классные блинчики",
				Description = "Нежные тонкие блинчики по бабушкиному рецепту",
				ImageUrls = ["/images/pancakes1.jpg", "/images/pancakes2.jpg"],
				Calories = 220,
				Likes = 2,
				AuthorId = user1.Id,
				DifficultyId = difficultyEasy.Id,
				CuisineId = cuisineEuropean.Id,
				Categories = [categoryDessert],
				Steps = [
					new() {
						Id = Guid.NewGuid(),
						Number = 1,
						Title = "Замесить тесто",
						Description = "Смешать муку, яйца и молоко до однородной массы",
						RecipeId = recipe1Id
					},
					new() {
						Id = Guid.NewGuid(),
						Number = 2,
						Title = "Добавить масло",
						Description = "Добавить сахар и растопленное масло, перемешать",
						RecipeId = recipe1Id
					},
					new() {
						Id = Guid.NewGuid(),
						Number = 3,
						Title = "Жарить блинчики",
						Description = "Жарить на разогретой сковороде по 1-2 минуты с каждой стороны",
						RecipeId = recipe1Id
					}
				],
				Ingredients = [
					new() { ProductId = productFlour.Id, Quantity = 200, Measure = Measure.Grams, RecipeId = recipe1Id },
					new() { ProductId = productEgg.Id, Quantity = 2, Measure = Measure.Pieces, RecipeId = recipe1Id },
					new() { ProductId = productMilk.Id, Quantity = 500, Measure = Measure.Milliliters, RecipeId = recipe1Id },
					new() { ProductId = productSugar.Id, Quantity = 30, Measure = Measure.Grams, RecipeId = recipe1Id },
					new() { ProductId = productButter.Id, Quantity = 50, Measure = Measure.Grams, RecipeId = recipe1Id }
				]
			};

			var recipe2Id = Guid.NewGuid();
			var recipe2 = new Recipe
			{
				Id = recipe2Id,
				Title = "Паста Маринара",
				Description = "Классическая итальянская паста с томатным соусом",
				ImageUrls = ["/images/marinara1.jpg"],
				Calories = 380,
				Likes = 1,
				AuthorId = user2.Id,
				DifficultyId = difficultyNormal.Id,
				CuisineId = cuisineItalian.Id,
				Categories = [categoryMain],
				Steps = [
					new() {
						Id = Guid.NewGuid(),
						Number = 1,
						Title = "Отварить пасту",
						Description = "Отварить пасту до состояния al dente",
						RecipeId = recipe2Id
					},
					new() {
						Id = Guid.NewGuid(),
						Number = 2,
						Title = "Обжарить чеснок",
						Description = "Обжарить чеснок на оливковом масле до золотистого цвета",
						RecipeId = recipe2Id
					},
					new() {
						Id = Guid.NewGuid(),
						Number = 3,
						Title = "Приготовить соус",
						Description = "Добавить нарезанные помидоры и тушить 15 минут",
						RecipeId = recipe2Id
					},
					new() {
						Id = Guid.NewGuid(),
						Number = 4,
						Title = "Подать блюдо",
						Description = "Смешать пасту с соусом и подавать",
						RecipeId = recipe2Id
					}
				],
				Ingredients = [
					new() { ProductId = productPasta.Id, Quantity = 300, Measure = Measure.Grams, RecipeId = recipe2Id },
					new() { ProductId = productTomato.Id, Quantity = 4, Measure = Measure.Pieces, RecipeId = recipe2Id },
					new() { ProductId = productGarlic.Id, Quantity = 3, Measure = Measure.Pieces, RecipeId = recipe2Id },
					new() { ProductId = productOliveOil.Id, Quantity = 30, Measure = Measure.Milliliters, RecipeId = recipe2Id }
				]
			};

			context.Recipes.AddRange(recipe1, recipe2);
			context.SaveChanges();

			var review1 = new Review
			{
				RecipeId = recipe1Id,
				AccountId = user2.Id,
				Comment = "Очень вкусные блинчики, спасибо за рецепт!"
			};

			var review2 = new Review
			{
				RecipeId = recipe1Id,
				AccountId = user1.Id,
				Comment = "Готовлю уже третий раз, всегда получается!"
			};

			var review3 = new Review
			{
				RecipeId = recipe2Id,
				AccountId = user1.Id,
				Comment = "Паста получилась отменная, буду готовить ещё"
			};

			context.Reviews.AddRange(review1, review2, review3);
			context.SaveChanges();

			var like1 = new Like { AccountId = user2.Id, RecipeId = recipe1Id };
			var like2 = new Like { AccountId = user1.Id, RecipeId = recipe1Id };
			var like3 = new Like { AccountId = user1.Id, RecipeId = recipe2Id };

			context.Likes.AddRange(like1, like2, like3);
			context.SaveChanges();
		}
	}
}