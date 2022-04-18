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
        //ShopItem endItem = bl.CreateNewFoodItemAsync(testItem);

        Assert.Equal(expectedItem.Name, testItem.Name);
        Assert.Equal(expectedItem.TypeOfFood, testItem.TypeOfFood);

    }
}
