using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CasinoTowerDefence
{
    public class MainMenu : GameObjectList
    {
        SpriteGameObject selector;
        SpriteGameObject SGOtexts;
        SpriteGameObject title;
        SpriteGameObject spacebar;
        SpriteGameObject background;
        //List<SlotMachineItem> MainMenuItems;
        float timer;
        float size = 1.0f;
        public MainMenu()
        {
            //MainMenuItems = new List<SlotMachineItem>();
            SGOtexts = new SpriteGameObject("sprites/ui/menu", 1000, "menu");
            SGOtexts.Position = new Vector2((1240-320)/2, 400);
            title = new SpriteGameObject("sprites/ui/casinotd", 1000, "title");
            title.Position = new Vector2((1240 - 718) / 2, 200);
            selector = new SpriteGameObject("sprites/ui/selector", 1000, "selector");
            selector.Position = new Vector2((1240 - 320) / 2 - 90, 425);
            spacebar = new SpriteGameObject("sprites/ui/spacebar", 1001, "spacebar");
            spacebar.Origin = new Vector2(spacebar.Width / 2, spacebar.Height / 2);
            spacebar.Position = new Vector2(170, 170);
            spacebar.Rotation = (float)(-0.25 * Math.PI);
            background = new SpriteGameObject("sprites/ui/menubackground", 1);
            background.Position = new Vector2(0, 0);
            this.Add(background);
            this.Add(selector);
            this.Add(SGOtexts);
            this.Add(title);
            this.Add(spacebar);
            GameEnvironment.AssetManager.PlayMusic("backgroundbeat");
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if(inputHelper.KeyPressed(Keys.Space))
            {
                if(selector.Position.Y == 425)
                    GameEnvironment.GameStateManager.SwitchTo("playingState");
                else if(selector.Position.Y == 425+72)
                    GameEnvironment.GameStateManager.SwitchTo("credits");
                else
                    Environment.Exit(0);
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer > 0.5)
            {
                selector.Position += new Vector2(0, 72);
                if (selector.Position.Y > 600)
                    selector.Position = new Vector2(selector.Position.X, 425);
                timer = 0;
            }

            spacebar.Scale = new Vector2((float)Math.Abs(Math.Sin(gameTime.TotalGameTime.TotalSeconds)) / 2 + 0.8f);
        }
    }

    
}
