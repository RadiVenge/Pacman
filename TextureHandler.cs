using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    public static class TextureHandler
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Batman { get; private set; }
        public static Texture2D Michael { get; private set; }
        public static Texture2D Denzel { get; private set; }
        public static Texture2D Food { get; private set; }
        private static Texture2D Walkable { get; set; }
        private static Texture2D NonWalkable { get; set; }

        public static Texture2D[] Tiles { get { return new Texture2D[] 
        { 
            Walkable,
            NonWalkable,
        }; } }

        public static void LoadTextures(ContentManager content)
        {
            Player = content.Load<Texture2D>("resources\\Player");
            Walkable = content.Load<Texture2D>("resources\\Walkable");
            NonWalkable = content.Load<Texture2D>("resources\\NonWalkable");
            Batman = content.Load<Texture2D>("resources\\Batman");
            Denzel = content.Load<Texture2D>("resources\\Denzel");
            Michael = content.Load<Texture2D>("resources\\Michael");
            Food = content.Load<Texture2D>("resources\\Food");
        }
    }
}
