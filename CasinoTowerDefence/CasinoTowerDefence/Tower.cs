using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    public class Tower : GameObject
    {
        protected float damage, range, timer, attackSpeed;
        protected GameObjectList enemyList;
        protected Projectile projectile;
        protected Enemy destinationEnemy;
        Point gridSize = new Point(48, 48);
        int towerLevel;
        Texture2D sprite;
        protected Vector2 center;
        GameGrid gameGrid;
        protected float attackDelay = 1.5f;

        public Tower(GameGrid gameGrid, GameObjectList enemyList, GameObjectList projectileList, string sprite, int level, int layer = 0, string id = "")
            : base(layer, id)
        {
            this.gameGrid = gameGrid;
            center = new Vector2(gameGrid.CellWidth / 2, gameGrid.CellHeight / 2);
            this.sprite = GameEnvironment.AssetManager.GetSprite(sprite);
            destinationEnemy = null;

            this.enemyList = enemyList;
            this.projectile = null;
            this.damage = 5;
            this.range = 0;
            towerLevel = level;
        }

        public int Level
        {
            get
            {
                return towerLevel;
            }
            set
            {
                towerLevel = value;
            }
        }

        public virtual void FireProjectileAt(Enemy enemy, Projectile newProj)
        {
            newProj.Position = GlobalPosition + new Vector2(24, 48) - new Vector2(0, 10 * (towerLevel - 1)) - new Vector2(0, sprite.Height / 2);
            newProj.SetDestination(enemy);
            newProj.Damage = damage;
            newProj.ParentTower = this;
            (parent.Parent as PlayingState).Add(newProj);
        }

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public float Range
        {
            get { return range; }
            set { range = value; }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < towerLevel; ++i)
            {
                spriteBatch.Draw(sprite, new Rectangle((int)(GlobalPosition.X + center.X - sprite.Width / 2), (int)(GlobalPosition.Y + gameGrid.CellHeight - sprite.Height - i * 4), sprite.Width, sprite.Height), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!(this is PoisonTower))
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (destinationEnemy == null || (destinationEnemy != null && destinationEnemy.Health < 0) || ((this is IceTower) && destinationEnemy.isFrozen) || (destinationEnemy != null && Vector2.Distance(GlobalPosition, destinationEnemy.GlobalPosition) > range))
                {
                    destinationEnemy = PlayingState.GetClosestEnemy(GlobalPosition, range);
                }
                if (timer > attackDelay && destinationEnemy != null)
                {
                    timer = 0;
                    if (this is FireTower) FireProjectileAt(destinationEnemy, new Fireball(enemyList, (float) (0.5f * Math.Pow(1 + (1f / (0.5f * towerLevel + 1)), this.towerLevel))));
                    else if (this is IceTower) FireProjectileAt(destinationEnemy, new Icicle(enemyList, (float) (0.9f / Math.Pow(1 + (1f / (0.5f * towerLevel + 1)), this.towerLevel)), (float) (60 * Math.Pow(1 + (1f / (0.5f * towerLevel + 1)), this.towerLevel))));
                    else if (!(this is PoisonTower)) FireProjectileAt(destinationEnemy, new Projectile());
                }
            }
        }

        public void LevelUp()
        {
            damage *= 1+(1f/(0.5f * towerLevel + 1));
            range *= 1+(1f/(0.5f * towerLevel + 1));
            attackDelay /= 1+(1f/(0.5f * towerLevel + 1));
            towerLevel++;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if(inputHelper.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F))
            {
                FireProjectileAt(PlayingState.GetClosestEnemy(GlobalPosition), new Projectile());
            }
            base.HandleInput(inputHelper);
        }    }
}
