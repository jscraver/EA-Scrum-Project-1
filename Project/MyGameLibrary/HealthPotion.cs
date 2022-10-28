using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    // health potion class
    public class HealthPotion : Item
    {
        // requires all attributes of Item
        public HealthPotion(string name, Player player, int quantity) : base(name, player, quantity){

        }
        // base healing
        public void useHealthPotion()
        {
            Player.AlterHealth(5);
        }
        // heals player to max health
        public void useMaxHealthPotion()
        {
            Player.AlterToMaxHealth();
        }
    }
}