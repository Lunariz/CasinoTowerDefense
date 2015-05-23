using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    class BasicEnemy : Enemy
    {
        public BasicEnemy(GameGrid gameGrid, Vector2 position, int speed, Pathfinder pathfinder, GameObjectList enemyList, DefaultPath defaultPath, Point spawn, Point end)
            : base(gameGrid, position, speed, pathfinder, enemyList, defaultPath, spawn, end)
        {
            this.position = position;
            this.speed = speed;
            this.scoreValue = 10;
            this.LoadAnimation("sprites/enemy@3", "walk", true);
            this.PlayAnimation("walk");

        }

        public override void Update(GameTime gameTime)
        {
            if (this.direction == new Vector2(0, 1))
                this.rotation = (float)(Math.PI);
            else if (this.direction == new Vector2(0, -1))
                this.rotation = (float)(0);
            else if (this.direction == new Vector2(1, 0))
                this.rotation = (float)(0.5 * Math.PI);
            else if (this.direction == new Vector2(-1, 0))
                this.rotation = (float)(-0.5 * Math.PI);

                        sprite.SheetIndex++;
            if(sprite.SheetIndex >2)
                sprite.SheetIndex = 0;

            base.Update(gameTime);
        }
    }
}
