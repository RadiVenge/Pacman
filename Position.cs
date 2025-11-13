using Microsoft.Xna.Framework;
using System;

namespace Pacman
{
    public class Position // Chose to use my own class for position since it allows me to freely switch between 48x27 and 1920x1080 since 48x27 is easier for the game logic, but when using "draw" I need to use the full resolution
    {
        public Vector2 Raw { get { return new Vector2(MathF.Round(X * 40), MathF.Round(Y * 40)); } } // The raw coordinates, aka in default resolution (1920x1080)
        public Position Rounded { get { return new(MathF.Round(X), MathF.Round(Y)); } }
        public static Position Zero { get { return new(0); } }

        public Vector2 Vector { get {  return new Vector2(X, Y); } }
        public float X { get; set; }
        public float Y { get; set; }

        public Position(float xy)
        {
            X = xy;
            Y = xy;
        }
        public Position(float x, float y)
        {
            X = x; 
            Y = y;
        }

        public static Position operator +(Position pos, Position other)
        {
            return new Position(pos.X + other.X, pos.Y + other.Y);
        }

        public static Position operator -(Position pos, Position other)
        {
            return new Position(pos.X - other.X, pos.Y - other.Y);
        }

        public static Position operator *(Position pos, float f)
        {
            return new Position(pos.X * f, pos.Y * f);
        }

        public static Position FromVector(Vector2 vector)
        {
            return new Position(vector.X, vector.Y);
        }

        public bool Equals(Position other)
        {
            return Vector2.Distance(Vector, other.Vector) < 0.01f;
        }

        public Position DirectionTowards(Position other)
        {
            return new(X < other.X ? 1 : X > other.X ? -1 : 0, Y < other.Y ? 1 : Y > other.Y ? -1 : 0);
        }
    }
}
