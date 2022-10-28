using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
	public abstract class Item
	{

        public string Name { get; set; }
        public Player Player { get; private set; }

        public Item(string name, int quantity, Player player)
        {
            Name = name;
            Player = player;
        }

        protected Item(string name, Player player)
        {
            Name = name;
            Player = player;
        }
    }
}
