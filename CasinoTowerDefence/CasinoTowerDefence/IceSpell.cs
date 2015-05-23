using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    class IceSpell : Spell
    {
        float radius = 1.5f;

        public IceSpell(GameGrid gameGrid, Vector2 position, int aliveTime, GameObjectList enemyList, GameObjectList effects)
            : base(gameGrid, position, aliveTime, enemyList)
        {
            Vector2 tPos = new Vector2((position.X + 1.5f) * gameGrid.CellWidth + position.X, (position.Y + 1.5f) * gameGrid.CellHeight + 0.5f + position.Y);
            effects.Add(new FreezeEffect(tPos, 100));
            GameEnvironment.AssetManager.PlaySound("sounds/freeze");
        }

        public override void DamageEnemies()
        {

            foreach (Enemy enemy in enemyList.Objects)
            {
                Vector2 currentPosition = new Vector2((int)Math.Max(0, Math.Round((enemy.Position.X - gameGrid.Position.X) / gameGrid.CellWidth - 0.5f)), (int)Math.Max(0, Math.Round((enemy.Position.Y - gameGrid.Position.Y) / gameGrid.CellHeight - 0.5f)));
                if ((currentPosition - position).Length() < radius)
                {
                    enemy.isFrozen = true;
                    enemy.frozenTimer = 160;
                }
            }
        }

        public override void UpgradeTower()
        {

        }
    }
}
