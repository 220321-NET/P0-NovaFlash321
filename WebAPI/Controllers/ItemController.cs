using Microsoft.AspNetCore.Mvc;
using JAConsoleBL;
using JAModel;

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
        //[Route("api/[stores]")]
        //[HttpGet("[Stores]")]
        //public List<Store> GetStores()
        //{
        //    return _bl.GetStores();
        //}

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

        [HttpGet("{itemName}")]
        public ShopItem GetItemByName(string itemName)
        {
            return _bl.SearchInventory(itemName);
        }

        //[HttpGet]
        //public string GetStoreName(int userID)
        //{
        //    return _bl.GetStoreName(userID);
        //}

        //[HttpGet]
        //public Dictionary<int, string> CheckOrderHistory(int _select, int _userID)
        //{
        //    return _bl.CheckOrderHistory(_select, _userID);
        //}

        #endregion
        // GET api/<ItemController>/5
        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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
