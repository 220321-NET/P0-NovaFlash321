namespace JAConsoleDL;

public interface IRepo
{

    void CreateNewUser(JAModel.UserPass _newUser);
    void CreateNewAdmin(JAModel.UserPass _newAdmin);
    Task<List<JAModel.UserPass>> GetAllAdminsAsync();
    Task<List<JAModel.UserPass>> GetAllUsersAsync();
    void SaveAdmins();
    Task<List<JAModel.ShopItem>> GetFoodInventoryAsync();
    void CreateNewFoodItem(JAModel.ShopItem _shopItem, int storeID);
    void SaveFoodInventory();
    JAModel.ShopItem SearchInventory(string itemName);
    List<JAModel.Store> GetStores();
    void UpdateFoodItem(JAModel.ShopItem _item, int _additionalQuantity);
    void CreateNewStore(JAModel.Store _newStore);
    void ChangePrice(JAModel.ShopItem _item, float _price, int storeID);
    void RemoveItem(JAModel.ShopItem _item, int storeID);
    void ChangeStore(int _newID, JAModel.UserPass _currentUser);
    List<JAModel.ShopItem> SearchForOrder();
    void AddOrderItem();
    void RemoveOrder();
    void ConfirmOrder(List<JAModel.ShopItem> _order, int storeID, int userID);
    /// <summary>
/// Checks the order history of the user that is logged in
/// </summary>
/// <param name="_select">Sort option for switch case</param>
/// <param name="_userID">Gets current ID from that is logged in</param>
/// <returns>Currently returns list of strings that describe orders, but needs to return dictionary</returns>
    Dictionary<int, string> CheckOrderHistory(int _select, int _userID);
    string GetStoreName(int userID);
    void SaveOrder(List<JAModel.ShopItem> _order);
}