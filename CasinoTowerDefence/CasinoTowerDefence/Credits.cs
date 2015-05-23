using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace CasinoTowerDefence
{
    public class Credits : GameObjectList
    {
        SpriteGameObject selector;
        SpriteGameObject SGOtexts;

        float timer = 0;
        public Credits()
        {
            SGOtexts = new SpriteGameObject("sprites/ui/thx", 1000, "menu");
            SGOtexts.Position = new Vector2(230, 50);
            selector = new SpriteGameObject("sprites/ui/selector", 1000, "selector");
            selector.Position = new Vector2(100, 350);
            this.Add(selector);
            this.Add(SGOtexts);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if(timer > 2)
            {
                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    timer = 0;
                    GameEnvironment.GameStateManager.SwitchTo("mainmenu");
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
