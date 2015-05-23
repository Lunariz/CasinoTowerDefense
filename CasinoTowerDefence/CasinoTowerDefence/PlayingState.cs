using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CasinoTowerDefence
{
    public class PlayingState : GameObjectList
    {
        static GameGrid gameGrid;
        PoisonTower towah;
        PlayerController controller;
        Pathfinder pathfinder;
        Spawner spawner;
        protected static GameObjectList enemyList;
        protected static GameObjectList spellList;
        protected static GameObjectList projectileList;
        DefaultPath defaultPath;
        protected static ScoreController scoreController;
        Point spawn = new Point(0, 0);
        Point end = new Point(16, 13);
        protected static List<Emitter> emitterList;
        SpriteGameObject bgBorder;
        SpriteGameObject casino;
        SpriteGameObject spawnSprite;
        GameObjectList effects;

        public static int lives = 25;

        public static GameGrid GameGrid
        {
            get { return gameGrid; }
        }

        public static GameObjectList EnemyList
        {
            get { return enemyList; }
        }

        public PlayingState()
        {
            // Effects
            effects = new GameObjectList(100);
            Add(effects);

            // Background
            this.Add(new SpriteGameObject("sprites/ui/background"));
            SpriteGameObject behindTube = new SpriteGameObject("sprites/ui/behindcashtube", 151);
            bgBorder = new SpriteGameObject("sprites/ui/border");
            bgBorder.Position = new Vector2(856, 0);
            // Grid
            gameGrid = new GameGrid(14, 17);
            gameGrid.Position = new Vector2(23, 23);
            this.Add(gameGrid);

            // Pathfinder
            pathfinder = new Pathfinder(gameGrid);

            // Particle emitter
            emitterList = new List<Emitter>();
            // Score Controller
            scoreController = new ScoreController(this);
            this.Add(scoreController);
            // Default path
            defaultPath = new DefaultPath();
            defaultPath.path = pathfinder.Findpath(spawn, end);

            enemyList = new GameObjectList();
            this.Add(enemyList);

            spawner = new Spawner(gameGrid, enemyList, pathfinder, defaultPath, spawn, end);
            this.Add(spawner);

            spellList = new GameObjectList();
            this.Add(spellList);

            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 3, 0);
            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 3, 3);
            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 0, 3);

            casino = new SpriteGameObject("sprites/ui/casino", 1200, "casino");
            casino.Position = new Vector2(775, 633);
            this.Add(casino);

            spawnSprite = new SpriteGameObject("sprites/ui/casino", 1200, "spawn");
            spawnSprite.Position = new Vector2(20, 20);
            spawnSprite.Origin = new Vector2(spawnSprite.Width / 2, spawnSprite.Height / 2);
            spawnSprite.Rotation = (float)(-0.25f * Math.PI);
            spawnSprite.Color = Color.DarkRed;
            this.Add(spawnSprite);
            // Controller
            controller = new PlayerController(pathfinder, enemyList, spellList, projectileList, gameGrid, defaultPath, spawn, end, effects);
            Add(controller);

            behindTube.Position = new Vector2(856, 0);
            this.Add(behindTube);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Emitter e in emitterList)
            {
                e.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (lives == 0)
            {
                this.Reset();
                GameEnvironment.GameStateManager.SwitchTo("credits");
            }

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach (Emitter e in emitterList)
            {
                e.Draw(spriteBatch);
            }
            bgBorder.Draw(gameTime, spriteBatch);
            foreach(GameObject obj in Objects)
            {
                if(obj is TextGameObject)
                {
                    obj.Draw(gameTime, spriteBatch);
                }
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //if(inputHelper.MouseLeftButtonPressed())
            //{
            //    this.Reset();
            //}
        }

        public static void AddEmitter(Emitter e)
        {
            emitterList.Add(e);
        }

        public static Enemy GetClosestEnemy(Vector2 fromPos, float range = -1)
        {
            Enemy tempEnemy = null;
            float distance = -1;
            foreach(GameObject obj in enemyList.Objects)
            {
                if(obj is Enemy)
                {
                    if (Vector2.Distance(fromPos, obj.GlobalPosition) < range || range == -1)
                    {
                        if (distance == -1)
                        {
                            tempEnemy = obj as Enemy;
                            distance = Vector2.Distance(fromPos, obj.GlobalPosition);
                        }
                        else if (Vector2.Distance(fromPos, obj.GlobalPosition) < distance)
                        {
                            tempEnemy = obj as Enemy;
                            distance = Vector2.Distance(fromPos, obj.GlobalPosition);
                        }
                    }
                }

            }

            return tempEnemy;
        }

        public static ScoreController ScoreController
        {
            get { return scoreController; }
        }

        public override void Reset()
        {
            base.Reset();
            
            //reset lives score multiplier
            lives = 25;
            scoreController.ResetMultiplier();
            scoreController.score = 0;

            //reset towers
            for (int x = 0; x < 17; x++)
                for (int y = 0; y < 14; y++)
                    gameGrid.Add(null, x, y);

            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 3, 0);
            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 3, 3);
            gameGrid.Add(new TowerBasic(gameGrid, enemyList, projectileList, 1), 0, 3);

            //reset enemylist
            enemyList.Objects.Clear();

            //reset spawning
            this.Remove(spawner);
            spawner = new Spawner(gameGrid, enemyList, pathfinder, defaultPath, spawn, end);
            this.Add(spawner);

            //reset placer level

            //reset slotmachine level
        }
    }
}