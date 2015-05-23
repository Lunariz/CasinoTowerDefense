using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    class FireTower : Tower
    {
        public FireTower(GameGrid gameGrid, GameObjectList enemyList, GameObjectList projectileList, int level, int layer = 0, string id = "")
            : base(gameGrid, enemyList, projectileList, "sprites/towers/fire", level, layer, id)
        {
            //this.projectile = new Fireball(enemyList, 50f);
            //projectileList.Add(this.projectile);
            this.damage = 5;
            this.range = 300;
        }
    }
}
