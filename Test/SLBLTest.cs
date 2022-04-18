using Xunit;
using JAModel;
using Moq;
using JAConsoleBL;
using JAConsoleDL;
using System.Threading.Tasks;
namespace Test;

public class SLBLTests
{
    [Fact]
    public async Task CreateShopItem()
    {
        var mockDL = new Mock<IRepo>();

        ShopItem testItem = new ShopItem
        
        {
            Name = "Test Name",
            TypeOfFood = "Test Type"
        };

        ShopItem expectedItem = new ShopItem
        {
            Id = 8,
            Name = "Test Name",
            TypeOfFood = "Test Type",
            Quantity = 20,
            StoreID = 1,
            Price = 0.50f
        };

        ConsoleProjBL bl = new ConsoleProjBL(mockDL.Object);

        Task test = bl.CreateNewFoodItemAsync(testItem);
        
        

        Assert.Equal(expectedItem.Name, testItem.Name);
        Assert.Equal(expectedItem.TypeOfFood, testItem.TypeOfFood);

        mockDL.Verify(dl => dl.CreateNewFoodItemAsync(testItem), Times.Once());

    }


    [Fact]
    public async Task GetStoresAsyncShouldGetAllStores()
    {
        List<Store> fakeStores = new List<Store>()
        {
            new Store
            {
                StoreName = "test store",
                storeID = 1,
                StoreAddress = "test address",
                StoreCity = "test city",
                StoreCountry = "test country",
                StoreState = "test state",
                StoreZIP = 11111
            }
        };

        var mockDL = new Mock<IRepo>();

        mockDL.Setup(dl => dl.GetStoresAsync()).ReturnsAsync(fakeStores);

        ConsoleProjBL bl = new ConsoleProjBL(mockDL.Object);
        List<Store> stores = await bl.GetStoresAsync();

        Assert.NotEmpty(stores);
        Console.WriteLine(stores.Count);
        Assert.Equal(1, stores.Count);
        mockDL.Verify(dl => dl.GetStoresAsync(), Times.Exactly(1));
    }
}



public class CustomerTests
{
    [Fact]
    public void ValidCustomerFirstName()
    {
        UserPass testUser = new UserPass();
        testUser.FirstName = "Test Name";
        Assert.Equal("Test Name", testUser.FirstName);
    }
    [Fact]
    public void ValidCustomerLastName()
    {
        UserPass testUser = new UserPass();
        testUser.LastName = "Test Name";
        Assert.Equal("Test Name", testUser.LastName);
    }
    [Fact]
    public void ValidCustomerUserName()
    {
        UserPass testUser = new UserPass();
        testUser.UserName = "TestName";
        Assert.Equal("TestName", testUser.UserName);
    }
    [Fact]
    public void ValidCustomerPassword()
    {
        UserPass testUser = new UserPass();
        testUser.PassWord = "TestName";
        Assert.Equal("TestName", testUser.PassWord);
    }

}
