using System;
using System.Collections.Generic;
using System.Linq;

namespace Fall2020_CSC403_Project.code
{
    // inventory class
    public class InventorySystem
    {
        private const int MAX_SLOTS = 5;
        public readonly List<Inventory> InventoryItems = new List<Inventory>();
        public void AddItem(Item item, int quantityToAdd)
        {
            while (quantityToAdd > 0)
            {
                // user has this item and can stack it
                if (InventoryItems.Exists(x => (x.InventoryItem.Name == item.Name) && (x.Quantity < item.MaxQuantity)))
                {
                    // item we are adding to
                    Inventory currentInventoryItem = InventoryItems.First(x => (x.InventoryItem.Name == item.Name) && (x.Quantity < item.MaxQuantity));
                    // check for how much more we can add
                    int maxQuantityCanAdd = (item.MaxQuantity - currentInventoryItem.Quantity);
                    // add either the amount of the item that is on the ground or the max amount you can add to stack
                    int quantityToAddToStack = Math.Min(quantityToAdd, maxQuantityCanAdd);
                    currentInventoryItem.AddToQuantity(quantityToAddToStack);

                    // decrease the quantity we need to add by the amount we added
                    quantityToAdd -= quantityToAddToStack;
                }
                // user does not have this item, so we add the item
                else if (InventoryItems.Count < MAX_SLOTS)
                {
                    // we do not set quantity here since the code above will take care of it when it goes to top of while
                    InventoryItems.Add(new Inventory(item, 0));
                }
                // user has no inventory space
                else
                {
                    // show user they are out of inventory space
                    Console.WriteLine("No more space for items.");
                }
            }
        }
    }
}