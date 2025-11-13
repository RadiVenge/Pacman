using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class AStarTile
    {
        public AStarTile Parent { get; private set; }
        public float G { get; private set; }
        public float H { get; private set; }
        public float F { get { return G + H; } }
        public Vector2 Position { get; private set; }

        public void SetParent(AStarTile parent) => Parent = parent;
        public void SetG(float g) => G = g;
        public void SetH(float h) => H = h;
        public void SetPosition(Vector2 position) => Position = position;

        public AStarTile(AStarTile? parent, float h, Vector2 position)
        {
            Parent = parent;
            G = parent == null ? 0: parent.G + 1;
            H = h;
            Position = position;
        }
    }
}
