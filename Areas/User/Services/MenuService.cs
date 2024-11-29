using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyReservationSystem.Services
{
    public class MenuService
    {
        private readonly HttpClient _httpClient;

        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            var response = await _httpClient.GetAsync("https://api.finch.dev.thickets.onl/api/menuitems");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<MenuItem>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            return new List<MenuItem>(); // Return an empty list on failure
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public bool Available { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        public string ImageUrl { get; set; }
    }
}
