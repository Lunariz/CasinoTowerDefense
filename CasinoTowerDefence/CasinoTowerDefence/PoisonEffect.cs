using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    class PoisonEffect : GameObject
    {
        Emitter emitter;

        float emitTimeLeft;
        float lifeTimeLeft;

        public PoisonEffect(Vector2 position, int layer, string id = "")
            : base(layer, id)
        {
            emitTimeLeft = 0.1f;
            lifeTimeLeft = 10.0f;

            Position = position;

            emitter = new Emitter("sprites/particles/poison", 0);

            //emitter.SetColorRange(new Color(128, 255, 128), new Color(80, 250, 80), new Color(0, 0, 0), new Color(0, 0, 0));
            emitter.SetAlphaRange(160, 96, 110, 80);
            emitter.StartVelocity = 25;
            emitter.SetLifeSpanRange(2, 2);
            emitter.SetRotationRange(0, 3, 3, 5);
            emitter.SpawnShape = new CircleShape(10);
            emitter.Position = Position;
            emitter.SetSizeRange(60, 70, 70, 60);
            emitter.particlesPerSecond = 100;
        }

        public override void Update(GameTime gameTime)
        {
            if (emitTimeLeft < 0)
            {
                emitter.particlesPerSecond = 0;
            }
            else
            {
                emitTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            emitter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (lifeTimeLeft <= 0)
            {
                (parent as GameObjectList).Remove(this);
            }
            lifeTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
        }
    }
}
