using JAModel;
using JAConsoleBL;
using System.ComponentModel.DataAnnotations;

namespace JAConsole;
    public class MainConsole
    {
        private ConsoleProjectUI.HttpService httpService;
        public MainConsole(ConsoleProjectUI.HttpService _httpService)
        {
            httpService = _httpService;
        }
        // private readonly IJABL _bl;
        //private readonly JAConsoleDL.IRepo _repo;
        private bool hasAdminPrivilages = false;
        private bool hasUserPrivilages = false;
        
        private UserPass currentUser = new UserPass();


    // public MainConsole(IJABL bl)
    // {
    //     _bl = bl;
    // }
    private void InitConsole()
    {   

    }

        public async Task PrivilageCheck()  
        {
            InitConsole();

            
            bool isValid = false;

            do{
                Console.WriteLine("Welcome to Full Foods Distribution!" 
                + "\n\nAre you an admin, or user?");

                CheckPrivilage:
                Console.WriteLine("1. Admin\n2. User\n3. Sign Up \nX. Exit\n");
            
                string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput)){
                    Console.WriteLine("Invalid answer. Please try again");
                    goto CheckPrivilage;
                }
                else
                {
                    uInput.ToUpper(); //Sets all characters to uppercase
                    char initInput = uInput[0];
                    switch(initInput)
                    {
                        case '1': 
                            await CheckAdminAsync(); 
                            if(hasAdminPrivilages)
                            {
                                await AdminMainMenuAsync();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("You do not have admin privilages!");
                                break;
                            }
                        case '2': 
                            await CheckUserAsync();
                            if(hasAdminPrivilages || hasUserPrivilages)
                            {
                                await UserMainMenu();
                            }
                            else
                            {
                                Console.WriteLine("You do not have any privilages!");
                            }
                            break;
                        case '3':
                            await CreateNewUserAsync();
                            break;
                        case 'x':
                            Console.WriteLine("Goodbye!"); 
                            return;
                        case 'X':
                            Console.WriteLine("Goodbye!"); 
                            return;

                    default:
                            Console.WriteLine("Invalid answer. Please try again"); 
                            goto CheckPrivilage;
                    }
                }
            }while(!isValid);
        }
        private async Task CreateNewUserAsync()
        {
            CreateName:
            Console.WriteLine("Enter your username:");
            string? uUser = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uUser))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto CreateName;
                }
                
            CreatePass:
            Console.WriteLine("Enter your password");
            string? uPass = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uPass))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto CreatePass;
                }
                
            EnterFN:
            Console.WriteLine("Enter your first name");
            string? uFirst = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uFirst))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto EnterFN;
                }
                
            EnterLN:
            Console.WriteLine("Enter your last name");
            string? uLast = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uLast))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto EnterLN;
                }
            EnterStore:
            List<Store> allStores = await httpService.GetStoresAsync();
            Console.WriteLine("Enter the franchise you are shopping at (this can be changed later):");
            foreach(Store _storeName in allStores)
            {
                Console.WriteLine("[" + _storeName.storeID + "]: " +_storeName.StoreName);
            }
            string? uStore = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uStore))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto EnterStore;
                }
                
        
        UserPass tempUser = new UserPass()
        {
            UserName = uUser,
            PassWord = uPass,
            FirstName = uFirst,
            LastName = uLast,
            StoreID = int.Parse(uStore),

        };
        List<UserPass> allUsers = await httpService.GetUsersAsync();
        foreach(UserPass _user in allUsers)
        {
            if(_user.UserName == tempUser.UserName)
            {
                Console.WriteLine("User already exists! Cannot create new user!");
                return;
            }
        }

        if(tempUser.FirstName == "rick" || tempUser.FirstName == "Rick" && tempUser.LastName == "astley" || tempUser.LastName == "Astley"){
            System.Diagnostics.Process.Start("C:/Program Files (x86)/Google/Chrome/Application/chrome.exe", @"https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
        else
        {
        }
        await httpService.CreateNewUserAsync(tempUser);
        

        


}
        private async Task CheckAdminAsync()
        {
            bool userIsValid = false;
            bool passIsValid =  false;
            string userInput = "";
            string passInput = "";
            while(!userIsValid)
            {
                Console.WriteLine("Enter Admin Username: ");
                string? aUser = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(aUser))
                {
                    Console.WriteLine("You must enter a username.\n");
                }
                else {
                    userIsValid = true;
                    userInput = aUser;
                }
            }
            
            while(!passIsValid)
                {
                Console.WriteLine("Enter Admin Password: ");
                string? aPass = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(aPass)){
                    Console.WriteLine("You must enter a password.\n");
                }
                else
                {
                    passIsValid = true;
                    passInput = aPass;
                } 
                    
            }
            
            List<UserPass> allAdmins = await httpService.GetAdminsAsync();
            

            for(int i = 0; i < allAdmins.Count; i++)
            {
                if(userInput == allAdmins[i].UserName && passInput == allAdmins[i].PassWord)
                {
                    Console.WriteLine("Valid login! Welcome back " + allAdmins[i].FirstName +" " + allAdmins[i].LastName);
                    currentUser = allAdmins[i];
                    hasAdminPrivilages =  true;
                    currentUser.IsAdmin = hasAdminPrivilages;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid login!");
                    hasAdminPrivilages = false;
                }
            }



            return;
        }
        private async Task CheckUserAsync()
        {
            string userInput = "";
            string passInput = "";
            bool userIsValid = false;
            bool passIsValid =  false;
            while(!userIsValid)
            {
                Console.WriteLine("Enter Customer Username: ");
                string? aUser = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(aUser))
                {
                    Console.WriteLine("You must enter a username.\n");
                }
                else
                {
                    userIsValid = true;
                    userInput = aUser;
                }
            }
            
            while(!passIsValid)
                {
                Console.WriteLine("Enter Customer Password: ");
                string? aPass = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(aPass)){
                    Console.WriteLine("You must enter a password.\n");
                }
                else
                {
                    passIsValid = true;
                    passInput = aPass;
                }
            }

            List<UserPass> allUsers = await httpService.GetUsersAsync();
            
            for(int i = 0; i < allUsers.Count; i++)
            {
                if(userInput == allUsers[i].UserName && passInput == allUsers[i].PassWord)
                {
                    Console.WriteLine("Valid login! Welcome back " + allUsers[i].FirstName +" " + allUsers[i].LastName);
                    currentUser = allUsers[i];
                    hasUserPrivilages =  true;
                    hasAdminPrivilages = false;
                    break;
                }
                else
                {
                    hasAdminPrivilages = false;
                    hasUserPrivilages = false;
                    
                }
            }
            
            
            return;
        }        
    private async Task AdminMainMenuAsync()
    {
        bool loggedIn = true;
        do{

        Console.WriteLine("What can I help you with today?");
        
        AdminMenu:
        Console.WriteLine(
            "\n1. Import Item"
            +"\n2. Remove Item"
            +"\n3. Change Price on Item"
            +"\n4. Add a new administrator"
            +"\n5. Add a new store"
            +"\n6. Change store"
            +"\nX. Exit"
            );

        string? uInput = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(uInput)){
                Console.WriteLine("Invalid answer. Please try again");
                goto AdminMenu;
            }
        char initInput = uInput[0];
        
        switch(initInput)
        {
            case '1': await AddItemAsync(); break;
            case '2': await RemoveItemAsync(); break;
            case '3': await ChangePriceAsync(); break;
            case '4': await CreateNewAdminAsync(); break;
            case '5': await CreateNewStoreAsync(); break;
            case '6': await ChangeStore(); break;
            case 'x':
                Console.WriteLine("Returning to Login"); 
                loggedIn = false;
                break;
            case 'X':
                Console.WriteLine("Returning to Login"); 
                loggedIn = false;
                break;            
            default:
                Console.WriteLine("Invalid Response.");
                goto AdminMenu; 
                

        }
        
        }while(loggedIn);
    }
    private async Task CreateNewStoreAsync()
    {
        StoreName:
        Console.WriteLine("Enter the name of the store:");
        string? sName = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sName))
        {
            Console.WriteLine("You must enter a store name.\n");
            goto StoreName;
        }

        StoreAddress:
        Console.WriteLine("Enter the address:");
        string? sAddress = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sAddress))
        {
            Console.WriteLine("You must enter an address.\n");
            goto StoreAddress;
        }

        StoreCity:
        Console.WriteLine("Enter the name of the city:");
        string? sCity = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sCity))
        {
            Console.WriteLine("You must enter a city!.\n");
            goto StoreCity;
        }

        StoreState:
        Console.WriteLine("Enter the state(county if not from the U.S.):");
        string? sState = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sState))
        {
            Console.WriteLine("You must enter a state or country!.\n");
            goto StoreState;
        }

        StoreCountry:
        Console.WriteLine("Enter the country:");
        string? sCountry = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sCountry))
        {
            Console.WriteLine("You must enter a country!\n");
            goto StoreCountry;
        }

        StoreZip:
        Console.WriteLine("Enter the ZIP Code:");
        string? sZip = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(sZip))
        {
            Console.WriteLine("You must enter a ZIP Code.\n");
            goto StoreZip;
        }

        Store newStore = new Store()
        {
            StoreName = sName,
            StoreAddress = sAddress,
            StoreCity = sCity,
            StoreState = sState,
            StoreCountry = sCountry,
            StoreZIP = int.Parse(sZip),
        };




        Console.WriteLine($"{sName} \nAddress: {sAddress}\n{sCity}, {sState}, {sCountry}, {sZip}");
        CStore:
        Console.WriteLine("Confirm what you have input [Y/N]");

        string? aStore = Console.ReadLine();
        char input;
        if(string.IsNullOrWhiteSpace(aStore))
            {
                Console.WriteLine("Invalid Response!");
                goto CStore;
            }
        else
                {
                    aStore = aStore.ToUpper();
                    input = aStore[0];
                }

                switch(input)
                {
                    case 'Y': 
                        await httpService.CreateNewStoreAsync(newStore);
                        
                        break;
                    case 'N':
                        Console.WriteLine("Returning to Admin Menu!");
                        return;
                }


    }
    private async Task CreateNewAdminAsync()
    {
        ACreateName:
        Console.WriteLine("Enter your username:");
            string? aUser = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(aUser))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto ACreateName;
                }
        ACreatePass:
        Console.WriteLine("Enter your password");
        string? aPass = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(aPass))
            {
                Console.WriteLine("You must enter a username.\n");
                goto ACreatePass;
            }
        AFirst:
        Console.WriteLine("Enter your first name");
        string? aFirst = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(aFirst))
            {
                Console.WriteLine("You must enter a username.\n");
                goto AFirst;
            }
        ALast:
        Console.WriteLine("Enter your last name");
        string? aLast = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(aLast))
            {
                Console.WriteLine("You must enter a username.\n");
                goto ALast;
            }

    AStore:
        List<Store> allStores = await httpService.GetStoresAsync();
            Console.WriteLine("Enter the franchise you are shopping at (this can be changed later):");
            foreach(Store _storeName in allStores)
            {
                Console.WriteLine("[" + _storeName.storeID + "]: " +_storeName.StoreName);
                
            }
            string? aStore = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(aStore))
                {
                    Console.WriteLine("You must enter a username.\n");
                    goto AStore;
                }

        UserPass newAdmin = new UserPass()
        {
            UserName = aUser,
            PassWord = aPass,
            FirstName = aFirst,
            LastName = aLast,
            StoreID = int.Parse(aStore),
        };
        List<UserPass> allAdmins = await httpService.GetAdminsAsync(); //_bl.GetAllAdmins();
        foreach(UserPass _admin in allAdmins)
        {
            if(_admin.UserName == newAdmin.UserName)
            {
                Console.WriteLine("User already exists! Cannot create new user!");
                return;
            }
        }
        await httpService.CreateNewAdminAsync(newAdmin);
        //_bl.CreateNewAdmin(newAdmin);

    }
    private async Task UpdateItemAsync(ShopItem _item)
    {
        int quantity = 0;
        char input = '1';
        UIQuantity:
        Console.WriteLine("Enter the quantity:");
        string? foodQuantity = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodQuantity) || foodQuantity == "0")
        {
            Console.WriteLine("You must enter a quantity!");
            goto UIQuantity;
        }
        else
        {
            quantity = int.Parse(foodQuantity);
        }
        UIConfirm:
        Console.WriteLine("Confirm you want to add " + quantity + " to " + _item.Name + ". [Y/N]");
        string? uInput = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto UIConfirm;
                }
                else
                {
                    uInput = uInput.ToUpper();
                    input = uInput[0];
                }

                switch(input)
                {
                    case 'Y': 
                        await httpService.UpdateItemQuantityAsync(_item, quantity);
                        //_bl.UpdateFoodItem(_item, quantity);
                        break;
                    case 'N':
                        Console.WriteLine("Returning to Admin Menu!");
                        return;
                }


        
    }
    private async Task AddNewItemAsync(ShopItem _item, string _name)
    {
        
        char input;
        ANIItemPrice:
        Console.WriteLine("Enter the price:");
        string? foodPrice = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodPrice))
        {
            Console.WriteLine("You must enter a price!");
            goto ANIItemPrice;
        }

        ANIQuantity:
        Console.WriteLine("Enter the quantity:");
        string? foodQuantity = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodQuantity) || foodQuantity == "0")
        {
            Console.WriteLine("You must enter a quantity!");
            goto ANIQuantity;
        }
        else
        {
            int quantity = int.Parse(foodQuantity);
        }

        ANIType:
        Console.WriteLine("Enter the food type:");
        string? foodType = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodPrice))
        {
            Console.WriteLine("You must enter the type of food!");
            goto ANIType;
        }
        _item.Name = _name;
        _item.Price = float.Parse(foodPrice);
        _item.Quantity = int.Parse(foodQuantity);
        _item.TypeOfFood = foodType;

        Console.WriteLine($"You have added {_name}(s), with a price of  $" + _item.Price.ToString("0.00") + $", with a quantity of {_item.Quantity}, as a(n) {_item.TypeOfFood}.");
        SaveItem:
        Console.WriteLine("Do you want to save this item to the database? [Y/N]");
        string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto SaveItem;
                }
                else
                {
                    uInput = uInput.ToUpper();
                    input = uInput[0];
                }

                switch(input)
                {
                    case 'Y':
                        await httpService.CreateNewFoodItemAsync(_item, currentUser.StoreID);
                        break;
                    case 'N':
                        Console.WriteLine("Returning to Admin Menu!");
                        return;
                }
    }
    private async Task AddItemAsync()
    {
        char input;
        ItemName:
        Console.WriteLine("Search Store #" + currentUser.StoreID + "'s inventory:");
        string? foodItem = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodItem))
        {
            Console.WriteLine("You must enter a name!");
            goto ItemName;
        }
        else
        {
            //Capitalizes first letter
            char upper = foodItem[0];
            upper = char.ToUpper(upper);
            foodItem = foodItem.Replace(foodItem[0], upper);
            Console.WriteLine(foodItem);

            ShopItem searchedItem = await httpService.SearchInventoryAsync(foodItem, currentUser.StoreID); 
            //_bl.SearchInventory(foodItem);
            
            if(searchedItem.Name == null || searchedItem.Name == "") 
            {
                Console.WriteLine("Item does not exist in Store #" + currentUser.StoreID + "!");
                AddToInventory:
                Console.WriteLine("Do you want to add it to the inventory? [Y/N]");
                string? aInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(aInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto AddToInventory;
                }
                else
                {
                    aInput = aInput.ToUpper();
                    input = aInput[0];
                }


                switch(input)
                {
                    case 'Y': 
                        await AddNewItemAsync(searchedItem, foodItem);
                        break;
                    case 'N':
                        Console.WriteLine("Returning to Admin Menu!");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Found an exisiting item [" + searchedItem.Name + "].");
                UpdateInventory:
                Console.WriteLine("Do you wish to add to Store #" + currentUser.StoreID + "'s inventory? [Y/N]"); 
                string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto UpdateInventory;
                }
                else
                {
                    uInput = uInput.ToUpper();
                    input = uInput[0];
                }

                switch(input)
                {
                    case 'Y': 
                        await UpdateItemAsync(searchedItem);
                        break;
                    case 'N':
                        Console.WriteLine("Returning to Admin Menu!");
                        return;
                }
            }


                
            
            
            
                // #region 
                // UpdateCurrentInventory:
                


                // #endregion


                
                // ItemQuantity:
                // Console.WriteLine("Enter the quantity:");
                // //string? foodQuantity = Console.ReadLine();
                // if(string.IsNullOrWhiteSpace(foodPrice))
                // {
                //     Console.WriteLine("You must enter a price!");
                //     goto ItemQuantity;
                // }
                // ItemType:
                
                // ShopItem itemToAdd = new ShopItem();
                // itemToAdd.Name = foodItem;
                // itemToAdd.Price = float.Parse(foodPrice);
                // itemToAdd.Quantity = int.Parse(foodQuantity);
                // itemToAdd.TypeOfFood = foodType;

                // Console.WriteLine($"You have added {itemToAdd.Name}(s), with a price of  $" + itemToAdd.Price.ToString("0.00") + $", with a quantity of {itemToAdd.Quantity}, as a(n) {itemToAdd.TypeOfFood}.");
                
                // SaveItem:
                // Console.WriteLine("Do you want to save this item to the database? [Y/N]");
                // string? uInput = Console.ReadLine().ToUpper();
                // if(string.IsNullOrWhiteSpace(uInput))
                // {
                //     Console.WriteLine("Invalid Response!");
                //     goto SaveItem;
                // }
                // char switchInput = uInput[0];
                // switch(switchInput)
                // {
                //     case 'Y': 
                //         _bl.CreateNewFoodItem(itemToAdd, currentUser.StoreID);
                //         //Console.WriteLine("Item Added! Returning to Admin Menu");
                //         break;
                //     case 'N':
                //         Console.WriteLine("Returning to Admin Menu!");
                //         return;
                // }
            
            
                // Console.WriteLine("Returning to Admin Menu!");
                // return;
            
        }
        #region OldItemName
        //Console.WriteLine("Type the name of the item:");
        #endregion
    }
    private async Task RemoveItemAsync()
    {
        char input;
        RIItemName:
        Console.WriteLine("Search Store #" + currentUser.StoreID + "'s inventory:");
        string? foodItem = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodItem))
        {
            Console.WriteLine("You must enter a name!");
            goto RIItemName;
        }
        else
        {
            //Capitalizes first letter
            char upper = foodItem[0];
            upper = char.ToUpper(upper);
            foodItem = foodItem.Replace(foodItem[0], upper);

            ShopItem searchedItem = await httpService.SearchInventoryAsync(foodItem, currentUser.StoreID);
            if(searchedItem == null || searchedItem.Name == "")
            {
                Console.WriteLine("Item does not exist in the current inventory!");
                RIValidation:
                Console.WriteLine("Do you wish to search again? [Y/N]");
                string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Input!");
                    goto RIValidation;
                }
                else
                {
                    uInput = uInput.ToUpper();
                    input = uInput[0];
                }
                switch(input)
                {
                    case 'Y': 
                        goto RIItemName;
                    case 'N': 
                        return;
                    default: 
                        Console.WriteLine("Invalid Input!"); 
                        goto RIValidation;
                }
            }
            else
            {   
                DelValidation:
                Console.WriteLine($"Item [{foodItem}] found in inventory. Do you wish to delete? [Y/N]");
                string? rInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(rInput))
                {
                    Console.WriteLine("Invalid Input!");
                    goto DelValidation;
                }
                else
                {
                    rInput = rInput.ToUpper();
                    input = rInput[0];
                }
                switch(input)
                {
                    case 'Y': 
                        await httpService.RemoveItem(searchedItem, currentUser.StoreID);
                        //_bl.RemoveItem(searchedItem, currentUser.StoreID);
                        break;
                    case 'N': 
                        return;
                    default: 
                        Console.WriteLine("Invalid Input!"); 
                        goto DelValidation;
                }

            }
        }
    }
    private async Task ChangePriceAsync()
    {
        char input;
        CPItemName:
        Console.WriteLine("Search Store #" + currentUser.StoreID + "'s inventory:");
        string? foodItem = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodItem))
        {
            Console.WriteLine("You must enter a name!");
            goto CPItemName;
        }
        else
        {
            //Capitalizes first letter
            char upper = foodItem[0];
            upper = char.ToUpper(upper);
            foodItem = foodItem.Replace(foodItem[0], upper);
            Console.WriteLine(foodItem);

            ShopItem searchedItem = await httpService.SearchInventoryAsync(foodItem, currentUser.StoreID);


            if(searchedItem == null || searchedItem.Name == "")
            {
                Console.WriteLine("Item does not exist in the current inventory!");
                CPValidation:
                Console.WriteLine("Do you wish to search again? [Y/N]");
                string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Input!");
                    goto CPValidation;
                }
                else
                {
                    uInput = uInput.ToUpper();
                    input = uInput[0];
                }
                switch(input)
                {
                    case 'Y': 
                        goto CPValidation;
                    case 'N': 
                        return;
                    default: 
                        Console.WriteLine("Invalid Input!"); 
                        goto CPValidation;
                }
                
            }
            else
            {
                CPValidation:
                Console.WriteLine($"Item [{foodItem}] found in inventory. The current price is: $" + searchedItem.Price +". Set the updated price below");
                string? rInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(rInput))
                {
                    Console.WriteLine("Invalid Input!");
                    goto CPValidation;
                }
                else
                {
                    
                    float _newPrice = float.Parse(rInput);
                    await httpService.ChangePriceAsync(searchedItem.Name, _newPrice, currentUser.StoreID);
                    //_bl.ChangePrice(searchedItem, _newPrice, currentUser.StoreID);

                }
                
            }
        }
    }

