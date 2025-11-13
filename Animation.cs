using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Animation
    {
        public bool Play { get; set; }
        public int Frame { get; private set; }

        private int MaxFrames;
        private Texture2D SpriteSheet;
        private int[] Interval;
        private int timer;

        private Vector2 Position;

        public Animation(Texture2D spriteSheet, int[] interval, Vector2 position)
        {
            SpriteSheet = spriteSheet;
            Interval = interval;
            timer = interval[0];
            MaxFrames = interval.Length;
            Position = position;
            Play = false;
        }

        public void Update(Vector2 position)
        {
            timer--;

            if (timer <= 0)
            {
                Frame = (Frame + 1) % MaxFrames;
                timer = Interval[Frame];
            }

            Position = position;
        }

        public void Draw(SpriteBatch sb, float rotation)
        {
            if (Play)
            {
                sb.Draw(SpriteSheet, Position, new Rectangle(SpriteSheet.Width / MaxFrames * Frame, 0, SpriteSheet.Width / MaxFrames, SpriteSheet.Height), Color.White, rotation, new Vector2(SpriteSheet.Width / (MaxFrames * 2), SpriteSheet.Height / 2), 1, SpriteEffects.None, 0);
            }
        }
    }
}
