using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace GrandmasRecipes.Infrastructure.Data
{
	public static class DbInitializer
	{
		public static async Task InitializeAsync ( ApplicationContext context )
		{
			await SeedMeasuresAsync(context);
			await SeedDifficultiesAsync(context);
			await context.SaveChangesAsync();

			if(context.Recipes.Any())
				return;

			await SeedSampleDataAsync(context);
		}

		private static async Task SeedMeasuresAsync ( ApplicationContext context )
		{
			var existing = await context.Measures.Select(m => m.Name).ToListAsync();
			var toAdd = Enum.GetValues<MeasureEnum>()
				.Select(m => GetDescription(m))
				.Where(name => !existing.Contains(name))
				.Select(name => new Measure { Name = name });
			await context.Measures.AddRangeAsync(toAdd);
		}

		private static async Task SeedDifficultiesAsync ( ApplicationContext context )
		{
			var existing = await context.Difficulties.Select(d => d.Name).ToListAsync();
			var toAdd = Enum.GetValues<DifficultyLevel>()
				.Select(d => GetDescription(d))
				.Where(name => !existing.Contains(name))
				.Select(name => new Difficulty { Name = name });
			await context.Difficulties.AddRangeAsync(toAdd);
		}

		private static async Task SeedSampleDataAsync ( ApplicationContext context )
		{
			// ── Юзери ────────────────────────────────────────────
			var user1 = new User { Id = Guid.NewGuid(), Nickname = "grandma_rose", Email = "rose@recipes.com", PasswordHash = "hashed_1" };
			var user2 = new User { Id = Guid.NewGuid(), Nickname = "chef_mario", Email = "mario@recipes.com", PasswordHash = "hashed_2" };
			var user3 = new User { Id = Guid.NewGuid(), Nickname = "olga_kitchen", Email = "olga@recipes.com", PasswordHash = "hashed_3" };
			var user4 = new User { Id = Guid.NewGuid(), Nickname = "foodie_max", Email = "max@recipes.com", PasswordHash = "hashed_4" };
			var user5 = new User { Id = Guid.NewGuid(), Nickname = "baker_anna", Email = "anna@recipes.com", PasswordHash = "hashed_5" };
			var user6 = new User { Id = Guid.NewGuid(), Nickname = "sushi_master", Email = "sushi@recipes.com", PasswordHash = "hashed_6" };
			context.Users.AddRange(user1, user2, user3, user4, user5, user6);
			await context.SaveChangesAsync();

			// ── Продукти ─────────────────────────────────────────
			var pFlour = new Product { Name = "Борошно" };
			var pEgg = new Product { Name = "Яйце" };
			var pMilk = new Product { Name = "Молоко" };
			var pSugar = new Product { Name = "Цукор" };
			var pButter = new Product { Name = "Вершкове масло" };
			var pTomato = new Product { Name = "Помідор" };
			var pPasta = new Product { Name = "Паста" };
			var pGarlic = new Product { Name = "Часник" };
			var pOliveOil = new Product { Name = "Оливкова олія" };
			var pOnion = new Product { Name = "Цибуля" };
			var pPotato = new Product { Name = "Картопля" };
			var pCarrot = new Product { Name = "Морква" };
			var pBeet = new Product { Name = "Буряк" };
			var pCabbage = new Product { Name = "Капуста" };
			var pPork = new Product { Name = "Свинина" };
			var pChicken = new Product { Name = "Куряче філе" };
			var pRice = new Product { Name = "Рис" };
			var pSoySauce = new Product { Name = "Соєвий соус" };
			var pGinger = new Product { Name = "Імбир" };
			var pSesame = new Product { Name = "Кунжут" };
			var pCream = new Product { Name = "Вершки" };
			var pCheese = new Product { Name = "Сир" };
			var pSalt = new Product { Name = "Сіль" };
			var pPepper = new Product { Name = "Чорний перець" };
			var pHoney = new Product { Name = "Мед" };
			var pSalmon = new Product { Name = "Лосось" };
			var pAvocado = new Product { Name = "Авокадо" };
			var pNori = new Product { Name = "Норі" };
			var pCucumber = new Product { Name = "Огірок" };
			var pBeef = new Product { Name = "Яловичина" };
			var pBakingPowder = new Product { Name = "Розпушувач" };
			var pVanilla = new Product { Name = "Ванілін" };
			var pCocoa = new Product { Name = "Какао" };
			var pOats = new Product { Name = "Вівсяні пластівці" };
			var pBanana = new Product { Name = "Банан" };
			var pMushrooms = new Product { Name = "Гриби" };

			context.Products.AddRange(
				pFlour, pEgg, pMilk, pSugar, pButter, pTomato, pPasta, pGarlic,
				pOliveOil, pOnion, pPotato, pCarrot, pBeet, pCabbage, pPork,
				pChicken, pRice, pSoySauce, pGinger, pSesame, pCream, pCheese,
				pSalt, pPepper, pHoney, pSalmon, pAvocado, pNori, pCucumber,
				pBeef, pBakingPowder, pVanilla, pCocoa, pOats, pBanana, pMushrooms
			);
			await context.SaveChangesAsync();

			// ── Категорії з картинками ────────────────────────────
			var catDessert = new Category { Name = "Десерти", Description = "Солодкі страви та випічка", ImageUrl = "https://global-blog.cpcdn.com/ua/2024/01/shokoladnaia-panna-k.jpg" };
			var catMain = new Category { Name = "Основні страви", Description = "Гарячі страви на обід та вечерю", ImageUrl = "https://www.smakolyky.com/wp-content/uploads/2018/07/IMG_7622-1.jpg" };
			var catSoup = new Category { Name = "Супи", Description = "Перші страви", ImageUrl = "https://images.unian.net/photos/2022_02/1643808952-7194.jpg?0.0069340925544987275" };
			var catSalad = new Category { Name = "Салати", Description = "Холодні закуски та салати", ImageUrl = "https://klopotenko.com/wp-content/uploads/2021/12/minipankeiki-z-ikrou-alyaski_sitewebukr-1000x600.jpg?v=1639735267" };
			var catBreakfast = new Category { Name = "Сніданки", Description = "Страви для ранкового прийому їжі", ImageUrl = "https://focus.ua/static/storage/thumbs/920x465/9/d6/7hsryb-5dc8b7679ec180248dec6ab170a7ad69.webp?v=9600_1" };
			var catBaking = new Category { Name = "Випічка", Description = "Хліб, булочки, пироги", ImageUrl = "https://ukr.media/static/ba/aimg/4/0/3/403572_1.jpg" };
			var catSushi = new Category { Name = "Суші та роли", Description = "Японська кухня", ImageUrl = "https://turpoisk.ua/images/blog/japonskaja-kuhnia/xjapon-kuhnia-2.jpg.pagespeed.ic.8pAJsilbYB.webp" };
			var catGrill = new Category { Name = "Гриль та BBQ", Description = "Страви на грилі", ImageUrl = "https://klopotenko.com/wp-content/uploads/2023/08/retsepty-dlya-hrylya-img.jpg?v=1720543401" };
			var catVegan = new Category { Name = "Веган", Description = "Страви без продуктів тваринного походження", ImageUrl = "https://cdn.vogue.ua/i/image_720x/uploads/article-inline/345/1a2/83c/5e9583c1a2345.jpeg.webp" };
			var catFastFood = new Category { Name = "Швидкі страви", Description = "Готується менше ніж за 30 хвилин", ImageUrl = "https://klopotenko.com/wp-content/uploads/2026/04/pitsa-z-porizanoho-lavasha-na-skovorodi-img-1000x600.jpg?v=1777444329" };

			context.Categories.AddRange(
				catDessert, catMain, catSoup, catSalad, catBreakfast,
				catBaking, catSushi, catGrill, catVegan, catFastFood
			);
			await context.SaveChangesAsync();

			// ── Кухні ─────────────────────────────────────────────
			var cuisineItalian = new Cuisine { Name = "Італійська", Description = "Кухня Італії" };
			var cuisineEuropean = new Cuisine { Name = "Європейська", Description = "Традиційна європейська кухня" };
			var cuisineUkrainian = new Cuisine { Name = "Українська", Description = "Традиційна українська кухня" };
			var cuisineJapanese = new Cuisine { Name = "Японська", Description = "Кухня Японії" };
			var cuisineFrench = new Cuisine { Name = "Французька", Description = "Вишукана французька кухня" };
			var cuisineAmerican = new Cuisine { Name = "Американська", Description = "Кухня США" };
			var cuisineGreek = new Cuisine { Name = "Грецька", Description = "Середземноморська кухня Греції" };

			context.Cuisines.AddRange(
				cuisineItalian, cuisineEuropean, cuisineUkrainian,
				cuisineJapanese, cuisineFrench, cuisineAmerican, cuisineGreek
			);
			await context.SaveChangesAsync();

			// ── Хелпери ───────────────────────────────────────────
			var measures = await context.Measures.ToListAsync();
			var difficulties = await context.Difficulties.ToListAsync();

			int MId ( MeasureEnum v ) => measures.First(m => m.Name == GetDescription(v)).Id;
			int DId ( DifficultyLevel v ) => difficulties.First(d => d.Name == GetDescription(v)).Id;

			// ── Рецепти ───────────────────────────────────────────

			// 1. Борщ
			var r1Id = Guid.NewGuid();
			var r1 = new Recipe
			{
				Id = r1Id,
				Title = "Класичний український борщ",
				Description = "Традиційний борщ з буряком, капустою та свининою за рецептом бабусі",
				ImageUrls = [
					"https://klopotenko.com/wp-content/uploads/2021/07/semeynyy-borshch_siteweb-1000x600.jpg?v=1627589095"
				],
				Calories = 280,
				Likes = 15,
				AuthorId = user3.Id,
				DifficultyId = DId(DifficultyLevel.Normal),
				CuisineId = cuisineUkrainian.Id,
				Categories = [catSoup],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Зварити бульйон",    Description = "Залити свинину холодною водою, довести до кипіння, зняти піну. Варити 1 годину.", RecipeId = r1Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Підготувати овочі",  Description = "Нарізати картоплю кубиками, капусту — соломкою, натерти буряк та моркву.", RecipeId = r1Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Зробити зажарку",    Description = "Обсмажити цибулю, моркву та буряк на олії 10 хвилин.", RecipeId = r1Id },
					new() { Id = Guid.NewGuid(), Number = 4, Title = "Зварити борщ",       Description = "Додати картоплю в бульйон, через 10 хв — капусту, потім зажарку. Варити ще 15 хвилин.", RecipeId = r1Id },
				],
				Ingredients =
				[
					new() { ProductId = pPork.Id,    Quantity = 500, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r1Id },
					new() { ProductId = pBeet.Id,    Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r1Id },
					new() { ProductId = pCabbage.Id, Quantity = 300, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r1Id },
					new() { ProductId = pPotato.Id,  Quantity = 3,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r1Id },
					new() { ProductId = pCarrot.Id,  Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r1Id },
					new() { ProductId = pOnion.Id,   Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r1Id },
					new() { ProductId = pTomato.Id,  Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r1Id },
				]
			};

			// 2. Паста Карбонара
			var r2Id = Guid.NewGuid();
			var r2 = new Recipe
			{
				Id = r2Id,
				Title = "Паста Карбонара",
				Description = "Класична римська паста з яйцями, сиром пекоріно та гуанчале",
				ImageUrls = [
					"https://hoff.ru/upload/medialibrary/8f8/uaiq76e5iforxfcfxn3j0w6kfaeyrvqg.jpg"
				],
				Calories = 520,
				Likes = 22,
				AuthorId = user2.Id,
				DifficultyId = DId(DifficultyLevel.Normal),
				CuisineId = cuisineItalian.Id,
				Categories = [catMain],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Відварити пасту", Description = "Відварити пасту в підсоленій воді до стану al dente.", RecipeId = r2Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Збити соус",      Description = "Збити яйця з натертим сиром та чорним перцем.", RecipeId = r2Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Змішати",         Description = "Зняти пасту з вогню, додати соус і швидко перемішати.", RecipeId = r2Id },
				],
				Ingredients =
				[
					new() { ProductId = pPasta.Id,  Quantity = 400, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r2Id },
					new() { ProductId = pEgg.Id,    Quantity = 4,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r2Id },
					new() { ProductId = pCheese.Id, Quantity = 100, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r2Id },
					new() { ProductId = pPepper.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons), RecipeId = r2Id },
				]
			};

			// 3. Млинці
			var r3Id = Guid.NewGuid();
			var r3 = new Recipe
			{
				Id = r3Id,
				Title = "Тонкі млинці",
				Description = "Ніжні тонкі млинці за бабусиним рецептом",
				ImageUrls = [
					"https://i.obozrevatel.com/food/recipemain/2018/12/29/suxie-slivki-bliny-na-suxix-slivkax14892182401max.jpg?size=636x424"
				],
				Calories = 220,
				Likes = 18,
				AuthorId = user1.Id,
				DifficultyId = DId(DifficultyLevel.Easy),
				CuisineId = cuisineEuropean.Id,
				Categories = [catBreakfast, catDessert],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Замісити тісто", Description = "Змішати борошно, яйця і молоко до однорідної маси без грудочок.", RecipeId = r3Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Додати масло",   Description = "Додати цукор і розтоплене масло, перемішати.", RecipeId = r3Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Смажити",        Description = "Смажити на розігрітій сковороді по 1-2 хвилини з кожного боку.", RecipeId = r3Id },
				],
				Ingredients =
				[
					new() { ProductId = pFlour.Id,  Quantity = 200, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r3Id },
					new() { ProductId = pEgg.Id,    Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r3Id },
					new() { ProductId = pMilk.Id,   Quantity = 500, MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r3Id },
					new() { ProductId = pSugar.Id,  Quantity = 30,  MeasureId = MId(MeasureEnum.Grams),       RecipeId = r3Id },
					new() { ProductId = pButter.Id, Quantity = 50,  MeasureId = MId(MeasureEnum.Grams),       RecipeId = r3Id },
				]
			};

			// 4. Роли з лососем
			var r4Id = Guid.NewGuid();
			var r4 = new Recipe
			{
				Id = r4Id,
				Title = "Роли з лососем",
				Description = "Класичні японські роли з лососем, авокадо та огірком",
				ImageUrls = [
					"https://cdn.xn--j1agri5c.xn--p1ai/preview/2f79c3a0-60ee-485a-9786-e7a35e981796.webp"
				],
				Calories = 310,
				Likes = 30,
				AuthorId = user6.Id,
				DifficultyId = DId(DifficultyLevel.Difficult),
				CuisineId = cuisineJapanese.Id,
				Categories = [catSushi],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Зварити рис",        Description = "Зварити рис для суші, заправити рисовим оцтом з цукром і сіллю.", RecipeId = r4Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Підготувати начинку", Description = "Нарізати лосось, авокадо та огірок тонкими смужками.", RecipeId = r4Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Скрутити роли",       Description = "Розкласти рис на норі, покласти начинку і скрутити за допомогою бамбукового килимка.", RecipeId = r4Id },
					new() { Id = Guid.NewGuid(), Number = 4, Title = "Нарізати",            Description = "Нарізати роли гострим ножем на 6-8 частин.", RecipeId = r4Id },
				],
				Ingredients =
				[
					new() { ProductId = pRice.Id,     Quantity = 300, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r4Id },
					new() { ProductId = pSalmon.Id,   Quantity = 200, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r4Id },
					new() { ProductId = pAvocado.Id,  Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r4Id },
					new() { ProductId = pNori.Id,     Quantity = 4,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r4Id },
					new() { ProductId = pCucumber.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r4Id },
					new() { ProductId = pSoySauce.Id, Quantity = 50,  MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r4Id },
				]
			};

			// 5. Курка теріякі
			var r5Id = Guid.NewGuid();
			var r5 = new Recipe
			{
				Id = r5Id,
				Title = "Курка теріякі",
				Description = "Соковита курка в солодко-солоному соусі теріякі з рисом",
				ImageUrls = [
					"https://klopotenko.com/wp-content/uploads/2019/09/kyryca-z-sousom-teriyaki_sitewebukr_new-1000x600.jpg?v=1622753146"
				],
				Calories = 420,
				Likes = 25,
				AuthorId = user6.Id,
				DifficultyId = DId(DifficultyLevel.Easy),
				CuisineId = cuisineJapanese.Id,
				Categories = [catMain, catFastFood],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Маринувати курку", Description = "Замаринувати куряче філе в суміші соєвого соусу, меду та імбиру на 30 хвилин.", RecipeId = r5Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Обсмажити",        Description = "Обсмажити курку на олії по 5 хвилин з кожного боку.", RecipeId = r5Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Додати соус",      Description = "Додати залишок маринаду і тушкувати 5 хвилин до загустіння.", RecipeId = r5Id },
				],
				Ingredients =
				[
					new() { ProductId = pChicken.Id,  Quantity = 600, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r5Id },
					new() { ProductId = pSoySauce.Id, Quantity = 80,  MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r5Id },
					new() { ProductId = pHoney.Id,    Quantity = 2,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r5Id },
					new() { ProductId = pGinger.Id,   Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r5Id },
					new() { ProductId = pGarlic.Id,   Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r5Id },
					new() { ProductId = pSesame.Id,   Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r5Id },
					new() { ProductId = pRice.Id,     Quantity = 300, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r5Id },
				]
			};

			// 6. Шоколадний кекс
			var r6Id = Guid.NewGuid();
			var r6 = new Recipe
			{
				Id = r6Id,
				Title = "Шоколадний кекс",
				Description = "Вологий та ароматний шоколадний кекс за 30 хвилин",
				ImageUrls = [
					"https://klopotenko.com/wp-content/uploads/2022/09/shokoladnyy-keks_sitewebukr-1000x600.jpg?v=1720548033"
				],
				Calories = 380,
				Likes = 12,
				AuthorId = user5.Id,
				DifficultyId = DId(DifficultyLevel.Easy),
				CuisineId = cuisineEuropean.Id,
				Categories = [catDessert, catBaking, catFastFood],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Змішати сухі інгредієнти", Description = "Змішати борошно, какао, цукор, розпушувач та ванілін.", RecipeId = r6Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Додати вологі",            Description = "Додати яйця, молоко та розтоплене масло. Перемішати до однорідності.", RecipeId = r6Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Випікати",                 Description = "Вилити тісто у форму і випікати при 180°C 25 хвилин.", RecipeId = r6Id },
				],
				Ingredients =
				[
					new() { ProductId = pFlour.Id,        Quantity = 200, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r6Id },
					new() { ProductId = pCocoa.Id,        Quantity = 50,  MeasureId = MId(MeasureEnum.Grams),       RecipeId = r6Id },
					new() { ProductId = pSugar.Id,        Quantity = 150, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r6Id },
					new() { ProductId = pEgg.Id,          Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r6Id },
					new() { ProductId = pMilk.Id,         Quantity = 200, MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r6Id },
					new() { ProductId = pButter.Id,       Quantity = 100, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r6Id },
					new() { ProductId = pBakingPowder.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r6Id },
					new() { ProductId = pVanilla.Id,      Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r6Id },
				]
			};

			// 7. Грецький салат
			var r7Id = Guid.NewGuid();
			var r7 = new Recipe
			{
				Id = r7Id,
				Title = "Грецький салат",
				Description = "Свіжий середземноморський салат з фетою та оливками",
				ImageUrls = [
					"https://klopotenko.com/wp-content/uploads/2022/01/greckyj-salat_sitewebukr-img-1000x600.jpg?v=1774026528"
				],
				Calories = 180,
				Likes = 20,
				AuthorId = user4.Id,
				DifficultyId = DId(DifficultyLevel.Easy),
				CuisineId = cuisineGreek.Id,
				Categories = [catSalad, catVegan, catFastFood],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Нарізати овочі", Description = "Нарізати помідори, огірок та цибулю великими шматками.", RecipeId = r7Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Заправити",       Description = "Полити оливковою олією, додати сіль і орегано.", RecipeId = r7Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Додати сир",      Description = "Зверху викласти шматочки фети.", RecipeId = r7Id },
				],
				Ingredients =
				[
					new() { ProductId = pTomato.Id,   Quantity = 3,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r7Id },
					new() { ProductId = pCucumber.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r7Id },
					new() { ProductId = pOnion.Id,    Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r7Id },
					new() { ProductId = pCheese.Id,   Quantity = 150, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r7Id },
					new() { ProductId = pOliveOil.Id, Quantity = 3,   MeasureId = MId(MeasureEnum.Spoons), RecipeId = r7Id },
					new() { ProductId = pSalt.Id,     Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons), RecipeId = r7Id },
				]
			};

			// 8. Вівсяна каша з бананом
			var r8Id = Guid.NewGuid();
			var r8 = new Recipe
			{
				Id = r8Id,
				Title = "Вівсяна каша з бананом",
				Description = "Корисний та ситний сніданок за 10 хвилин",
				ImageUrls = [
					"https://klopotenko.com/wp-content/uploads/2018/05/Ovsyanka-z-bananovymi-chipsami_SiteWeb-Ukr1.jpg"
				],
				Calories = 310,
				Likes = 8,
				AuthorId = user5.Id,
				DifficultyId = DId(DifficultyLevel.Easy),
				CuisineId = cuisineEuropean.Id,
				Categories = [catBreakfast, catVegan, catFastFood],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Зварити кашу",  Description = "Залити вівсяні пластівці молоком і варити 5 хвилин помішуючи.", RecipeId = r8Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Додати банан",   Description = "Нарізати банан кружечками і викласти зверху. Полити медом.", RecipeId = r8Id },
				],
				Ingredients =
				[
					new() { ProductId = pOats.Id,   Quantity = 100, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r8Id },
					new() { ProductId = pMilk.Id,   Quantity = 300, MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r8Id },
					new() { ProductId = pBanana.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r8Id },
					new() { ProductId = pHoney.Id,  Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r8Id },
				]
			};

			// 9. Яловичий стейк
			var r9Id = Guid.NewGuid();
			var r9 = new Recipe
			{
				Id = r9Id,
				Title = "Яловичий стейк Medium Rare",
				Description = "Соковитий стейк з яловичини з ідеальним ступенем прожарки",
				ImageUrls = [
					"https://myasnuyray.com.ua/wp-content/uploads/2018/11/01.jpg"
				],
				Calories = 650,
				Likes = 35,
				AuthorId = user2.Id,
				DifficultyId = DId(DifficultyLevel.GordonRamsay),
				CuisineId = cuisineAmerican.Id,
				Categories = [catMain, catGrill],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Підготувати м'ясо", Description = "Дістати стейк з холодильника за годину до приготування. Обсушити паперовим рушником.", RecipeId = r9Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Приправити",         Description = "Натерти сіллю і перцем з обох боків безпосередньо перед смаженням.", RecipeId = r9Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Обсмажити",          Description = "Смажити на дуже гарячій сковороді по 2.5 хвилини з кожного боку.", RecipeId = r9Id },
					new() { Id = Guid.NewGuid(), Number = 4, Title = "Дати відпочити",     Description = "Зняти з вогню і дати м'ясу відпочити 5 хвилин перед подачею.", RecipeId = r9Id },
				],
				Ingredients =
				[
					new() { ProductId = pBeef.Id,   Quantity = 400, MeasureId = MId(MeasureEnum.Grams),  RecipeId = r9Id },
					new() { ProductId = pSalt.Id,   Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons), RecipeId = r9Id },
					new() { ProductId = pPepper.Id, Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons), RecipeId = r9Id },
					new() { ProductId = pButter.Id, Quantity = 30,  MeasureId = MId(MeasureEnum.Grams),  RecipeId = r9Id },
					new() { ProductId = pGarlic.Id, Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces), RecipeId = r9Id },
				]
			};

			// 10. Крем-суп з грибів
			var r10Id = Guid.NewGuid();
			var r10 = new Recipe
			{
				Id = r10Id,
				Title = "Крем-суп з грибів",
				Description = "Ніжний вершковий суп-пюре з печерицями та часником",
				ImageUrls = [
					"https://www.torchyn.ua/sites/default/files/2021-03/b93baadcdb63cef9bc6979b0ac9d4594.jpg"
				],
				Calories = 240,
				Likes = 16,
				AuthorId = user3.Id,
				DifficultyId = DId(DifficultyLevel.Normal),
				CuisineId = cuisineFrench.Id,
				Categories = [catSoup],
				Steps =
				[
					new() { Id = Guid.NewGuid(), Number = 1, Title = "Обсмажити гриби",  Description = "Нарізати гриби та цибулю, обсмажити на вершковому маслі 10 хвилин.", RecipeId = r10Id },
					new() { Id = Guid.NewGuid(), Number = 2, Title = "Додати вершки",    Description = "Залити вершками, додати сіль та перець. Довести до кипіння.", RecipeId = r10Id },
					new() { Id = Guid.NewGuid(), Number = 3, Title = "Збити блендером",  Description = "Збити суп занурювальним блендером до однорідної кремової консистенції.", RecipeId = r10Id },
				],
				Ingredients =
				[
					new() { ProductId = pMushrooms.Id, Quantity = 500, MeasureId = MId(MeasureEnum.Grams),       RecipeId = r10Id },
					new() { ProductId = pCream.Id,     Quantity = 200, MeasureId = MId(MeasureEnum.Milliliters), RecipeId = r10Id },
					new() { ProductId = pOnion.Id,     Quantity = 1,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r10Id },
					new() { ProductId = pGarlic.Id,    Quantity = 2,   MeasureId = MId(MeasureEnum.Pieces),      RecipeId = r10Id },
					new() { ProductId = pButter.Id,    Quantity = 50,  MeasureId = MId(MeasureEnum.Grams),       RecipeId = r10Id },
					new() { ProductId = pSalt.Id,      Quantity = 1,   MeasureId = MId(MeasureEnum.Spoons),      RecipeId = r10Id },
				]
			};

			context.Recipes.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);
			await context.SaveChangesAsync();

			// ── Відгуки ───────────────────────────────────────────
			context.Reviews.AddRange(
				new Review { RecipeId = r1Id, AccountId = user2.Id, Comment = "Найкращий борщ який я їв! Точно як у бабусі." },
				new Review { RecipeId = r1Id, AccountId = user4.Id, Comment = "Готувала вже тричі, завжди виходить чудово!" },
				new Review { RecipeId = r2Id, AccountId = user1.Id, Comment = "Карбонара вийшла ідеальна, дуже смачно!" },
				new Review { RecipeId = r2Id, AccountId = user3.Id, Comment = "Справжня римська карбонара, без вершків — так і має бути." },
				new Review { RecipeId = r3Id, AccountId = user2.Id, Comment = "Млинці вийшли тонкі і ніжні, дякую за рецепт!" },
				new Review { RecipeId = r4Id, AccountId = user4.Id, Comment = "Роли вийшли як в ресторані, дуже задоволена!" },
				new Review { RecipeId = r5Id, AccountId = user1.Id, Comment = "Теріякі просто відмінна, готую кожного тижня." },
				new Review { RecipeId = r6Id, AccountId = user3.Id, Comment = "Кекс вийшов дуже вологий і шоколадний, 10/10!" },
				new Review { RecipeId = r7Id, AccountId = user5.Id, Comment = "Простий і смачний салат для літа." },
				new Review { RecipeId = r9Id, AccountId = user4.Id, Comment = "Стейк вийшов ідеальний! Дотримувався рецепту і не помилився." },
				new Review { RecipeId = r10Id, AccountId = user1.Id, Comment = "Крем-суп просто топ, вся сім'я в захваті." }
			);

			// ── Лайки ─────────────────────────────────────────────
			context.Likes.AddRange(
				new Like { AccountId = user1.Id, RecipeId = r1Id },
				new Like { AccountId = user2.Id, RecipeId = r1Id },
				new Like { AccountId = user3.Id, RecipeId = r1Id },
				new Like { AccountId = user4.Id, RecipeId = r2Id },
				new Like { AccountId = user5.Id, RecipeId = r2Id },
				new Like { AccountId = user1.Id, RecipeId = r3Id },
				new Like { AccountId = user2.Id, RecipeId = r4Id },
				new Like { AccountId = user3.Id, RecipeId = r4Id },
				new Like { AccountId = user4.Id, RecipeId = r4Id },
				new Like { AccountId = user5.Id, RecipeId = r5Id },
				new Like { AccountId = user6.Id, RecipeId = r5Id },
				new Like { AccountId = user1.Id, RecipeId = r6Id },
				new Like { AccountId = user2.Id, RecipeId = r7Id },
				new Like { AccountId = user3.Id, RecipeId = r7Id },
				new Like { AccountId = user1.Id, RecipeId = r9Id },
				new Like { AccountId = user2.Id, RecipeId = r9Id },
				new Like { AccountId = user4.Id, RecipeId = r9Id },
				new Like { AccountId = user5.Id, RecipeId = r10Id }
			);

			await context.SaveChangesAsync();
		}

		private static string GetDescription ( Enum value )
		{
			return value.GetType()
				.GetField(value.ToString())
				?.GetCustomAttribute<DescriptionAttribute>()
				?.Description ?? value.ToString();
		}
	}
}