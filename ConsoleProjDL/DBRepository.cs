using Microsoft.Data.SqlClient;
using System.Text.Json;
namespace JAConsoleDL;


/*
ADO.NET: is a collection of libraries that helps developers write data access
code in a uniform fashion regardless of what data source they are dealing with

All you need to be able to work with the data source is a driver that is suited for a particular data source. For example, for SQL Server,
we need Microsoft.Data.SqlClient. Install that to DL layer by navigating to DL folder and running "dotnet add package Microsoft.Data.SqlClient"

Two architecture styles: Connected and Disconnected

Connected:  We use objects such as DBConnection, DBCommand, DataRader to accesss data while we're connected to the database. DataReader is much faster at reading
large amount of data compared to Disconnected Architecture 

Disconnected Architecture: We use objects such as Data Adapter and DataSet to have access to the data even when we are not connected to the database. 
Advantage is that we have access to the schema of result set, so we can refer to the column by their name, instead of accessing by index
*/
public class DBRespository : IRepo
{
    private readonly string orderFilePath = "G:/Training/Week1/P1/ConsoleProjDL/Order.json";
    private readonly string errorFilePath = "G:/Training/Week1/P1/ConsoleProjDL/ConsoleLog.json";

    private readonly string _connectionString;


    public DBRespository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateNewStore(JAModel.Store _newStore)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO StoreFront(storeName, storeAddress, storeCity, storeState, storeCountry, storeZIP) OUTPUT INSERTED.storeID VALUES (@name, @address, @city, @state, @country, @zip)", connection);
        cmd.Parameters.AddWithValue("@name",_newStore.StoreName);
        cmd.Parameters.AddWithValue("@address", _newStore.StoreAddress);
        cmd.Parameters.AddWithValue("@city", _newStore.StoreCity);
        cmd.Parameters.AddWithValue("@state", _newStore.StoreState);
        cmd.Parameters.AddWithValue("@country", _newStore.StoreCountry);
        cmd.Parameters.AddWithValue("@zip", _newStore.StoreZIP);

        try
        {
            _newStore.storeID = (int)cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
// cmd = new SqlCommand("INSERT INTO ShopItem(productName, productPrice, productQuantity, productType, storeID) OUTPUT INSERTED.productID VALUES(@name, @price, @quantity, @type, @storeid)", connection);

    }



    public void CreateNewAdmin(JAModel.UserPass _newAdmin)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO Users(userName, passWord, firstName, lastName, isAdmin, storeID) OUTPUT INSERTED.userID VALUES(@user, @pass, @first, @last, @isadmin, @storeid)", connection);
        cmd.Parameters.AddWithValue("@user", _newAdmin.UserName);
        cmd.Parameters.AddWithValue("@pass", _newAdmin.PassWord);
        cmd.Parameters.AddWithValue("@first", _newAdmin.FirstName);
        cmd.Parameters.AddWithValue("@last", _newAdmin.LastName);
        cmd.Parameters.AddWithValue("@storeid", _newAdmin.StoreID);
        cmd.Parameters.AddWithValue("@isadmin", 1);

        try
        {
            _newAdmin.UserID = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
    }

    public void CreateNewUser(JAModel.UserPass _newUser)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO Users(userName, passWord, firstName, lastName, isAdmin, storeID) OUTPUT INSERTED.userID VALUES(@user, @pass, @first, @last, @isadmin, @storeid)", connection);
        cmd.Parameters.AddWithValue("@user",_newUser.UserName);
        cmd.Parameters.AddWithValue("@pass",_newUser.PassWord);
        cmd.Parameters.AddWithValue("@first",_newUser.FirstName);
        cmd.Parameters.AddWithValue("@last",_newUser.LastName);
        cmd.Parameters.AddWithValue("@isadmin",0);
        cmd.Parameters.AddWithValue("@storeid", _newUser.StoreID);
        

        try
        {
            _newUser.UserID = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        
        
        //        cmd = new SqlCommand("INSERT INTO ShopItem(productName, productPrice, productQuantity, productType, storeID) OUTPUT INSERTED.productID VALUES(@name, @price, @quantity, @type, @storeid)", connection);
    }

