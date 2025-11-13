using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Denzel : Enemy
    {
        public override string Name => "Denzel";

        public override Vector2 Pathfind(Player player, Map map) // Same as Michael, but 4 tiles behind
        {
            Vector2 result = Vector2.Zero;
            for (int i = 4; i > -1; i--)
            {
                try
                {
                    result = AngleToVector(player.Rotation) * -i + player.Position.Vector;
                    if (map[result].Walkable)
                    {
                        break;
                    }
                }
                catch (Exception) 
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
        public Denzel(Position position) : base(position)
        {
            Texture = TextureHandler.Denzel;
        }
    }
}
