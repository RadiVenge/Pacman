using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pacman
{
    public abstract class Enemy
    {
        public abstract string Name { get; }
        public abstract Vector2 Pathfind(Player player, Map map);
        public Position Position { get; private set; }
        public bool Scared { get; set; }
        public Texture2D Texture { get; set; }

        public Enemy(Position position)
        {
            Position = position;
        }

        private bool targetReached = false;
        private Position target;
        private readonly float speed = 0.05f;

        public void Update(Player player, Map map)
        {
            if (targetReached || target == null)
            {
                Vector2 destination = Pathfind(player, map);
                List<Vector2> path = AStar.FindPath(Position.Vector, destination, map);

                if (path.Count > 0)
                {
                    Vector2 nextStep = path[0];
                    if (Vector2.Distance(nextStep, Position.Vector) < 0.01f && path.Count > 1)
                        nextStep = path[1];

                    target = Position.FromVector(nextStep);
                    targetReached = false;
                }
                else
                {
                    targetReached = true;
                    return;
                }
                
            }

            Position direction = Position.DirectionTowards(target);
            Position += direction * speed;

            if ((Position - target).Vector.Length() < speed)
            {
                Position = target;
                targetReached = true;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position.Raw, Color.White);
        }
    }
}
