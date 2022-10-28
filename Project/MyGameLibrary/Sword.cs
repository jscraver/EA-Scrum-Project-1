using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    // sword class
    public class Sword : Item
    {
        public int Durability { get; set; }
        // sword requires all attributes of Item and a Durability
        public Sword(string name, Player player, int quantity, int durability) : base(name, player, quantity) {
            Durability = durability;
        }
        // light attack
        public void attackWithSwordLight()
        {
            Player.OnAttack(-3);
        }
        // heavy attack
        public void attackWithSwordHeavy()
        {
            Player.OnAttack(-10);
        }
 
        public void subtractDurability(int amount)
        {
            Durability -= amount;
        }

    }
}