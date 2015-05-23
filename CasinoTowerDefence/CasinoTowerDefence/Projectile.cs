using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    public class Projectile : AnimatedGameObject
    {
        protected Boolean damageBugFixer = false;
        protected float maxSpeed, damage = 1;
        protected Vector2 destination;
        protected Enemy destinationEnemy;
        protected bool homing, playedSoundNorm = false;
        protected Tower parentTower;

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public virtual void Die()
        {
            (parent as PlayingState).Remove(this);
        }

        public Projectile(int layer = 0, string id = "") : base(layer, id)
        {
            this.LoadAnimation("sprites/projectiles/normalbullet@4", "flying", false);
            this.LoadAnimation("sprites/projectiles/normalbullet_fizzle@4", "fizzle", false);
            this.PlayAnimation("flying");
            this.maxSpeed = 400;
            this.homing = true;
            velocity = new Vector2(0.0000001f);
        }

        public void SetDestination(Enemy enemy)
        {
            destinationEnemy = enemy;
        }

        public virtual void DealDamage(Enemy enemy)
        {
            enemy.Damage(damage);
            //Console.WriteLine("I did " + damage + " damage");
        }

        public virtual void GoTowardsDestination(GameTime gameTime)        
        {           
            if (destinationEnemy != null)
            {
                Vector2 posDiff = destination - position;
                //if (Math.Abs(posDiff.X) < maxSpeed * gameTime.ElapsedGameTime.TotalSeconds && Math.Abs(posDiff.Y) < maxSpeed * gameTime.ElapsedGameTime.TotalSeconds)
                if(CollidesWith(destinationEnemy))
                {
                    if(!playedSoundNorm)
                        GameEnvironment.AssetManager.PlaySound("sounds/normalhit");
                    velocity = Vector2.Zero;
                    DealDamage(destinationEnemy);
                    this.Die();
                }
                else
                {
                    posDiff.Normalize();
                    if (velocity.Length() < maxSpeed)
                    {

                        velocity = posDiff * maxSpeed;

                    }
                    else
                    {
                        velocity.Normalize();
                        if (velocity == posDiff)
                        {
                            velocity *= maxSpeed;
                        }
                        else
                        {
                            velocity = posDiff * maxSpeed;
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (/*(destinationEnemy != null && destinationEnemy.Health < 0 && homing) || */velocity == Vector2.Zero || (position.X < 0 || position.Y < 0 || position.X > GameEnvironment.Screen.X || position.Y > GameEnvironment.Screen.Y))
            {
                this.Die();
            }

            if(destinationEnemy != null)
            {
                    destination = destinationEnemy.GlobalPosition;
            }
            GoTowardsDestination(gameTime);
            if (!(this is Poison))
            {
                maxSpeed += 15;
            }

        }

        public Tower ParentTower
        {
            set { parentTower = value; }
        }
    }
}
