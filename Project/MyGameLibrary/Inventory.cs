using System;
using System.Drawing;

namespace Fall2020_CSC403_Project.code
{
    public class Inventory
    {
        public Item InventoryItem { get; set; }
        public int Quantity { get; set; }
        public Image Img { get; set; }
        public Inventory(Item item, int quantity)
        {
            InventoryItem = item;
            Quantity = quantity;
        }
        public void AddToQuantity(int amount)
        {
            Quantity += amount;
        }
        public void DeleteFromQuantity(int amount)
        {
            Quantity -= amount;
        }
        public void setQuantity(int amount)
        {
            Quantity = amount;
        }
    }
}