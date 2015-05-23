using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    public class Fireball : Projectile
    {
        GameObjectList enemyList;
        float blastRadius;
        bool playedSound = false;
        public Fireball(GameObjectList enemyList, float blastRadius, int layer = 0, string id = "") : base(layer, id)
        {
            this.LoadAnimation("sprites/projectiles/fireball@4", "flying", false);
            this.LoadAnimation("sprites/projectiles/fireball_fizzle@4", "fizzle", false);
            this.PlayAnimation("flying");
            this.maxSpeed = 500;
            this.homing = true;
            this.enemyList = enemyList;
            this.blastRadius = blastRadius;
            velocity = new Vector2(0.0000001f);
        }

        public override void Die()
        {
            this.PlayAnimation("fizzle");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (/*(destinationEnemy != null && destinationEnemy.Health < 0 && homing) || */velocity == Vector2.Zero || (position.X < 0 || position.Y < 0 || position.X > GameEnvironment.Screen.X || position.Y > GameEnvironment.Screen.Y))
            {
                this.Die();
                if(!playedSound)
                {
                    GameEnvironment.AssetManager.PlaySound("sounds/fire");
                    playedSound = true;
                }
            }

            if (this.Current.AnimationEnded && this.IsAnimationPlaying("fizzle"))
            {
                (parent as PlayingState).Remove(this);
                
            }

            maxSpeed += 15;
        }

        public override void DealDamage(Enemy targetEnemy)
        {
            
            if (damageBugFixer) return;
            float damageDone = 0;
            foreach (Enemy enemy in enemyList.Objects)
            {
                GameGrid gameGrid = enemy.gameGrid;
                Vector2 currentPosition = new Vector2((int)Math.Max(0, Math.Round((enemy.Position.X - gameGrid.Position.X) / gameGrid.CellWidth - 0.5f)), (int)Math.Max(0, Math.Round((enemy.Position.Y - gameGrid.Position.Y) / gameGrid.CellHeight - 0.5f)));

                if ((currentPosition - position).Length() < blastRadius)
                {
                    damageBugFixer = true;
                    enemy.Damage(damage);
                    damageDone += damage;
                }
            }
        }
    }
}
