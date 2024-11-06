//namespace ReservationSystem.Models
//{
//    public class MenuItemViewModel
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public decimal Price { get; set; }
//        public string Category { get; set; }

//        // Property to hold the grouped items
//        public List<MenuItemViewModel> Items { get; set; }

//        public static List<CategoryGroup> GetCategorizedMenu(List<MenuItemViewModel> menuItems)
//        {
//            return new List<CategoryGroup>
//            {
//                new CategoryGroup
//                {
//                    Category = "Breakfast",
//                    Items = menuItems.Where(m => m.Category == "Breakfast").Take(5).ToList()
//                },
//                new CategoryGroup
//                {
//                    Category = "Lunch",
//                    Items = menuItems.Where(m => m.Category == "Lunch").Take(5).ToList()
//                },
//                new CategoryGroup
//                {
//                    Category = "Dinner",
//                    Items = menuItems.Where(m => m.Category == "Dinner").Take(5).ToList()
//                },
//                new CategoryGroup
//                {
//                    Category = "Dessert",
//                    Items = menuItems.Where(m => m.Category == "Dessert").Take(5).ToList()
//                },
//                new CategoryGroup
//                {
//                    Category = "Drink",
//                    Items = menuItems.Where(m => m.Category == "Drink").Take(5).ToList()
//                }
//            };
//        }
//    }

//    // New class to represent a category group
//    public class CategoryGroup
//    {
//        public string Category { get; set; }
//        public List<MenuItemViewModel> Items { get; set; }
//    }
//}