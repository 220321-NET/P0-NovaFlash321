using System.ComponentModel.DataAnnotations;
using System;
namespace JAModel;

public class OrderInstance
{
    private int itemID;
    private int cartID;
    private int productQuantity;
    private int userID;

    public int ItemId
    {
        get => itemID;
        set
        {
            itemID = value;
        }
    }

    public int CartID
    {
        get => cartID;
        set
        {
            cartID = value;
        }
    }
    public int ProductQuantity
    {
        get => productQuantity;
        set
        {
            productQuantity = value;
        }
    }
    public int UserID
    {
        get => userID;
        set
        {
            userID = value;
        }
    }
}