using System;

namespace Fall2020_CSC403_Project.code
{
    public class Inventory
    {
        public Item InventoryItem { get; set; }
        public int Quantity { get; set; }
        public Inventory(Item item, int quantity)
        {
            InventoryItem = item;
            Quantity = quantity;
        }
        public void AddToQuantity(int amountToAdd)
        {
            Quantity += amountToAdd;
        }
        public void DeleteFromQuantity(int amountToDelete)
        {
            Quantity -= amountToDelete;
        }
    }
}