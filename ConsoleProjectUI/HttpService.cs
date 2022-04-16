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

        public async Task ChangePriceAsync(ShopItem searchedItem, float newPrice)
        {
            searchedItem.Price = newPrice;

            string url = _apiBaseURL + $"ChangeItemPrice/{searchedItem.Id}";
            string serializedItem = JsonSerializer.Serialize(searchedItem);
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

            return allStores;
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
        #region  Functions to Add


        public async Task<Dictionary<int,List<ShopItem>>> SearchForOrderAsync(int userID)
        {   
            Dictionary<int, List<ShopItem>> _order = new Dictionary<int, List<ShopItem>>();
            string url = _apiBaseURL + $"GetCart/{userID}";
            HttpClient client = new HttpClient();
            try
            {
                string responseString = await client.GetStringAsync(url);
                _order = JsonSerializer.Deserialize<Dictionary<int, List<ShopItem>>>(responseString) ?? new Dictionary<int, List<ShopItem>>();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            
            //http get
        //search for order from json
        return _order;
        }

        public async Task<string> GetStoreNameAsync(int userID)
        {   
            string url = _apiBaseURL + $"GetStoreName/{userID}";
            HttpClient client = new HttpClient();
            string storeName = "";
            try
            {
                storeName = await client.GetStringAsync(url);

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return storeName;
        //get store name
        }

        public async Task RemoveOrderItemAsync(int _itemID, int _userID)
        {
            string url = _apiBaseURL + $"RemoveOrderItem/{_itemID}/{_userID}";
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage responseMessage = await client.DeleteAsync(url);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            //remove item from order cart
            //http delete
        }

        
        public async Task SaveOrderAsync(OrderInstance _instance)
        {
            string url = _apiBaseURL + $"UpdateCart/{_instance.CartID}/{_instance.UserID}";

            string serializedItem = JsonSerializer.Serialize(_instance);
            StringContent sContent = new StringContent(serializedItem, Encoding.UTF8, "application/json"); 
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.PostAsync(url, sContent);
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        //creates to cart instance table
        //http post
        //save cart to database as temp row
        }

        public async Task<int> GetCartId(int userID)
        {
            int cartID = 0;
            string url = _apiBaseURL + $"GetCartId/{userID}";
            HttpClient client = new HttpClient();
            try
            {
                cartID = int.Parse(await client.GetStringAsync(url));
            }
            catch(HttpRequestException e){
                Console.WriteLine(e.Message);
            }



            return cartID;
        }

        public async Task CreateOrderAsync(int userID)
        {
            string url = _apiBaseURL + $"CreateCart/{userID}";
            string serializedItem = JsonSerializer.Serialize(userID);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client =  new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public async Task ConfirmOrderAsync(List<ShopItem> _order, int _userID, int cartID)
        {
            string url = _apiBaseURL + $"ConfirmOrder/{cartID}/{_userID}";
            Dictionary<int, List<ShopItem>> Cart = new Dictionary<int, List<ShopItem>>();
            Cart.Add(_userID, _order);
            
            string serializedItem = JsonSerializer.Serialize(Cart);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PostAsync(url,content);
                response.EnsureSuccessStatusCode();

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

        } 

        public async Task<Dictionary<int,string>> CheckOrderHistoryAsync(int _select, int _userID)
        {
            string url = _apiBaseURL + $"GetUserHistory/{_select}/{_userID}";

            Dictionary<int,string> _history = new Dictionary<int, string>();
            HttpClient client = new HttpClient();
            try
            {
                string responseString = await client.GetStringAsync(url);
                _history = JsonSerializer.Deserialize<Dictionary<int, string>>(responseString) ?? new Dictionary<int, string>();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }


            return _history;
        }

        public async Task ChangeStoreAsync(int _storeID, UserPass _currentUser)
        {
            string url = _apiBaseURL + "ChangeStore";
            _currentUser.StoreID = _storeID;
            string serializedItem = JsonSerializer.Serialize(_currentUser);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            //http put
        //change store
        }


        public async Task CreateNewUserAsync(UserPass _tempUser)
        {
            string url = _apiBaseURL + "CreateNewUser";
            string serializedItem = JsonSerializer.Serialize(_tempUser);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            //http post
        //create new user
        }

        public async Task CreateNewAdminAsync(UserPass _tempAdmin)
        {
            string url = _apiBaseURL + "CreateNewAdmin";
            string serializedItem = JsonSerializer.Serialize(_tempAdmin);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            //http post
        //create new admin
        }

        public async Task UpdateItemQuantityAsync(ShopItem _item, int _quantity)
        {
            string url = _apiBaseURL + $"UpdateItemQuantity/{_item.Name}/{_item.StoreID}";
            _item.Quantity += _quantity;
            string serializedItem = JsonSerializer.Serialize(_item);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        public async Task RemoveItem(ShopItem _searchedItem, int _storeID)
        {
            string url = _apiBaseURL + $"RemoveInventoryItem/{_searchedItem.Name}/{_storeID}";
            _searchedItem.StoreID = _storeID;
            string serializedItem = JsonSerializer.Serialize(_searchedItem);
            StringContent content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
