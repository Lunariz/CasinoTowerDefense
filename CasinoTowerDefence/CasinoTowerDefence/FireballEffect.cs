using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    class FireballEffect : GameObject
    {
        Emitter emitter;

        float emitTimeLeft;
        float lifeTimeLeft;

        public FireballEffect(Vector2 position, int layer, string id = "")
            : base(layer, id)
        {
            emitTimeLeft = 0.3f;
            lifeTimeLeft = 2.0f;

            Position = position;

            emitter = new Emitter("sprites/particles/fire", 0);

            emitter.SetColorRange(new Color(255, 128, 128), new Color(250, 80, 80), new Color(0, 0, 0), new Color(0, 0, 0));
            emitter.SetAlphaRange(160, 96, 20, 20);
            emitter.StartVelocity = 400;
            emitter.SetLifeSpanRange(0.6f, 0.6f);
            emitter.SetRotationRange(0, 3, 3, 5);
            emitter.SpawnShape = new CircleShape(32);
            emitter.Position = Position;
            emitter.SetSizeRange(30, 55, 55, 30);
            emitter.particlesPerSecond = 120;


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