    public List<JAModel.Store> GetStores()
    {
        List<JAModel.Store> allStores = new List<JAModel.Store>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM StoreFront", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            int storeID = reader.GetInt32(0);
            string storeName = reader.GetString(1);
            string storeAddress = reader.GetString(2);
            string storeCity = reader.GetString(3);
            string storeState = reader.GetString(4);
            string storeCountry = reader.GetString(5);
            int storeZIP = reader.GetInt32(6);

            JAModel.Store _newStore = new JAModel.Store()
            {
                storeID = storeID,
                StoreName = storeName,
                StoreAddress = storeAddress,
                StoreCity = storeCity,
                StoreState = storeState,
                StoreCountry = storeCountry,
                StoreZIP = storeZIP
            };

            //string Store = $"[{storeID}] - {storeName}";
            allStores.Add(_newStore);
        }
        connection.Close();
        return allStores;
    }

    public async Task<List<JAModel.UserPass>> GetAllUsersAsync()
    {
        //Closed connection
        List<JAModel.UserPass> allUsers = new List<JAModel.UserPass>();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("SELECT * FROM Users;", connection);
        SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            int userID = reader.GetInt32(0);
            string username = reader.GetString(1);
            string password = reader.GetString(2);
            int storeID = reader.GetInt32(4);
            string firstname = reader.GetString(6);
            string lastname = reader.GetString(5);

            JAModel.UserPass user = new JAModel.UserPass
            {
                UserID = userID,
                UserName = username,
                PassWord = password,
                FirstName = firstname,
                LastName = lastname,
                StoreID = storeID,
            };
            allUsers.Add(user);
        }
        connection.Close();

        return allUsers;
    }

    public void SaveAdmins()
    {

    }

    /// <summary>
    /// Returns a list of all food items in food table
    /// </summary>
    /// <returns>List of ShopItem, if none, empty list</returns>



