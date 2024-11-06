//using Microsoft.AspNetCore.Mvc;
//using ReservationSystem.Models;

//namespace ReservationSystem.Controllers
//{
//    public class MenuController : Controller
//    {
//        public IActionResult Index()
//        {
//            var menuItems = new List<MenuItemViewModel>
//            {
//                // Breakfast Items
//                new MenuItemViewModel { Id = 1, Title = "Pancakes", Description = "Fluffy pancakes served with maple syrup and fresh berries.", Price = 12.00m, Category = "Breakfast" },
//                new MenuItemViewModel { Id = 2, Title = "Avocado Toast", Description = "Smashed avocado on sourdough, topped with poached eggs and chili flakes.", Price = 14.00m, Category = "Breakfast" },
//                new MenuItemViewModel { Id = 3, Title = "French Toast", Description = "Thick-cut brioche soaked in cinnamon-vanilla batter, served with whipped cream and berries.", Price = 13.00m, Category = "Breakfast" },
        
//                // Lunch Items
//                new MenuItemViewModel { Id = 4, Title = "Caesar Salad", Description = "Romaine lettuce, parmesan, croutons, and Caesar dressing, with grilled chicken.", Price = 14.00m, Category = "Lunch" },
//                new MenuItemViewModel { Id = 5, Title = "Grilled Chicken Sandwich", Description = "Grilled chicken breast, lettuce, tomato, avocado, and chipotle mayo on a ciabatta roll.", Price = 16.00m, Category = "Lunch" },
//                new MenuItemViewModel { Id = 6, Title = "Club Sandwich", Description = "Turkey, bacon, lettuce, tomato, and mayo layered on toasted bread.", Price = 15.00m, Category = "Lunch" },

//                // Dinner Items
//                new MenuItemViewModel { Id = 7, Title = "Grilled Ribeye Steak", Description = "300g ribeye, served with garlic mashed potatoes and steamed vegetables.", Price = 34.00m, Category = "Dinner" },
//                new MenuItemViewModel { Id = 8, Title = "Pan-Seared Salmon", Description = "Served with quinoa, roasted asparagus, and lemon-butter sauce.", Price = 28.00m, Category = "Dinner" },
//                new MenuItemViewModel { Id = 9, Title = "Chicken Parmesan", Description = "Breaded chicken breast topped with marinara and mozzarella, served with spaghetti.", Price = 22.00m, Category = "Dinner" },

//                // Dessert Items
//                new MenuItemViewModel { Id = 10, Title = "Chocolate Lava Cake", Description = "Warm chocolate cake with a gooey center, served with vanilla ice cream.", Price = 10.00m, Category = "Dessert" },
//                new MenuItemViewModel { Id = 11, Title = "Vegan Chocolate Lava Cake", Description = "Plant-based warm chocolate cake with a gooey center, served with coconut ice cream.", Price = 11.00m, Category = "Dessert" },
//                new MenuItemViewModel { Id = 12, Title = "Apple Crumble", Description = "Baked apples with cinnamon, topped with a buttery crumble and served with whipped cream.", Price = 9.00m, Category = "Dessert" },

//                // Drink Items
//                new MenuItemViewModel { Id = 13, Title = "Coke", Description = "Chilled Coca-Cola.", Price = 1.99m, Category = "Drink" },
//                new MenuItemViewModel { Id = 14, Title = "Sprite", Description = "Refreshing lemon-lime soda.", Price = 1.99m, Category = "Drink" },
//                new MenuItemViewModel { Id = 15, Title = "Lemonade", Description = "Freshly squeezed lemonade.", Price = 2.49m, Category = "Drink" },
//            };

//            var categorizedMenu = menuItems
//                .GroupBy(m => m.Category)
//                .Select(g => new CategoryGroup
//                {
//                    Category = g.Key,
//                    Items = g.ToList()
//                }).ToList();

//            return View(categorizedMenu);
//        }
//    }
//}