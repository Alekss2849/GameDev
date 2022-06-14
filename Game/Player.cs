using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    public class Player
    {
        public int x;
        public int y;
        public int winLine;
        public uint walls = 10;
        public bool movable = true;
        public Color color;

        public List<Point> moves;

        public Player(int x, int y, int line, Color color)
        {
            this.x = x;
            this.y = y;
            winLine = line;
            this.color = color;
            moves = new List<Point>();
        }

        public Point getPoint()
        {
            return new Point(x, y);
        }
    }
}