private async Task UserMainMenu()
{
    bool loggedIn = true;
    do
    {

AdminMenu:
        Console.WriteLine(
             "\n1. Place New Order"
            +"\n2. Remove Item From Order"
            +"\n3. Confirm Order"
            +"\n4. View Order History"
            +"\n5. Change store"
            +"\nX. Exit"
            );

        string? uInput = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(uInput)){
                Console.WriteLine("Invalid answer. Please try again");
                goto AdminMenu;
            }
        char initInput = uInput[0];
        
        switch(initInput)
        {
            case '1': await PlaceNewOrderAsync(); break;
            case '2': await RemoveOrderItem(); break;
            case '3': await ConfirmOrderAsync(); break;
            case '4': await CheckOrderHistoryAsync(); break;
            case '5': await ChangeStore(); break;
            case 'x':
                Console.WriteLine("Returning to Login"); 
                return;
            case 'X':
                Console.WriteLine("Returning to Login"); 
                return;
            
            default:
                Console.WriteLine("Invalid Response.");
                goto AdminMenu; 
                

        }
    }while(loggedIn);
    //await httpService.RemoveOrderCartAsync();
    //_bl.RemoveOrder(); //Removes order history when logged out
}



private async Task<Dictionary<int, List<ShopItem>>> SearchForOrderAsync(int userID)
{
    Dictionary<int, List<ShopItem>> orderContents = await httpService.SearchForOrderAsync(currentUser.UserID);
    

    return orderContents;
}
    private async Task PlaceNewOrderAsync()
    {
        Dictionary<int, List<ShopItem>> orderCart = await SearchForOrderAsync(currentUser.UserID);
        Console.WriteLine(orderCart);
        List<ShopItem> orderContents = new List<ShopItem>();
        
        int key = 0;
        if(orderCart.Count > 0)
        {
            foreach(KeyValuePair<int, List<ShopItem>> _order in orderCart)
            {
                key = _order.Key;
            }
            orderContents = orderCart[key];
        }
        //List<ShopItem> orderContents = await SearchForOrderAsync();
        
        if(key == 0 || orderContents == null)
        {
            Console.WriteLine("No order was found! Checking if key exists");
            int cartID = await httpService.GetCartId(currentUser.UserID);
            if(cartID == 0)
            {
                Console.WriteLine("No tag found! Creating a new order tag");
                await httpService.CreateOrderAsync(currentUser.UserID);
            }
            else
            {
                Console.WriteLine("Tag found. Using existing tag");
            }

            orderContents = new List<ShopItem>();
            await AddOrderItemAsync(orderContents, cartID);
            
        }
        else
        {   
            PNOYesNo:
            Console.WriteLine("Order was found. Do you wish to add to your existing order? [Y/N]");
            string? uInput = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uInput))
            {
                Console.WriteLine("Invalid Response!");
                goto PNOYesNo;
            }
            uInput = uInput.ToUpper();
            char input = uInput[0];

            switch(input)
            {
                case 'Y': 
                    await AddOrderItemAsync(orderContents, key);
                    break;
                case 'N': 
                    Console.WriteLine("Creating new order"); 
                    foreach(ShopItem _item in orderContents)
                    {
                        await httpService.RemoveOrderItemAsync(_item.Id, currentUser.UserID);
                    }
                    await httpService.CreateOrderAsync(currentUser.UserID);
                    int cartID = await httpService.GetCartId(currentUser.UserID);
                    orderContents = new List<ShopItem>();
                    await AddOrderItemAsync(orderContents, cartID);
                    break;

                default: Console.WriteLine("Invalid Response!"); goto PNOYesNo;
            };
        }
    }   
