using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasinoTowerDefence
{
    class PlayerController : GameObject
    {
        GameGrid grid;
        LocationSelector locationSelector;
        SlotMachine slotMachine;
        Pathfinder pathfinder;
        GameObjectList enemyList;
        GameObjectList spellList;
        GameObjectList projectileList;
        Point spawn;
        Point end;
        DefaultPath defaultPath;
        GameObjectList effects;

        enum State
        {
            Selecting,
            Placing
        }

        State state;

        public PlayerController(Pathfinder pathfinder, GameObjectList enemyList, GameObjectList spellList, GameObjectList projectileList, GameGrid grid, DefaultPath defaultPath, Point spawn, Point end, GameObjectList effects)
            : base(100)
        {
            this.effects = effects;
            this.defaultPath = defaultPath;
            this.spawn = spawn;
            this.end = end;
            this.grid = grid;
            this.pathfinder = pathfinder;
            this.enemyList = enemyList;
            this.spellList = spellList;
            this.projectileList = projectileList;
            locationSelector = new LocationSelector(grid);
            slotMachine = new SlotMachine(new Vector2(1080, 400));
            GotoSelecting(1500);
        }

        void GotoSelecting(int speed)
        {
            state = State.Selecting;
            slotMachine.Reset(speed);
        }

        void GotoPlacing()
        {
            state = State.Placing;
            locationSelector.StartSelecting();
        }

        public override void Update(GameTime gameTime)
        {
            locationSelector.Update(gameTime);
            slotMachine.Update(gameTime);
            switch (state)
            {
                case State.Selecting:
                    if (slotMachine.SelectedAbility != Ability.Nothing)
                    {
                        GotoPlacing();
                    }
                    break;
                case State.Placing:
                    if (!locationSelector.IsBusy)
                    {
                        Place(locationSelector.Result, slotMachine.SelectedAbility);
                        GotoSelecting(2000);
                    }
                    break;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            locationSelector.HandleInput(inputHelper);
            slotMachine.HandleInput(inputHelper);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            locationSelector.Draw(gameTime, spriteBatch);
            slotMachine.Draw(gameTime, spriteBatch);
        }

        void Place(Point position, Ability ability)
        {
            switch (ability)
            {
                case Ability.Tower:
                    PlaceTower(position);
                    break;
                case Ability.Fire:
                    UseFire(position);
                    break;
                case Ability.Ice:
                    UseIce(position);
                    break;
                case Ability.Poison:
                    UsePoison(position);
                    break;
            }
        }

        void PlaceTower(Point position)
        {
            Tower tower = new TowerBasic(grid, enemyList, projectileList, 1);
            grid.Add(tower, position.X, position.Y);
            List<Enemy> toKill = new List<Enemy>();
            foreach (Enemy enemy in enemyList.Objects)
            {
                if (Math.Abs(grid.Position.X + (position.X + 0.5f) * grid.CellWidth - enemy.Position.X) <= grid.CellWidth && Math.Abs(grid.Position.Y + (position.Y + 0.5f) * grid.CellHeight - enemy.Position.Y) <= grid.CellHeight)
                {
                    toKill.Add(enemy);
                }
            }
            foreach (Enemy enemy in toKill)
            {
                enemy.Die();
            }
            EnemyUpdater.updateEnemies(pathfinder, enemyList, defaultPath, spawn, end);
        }

        void UseFire(Point position)
        {
            Tower tower = (Tower)grid.Get(position.X, position.Y);
            if (tower == null)
            {
                FireSpell fireSpell = new FireSpell(grid, new Vector2(position.X, position.Y), 120, enemyList, effects);
                spellList.Add(fireSpell);
            }
            else
            {
                if (tower is FireTower)
                    tower.LevelUp();
                else if (tower is TowerBasic)
                {
                    int level = tower.Level;
                    grid.Add(new FireTower(grid, enemyList, projectileList, level), position.X, position.Y);
                }
                else
                {
                    grid.Add(new FireTower(grid, enemyList, projectileList, 1), position.X, position.Y);
                }
            }
        }

        void UseIce(Point position)
        {
            Tower tower = (Tower)grid.Get(position.X, position.Y);
            if (tower == null)
            {
                IceSpell iceSpell = new IceSpell(grid, new Vector2(position.X, position.Y), 120, enemyList, effects);
                spellList.Add(iceSpell);
            }
            else
            {
                if (tower is IceTower)
                    tower.LevelUp();
                else if (tower is TowerBasic)
                {
                    int level = tower.Level;
                    grid.Add(new IceTower(grid, enemyList, projectileList, level), position.X, position.Y);
                }
                else
                {
                    grid.Add(new IceTower(grid, enemyList, projectileList, 1), position.X, position.Y);
                }
            }
        }

        void UsePoison(Point position)
        {
            Tower tower = (Tower)grid.Get(position.X, position.Y);
            if (tower == null)
            {
                PoisonSpell poisonSpell = new PoisonSpell(grid, new Vector2(position.X, position.Y), 120, enemyList, effects);
                spellList.Add(poisonSpell);
            }
            else
            {
                if (tower is PoisonTower)
                    tower.LevelUp();
                else if (tower is TowerBasic)
                {
                    int level = tower.Level;
                    grid.Add(new PoisonTower(grid, enemyList, projectileList, level), position.X, position.Y);
                }
                else
                {
                    grid.Add(new PoisonTower(grid, enemyList, projectileList, 1), position.X, position.Y);
                }
            }
        }
    }
}