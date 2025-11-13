using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Player
    {
        private enum MoveState
        {
            None,
            Right,
            Left,
            Up,
            Down
        }

        public Position Position { get; private set; }
        public float Rotation { get; private set; }
        public Animation Animation { get; private set; }

        public int Lives = 3;

        private int iFrames = 0;

        private readonly float speed = 0.1f;

        private MoveState moveState = MoveState.None;

        private Position target = new(1, 1);

        private bool isMoving = false;

        
        public Player(Position startPosition)
        {
            Position = startPosition;
            Animation = new(TextureHandler.Player, new int[] { 15, 15 }, Position.Raw);
        }

        public void Update(Map map, List<Position> food, List<Enemy> enemies)
        {
            UpdatePosition(Keyboard.GetState(), map, food, enemies);
            Animation.Update(Position.Raw + new Vector2(TextureHandler.Player.Width / 4, TextureHandler.Player.Height / 2));
        }

        private void UpdatePosition(KeyboardState ks, Map map, List<Position> food, List<Enemy> enemies)
        {
            iFrames--;
            Rotation = 0;
            if (moveState == MoveState.Down)
            {
                Rotation = MathF.PI * 0.5f;
            }
            else if (moveState == MoveState.Left)
            {
                Rotation = MathF.PI;
            }
            else if (moveState == MoveState.Up)
            {
                Rotation = MathF.PI * 1.5f;
            }

            if (ks.IsKeyDown(Keys.W))
            {
                if (map[target + new Position(0, -1)].Walkable)
                {
                    moveState = MoveState.Up;
                }
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                if (map[target + new Position(0, 1)].Walkable)
                {
                    moveState = MoveState.Down;
                }
            }
            else if (ks.IsKeyDown(Keys.A))
            {
                if (map[target + new Position(-1, 0)].Walkable)
                {
                    moveState = MoveState.Left;
                }
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                if (map[target + new Position(1, 0)].Walkable)
                {
                    moveState = MoveState.Right;
                }
            }

            if (!isMoving)
            {
                Position direction = Position.Zero;

                if (moveState == MoveState.Left && map[Position + new Position(-1, 0)].Walkable)
                    direction = new Position(-1, 0);
                else if (moveState == MoveState.Right && map[Position + new Position(1, 0)].Walkable)
                    direction = new Position(1, 0);
                else if (moveState == MoveState.Up && map[Position + new Position(0, -1)].Walkable)
                    direction = new Position(0, -1);
                else if (moveState == MoveState.Down && map[Position + new Position(0, 1)].Walkable)
                    direction = new Position(0, 1);

                if (direction != Position.Zero)
                {
                    target = (Position + direction).Rounded;
                    isMoving = true;
                }
            }
            else
            {
                Position += Position.DirectionTowards(target) * speed;

                if ((Position - target).Vector.Length() < speed)
                {
                    Position = target;
                    isMoving = false;
                }
            }

            for (int i = 0; i < food.Count; i++)
            {
                if (food[i] != null && Position.Equals(food[i]))
                {
                    food.Remove(food[i]);
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (Position.Equals(enemies[i].Position) && iFrames < 0)
                {
                    Lives--;
                    iFrames = 150;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            Animation.Draw(sb, Rotation);
        }
    }
}