using Microsoft.AspNetCore.Mvc;
using JAConsoleBL;
using JAModel;
using Microsoft.Extensions.Caching.Memory;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IJABL _bl;

        public ItemController(IJABL bl)
        {
            _bl = bl;

        
        }
        #region HTTP Get
        // GET: api/<ItemController>
        //[Route("api/[foodinventory]")]
        [HttpGet("GetInventory")]
        public async Task<List<ShopItem>> GetFoodInventoryAsync()
        {
            return await _bl.GetFoodInventoryAsync();
        }
        
        [HttpGet("GetStores")]
        public async Task<List<Store>> GetStores()
        {
            return await _bl.GetStoresAsync();
        }

        [HttpPost("CreateFoodItem")]
        public async Task CreateNewFoodItem(ShopItem _item) 
        {
            await _bl.CreateNewFoodItemAsync(_item); 
        }
        [HttpPost("CreateNewStore")]
        public async Task CreateNewStoreAsync(Store _store)
        {
            await _bl.CreateNewStoreAsync(_store);
        }

        [HttpGet("GetUsers")]
        public async Task<List<UserPass>> GetAllUsersAsync()
        {
            return await _bl.GetAllUsersAsync(); 
        }

        [HttpGet("GetAdmins")]
        public async Task<List<UserPass>> GetAllAdminsAsync()
        {
            return await _bl.GetAllAdminsAsync();
        }

        [HttpGet("{itemName}/{storeID}")]
        public async Task<ShopItem> GetItemByName(string itemName, int storeID)
        {
            return await _bl.SearchInventoryAsync(itemName, storeID);
        }

        [HttpPut("ChangeItemPrice/{itemID}")]
        public async Task ChangePriceAsync(ShopItem searchedItem)
        {
            await _bl.ChangePriceAsync(searchedItem, searchedItem.Price, searchedItem.StoreID);
        }

        [HttpPut("ChangeStore")]
        public async Task ChangeStoreAsync(UserPass _currentUser)
        {
            int _store = _currentUser.StoreID;
            await _bl.ChangeStoreAsync(_store, _currentUser);
        }

        [HttpPost("CreateNewUser")]
        public async Task CreateNewUserAsync(UserPass _tempUser)
        {
            await _bl.CreateNewUserAsync(_tempUser);
        }

        [HttpPost("CreateNewAdmin")]
        public async Task CreateNewAdminAsync(UserPass _admin)
        {
            await _bl.CreateNewAdminAsync(_admin);
        }

        [HttpPut("UpdateItemQuantity/{_name}/{_storeID}")]
        public async Task UpdateItemQuantityAsync(ShopItem _item, string _name, int _storeID)
        {
            await _bl.UpdateFoodItemAsync(_item);
        }
        
        [HttpGet("GetCartId/{userID}")]
        public async Task<int> GetCartID(int userID)
        {
            return await _bl.GetCartID(userID);
        }


        [HttpGet("GetCart/{userID}")]
        public async Task<Dictionary<int, List<ShopItem>>>GetUserCartAsync(int userID)
        {
            return await _bl.SearchForOrderAsync(userID);
        }
        [HttpPost("CreateCart/{userID}")]
        public async Task CreateCartAsync(int userID)
        {
            await _bl.CreateOrderAsync(userID);
        }

        [HttpPost("UpdateCart/{cartID}/{userID}")]
        public async Task UpdateCart(OrderInstance _instance)
        {
            await _bl.SaveOrderAsync(_instance);
        }
        //[HttpGet]
        //public Dictionary<int, string> CheckOrderHistory(int _select, int _userID)
        //{
        //    return _bl.CheckOrderHistory(_select, _userID);
        //}

        #endregion
        // GET api/<ItemController>/5
        // POST api/<ItemController>

        [HttpPost("ConfirmOrder/{cartID}/{userID}")]
        public async Task ConfirmOrderAsync(Dictionary<int, List<ShopItem>> CartContents)
        {
            
            await _bl.ConfirmOrderAsync(CartContents);

        }

        // DELETE api/<ItemController>/5
        [HttpPut("RemoveInventoryItem/{_itemName}/{storeID}")]
        public async Task RemoveInventoryItemAsync(string _itemName, int storeID)
        {
            ShopItem _item = new ShopItem()
            {
                Name = _itemName,
                StoreID = storeID
            };
            await _bl.RemoveItemAsync(_item, storeID);

        }
        [HttpGet("GetStoreHistory/{_select}/{_storeID}")]
        public async Task<Dictionary<int, string>> GetStoreHistoryAsync(int _select, int _storeID)
        {
            return await _bl.CheckOrderHistoryAsyncAdmin(_select, _storeID);
        }
        [HttpGet("GetUserHistory/{_select}/{_userID}")]
        
        public async Task<Dictionary<int,string>> GetUserOrderHistoryAsync(int _select, int _userID)
        {
            return await _bl.CheckOrderHistoryAsync(_select, _userID);
        }
        
        [HttpGet("GetStoreInventory/{storeID}")]
        public async Task<List<ShopItem>> GetStoreInventoryAsync(int storeID)
        {
            return await _bl.GetStoreInventoryAsync(storeID);
        }


        [HttpGet("GetStoreName/{userID}")]
        public async Task<string> GetStoreNameAsync(int userID)
        {
            return await _bl.GetStoreNameAsync(userID);
        }


        [HttpDelete("RemoveOrderItem/{_itemID}/{_userID}")]
        public async Task RemoveOrderItemAsync(int _itemID, int _userID)
        {
            await _bl.RemoveOrderItemAsync(_itemID, _userID);
        }
    }

    
}
