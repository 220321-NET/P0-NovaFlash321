namespace JAConsoleDL;

public interface IRepo
{

    Task CreateNewUserAsync(JAModel.UserPass _newUser);
    Task CreateNewAdminAsync(JAModel.UserPass _newAdmin);
    Task<List<JAModel.UserPass>> GetAllAdminsAsync();
    Task<List<JAModel.UserPass>> GetAllUsersAsync();
    Task SaveAdminsAsync(); 
    Task<List<JAModel.ShopItem>> GetFoodInventoryAsync();
    Task CreateNewFoodItemAsync(JAModel.ShopItem _shopItem);
    Task SaveFoodInventoryAsync();
    Task <JAModel.ShopItem> SearchInventoryAsync(string itemName, int storeID);
    Task<List<JAModel.Store>> GetStoresAsync(); 
    Task UpdateFoodItemAsync(JAModel.ShopItem _item);
    Task CreateNewStoreAsync(JAModel.Store _newStore);
    Task ChangePriceAsync(JAModel.ShopItem _item, float _price, int storeID);
    Task RemoveItemAsync(JAModel.ShopItem _item, int storeID);
    Task ChangeStoreAsync(int _newID, JAModel.UserPass _currentUser);
    Task<Dictionary<int, List<JAModel.ShopItem>>> SearchForOrderAsync(int userID);
    Task AddOrderItemAsync();
    Task<Dictionary<int, string>> CheckOrderHistoryAsyncAdmin(int _select, int _storeID);
    Task RemoveOrderAsync();
    Task ConfirmOrderAsync(Dictionary<int, List<JAModel.ShopItem>> Order);
    /// <summary>
/// Checks the order history of the user that is logged in
/// </summary>
/// <param name="_select">Sort option for switch case</param>
/// <param name="_userID">Gets current ID from that is logged in</param>
/// <returns>Currently returns list of strings that describe orders, but needs to return dictionary</returns>
    Task<Dictionary<int, string>> CheckOrderHistoryAsync(int _select, int _userID);
    Task<string> GetStoreNameAsync(int userID);
    Task SaveOrderAsync(JAModel.OrderInstance _instance);
    Task CreateOrderAsync(int userID);
    Task<int> GetCartID(int userID);
    Task RemoveOrderItemAsync(int _itemID, int _userID);
    Task<List<JAModel.ShopItem>> GetStoreInventoryAsync(int _storeID);
}

    