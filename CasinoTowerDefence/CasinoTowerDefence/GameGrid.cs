using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    public class GameGrid : GameObjectGrid
    {
        public GameGrid(int rows, int columns, int layer = 0, string id = "gameGrid") : base(rows, columns, layer, id)
        {
            cellWidth = 48;
            cellHeight = 48;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < grid.GetLength(0); x++) 
            {
                for (int y = 0; y < grid.GetLength(1); y++) 
                {
                    DrawingHelper.DrawRectangle(new Rectangle((int)this.position.X + x * cellWidth, (int)this.position.Y + y * cellHeight, cellWidth, cellHeight), spriteBatch, new Color(150, 150, 150, 255));
                }
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
