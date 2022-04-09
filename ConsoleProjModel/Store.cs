using System.ComponentModel.DataAnnotations;
namespace JAModel;

public class Store
{
    private string storeName;
    private string storeAddress;
    private string storeCity;
    private string storeState;
    private string storeCountry;
    private int storeZIP;
    public int storeID{get; set;}
    
    public string StoreName
    {
        get => storeName;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeName = value;
        }
    }
    public  string StoreAddress
    {
        get => storeAddress;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeAddress = value;
        }
    }
    public string StoreCity
    {
        get => storeCity;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeCity = value;
        }
    }
    public string StoreState
    {
        get => storeState;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeState = value;
        }
    }
    public string StoreCountry
    {
        get => storeCountry;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeCountry = value;
        }
    }

    public int StoreZIP
    {
        get => storeZIP;
        set
        {
            if(value <= 0)
            {
                throw new ValidationException("You need to enter a proper ZIP code");
            }
            storeZIP = value;
        }
    }


}
