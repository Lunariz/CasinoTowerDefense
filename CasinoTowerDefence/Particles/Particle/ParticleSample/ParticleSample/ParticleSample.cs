using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ParticleSample
{
    public class ParticleSample : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D particleTexture, particleTexture2, particleTexture3;
        Color fire;
        Vector2 velocityThrusters;
        Random random;
        

        List<Emitter> EmitterList;

        public ParticleSample()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            EmitterList = new List<Emitter>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            particleTexture = Content.Load<Texture2D>("Test");

            //Emitter e = new Emitter(particleTexture,);
            //EmitterList.Add(e);

            // Create an emitter, use the box shape and adda gravity modifier.
            //e = new Emitter(particleTexture);
            //e.SpawnShape = new BoxShape(200, 20);
            //e.Position.Y = 500;
            //e.Position.X = 600;
            //e.AddModifier(new GravityModifier(new Vector2(0, 1), 200));
            //e.AddModifier(new MouseForceModifier());
            //e.AddModifier(new BoxedBoundsModifier(new Vector2(1860, 1000),Vector2.Zero));
            //EmitterList.Add(e);

            //e = new Emitter(particleTexture);
            //e.SpawnShape = new BoxShape(400, 1);
            //e.Position.Y = 400;
            //e.Position.X = 400;
            //e.Color1 = Color.LawnGreen;
            //e.Color2 = Color.DarkOliveGreen;
            //e.lifeSpanUpper = 10;
            //e.lifeSpanLower = 3;
            //e.particlesPerSecond = 50;
            //e.AddModifier(new GravityModifier(new Vector2(1,-1),200));
            //e.AddModifier(new BoxedBoundsModifier(new Vector2(900, 400), new Vector2(400, 350)));
            //EmitterList.Add(e);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (Emitter e in EmitterList)
            {
                e.Update((float)gameTime.ElapsedGameTime.TotalSeconds);      
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            foreach (Emitter e in EmitterList)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
