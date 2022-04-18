namespace JAConsoleBL;

public interface IJABL
{
    /// <summary>
    /// Creats a new admin, that can restock inventory, and add new stores if opened up
    /// </summary>
    /// <param name="_newAdmin">Credentials of the new admin</param>
    Task CreateNewAdminAsync(JAModel.UserPass _newAdmin);
    /// <summary>
    /// Gets all administrators from the application
    /// </summary>
    /// <returns>Returns a list of admins to check if the user is an admin</returns>
    Task<List<JAModel.UserPass>> GetAllAdminsAsync();
    /// <summary>
    /// Gets all users from the application, including administrators
    /// </summary>
    /// <returns>Returns a list of users to check if a user currently exists with the same credentials</returns>
    Task<List<JAModel.UserPass>> GetAllUsersAsync();
    Task SaveAdminsAsync();
    Task< List<JAModel.ShopItem>> GetFoodInventoryAsync();
    /// <summary>
    /// Creates a new food item in the database
    /// </summary>
    /// <param name="_shopItem">Item that holds data that is stored to the database</param>
    /// <param name="storeID">The store that the admin is restocking at</param>
    Task CreateNewFoodItemAsync(JAModel.ShopItem _shopItem);

    Task SaveFoodInventoryAsync();
    /// <summary>
    /// Searches the current food inventory to check for existing food items any admin is restocking with
    /// </summary>
    /// <param name="itemName">The name of the food item that the user has searched for</param>
    /// <returns>Returns the shop item if there is a match; returns null if no match</returns>
    Task <JAModel.ShopItem> SearchInventoryAsync(string itemName, int storeID);
    
    /// <summary>
    /// Creates a new user in the database that can create orders
    /// </summary>
    /// <param name="_newUser">User credentials</param>
    Task CreateNewUserAsync(JAModel.UserPass _newUser);

    /// <summary>
    /// Gets all the stores in the database
    /// </summary>
    /// <returns>Returns a list of stores</returns>
    Task<List<JAModel.Store>> GetStoresAsync();
    /// <summary>
    /// Updates a food item, relating to modifying the quantity of the inventory
    /// </summary>
    /// <param name="_item">The food item that is being modified</param>
    /// <param name="_additionalQuantity">The additional quantity that the admin is restocking with</param>
    Task UpdateFoodItemAsync(JAModel.ShopItem _item);
    /// <summary>
    /// Creates a new store in the franchise
    /// </summary>
    /// <param name="_newStore"></param>
    Task CreateNewStoreAsync(JAModel.Store _newStore);
    
    /// <summary>
    /// Removes an item from a user's current order
    /// </summary>
    /// <param name="_item">Name of the item that the user wants to remove</param>
    /// <param name="storeID">The store ID that the user is shopping at</param>
    Task RemoveItemAsync(JAModel.ShopItem _item, int storeID);
    /// <summary>
    /// Modifies the price of an existing item in the store
    /// </summary>
    /// <param name="_item">The item that the price is being modified to</param>
    /// <param name="_price">The new price that the admin inputs</param>
    /// <param name="storeID">The store that the admin is changing the price for</param>
    Task ChangePriceAsync(JAModel.ShopItem _item, float _price, int storeID);
    
    /// <summary>
    /// Changes the store that an admin or a user is shopping/restocking at
    /// </summary>
    /// <param name="_newID">The ID of the store that the user/admin inputs</param>
    /// <param name="_currentUser">The current user that is logged in</param>
    Task ChangeStoreAsync(int _newID, JAModel.UserPass _currentUser);

    /// <summary>
    /// Searches if an order exists with the user that is currently logged in
    /// </summary>
    /// <returns>Returns a list that the user can resume shopping with</returns>
    Task<Dictionary<int, List<JAModel.ShopItem>>> SearchForOrderAsync(int userID);
    Task AddOrderItemAsync();
    Task RemoveOrderAsync();
    /// <summary>
    /// Confirms the order that a user has input, based on a JSON file
    /// </summary>
    /// <param name="_order">The list of items in the user's order</param>
    /// <param name="storeID">The store ID that the user is shopping at, that the inventory is taken from</param>
    /// <param name="userID">Gets current ID from the user that is logged in</param>
    Task ConfirmOrderAsync(Dictionary<int, List<JAModel.ShopItem>> Order);
        /// <summary>
/// Checks the order history of the user that is logged in
/// </summary>
/// <param name="_select">Sort option for switch case</param>
/// <param name="_userID">Gets current ID from that is logged in</param>
/// <returns>Returns the order history based on the user that is currently logged in</returns>
    Task<Dictionary<int, string>> CheckOrderHistoryAsync(int _select, int _userID);
        Task<Dictionary<int, string>> CheckOrderHistoryAsyncAdmin(int _select, int _storeID);
    /// <summary>
    /// Saves the order to a JSON file, that can be used later
    /// </summary>
    /// <param name="_order">The list that the user has on hand currently, which will be saved with</param>
    Task SaveOrderAsync(JAModel.OrderInstance _instance);
    /// <summary>
    /// Gets the store name that the user is shopping at
    /// </summary>
    /// <param name="userID">Gets the ID of the current user that is shopping</param>
    /// <returns></returns>

    Task<string> GetStoreNameAsync(int userID);
    Task<List<JAModel.ShopItem>> GetStoreInventoryAsync(int _storeID);
    Task CreateOrderAsync(int userID);
    Task<int> GetCartID(int userID);
    Task RemoveOrderItemAsync(int _itemID, int _userID);

}