using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasinoTowerDefence
{
    public class Enemy : AnimatedGameObject
    {
        Point[] gridQueue;
        Point[] queue;
        protected int currentQueue;
        protected int speed;
        protected Vector2 direction;
        public GameGrid gameGrid;
        protected float health;
        protected double scoreValue;
        Pathfinder pathfinder;
        GameObjectList enemyList;
        Point spawn;
        Point end;
        DefaultPath defaultPath;

        public Boolean isFrozen;
        public float frozenTimer;
        public Boolean isSlowed;
        public float slowedTimer;
        public float slowStrength;
        public Boolean isWeak;
        public float weakTimer;
        public Boolean isPoisoned;
        public float poisonTimer;
        public float poisonStacks;

        Texture2D debuff;

        public Enemy(GameGrid gameGrid, Vector2 position, int speed, Pathfinder pathfinder, GameObjectList enemyList, DefaultPath defaultPath, Point spawn, Point end)
            : base(0, "enemy")
        {
            debuff = GameEnvironment.AssetManager.GetSprite("sprites/weakness");
            this.defaultPath = defaultPath;
            this.spawn = spawn;
            this.scoreValue = 0;
            this.end = end;
            this.pathfinder = pathfinder;
            this.enemyList = enemyList;
            this.gameGrid = gameGrid;
            this.position = position;
            this.speed = speed;
            this.health = 1500;
        }

        public void Move(Pathfinder pathfinder, Point[] points)
        {
            if (points != null && (queue == null || points.Length < queue.Length || !pathfinder.IsPathValid(gridQueue)))
            {
                currentQueue = 1;
                queue = new Point[points.Length];
                gridQueue = points;
                for (int i = 0; i < queue.Length; i++)
                {
                    queue[i] = new Point((int)gameGrid.Position.X + (int)((points[i].X + 0.5f) * gameGrid.CellWidth), (int)gameGrid.Position.Y + (int)((points[i].Y + 0.5f) * gameGrid.CellHeight));
                }
            }
        }

        public void Die(bool reachedEnd = false)
        {
            (parent as GameObjectList).Remove(this);
            if (!reachedEnd)
            {
                PlayingState.ScoreController.Score += (int)(scoreValue * PlayingState.ScoreController.Multiplier);
                PlayingState.ScoreController.Multiplier += 0.2;
            }
            else
            {
                PlayingState.ScoreController.ResetMultiplier();
            }
        }

        public void Damage(float amount)
        {
            health -= amount;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if(isWeak)
                spriteBatch.Draw(debuff, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, debuff.Width, debuff.Height-10), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Color = Color.White;
            if (isSlowed || isFrozen)
                Color = Color.Blue;
            if (isPoisoned)
                Color = Color.Green;
            if (isPoisoned && (isSlowed || isFrozen))
                Color = Color.Turquoise;

            if (health < 0)
            {
                this.Die();
                return;
            }

            if (frozenTimer > 0) frozenTimer--;
            else isFrozen = false;
            if (slowedTimer > 0) slowedTimer--;
            else isSlowed = false;
            if (weakTimer > 0) weakTimer--;
            else isWeak = false;
            if (poisonTimer > 0)
            {
                isPoisoned = true;
                poisonTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                isPoisoned = false;
            } 

            if (isPoisoned) health -= 0.1f * poisonStacks;

            float tempSpeed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.isFrozen) tempSpeed = 0;
            else if (this.isSlowed) tempSpeed *= this.slowStrength;
            if (currentQueue != null)
            if(Vector2.Distance(Position, new Vector2(queue[currentQueue].X, queue[currentQueue].Y)) <= 0.1f)
            {

                    currentQueue++;
            }
            if (currentQueue >= queue.Length)
            {
                PlayingState.lives--;
                this.Die(true);
                return;
            }

            Point destination = queue[currentQueue];

            if (destination.X - position.X >= tempSpeed)
                position.X += tempSpeed;
            else if (position.X - destination.X >= tempSpeed)
                position.X -= tempSpeed;
            else position.X = destination.X;

            if (destination.Y - position.Y >= tempSpeed)
                position.Y += tempSpeed;
            else if (position.Y - destination.Y >= tempSpeed)
                position.Y -= tempSpeed;
            else position.Y = destination.Y;

            Vector2 tempDestination = new Vector2(destination.X, destination.Y);
            if (tempDestination != position)
            {
                Vector2 dir = tempDestination - position;
                dir.Normalize();
                direction = dir;
            }

            if (gameGrid.Get(gridQueue[currentQueue].X, gridQueue[currentQueue].Y) != null)
            {
                gameGrid.Add(null, gridQueue[currentQueue].X, gridQueue[currentQueue].Y);
                EnemyUpdater.updateEnemies(pathfinder, enemyList, defaultPath, spawn, end);
            }
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public Vector2 Direction
        {
            get { return direction; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public float PoisonStacks
        {
            get { return poisonStacks; }
            set { poisonStacks = value; }
        }
    }
}
