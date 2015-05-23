using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    class Spawner : GameObject
    {
        GameGrid gameGrid;
        GameObjectList enemyList;
        Pathfinder pathfinder;
        float enemySpeed = 120;
        float health = 10;
        float spawnInterval = 1;
        float currentInterval;
        float waveInterval = 10;
        float baseWaveInterval = 10;
        int amount = 0;
        int baseAmount = 5;
        int tempBaseAmount;
        float wave = 0;
        Point spawn;
        Point end;
        DefaultPath defaultPath;

        public Spawner(GameGrid gameGrid, GameObjectList enemyList, Pathfinder pathfinder, DefaultPath defaultPath, Point spawn, Point end)
            : base()
        {
            this.defaultPath = defaultPath;
            this.spawn = spawn;
            this.end = end;
            this.gameGrid = gameGrid;
            this.enemyList = enemyList;
            this.pathfinder = pathfinder;
        }

        public void Spawn(float enemySpeed, float health, float spawnInterval, int amount)
        {
            this.enemySpeed = enemySpeed;
            this.health = health;
            this.spawnInterval = spawnInterval;
            this.currentInterval = spawnInterval;
            this.amount = baseAmount;
        }

        public void nextWave()
        {
            Console.WriteLine(wave);
            wave++;
            enemySpeed *= 1 + (1f / (5f * wave + 1));
            health *= 1 + (1f / (1.5f * wave + 1));
            spawnInterval /= 1 + (1f / (1.5f * wave + 1));
            baseAmount *= (int) (1 + (1f / (0.5f * wave + 1)));
            if ((int) wave % 5 == 0 && wave != 0)
            {
                health *= baseAmount;
                tempBaseAmount = baseAmount;
                baseAmount = 1;
            }
            if ((int) wave % 5 == 1 && wave != 1)
            {
                health /= baseAmount;
                baseAmount = tempBaseAmount;
            }
            Spawn(enemySpeed, health, spawnInterval, amount);
        }

        public override void Update(GameTime gameTime)
        {
            if (amount == 0)
            {
                if (waveInterval > 0) waveInterval -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    waveInterval = baseWaveInterval;
                    nextWave();
                }
            }
            else
            {
                if (currentInterval > 0) currentInterval -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    BasicEnemy enemy = new BasicEnemy(gameGrid, new Vector2(0, 0), (int) enemySpeed, pathfinder, enemyList, defaultPath, spawn, end);
                    Point[] path = pathfinder.Findpath(spawn, end);
                    if(path == null)
                        path = defaultPath.path;
                    enemy.Move(pathfinder, path);
                    enemy.Health = health;
                    enemyList.Add(enemy);
                    amount--;
                    currentInterval = spawnInterval;
                }
            }
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
