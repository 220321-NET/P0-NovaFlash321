using System.ComponentModel.DataAnnotations;
namespace JAModel;

public class UserPass
{
    private string userName = "";
    private string passWord = "";
    private bool isAdmin = false;
    private int storeID = 0;

    private string lastName ="";
    private string firstName = "";

    public string LastName
        {
        get => lastName;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            lastName = value;
        }
    }
    public string FirstName
    {
        get => firstName;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            firstName = value;
        }
    }

    public int UserID{get; set;}



    public int StoreID
    {
        get => storeID;
        set
        {
            if(value <= 0)
            {
                throw new ValidationException("Content cannot be empty");
            }
            storeID = value;
        }
    }

    public string UserName
    {
        get => userName;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            userName = value;
        }
        
    }

    public string PassWord
    {
        get => passWord;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Content cannot be empty");
            }
            passWord = value; 
        }
    }

    public bool IsAdmin
    {
        get => isAdmin;
        set
        {
            if(storeID == 0) isAdmin = false;
            else isAdmin = value;
        }
    }

}