using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Batman : Enemy
    {
        public override string Name => "Batman";
        public override Vector2 Pathfind(Player player, Map map) // Looks at the player
        {
            return player.Position.Vector;
        }
        public Batman(Position position) : base(position)
        {
            Texture = TextureHandler.Batman;
        }
    }
}