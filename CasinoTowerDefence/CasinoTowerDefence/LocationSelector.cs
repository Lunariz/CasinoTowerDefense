using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CasinoTowerDefence
{
    class LocationSelector : GameObject
    {
        GameObjectGrid grid;

        bool selectingX;
        bool selectingY;

        float selectPos;
        bool reverse;

        int posX;
        int posY;

        float speed = 10.0f;

        bool buttonPressed;

        // Start the selecting process
        public LocationSelector(GameObjectGrid grid)
        {
            this.grid = grid;

            selectingX = false;
            selectingY = false;
            selectPos = 0;
            buttonPressed = false;
        }

        public void StartSelecting()
        {
            StartSelectingX();
        }

        void StartSelectingX()
        {
            selectPos = 0;
            selectingX = true;
            reverse = false;
        }

        void StartSelectingY()
        {
            selectingX = false;
            selectPos = 0;
            selectingY = true;
            reverse = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Space))
                buttonPressed = true;
            else
                buttonPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (selectingX)
            {
                if (reverse)
                {
                    selectPos -= (float)gameTime.ElapsedGameTime.Milliseconds * speed / 1000.0f;
                    if (selectPos <= 0)
                        reverse = false;
                }
                else
                {
                    selectPos += (float)gameTime.ElapsedGameTime.Milliseconds * speed / 1000.0f;
                    if (selectPos >= grid.Objects.GetLength(0) - 1)
                        reverse = true;
                }
                if (buttonPressed)
                {
                    posX = (int)Math.Round(selectPos);
                    StartSelectingY();
                }
            }
            else if (selectingY)
            {
                if (reverse)
                {
                    selectPos -= (float)gameTime.ElapsedGameTime.Milliseconds * speed / 1000.0f;
                    if (selectPos <= 0)
                        reverse = false;
                }
                else
                {
                    selectPos += (float)gameTime.ElapsedGameTime.Milliseconds * speed / 1000.0f;
                    if (selectPos >= grid.Objects.GetLength(1) - 1)
                        reverse = true;
                }
                if (buttonPressed)
                {
                    posY = (int)Math.Round(selectPos);
                    Done();
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (selectingX)
            {
                Rectangle getRekt = new Rectangle((int)grid.Position.X + (int)(selectPos * grid.CellWidth), (int)grid.Position.Y, grid.CellWidth, grid.CellHeight * grid.Objects.GetLength(1));
                DrawingHelper.DrawRectangleFilled(getRekt, spriteBatch, new Color(100, 100, 100, 128));
            }
            else if (selectingY)
            {
                Rectangle getRekt = new Rectangle((int)grid.Position.X + posX * grid.CellWidth, (int)grid.Position.Y + (int)(selectPos * grid.CellHeight), grid.CellWidth, grid.CellHeight);
                DrawingHelper.DrawRectangleFilled(getRekt, spriteBatch, new Color(100, 100, 100, 128));
            }
        }

        // If the player is busy selecting
        public bool IsBusy
        {
            get
            {
                return selectingX || selectingY;
            }
        }

        public Point Result
        {
            get
            {
                return new Point(posX, posY);
            }
        }

        void Done()
        {
            selectingY = false;
        }
    }
}
