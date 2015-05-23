using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    class PoisonSpell : Spell
    {
        float radius = 1.5f;

        public PoisonSpell(GameGrid gameGrid, Vector2 position, int aliveTime, GameObjectList enemyList, GameObjectList effects)
            : base(gameGrid, position, aliveTime, enemyList)
        {
            Vector2 tPos = new Vector2((position.X + 1.5f) * gameGrid.CellWidth + position.X, (position.Y + 1.5f) * gameGrid.CellHeight + 0.5f + position.Y);
            effects.Add(new PoisonEffect(tPos, 100));
            GameEnvironment.AssetManager.PlaySound("sounds/poison");
        }

        public override void DamageEnemies()
        {
            foreach (Enemy enemy in enemyList.Objects)
            {
                Vector2 currentPosition = new Vector2((int)Math.Max(0, Math.Round((enemy.Position.X - PlayingState.GameGrid.Position.X) / PlayingState.GameGrid.CellWidth - 0.5f)), (int)Math.Max(0, Math.Round((enemy.Position.Y - PlayingState.GameGrid.Position.Y) / PlayingState.GameGrid.CellHeight - 0.5f)));
                if ((currentPosition - position).Length() < radius)
                {
                    enemy.isPoisoned = true;
                    enemy.poisonStacks++;
                    enemy.poisonTimer = 300;
                    enemy.isWeak = true;
                    enemy.weakTimer = 300;
                }
            }
        }

        public override void UpgradeTower()
        {

        }
    }
}
