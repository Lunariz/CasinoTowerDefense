using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CasinoTowerDefence
{
    static class EnemyUpdater
    {
        public static void updateEnemies(Pathfinder pathfinder, GameObjectList enemyList, DefaultPath defaultPath, Point spawn, Point end)
        {
            foreach (Enemy enemy in enemyList.Objects)
            {
                GameGrid gameGrid = enemy.gameGrid;

                Point currentPosition = new Point((int) Math.Max(0, Math.Round((enemy.Position.X - gameGrid.Position.X) / gameGrid.CellWidth - 0.5f)), (int) Math.Max(0, Math.Round((enemy.Position.Y - gameGrid.Position.Y) / gameGrid.CellHeight - 0.5f)));
                enemy.Move(pathfinder, pathfinder.Findpath(currentPosition, end));
            }
            Point[] path = pathfinder.Findpath(spawn, end);
            if (path != null && (defaultPath.path == null || path.Length < defaultPath.path.Length))
                defaultPath.path = path;
        }
    }
}
