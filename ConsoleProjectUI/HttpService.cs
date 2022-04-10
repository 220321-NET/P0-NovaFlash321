using System.Net.Http;
using System.Text.Json;
using System.Text;
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

        public async Task<ShopItem> SearchInventoryAsync(string _itemName, int storeID)
        {
            ShopItem searchedItem = new ShopItem();
            string url = _apiBaseURL + _itemName + "/" + storeID;
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

            return searchedItem;
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

        public async Task ChangePriceAsync(string searchedItem, float newPrice, int storeID)
        {
            ShopItem searchedShopItem = await SearchInventoryAsync(searchedItem, storeID);
            searchedShopItem.Price = newPrice;
            searchedShopItem.StoreID = storeID;
            string url = _apiBaseURL + searchedItem  +"Price";
            string serializedItem = JsonSerializer.Serialize(searchedShopItem);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
             HttpResponseMessage response = await client.PutAsync(url, content);
             response.EnsureSuccessStatusCode();   
                
            }
            catch (HttpRequestException e)
            {
               Console.Write(e.Message);
            }
        }

        public async Task<List<Store>> GetStoresAsync() 
        {
            List<Store> allStores = new List<Store>();
            string url = _apiBaseURL + "GetStores";
            HttpClient client = new HttpClient();
            try
            {
                string responseString = await client.GetStringAsync(url);
                allStores = JsonSerializer.Deserialize<List<Store>>(responseString) ?? new List<Store>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

            return new List<Store>();
        }

        public async Task CreateNewFoodItemAsync(ShopItem _shopItem, int storeID)
        {
            string url = _apiBaseURL + "CreateFoodItem";
            _shopItem.StoreID = storeID;
            string serializedItem = JsonSerializer.Serialize(_shopItem);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Shop Successfully Created!");
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task CreateNewStoreAsync(Store _store)
        {
            string url = _apiBaseURL + "CreateNewStore";
            string serializedStore = JsonSerializer.Serialize(_store);
            StringContent content = new StringContent(serializedStore, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

