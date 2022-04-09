using JAModel;
using JAConsoleDL;
namespace JAConsoleBL;

public class ConsoleProjBL : IJABL 
{ 
    public void ChangeStore(int _newID, JAModel.UserPass _currentUser)
    {
        _repo.ChangeStore(_newID, _currentUser);
    }
    private readonly IRepo _repo;
    public ConsoleProjBL(IRepo repo)
    {
        _repo = repo;
    }
    public void CreateNewAdmin(UserPass _newAdmin)
    {
            _repo.CreateNewAdmin(_newAdmin);
    }

    public void SaveOrder(List<JAModel.ShopItem> _order)
    {
        _repo.SaveOrder(_order);
    }
    public  void RemoveItem(JAModel.ShopItem _item, int storeID)
    {
        _repo.RemoveItem(_item, storeID);
    }

    public async Task<List<UserPass>> GetAllAdminsAsync()
    {
        
        return await _repo.GetAllAdminsAsync();
        
    }
    public async Task<List<UserPass>> GetAllUsersAsync()
    {
        return await _repo.GetAllUsersAsync();
    }
    public void SaveAdmins()
    {
        _repo.SaveAdmins();
    }
    

    public async Task<List<ShopItem>> GetFoodInventoryAsync()
    {
        
        return await _repo.GetFoodInventoryAsync();
    }

    public void CreateNewFoodItem(ShopItem _shopItem, int storeID)
    {
        _repo.CreateNewFoodItem(_shopItem, storeID);
    }

public void UpdateFoodItem(JAModel.ShopItem _item, int _additionalQuantity)
    {
        _repo.UpdateFoodItem(_item, _additionalQuantity);
    }
    public void SaveFoodInventory()
    {
        _repo.SaveFoodInventory();
    }

    public JAModel.ShopItem SearchInventory(string itemName)
    {

        return _repo.SearchInventory(itemName);
    }
    public void CreateNewUser(JAModel.UserPass _newUser)
    {
        _repo.CreateNewUser(_newUser);
    }

    public List<Store> GetStores()
    {
        return _repo.GetStores();
    }
    public void CreateNewStore(JAModel.Store _newStore)
    {
        _repo.CreateNewStore(_newStore);
    }

    public void ChangePrice(JAModel.ShopItem _item, float _newPrice, int storeID)
    {
        _repo.ChangePrice(_item,_newPrice, storeID);
    }


    #region UserMenu

public List<JAModel.ShopItem> SearchForOrder()
{
    return _repo.SearchForOrder();
}
public void AddOrderItem()
{
_repo.AddOrderItem();
}

public void RemoveOrder()
{
_repo.RemoveOrder();
}
public void ConfirmOrder(List<JAModel.ShopItem> _order, int storeID, int userID)
{
    _repo.ConfirmOrder(_order, storeID, userID);
}
public string GetStoreName(int userID)
{
    return _repo.GetStoreName(userID);
}
public Dictionary<int, string> CheckOrderHistory(int _select, int _userID)
{
    return _repo.CheckOrderHistory(_select, _userID);
}


    #endregion


}
