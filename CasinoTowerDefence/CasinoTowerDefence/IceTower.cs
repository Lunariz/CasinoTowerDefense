using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace CasinoTowerDefence
{
    class IceTower : Tower
    {
        public IceTower(GameGrid gameGrid, GameObjectList enemyList, GameObjectList projectileList, int level, int layer = 0, string id = "")
            : base(gameGrid, enemyList, projectileList, "sprites/towers/ice", level, layer, id)
        {
            //this.projectile = new Projectile();

            this.damage = 10;
            this.range = 300;  
        }
    }
}
