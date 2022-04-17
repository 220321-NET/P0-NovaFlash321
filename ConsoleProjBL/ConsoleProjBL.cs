using JAModel;
using JAConsoleDL;
namespace JAConsoleBL;

public class ConsoleProjBL : IJABL 
{ 
    public async Task CreateOrderAsync(int userID)
    {
        await _repo.CreateOrderAsync(userID);
    }
    public async Task ChangeStoreAsync(int _newID, JAModel.UserPass _currentUser)
    {
        await _repo.ChangeStoreAsync(_newID, _currentUser);
    }
    private readonly IRepo _repo;
    public ConsoleProjBL(IRepo repo)
    {
        _repo = repo;
    }
    public async Task CreateNewAdminAsync(UserPass _newAdmin)
    {
            await _repo.CreateNewAdminAsync(_newAdmin);
    }

    public async Task SaveOrderAsync(OrderInstance _instance)
    {
        await _repo.SaveOrderAsync(_instance);
    }
    public  async Task RemoveItemAsync(JAModel.ShopItem _item, int storeID)
    {
        await _repo.RemoveItemAsync(_item, storeID);
    }

    public  async Task RemoveOrderItemAsync(int _itemID, int _userID)
    {
        await _repo.RemoveOrderItemAsync(_itemID, _userID);
    }

    public async Task<int> GetCartID(int userID)
    {
        return await _repo.GetCartID(userID);
    }
    public async Task<List<UserPass>> GetAllAdminsAsync()
    {
        
        return await _repo.GetAllAdminsAsync();
        
    }
    public async Task<List<UserPass>> GetAllUsersAsync()
    {
        return await _repo.GetAllUsersAsync();
    }
    public async Task SaveAdminsAsync()
    {
        await _repo.SaveAdminsAsync();
    }
    

    public async Task<List<ShopItem>> GetFoodInventoryAsync()
    {
        
        return await _repo.GetFoodInventoryAsync();
    }

    public async Task CreateNewFoodItemAsync(ShopItem _shopItem)
    {
        await _repo.CreateNewFoodItemAsync(_shopItem);
    }

public async Task UpdateFoodItemAsync(JAModel.ShopItem _item)
    {
        await _repo.UpdateFoodItemAsync(_item);
    }
    public async Task SaveFoodInventoryAsync()
    {
        await _repo.SaveFoodInventoryAsync();
    }

    public async Task <JAModel.ShopItem> SearchInventoryAsync(string itemName, int storeID)
    {

        return await _repo.SearchInventoryAsync(itemName, storeID);
    }
    public async Task CreateNewUserAsync(JAModel.UserPass _newUser)
    {
        await _repo.CreateNewUserAsync(_newUser);
    }

    public async Task<List<Store>> GetStoresAsync()
    {
        return await _repo.GetStoresAsync();
    }
    public async Task CreateNewStoreAsync(JAModel.Store _newStore)
    {
        await _repo.CreateNewStoreAsync(_newStore);
    }

    public async Task ChangePriceAsync(JAModel.ShopItem _item, float _newPrice, int storeID)
    {
        await _repo.ChangePriceAsync(_item,_newPrice, storeID);
    }


    #region UserMenu

public async Task<Dictionary<int, List<JAModel.ShopItem>>> SearchForOrderAsync(int userID)
{
    return await _repo.SearchForOrderAsync(userID);
}
public async Task AddOrderItemAsync()
{
    await _repo.AddOrderItemAsync();
}

public async Task RemoveOrderAsync()
{
    await _repo.RemoveOrderAsync();
}
public async Task ConfirmOrderAsync(Dictionary<int, List<JAModel.ShopItem>> Order)
{
    await _repo.ConfirmOrderAsync(Order);
}
public async Task<string> GetStoreNameAsync(int userID)
{
    return await _repo.GetStoreNameAsync(userID);
}

public async   Task<List<JAModel.ShopItem>> GetStoreInventoryAsync(int _storeID)
{
    return await _repo.GetStoreInventoryAsync(_storeID);
}


public async Task< Dictionary<int, string>> CheckOrderHistoryAsync(int _select, int _userID)
{
    return await _repo.CheckOrderHistoryAsync(_select, _userID);
}
public async Task<Dictionary<int, string>> CheckOrderHistoryAsyncAdmin(int _select, int _storeID)
{
    return await _repo.CheckOrderHistoryAsyncAdmin(_select, _storeID);
}


    #endregion


}
