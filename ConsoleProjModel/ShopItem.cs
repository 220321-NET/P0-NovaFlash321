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
                    typeOfFood = "null";
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
                    name = "null";
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
                    price = 0;
                }

                price = value;
            }
        }
        public int Quantity{
            get => quantity;
            set{
                if(value <= 0){
                    quantity = 0;

                }
                quantity = value;
            }
        }
    }