private async Task AddOrderItemAsync(List<ShopItem> _order, int cartID)
{
    ShopItem _tempItem = new ShopItem();
        string _storeName = await httpService.GetStoreNameAsync(currentUser.UserID);
        
        bool isOrdering = true;
        Console.WriteLine($"You are currently shopping at: {_storeName}");
        do
        {
        PNOValidation:
        Console.WriteLine("Search for your item below:");
        string? foodItem = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(foodItem))
        {
            Console.WriteLine("Invalid Response!");
            goto PNOValidation;
        }

        char upper = foodItem[0];
        upper = char.ToUpper(upper);
        foodItem = foodItem.Replace(foodItem[0], upper);

        ShopItem searchedItem = await httpService.SearchInventoryAsync(foodItem, currentUser.StoreID);
        if(searchedItem.Id == 0)
        {
            Console.WriteLine("Item does not exist!");
        }
        else
        {
            PNOQAdd:
            Console.WriteLine($"Item [{searchedItem.Name}] found at $" + (searchedItem.Price).ToString("###.00") +" each. Input how much to add to your order");
            string? quantityInput = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(quantityInput))
            {
                Console.WriteLine("Invalid Response!");
                goto PNOQAdd;
            }
            else if(int.Parse(quantityInput) >= searchedItem.Quantity)
            {
                Console.WriteLine("Cannot add more than current inventory!");
                goto PNOQAdd;
            }
            else
            {
                int quantity = int.Parse(quantityInput);
                Console.WriteLine($"Adding [{quantityInput}] to current order. Price is $" + (quantity * searchedItem.Price).ToString("###.00"));
                _tempItem = new ShopItem()
                {
                    Name = searchedItem.Name,
                    Quantity = quantity,
                    Price = searchedItem.Price,
                    Id = searchedItem.Id,
                    StoreID = searchedItem.StoreID,
                    TypeOfFood = searchedItem.TypeOfFood,
                };
                _order.Add(_tempItem);
                OrderInstance _instance = new OrderInstance
                {
                    ProductQuantity = _tempItem.Quantity,
                    UserID = currentUser.UserID,
                    ItemId = _tempItem.Id,
                    CartID = cartID
                };
                await httpService.SaveOrderAsync(_instance);

            }
            PNOFinal:
                Console.WriteLine("Do you wish to add another item? [Y/N]");
                string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto PNOFinal;
                }
                uInput = uInput.ToUpper();
                char input = uInput[0];
                switch(input)
                {
                    case 'Y': break;
                    case 'N': 
                        isOrdering = false; 
                        break;
                }
        }
        //_bl.SaveOrder(_order);
        }while(isOrdering);
}

