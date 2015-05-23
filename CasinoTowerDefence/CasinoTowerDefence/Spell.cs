using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    abstract class Spell : AnimatedGameObject
    {
        protected GameGrid gameGrid;
        int aliveTime;
        protected GameObjectList enemyList;

        public Spell(GameGrid gameGrid, Vector2 position, int aliveTime, GameObjectList enemyList)
            : base ()
        {
            this.gameGrid = gameGrid;
            this.position = position;
            this.aliveTime = aliveTime;
            this.enemyList = enemyList;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (aliveTime == 0)
            {
                (parent as GameObjectList).Remove(this);
                return;
            }
            aliveTime--;
            DamageEnemies();
        }

        public abstract void DamageEnemies();
        public abstract void UpgradeTower();
    }
}
