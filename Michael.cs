using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Michael : Enemy
    {
        public override string Name => "Michael";

        public override Vector2 Pathfind(Player player, Map map) // Looks 4 tiles to where the player is walking, counting down towards 0 when those tiles are unavailible
        {
            Vector2 result = Vector2.Zero;
            for(int i = 4; i > -1; i--)
            {
                try
                {
                    result = AngleToVector(player.Rotation) * i + player.Position.Vector;
                    if (map[result].Walkable)
                    {
                        break;
                    }
                }
                catch (Exception) // When the position it want to look at is outside the map it will return an exception. This is to simply not bother with preventing it, since im just going to continue if thats the case anyways.
                {
                    continue;
                }
            }

            return result;
        }

        private Vector2 AngleToVector(float angle)
        {
            return new Vector2(MathF.Cos(angle), -MathF.Sin(angle));
        }
        public Michael(Position position) : base(position)
        {
            Texture = TextureHandler.Michael;
        }
    }
}