private async Task RemoveOrderItem()
{
    Dictionary<int, List<ShopItem>> _orderCart = await SearchForOrderAsync(currentUser.UserID);
    List<ShopItem> _order = new List<ShopItem>();
    if(_orderCart.ContainsKey(0))
    {
        Console.WriteLine("Your cart is currently empty!");
        return;
    }
    else
    {
    int key = await httpService.GetCartId(currentUser.UserID);
    _order = _orderCart[key];
    }
    

    if(_order.Count > 0)
    {
        Console.WriteLine("Your current order:");
        int index = 1;
        foreach(ShopItem _shopItem in _order)
        {
            
            
            Console.WriteLine($"Item #{index}: {_shopItem.Quantity} {_shopItem.Name}, $" + (_shopItem.Price * _shopItem.Quantity).ToString("###.00"));

            
            index++;
        }
        ROIValidation:
        Console.WriteLine("Choose the item you want to remove by its index:");
        string? uInput = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(uInput))
            {
                Console.WriteLine("Invalid Response!");
                goto ROIValidation;
            }
        char input = uInput[0];
        int indexInput = input - '0';
        if((indexInput - 1) < _order.Count)
        {
            await httpService.RemoveOrderItemAsync(_order[indexInput - 1].Id, currentUser.UserID);
            Console.WriteLine($"{_order[indexInput - 1].Name} has been removed from your order!");
            _order.RemoveAt(indexInput - 1);
        }
        else
        {
            Console.WriteLine("Out of range!");
            goto ROIValidation;
        }
    }
    else
    {
        Console.WriteLine("No order found!");
    }
}
private async Task ConfirmOrderAsync()
{
    Dictionary<int, List<ShopItem>> _orderContents = await SearchForOrderAsync(currentUser.UserID);
    int key = await httpService.GetCartId(currentUser.UserID);
    List<ShopItem> _order = _orderContents[key];
    float totalPrice = 0;
    if(_order.Count > 0)
    {

    Console.WriteLine("Your current order");
    int index = 1;
    foreach(ShopItem _item in _order)
    {
        Console.WriteLine($"Item #{index}: {_item.Quantity} {_item.Name}, $" + (_item.Price * _item.Quantity).ToString("###.00"));
        index++;
        totalPrice += (_item.Price * _item.Quantity);
    }

    Console.WriteLine( "Your total price is $" +totalPrice.ToString("######.00") );
    COValidation:
    Console.WriteLine("Do you wish to place this order? [Y/N]");
    string? uInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(uInput))
                {
                    Console.WriteLine("Invalid Response!");
                    goto COValidation;
                }
                uInput = uInput.ToUpper();
                char input = uInput[0];
                
                switch(input)
                {
                    case 'Y':
                    await httpService.ConfirmOrderAsync(_order, currentUser.StoreID, currentUser.UserID);
                    //_bl.ConfirmOrder(_order, currentUser.StoreID, currentUser.UserID);
                    break;
                    case 'N':
                        break;
                }
    }
    else
    {
        Console.WriteLine("No order found!");
    }
}

