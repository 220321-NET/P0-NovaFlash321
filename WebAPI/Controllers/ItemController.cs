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

        [HttpPut("{itemName}Price")]
        public async Task ChangePriceAsync(ShopItem searchedItem, float newPrice, int storeID)
        {
            await _bl.ChangePriceAsync(searchedItem, newPrice, storeID);
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
        
        //[HttpGet]
        //public Dictionary<int, string> CheckOrderHistory(int _select, int _userID)
        //{
        //    return _bl.CheckOrderHistory(_select, _userID);
        //}

        #endregion
        // GET api/<ItemController>/5
        // POST api/<ItemController>

        


        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    
}
