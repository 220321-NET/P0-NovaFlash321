using System.ComponentModel.DataAnnotations;
using System;
namespace JAModel;




    public class ShopItem
    {
        public int Id{get;set;}
        private string name = "";
        private float price;
        private int quantity;
        private string typeOfFood = "";
        private int storeID = 0;

        public int StoreID
        {
        get => storeID;
        set
        {
            
            storeID = value;
        }
    }

        public string TypeOfFood
        {   
            get => typeOfFood;
            set

            
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Type cannot be empty");
                }
                typeOfFood = value;
            }


        }


        public string Name
        {
            get => name;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                
                name = value;
            }    
        }
        
        public float Price
        {
            get => price; 
            set
            {
                if(value <= 0){
                    throw new Exception("Price cannot be empty");
                }

                price = value;
            }
        }
        public int Quantity{
            get => quantity;
            set{
                if(value <= 0){
                    throw new Exception("Quantity cannot be empty");

                }
                quantity = value;
            }
        }
    }
