using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class HealthPotion : Item
    {
        public HealthPotion(string name, int quantity, Player player) : base(name, quantity, player)
        {
        }

        public void useHealthPotion()
        {
            Player.AlterHealth(5);
        }

        public void useMaxHealthPotion()
        {
            Player.AlterToMaxHealth();
        }
    }
}