using JAModel;
namespace JAConsoleDL;
public static class ConsoleProjStaticStorage
{
    
    public static List<ShopItem> ShopItems {get; set;} = new List<ShopItem>()
    {
        new ShopItem
        {
            Name = "Banana",
            Price = 0.95f,
            Quantity = 10
        }
    };


    public static List<UserPass> adminCredentials {get; set; } = new List<UserPass>()
    {
        new UserPass
        {
            UserName = "nova_flash",
            PassWord = "password"
        }


    };

    public static List<ShopItem> shopItems {get; set;} = new List<ShopItem>()
    {
        new ShopItem
        {
            Name = "Banana",
            Price = 0.95f,
            Quantity = 20,
            TypeOfFood = "Fruit",
        }


    };

}