#region FUNCTIONAL 
    public void UpdateFoodItem(JAModel.ShopItem _item, int _additionalQuantity)
    {
        //Closed connection
        int quantity = 0;
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand();
        
        cmd = new SqlCommand("SELECT * FROM ShopItem WHERE productName = @name", connection);
        cmd.Parameters.AddWithValue("@name",_item.Name);
        SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            quantity = _additionalQuantity + reader.GetInt32(3);
            
        }
        connection.Close();
        connection.Open();
        cmd = new SqlCommand("UPDATE ShopItem SET productQuantity = @quantity WHERE productName = @name", connection);

        cmd.Parameters.AddWithValue("@quantity", quantity);
        cmd.Parameters.AddWithValue("@name", _item.Name);

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
            
        }
    
        connection.Close();
    }

    public async Task<List<JAModel.UserPass>> GetAllAdminsAsync()
    {
        //Closed connection
        List<JAModel.UserPass> allAdmins = new List<JAModel.UserPass>();

        SqlConnection connection = new SqlConnection(_connectionString);
        
        connection.Open();
        //List<int> storeID = new List<int>();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE isAdmin = 1;", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            
            if(reader.GetBoolean(3))
            {
                
            int userID = reader.GetInt32(0);
            string username = reader.GetString(1);
            string password = reader.GetString(2);
            int storeID = reader.GetInt32(4);
            string firstname = reader.GetString(6);
            string lastname = reader.GetString(5);
        
            JAModel.UserPass admin = new JAModel.UserPass
            {
                UserID = userID,
                UserName = username,
                PassWord = password,
                StoreID = storeID,
                FirstName = firstname,
                LastName = lastname
            };
            allAdmins.Add(admin);
            }
        }
        


        connection.Close();

        return allAdmins;
    }
    public async Task<List<JAModel.ShopItem>> GetFoodInventoryAsync()
    {
        List<JAModel.ShopItem> allFood = new List<JAModel.ShopItem>();

        SqlConnection connection = new SqlConnection(_connectionString);
        
        connection.Open();
        

        SqlCommand cmd = new SqlCommand("SELECT * FROM ShopItem;", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        
        while(reader.Read())
            {
                //int productID = reader.GetInt32(0);
                string productName = reader.GetString(1);
                decimal productPrice = reader.GetDecimal(2);
                int productQuantity = reader.GetInt32(3);
                int storeID = reader.GetInt32(4);
                string productType = reader.GetString(5);

                JAModel.ShopItem item = new JAModel.ShopItem
                {
                    TypeOfFood = productType,
                    Name = productName,
                    Price = (float)productPrice,
                    Quantity = productQuantity,
                    StoreID = storeID,
                };
                allFood.Add(item);
            }

        connection.Close();
        return allFood;
    }
    public void CreateNewFoodItem(JAModel.ShopItem _shopItem, int storeID)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        
        SqlCommand cmd = new SqlCommand();

        List<JAModel.ShopItem> correspondingItems = new List<JAModel.ShopItem>();


        
        cmd = new SqlCommand("INSERT INTO ShopItem(productName, productPrice, productQuantity, productType, storeID) OUTPUT INSERTED.productID VALUES(@name, @price, @quantity, @type, @storeid)", connection);

        cmd.Parameters.AddWithValue("@name", _shopItem.Name);
        cmd.Parameters.AddWithValue("@price", _shopItem.Price);
        cmd.Parameters.AddWithValue("@quantity", _shopItem.Quantity);
        cmd.Parameters.AddWithValue("@type", _shopItem.TypeOfFood);
        cmd.Parameters.AddWithValue("@storeid", storeID);


        try
        {
            _shopItem.Id = (int) cmd.ExecuteScalar();


        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }

        // cmd = new SqlCommand("UPDATE ShopItem SET productPrice = @price WHERE productName = @name", connection);
        // cmd.Parameters.AddWithValue("@name", _shopItem.Name);
        // cmd.Parameters.AddWithValue("@price", (decimal)_shopItem.Price);

        // try
        // {
        // _shopItem.Id = (int) cmd.ExecuteScalar();
        // }
        // catch(Exception e)
        // {
        //     Console.WriteLine(e.Message);
        // }

        connection.Close();
    }
    public void SaveFoodInventory()
    {

    }

    public JAModel.ShopItem SearchInventory(string itemName)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        JAModel.ShopItem item = new JAModel.ShopItem();
        SqlCommand cmd = new SqlCommand();

        cmd = new SqlCommand("SELECT * FROM ShopItem WHERE productName = @name",connection);
        cmd.Parameters.AddWithValue("@name", itemName);
        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            //int productID = reader.GetInt32(0);
                int productID = reader.GetInt32(0);
                string productName = reader.GetString(1);
                decimal productPrice = reader.GetDecimal(2);
                int productQuantity = reader.GetInt32(3);
                int storeID = reader.GetInt32(4);
                string productType = reader.GetString(5);

                item = new JAModel.ShopItem
                {   
                    
                    TypeOfFood = productType,
                    Name = productName,
                    Price = (float)productPrice,
                    Quantity = productQuantity,
                    StoreID = storeID,
                    Id = productID,
                };
                


        }
        
        connection.Close();
        return item;
    }

    
    public void RemoveItem(JAModel.ShopItem _item, int storeID)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("DELETE FROM ShopItem WHERE productName = @name AND storeID = @storeid",connection);
        cmd.Parameters.AddWithValue("@name", _item.Name);
        cmd.Parameters.AddWithValue("storeid", storeID);
        
        try
        {
            _item.Id = (int)cmd.ExecuteNonQuery();
        }
        catch(Exception  e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        connection.Close();
    }
    
    public void ChangePrice(JAModel.ShopItem _item, float _price, int storeID)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("UPDATE ShopItem SET productPrice = @price WHERE productName = @name AND storeID = @storeid",connection);
        cmd.Parameters.AddWithValue("@price", _price);
        cmd.Parameters.AddWithValue("@name", _item.Name);
        cmd.Parameters.AddWithValue("@storeid", storeID);

        try
        {
            _item.Id = (int)cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        connection.Close();
    }

    public void ChangeStore(int _newID, JAModel.UserPass _currentUser)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand();
        cmd = new SqlCommand("UPDATE Users SET storeID = @storeid WHERE userName = @userid", connection);
        cmd.Parameters.AddWithValue("@storeid", _newID);
        cmd.Parameters.AddWithValue("@userid", _currentUser.UserName);

        try
        {
            _currentUser.UserID = (int)cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        connection.Close();
    }


    public void SaveOrder(List<JAModel.ShopItem> _order)
    {
        string jsonContents = JsonSerializer.Serialize(_order);
        File.WriteAllText(orderFilePath, jsonContents);

            
    }

    public List<JAModel.ShopItem> SearchForOrder()
    {
        string jsonContents = "";
        try
        {
            jsonContents = File.ReadAllText(orderFilePath);
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        List<JAModel.ShopItem> _order = new List<JAModel.ShopItem>();
        try
        {
            _order = JsonSerializer.Deserialize<List<JAModel.ShopItem>>(jsonContents) ?? new List<JAModel.ShopItem>();

        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }

        return _order;
    }
    public void AddOrderItem()
    {   

    }

    public string GetStoreName(int userID)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("SELECT StoreFront.storeID, StoreFront.storeName, Users.userID FROM StoreFront JOIN Users ON StoreFront.storeID = Users.storeID WHERE Users.userID = @userid",connection);
        cmd.Parameters.AddWithValue("@userid", userID);
        SqlDataReader reader = cmd.ExecuteReader();
        string _storeName = "";
        while(reader.Read())
        {
            _storeName = reader.GetString(1);
        }
        return _storeName;
    }

        public void RemoveOrder()
    {
        string jsonContents = "[]";
        File.WriteAllText(orderFilePath, jsonContents);
    }
    public void ConfirmOrder(List<JAModel.ShopItem> _order, int storeID, int userID)
    {
        List<JAModel.ShopItem> _curInventory = new List<JAModel.ShopItem>();
        SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();
        SqlCommand neworder = new SqlCommand("INSERT INTO OrderHistory (userID, orderDate) VALUES (@userid, GETDATE())", connection);
        neworder.Parameters.AddWithValue("userid", userID);

        try
        {
            neworder.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        connection.Close();
        connection.Open();

        neworder = new SqlCommand("SELECT userID, MAX(orderID) FROM OrderHistory WHERE userID = @userid GROUP BY userID",connection);
        neworder.Parameters.AddWithValue("@userid", userID);
        SqlDataReader newReader = neworder.ExecuteReader();
        int orderID = 0; 
        while(newReader.Read())
        {
            orderID = newReader.GetInt32(1);

        }

        connection.Close();
        float totalPrice = 0f;
        foreach(JAModel.ShopItem _item in _order)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ShopItem WHERE productName = @name AND storeID = @storeid", connection);
            cmd.Parameters.AddWithValue("@name", _item.Name);
            cmd.Parameters.AddWithValue("@storeid", storeID);
            SqlDataReader reader = cmd.ExecuteReader();
            int quantity = 0;
            while(reader.Read())
            {
                quantity = (reader.GetInt32(3)) - _item.Quantity;
            }
            connection.Close();
            connection.Open();
            cmd = new SqlCommand("UPDATE ShopItem SET productQuantity = @quantity WHERE productName = @name AND storeID = @storeid",connection);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@name", _item.Name);
            cmd.Parameters.AddWithValue("@storeid", storeID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
            }
            connection.Close();
            totalPrice += (_item.Price * _item.Quantity);
        }
        
        totalPrice = float.Parse((totalPrice).ToString("########.00"));
        foreach(JAModel.ShopItem _item in _order)
        {
            connection.Open();
            SqlCommand ordercmd = new SqlCommand("INSERT INTO OrderInstance(orderID, storeID, totalPrice, userID, productID, orderQuantity)  VALUES(@orderid, @storeid, @totalprice, @userid, @productid, @quantity)", connection);
            ordercmd.Parameters.AddWithValue("@orderid", orderID);
            ordercmd.Parameters.AddWithValue("@storeid", storeID);
            ordercmd.Parameters.AddWithValue("@totalprice", float.Parse((_item.Price * _item.Quantity).ToString("###.00")));
            ordercmd.Parameters.AddWithValue("@userid", userID);
            ordercmd.Parameters.AddWithValue("@productid", _item.Id);
            ordercmd.Parameters.AddWithValue("@quantity", _item.Quantity);

            try
            {
                ordercmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                    Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
            }
            connection.Close();
        }   
        connection.Open();
        SqlCommand costcmd = new SqlCommand("UPDATE OrderHistory SET orderCost = @totalprice WHERE orderID = @orderid", connection);
        costcmd.Parameters.AddWithValue("@orderid", orderID);
        costcmd.Parameters.AddWithValue("@totalprice", totalPrice);
        try
        {
            costcmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Console.WriteLine("Runtime Error. Check ConsoleLog.json for more info"); LogError(e);
        }
        connection.Close();
        string jsonContents = "[]";
        File.WriteAllText(orderFilePath, jsonContents);

    }

