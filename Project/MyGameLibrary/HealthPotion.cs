using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class HealthPotion : Item
    {
        public HealthPotion(string name, Player player) : base(name, player){

        }

        public void useHealthPotion(Player player)
        {
            player.AlterHealth(5);
        }

        public void useMaxHealthPotion(Player player)
        {
            player.AlterToMaxHealth();
        }
    }
}