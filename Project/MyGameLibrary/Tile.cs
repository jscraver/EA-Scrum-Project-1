using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameLibrary
{
    public class Tile : Character
    {
        public Boolean isSolid;
        public Boolean isDamaging;
        public Boolean isSlippery;
        public Boolean isTeleporter;
        public Boolean isSlowing;

        public Tile(Vector2 initPos, Collider collider, char[] flags) : base(initPos, collider)
        {
            this.isSolid = flags[0] == '1' ? true : false;
            this.isDamaging = flags[1] == '1'? true : false;
            this.isSlippery = flags[2] == '1' ? true : false;
            this.isTeleporter = flags[3] == '1' ? true : false;
            this.isSlowing = flags[4] == '1' ? true : false;
        }
    }
}
