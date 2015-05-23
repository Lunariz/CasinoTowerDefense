using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    class FreezeEffect : GameObject
    {
        Emitter emitter;

        float emitTimeLeft;
        float lifeTimeLeft;

        public FreezeEffect(Vector2 position, int layer, string id = "")
            : base(layer, id)
        {
            emitTimeLeft = 0.3f;
            lifeTimeLeft = 2.0f;

            Position = position;

            emitter = new Emitter("sprites/particles/ice", 0);

            emitter.SetAlphaRange(255, 230, 70, 60);
            emitter.StartVelocity = 0;
            emitter.SetLifeSpanRange(1.0f, 1.2f);
            emitter.SpawnShape = new CircleShape(70);
            emitter.Position = Position;
            emitter.SetSizeRange(30, 55, 0, 0);
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

/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    class FreezeEffect : GameObject
    {
        float lifeTimeLeft;
        SpriteGameObject[] icicles;

        public FreezeEffect(Vector2 position, int layer, string id = "")
            : base(layer, id)
        {
            lifeTimeLeft = 3.0f;
            icicles = new SpriteGameObject[10];
            for (int i = 0; i < icicles.Length; ++i)
            {
                icicles[i] = new SpriteGameObject("sprites/particles/ice", layer);
                icicles[i].Rotation = (float)GameEnvironment.Random.NextDouble() * 360;
                icicles[i].Position = position + new Vector2((float)GameEnvironment.Random.NextDouble() * 100 - 50, (float)GameEnvironment.Random.NextDouble() * 100 - 50);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (lifeTimeLeft <= 0)
            {
                (parent as GameObjectList).Remove(this);
            }
            lifeTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < icicles.Length; ++i)
            {
                icicles[i].Draw(gameTime, spriteBatch);
            }
        }
    }
}
*/