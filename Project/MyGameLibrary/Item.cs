using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
	public abstract class Item
	{
        public Player Player { get; private set; }

        public string Name { get; set; }

        public int MaxQuantity { get; set; }

        protected Item()
        {
            MaxQuantity = 1;
        }

        public Item(Player player)
        {
            this.Player = player;
        }
    }
}
