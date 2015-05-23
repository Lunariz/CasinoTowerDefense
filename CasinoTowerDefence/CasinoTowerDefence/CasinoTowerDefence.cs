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

namespace CasinoTowerDefence
{
    public class CasinoTowerDefence : GameEnvironment
    {
        public CasinoTowerDefence()
        {
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            gameStateManager.AddGameState("mainmenu", new MainMenu());
            gameStateManager.AddGameState("playingState", new PlayingState());
            gameStateManager.AddGameState("credits", new Credits());
            gameStateManager.SwitchTo("mainmenu");
            screen = new Point(1280, 720);
            this.SetFullScreen(false);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(201, 172, 117));
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TransformMatrix * spriteScale);
            gameStateManager.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

    }
}
