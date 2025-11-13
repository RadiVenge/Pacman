using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pacman
{
    public class Map
    {
        private Tile[,] MapArray { get; set; }

        public Tile this[int y, int x] { get { return MapArray[y, x]; } }
        public Tile this[Position position] { get { return MapArray[(int)MathF.Round(position.Y), (int)MathF.Round(position.X)]; } }
        public Tile this[Vector2 vector] { get { return MapArray[(int)MathF.Round(vector.Y), (int)MathF.Round(vector.X)]; } }

        public readonly int X = 19;
        public readonly int Y = 21;

        public void LoadFromFile(string path, ref List<Enemy> enemies, ref Player player, ref List<Position> food)
        {
            MapArray = new Tile[Y, X];
            string text = File.ReadAllText(path);

            int[,] digits = new int[Y, X];

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    string letter = text.Split('\n')[y][x].ToString();

                    if (letter == "P")
                    {
                        player = new(new Position(x, y));
                        digits[y, x] = 0;
                    }
                    else if (letter == "B")
                    {
                        enemies.Add(new Batman(new Position(x, y)));
                        digits[y, x] = 0;
                    }
                    else if (letter == "M")
                    {
                        enemies.Add(new Michael(new Position(x, y)));
                        digits[y, x] = 0;
                    }
                    else if (letter == "D")
                    {
                        enemies.Add(new Denzel(new Position(x, y)));
                        digits[y, x] = 0;
                    }
                    else if (letter == "f")
                    {
                        food.Add(new Position(x, y));
                        digits[y, x] = 0;
                    }
                    else
                    {
                        digits[y, x] = Convert.ToInt32(letter); 
                    }
                }
            }

            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    MapArray[y, x] = new Tile((Tile.TileType)digits[y, x], new Position(x, y));
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    MapArray[y, x].Draw(sb);
                }
            }
        }

        public bool IsWithinBounds(Vector2 vector)
        {
            return vector.X >= 0 && vector.X < X && vector.Y >= 0 && vector.Y < Y;
        }
    }
}
