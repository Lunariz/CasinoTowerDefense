using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    public class Poison : Projectile
    {
        public float pTimer;
        public Emitter poisonEmitter;
        public Poison(int layer = 0, string id = "") : base(layer, id)
        {
            //this.LoadAnimation("sprites/projectiles/poison@1", "idle", true);
            //this.PlayAnimation("idle");
            this.maxSpeed = 100;
            this.homing = false;
            this.damage = 1;

            poisonEmitter = new Emitter("sprites/particles/poison", 0);

            poisonEmitter.SetColorRange(Color.White, Color.Purple, Color.PeachPuff, Color.PaleTurquoise);
            poisonEmitter.SetAlphaRange(120, 150, 0, 0);
            poisonEmitter.particlesPerSecond = 10;
            poisonEmitter.StartVelocity = 0.1f;
            poisonEmitter.SpawnShape = new BoxShape(50, 50);
            poisonEmitter.SetSizeRange(10, 40, 40, 50);
            poisonEmitter.SetLifeSpanRange(2f, 3f);
            poisonEmitter.SetRotationRange(0, 1, 2, 3);
            poisonEmitter.AddModifier(new GravityModifier(new Vector2(GameEnvironment.Random.Next(-7, 7), GameEnvironment.Random.Next(-7, 7)), 1f));

            PlayingState.AddEmitter(poisonEmitter);
        }
        public override void GoTowardsDestination(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // DO NASING
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            pTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(pTimer > 2.1f)
            {
                poisonEmitter.particlesPerSecond = 0;
                this.Die();
            }
        }
    }
}

