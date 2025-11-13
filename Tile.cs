using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Pacman
{
    public class Tile
    {
        private Position Position { get; }
        public TileType Type { get; private set; }
        public bool Walkable { get { return Type == TileType.Walkable; } }
        public enum TileType
        {
            Walkable,
            NonWalkable
        }

        public Tile(TileType type, Position position)
        {
            Type = type;
            Position = position;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureHandler.Tiles[(int)Type], Position.Raw, Color.White);
        }
    }
}