//use dictionary

//public Dictionary<int, string>
/// <summary>
/// Checks the order history of the user that is logged in
/// </summary>
/// <param name="_select">Sort option for switch case</param>
/// <param name="_userID">Gets current ID from that is logged in</param>
/// <returns>Currently returns list of strings that describe orders, but needs to return dictionary</returns>
    public Dictionary<int, string> CheckOrderHistory(int _select, int _userID)
    {
        Dictionary<int,string> orderHistory = new Dictionary<int, string>();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand();
        switch(_select)
        
        {
            case 1: 
            cmd = new SqlCommand("SELECT StoreFront.storeName, OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID JOIN StoreFront ON ShopItem.storeID = StoreFront.storeID WHERE OrderHistory.userID = @userid ORDER BY orderDate ASC",connection);
            break;
            case 2: 
            cmd = new SqlCommand("SELECT StoreFront.storeName, OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID JOIN StoreFront ON ShopItem.storeID = StoreFront.storeID WHERE OrderHistory.userID = @userid ORDER BY orderDate DESC",connection);
            break;
            case 3: 
            cmd = new SqlCommand("SELECT StoreFront.storeName, OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID JOIN StoreFront ON ShopItem.storeID = StoreFront.storeID WHERE OrderHistory.userID = @userid ORDER BY orderCost ASC",connection);
            break;
            case 4: 
            cmd = new SqlCommand("SELECT StoreFront.storeName, OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID JOIN StoreFront ON ShopItem.storeID = StoreFront.storeID WHERE OrderHistory.userID = @userid ORDER BY orderCost DESC",connection);
            break;
        };
        cmd.Parameters.AddWithValue("@userid", _userID);
        SqlDataReader reader = cmd.ExecuteReader();
        int orderID = 0;
        
        decimal totalPrice = 0;
        decimal orderCost = 0;
        string _ordertext = "";

        while(reader.Read())
        {
            int newOrderID = reader.GetInt32(1);

            if(newOrderID != orderID)
            {
                totalPrice = 0;
                _ordertext = "";
                orderID = newOrderID;
                string orderDate = reader.GetDateTime(2).ToString();
                orderCost = reader.GetDecimal(3);
                string storeName = reader.GetString(0);
                _ordertext += $"[{newOrderID}]: Ordered at {orderDate}; Total Price: ${orderCost}; Store: {storeName}\n";
            }

            string productQuantity = reader.GetInt32(7).ToString();
            string productName = reader.GetString(9);
            decimal productPrice = reader.GetDecimal(6);
            totalPrice += productPrice;
            _ordertext += $"Name: {productName}; Amt bought: {productQuantity}; Amt charged: ${productPrice}\n";
            if(orderCost == totalPrice)
            {
                orderHistory.Add(newOrderID, _ordertext);
            }
        }
    connection.Close();
    
    return orderHistory;
    //return new Dictionary<int, string>();//orderHistory;
/*

 "1. View by date (oldest to newest)"
        +"\n2. View by date(newest to oldest)"
        +"\n3. View by price(lowest to highest)"
        +"\n4. View by price(highest to lowest)"
*/
/*
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderDate ASC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderDate DESC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderCost ASC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderCost DESC;

SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice, OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderDate ASC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice,OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderDate DESC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice,OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderCost ASC;
SELECT OrderHistory.orderID, OrderHistory.orderDate, OrderHistory.orderCost, OrderHistory.userID, OrderInstance.productID, OrderInstance.totalPrice,OrderInstance.orderQuantity, ShopItem.productID, ShopItem.productName FROM OrderHistory JOIN OrderInstance ON OrderHistory.orderID = OrderInstance.orderID JOIN ShopItem ON ShopItem.productID = OrderInstance.productID WHERE OrderHistory.userID = 16 ORDER BY orderCost DESC;



*/



    }
    
    private void LogError(Exception e)
    {
        string jsonContents = JsonSerializer.Serialize(e.Message);
        File.WriteAllText(errorFilePath, jsonContents);
        
    }

}

#endregion


//    public void CreateNewFoodItem(JAModel.ShopItem _shopItem, int storeID)
//     {
//         SqlConnection connection = new SqlConnection(_connectionString);
//         connection.Open();
        
        
//         using SqlCommand cmd = new SqlCommand("INSERT INTO ShopItem(productName, productPrice, productQuantity, productType, storeID) OUTPUT INSERTED.productID VALUES(@name, @price, @quantity, @type, @storeid)", connection);

//         cmd.Parameters.AddWithValue("@name", _shopItem.Name);
//         cmd.Parameters.AddWithValue("@price", _shopItem.Price);
//         cmd.Parameters.AddWithValue("@quantity", _shopItem.Quantity);
//         cmd.Parameters.AddWithValue("@type", _shopItem.TypeOfFood);
//         cmd.Parameters.AddWithValue("@storeid", storeID);


//         try
//         {
//             _shopItem.Id = (int) cmd.ExecuteScalar();
//         }
//         catch(Exception e)
//         {
//             Console.WriteLine(e.Message);
//         }

//         connection.Close();
        
//     }
