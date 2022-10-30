using System.Text;
using System.Threading.Tasks;

namespace Fall2020_CSC403_Project.code
{
    public class Sword : Item
    {
        public int Durability { get; set; }

        public Sword(string name, Player player, int durability) : base(name, player) {
            Durability = durability;
        }

        public void attackWithSwordLight()
        {
            Player.OnAttack(-3);
        }

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