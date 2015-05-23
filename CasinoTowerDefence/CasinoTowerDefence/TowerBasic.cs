using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    public class TowerBasic : Tower
    {
        public TowerBasic(GameGrid gameGrid, GameObjectList enemyList, GameObjectList projectileList, int level, int layer = 0, string id = "")
            : base(gameGrid, enemyList, projectileList, "sprites/towers/basic", level, layer, id)
        {
            this.projectile = new Projectile();
            this.attackDelay = 0.8f;
            this.range = 300;
            this.damage = 10;
        }
    }
}
