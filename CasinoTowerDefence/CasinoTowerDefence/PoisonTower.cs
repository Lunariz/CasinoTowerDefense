using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    public class PoisonTower : Tower
    {
        bool shooting = false;
        bool placedPoison = false;
        bool init = false;
        Poison poison;

        public PoisonTower(GameGrid gameGrid, GameObjectList enemyList, GameObjectList projectileList, int level, int layer = 0, string id = "")
            : base(gameGrid, enemyList, projectileList, "sprites/towers/poison", level, layer, id)
        {
            poison = new Poison();
            this.damage = 0.1f;
            this.range = 60;

        }
        public override void FireProjectileAt(Enemy enemy, Projectile newProj)
        {
            // THIS DOES NOTHING DO NOT REMOVE
        }
        public override void Update(GameTime gameTime)
        {
            poison.poisonEmitter.Position = this.GlobalPosition + center* 0.7f;
            base.Update(gameTime);

            foreach (Enemy enemy in PlayingState.EnemyList.Objects)
            {
                if (Vector2.Distance(this.GlobalPosition + new Vector2(24), enemy.GlobalPosition) < range)
                {
                    //SET STACKS
                    enemy.PoisonStacks += 1;
                    enemy.poisonTimer = 1f;
                    shooting = true;
                    poison.pTimer = 0;
                }

            }

            if (shooting == true)
            {
                if (!init)
                {
                    init = true;
                    SetPoison(poison);
                }

                if(!placedPoison)
                {
                    poison.Visible = true;
                    placedPoison = true;
                }

                poison.poisonEmitter.particlesPerSecond = 10;
            }
            else
            {
                poison.poisonEmitter.particlesPerSecond = 0;
            }

            if(poison.pTimer > 2)
            {
                poison.pTimer = 0;
                placedPoison = false;
                shooting = false;
                poison.Visible = false;
            }

            
        }

        public void SetPoison(Projectile newProj)
        {
            poison.Visible = false;
            newProj.Velocity = new Vector2(0.000001f);
            newProj.Position = position + center * 2;
            newProj.ParentTower = this;
            (parent.Parent as PlayingState).Add(newProj);
            GameEnvironment.AssetManager.PlaySound("sounds/poison");
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
        }

        public void Die()
        {
            //(parent as GameObjectGrid).Remove(this);
            //(parent as PlayingState).Remove(this);
            //poison = null;
        }

        public void damageEnemies()
        {

        }
    }
}
