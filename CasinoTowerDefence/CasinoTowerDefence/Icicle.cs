using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    class Icicle : Projectile
    {
        GameObjectList enemyList;
        float slowStrength;
        float duration;

        public Icicle(GameObjectList enemyList, float slowStrength, float duration, int layer = 0, string id = "")
            : base(layer, id)
        {
            this.LoadAnimation("sprites/projectiles/fireball@4", "flying", false);
            this.LoadAnimation("sprites/projectiles/fireball_fizzle@4", "fizzle", false);
            this.PlayAnimation("flying");
            this.color = Color.Blue;
            this.maxSpeed = 500;
            this.homing = true;
            this.enemyList = enemyList;
            this.slowStrength = slowStrength;
            this.duration = duration;
            velocity = new Vector2(0.0000001f);
        }

        public override void Die()
        {
            this.PlayAnimation("fizzle");
            velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (/*(destinationEnemy != null && destinationEnemy.Health < 0 && homing) || */velocity == Vector2.Zero || (position.X < 0 || position.Y < 0 || position.X > GameEnvironment.Screen.X || position.Y > GameEnvironment.Screen.Y) || destinationEnemy.isFrozen)
            {
                this.Die();
                velocity = Vector2.Zero;
                destinationEnemy = null;
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
            damageBugFixer = true;
            targetEnemy.Damage(damage);
            targetEnemy.isSlowed = true;
            targetEnemy.slowedTimer = duration;
            targetEnemy.slowStrength = slowStrength;
            GameEnvironment.AssetManager.PlaySound("sounds/freeze");
        }
    }
}
