using System.Net.Http;
using System.Text.Json;
using JAModel;
namespace ConsoleProjectUI
{
    public class HttpService
    {
        private readonly string _apiBaseURL = "https://localhost:7143/api/Item/";

        public async Task<List<UserPass>> GetAdminsAsync()
        {
            List<UserPass> _admins = new List<UserPass>();
            //Assemble URL for resource
            //Set a GET request to API endpoint
            //Deserialize and return to caller as List<UserPass>

            string url = _apiBaseURL + "GetAdmins";
            HttpClient client = new HttpClient();
            try
            {
            //  HttpResponseMessage response = await client.GetAsync(url);
            //  response.EnsureSuccessStatusCode();
            //  string responseString = await response.Content.ReadAsStringAsync();

            string responseString = await client.GetStringAsync(url);

            _admins = JsonSerializer.Deserialize<List<UserPass>>(responseString) ?? new List<UserPass>();

            //_admins = await JsonSerializer.DeserializeAsync<List<UserPass>>(await client.GetStreamAsync(url)) ?? new List<UserPass>();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return _admins;
        }

        public async Task<ShopItem> SearchInventoryAsync(string _itemName)
        {
            ShopItem searchedItem = new ShopItem();
            string url = _apiBaseURL + _itemName ;
            HttpClient client =  new HttpClient();

            try
            {
                string responseString = await client.GetStringAsync(url);
                searchedItem = JsonSerializer.Deserialize<ShopItem>(responseString) ?? new ShopItem();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return  new ShopItem();
        }

        public async Task<List<UserPass>> GetUsersAsync()
        {
            List<UserPass> _users = new List<UserPass>();
            string url = _apiBaseURL + "GetUsers";
            HttpClient client = new HttpClient();
            try
            {
                string responseString = await client.GetStringAsync(url);
                _users = JsonSerializer.Deserialize<List<UserPass>>(responseString) ?? new List<UserPass>();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return _users;
        }
    }
}

