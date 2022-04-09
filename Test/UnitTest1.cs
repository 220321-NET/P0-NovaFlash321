using Xunit;
using JAModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
namespace Test;
//dotnet test: USE THIS TO RUN TESTS
public class UnitTest1
{
    [Fact]
    public void ShopItemIsSet()
    {
        ShopItem _item = new ShopItem();
        _item.Name = "Test Name";
        Assert.Equal("Test Name", _item.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("        ")]
    public void ShopItemNoInvalidName(string input)
    {
        ShopItem _item = new ShopItem();

        Assert.Throws<ValidationException>(() => _item.Name = input);
    }

    [Theory]
    [InlineData(-2.56f)]
    [InlineData(0f)]
    public void ShopItemLessThanZeroPrice(float input)
    {
        ShopItem _item = new ShopItem();

        Assert.Throws<ValidationException>(() => _item.Price = input);
    }

    [Theory]
    [InlineData(-5123)]
    [InlineData(0)]
    public void ShopItemLessThanZeroQuantity(int input)
    {
        ShopItem _item = new ShopItem();

        Assert.Throws<ValidationException>(()=> _item.Quantity = input);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public void ShopItemFoodTypeNull(string input)
    {
        ShopItem _item = new ShopItem();

        Assert.Throws<ValidationException>(() => _item.TypeOfFood = input);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void UserDataNullString(string input)
    {
        UserPass _user = new UserPass();

        Assert.Throws<ValidationException>(() => _user.LastName = input);
        Assert.Throws<ValidationException>(() => _user.FirstName= input);
        Assert.Throws<ValidationException>(() => _user.PassWord= input);
        Assert.Throws<ValidationException>(() => _user.UserName = input);
    }

    [Theory]
    [InlineData(-413)]
    [InlineData(0)]
    public void UserDataNullInt(int input)
    {
        UserPass _user = new UserPass();

        Assert.Throws<ValidationException>(() => _user.StoreID = input);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("      ")]
    public void StoreDataNull(string input)
    {
        Store _store = new Store();

        Assert.Throws<ValidationException>(() => _store.StoreAddress = input);
        Assert.Throws<ValidationException>(() => _store.StoreName = input);
        Assert.Throws<ValidationException>(() => _store.StoreCity = input);
        Assert.Throws<ValidationException>(() => _store.StoreCountry = input);
        Assert.Throws<ValidationException>(() => _store.StoreState = input);
        
    }


    [Theory]
    [InlineData(-412)]
    [InlineData(0)]
    public void StoreDataNullInt(int input)
    {
        Store _store = new Store();

        Assert.Throws<ValidationException>(() => _store.StoreZIP = input);
    }

}