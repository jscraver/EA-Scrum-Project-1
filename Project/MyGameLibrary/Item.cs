using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    // item class
	public abstract class Item
	{

        public string Name { get; set; }
        public Player Player { get; private set; }
        public int MaxQuantity { get; set; }
        protected Item()
        {
            MaxQuantity = 1;
        }
        // an item requires a name and the player its aasociated with
        public Item(string name, Player player, int quantity)
        {
            Name = name;
            Player = player;
            MaxQuantity = quantity;
        }

    }
}
