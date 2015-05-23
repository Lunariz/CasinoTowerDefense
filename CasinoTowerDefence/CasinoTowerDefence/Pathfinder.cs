using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CasinoTowerDefence
{
    public class Pathfinder
    {
        GameObjectGrid grid;
        Dictionary<Point, Point> cameFrom = new Dictionary<Point,Point>();

        public Pathfinder(GameObjectGrid grid)
        {
            this.grid = grid;
        }

        // Returns an array of points which describe the grid points to follow.
        // Returns null if no path is possible.
        public Point[] Findpath(Point from, Point to)
        {
            if (grid.Get(from.X, from.Y) != null)
                return null;

            HashSet<Point> closedSet = new HashSet<Point>();
            HashSet<Point> openSet = new HashSet<Point>();
            openSet.Add(from);
            cameFrom.Clear();

            int[,] g_score = new int[grid.Objects.GetLength(0), grid.Objects.GetLength(1)];
            int[,] f_score = new int[grid.Objects.GetLength(0), grid.Objects.GetLength(1)];

            g_score[from.X, from.Y] = 0;
            f_score[from.X, from.Y] = g_score[from.X, from.Y] + HeuristicEstimate(from, to);

            while (openSet.Count > 0)
            {
                Point smallest = new Point(-1, -1);
                foreach (Point p in openSet)
                {
                    if (smallest.X == -1) // SUPAR HACKY
                        smallest = p;
                    if (f_score[p.X, p.Y] < f_score[smallest.X, smallest.Y])
                    {
                        smallest = p;
                    }
                }

                if (smallest == to)
                    return ReconstructPath(from, to);

                openSet.Remove(smallest);
                closedSet.Add(smallest);

                foreach (Point neigh in NeighbourNodes(smallest))
                {
                    if (closedSet.Contains(neigh))
                        continue;

                    int tentScore = g_score[smallest.X, smallest.Y] + 1;

                    if (!openSet.Contains(neigh) || tentScore < g_score[neigh.X, neigh.Y])
                    {
                        cameFrom.Remove(neigh);
                        cameFrom.Add(neigh, smallest);
                        g_score[neigh.X, neigh.Y] = tentScore;
                        f_score[neigh.X, neigh.Y] = g_score[neigh.X, neigh.Y] + HeuristicEstimate(neigh, to);
                        if (!openSet.Contains(neigh))
                            openSet.Add(neigh);
                    }
                }
            }

            return null;
        }

        public int HeuristicEstimate(Point from, Point to)
        {
            return Math.Abs(to.X - from.X) + Math.Abs(to.Y - from.Y);
        }

        public Point[] NeighbourNodes(Point p)
        {
            List<Point> neighs = new List<Point>();

            if (p.X > 0 && grid.Get(p.X - 1, p.Y) == null)
                neighs.Add(new Point(p.X - 1, p.Y));
            if (p.X < grid.Objects.GetLength(0) - 1 && grid.Get(p.X + 1, p.Y) == null)
                neighs.Add(new Point(p.X + 1, p.Y));
            if (p.Y > 0 && grid.Get(p.X, p.Y - 1) == null)
                neighs.Add(new Point(p.X, p.Y - 1));
            if (p.Y < grid.Objects.GetLength(1) - 1 && grid.Get(p.X, p.Y + 1) == null)
                neighs.Add(new Point(p.X, p.Y + 1));

            return neighs.ToArray();
        }

        public Point[] ReconstructPath(Point from, Point current)
        {
            List<Point> path = new List<Point>();
            path.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }
            path.Reverse();
            return path.ToArray();
        }

        public bool IsPathValid(Point[] points)
        {
            if (points == null)
                return false;
            foreach(Point point in points)
            {
                if (grid.Get(point.X, point.Y) != null)
                    return false;
            }
            return true;
        }
    }
}