private async Task CheckOrderHistoryAsync()
{
    COHValidation:
    Console.WriteLine("How do you wish to view your order history?");
    Console.WriteLine(
        "1. View by date (oldest to newest)"
        +"\n2. View by date(newest to oldest)"
        +"\n3. View by price(lowest to highest)"
        +"\n4. View by price(highest to lowest)");
    string? uInput = Console.ReadLine();
    if(string.IsNullOrWhiteSpace(uInput))
    {
        Console.WriteLine("Invalid Response!");
        goto COHValidation;
    }
    char input = uInput[0];
    int select = input - '0';
    
    if(select >= 1 && select <= 4)
    {
        Dictionary<int, string> orderHistory = new Dictionary<int, string>(); 
        await httpService.CheckOrderHistoryAsync(select, currentUser.UserID);
        //_bl.CheckOrderHistory(select, currentUser.UserID);
        foreach(KeyValuePair<int,string> _order in orderHistory)
        {
            Console.WriteLine(_order.Value);
        }
    }
    else
    {
        Console.WriteLine("Invalid Response!");
        goto COHValidation;
    }
    
        
    
}
private async Task ChangeStore()
{
    List<Store> allStores = await httpService.GetStoresAsync();
    Console.WriteLine("Select the store you want to redirect to by its number:");
    foreach(Store _store in allStores)
    {
        Console.WriteLine($"[{_store.storeID}] - [{_store.StoreName}]: {_store.StoreAddress}, {_store.StoreCity}, {_store.StoreState}, {_store.StoreZIP}");
    }
    CSValidation:
    string? uInput = Console.ReadLine();
    if(string.IsNullOrWhiteSpace(uInput))
    {
        Console.WriteLine("Invalid Response!");
        goto CSValidation;
    }
    else if(!uInput.Any(char.IsDigit))
    {
        Console.WriteLine("Invalid Response! Needs a numeric value");
        goto CSValidation;
    }
    else
    {
        int storeID = int.Parse(uInput);
        await httpService.ChangeStoreAsync(storeID, currentUser);
        //_bl.ChangeStore(storeID, currentUser);
    }

}


} //End of Line





/*

do{
                MainMenu:
            Console.WriteLine(
            "1. Search for parts"
            +"\n2. Pull up a previous order"
            +"\n3. Exit"
            );
            
            string menuInput = Console.ReadLine();
            
            if(string.IsNullOrWhiteSpace(menuInput))
            {
                Console.WriteLine("You cannot have an empty answer. Please try again.");

            }
            else
            {
                char initMenuInput = menuInput[0];
                switch(initMenuInput)
                {
                    case '1': partsConsole.RootPartSearch();
                        break;
                    case '2':orderHistoryConsole.RootOrderHistory();
                        break;
                    case '3':
                        Console.WriteLine("Goodbye!");
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
            }

            }while(!isValid);

            */
