using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pacman
{
    public static class AStar
    {
        public static List<Vector2> FindPath(Vector2 start, Vector2 goal, Map map)
        {
            List<AStarTile> openSet = new List<AStarTile>();
            HashSet<Vector2> closedSet = new HashSet<Vector2>();
            openSet.Add(new AStarTile(null, Vector2.Distance(start, goal), start));

            while (openSet.Count > 0)
            {
                AStarTile current = LowestF(openSet);
                if (current == null)
                    break;

                if (SamePosition(current.Position, goal))
                    return ReconstructPath(current);

                openSet.Remove(current);
                closedSet.Add(current.Position);

                foreach (Vector2 dir in new[] {
                    new Vector2(0, -1), new Vector2(0, 1),
                    new Vector2(-1, 0), new Vector2(1, 0)
                })
                {
                    Vector2 neighborPos = current.Position + dir;

                    if (!map.IsWithinBounds(neighborPos) || !map[neighborPos].Walkable || closedSet.Contains(neighborPos))
                        continue;

                    AStarTile existing = openSet.Find(t => SamePosition(t.Position, neighborPos));
                    float tentativeG = current.G + 1;

                    if (existing == null)
                    {
                        openSet.Add(new AStarTile(current, Vector2.Distance(neighborPos, goal), neighborPos));
                    }
                    else if (tentativeG < existing.G)
                    {
                        existing.SetG(tentativeG);
                        existing.SetParent(current);
                    }
                }
            }

            return new List<Vector2>();
        }

        private static List<Vector2> ReconstructPath(AStarTile endTile)
        {
            List<Vector2> path = new List<Vector2>();
            AStarTile current = endTile;
            while (current.Parent != null)
            {
                path.Add(current.Position);
                current = current.Parent;
            }
            path.Reverse();
            return path;
        }

        private static AStarTile LowestF(List<AStarTile> tiles)
        {
            return tiles.OrderBy(t => t.F).ThenBy(t => t.G).FirstOrDefault();
        }

        private static bool SamePosition(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b) < 0.01f;
        }
    }
}