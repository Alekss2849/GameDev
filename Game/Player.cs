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
        public bool movable = false;
        public Color color;
        private int initX, initY;

        public List<Point> moves;

        public Player(int x, int y, int line, Color color)
        {
            this.x = initX = x;
            this.y = initY = y;
            winLine = line;
            this.color = color;
            moves = new List<Point>();
        }

        public Point getPoint()
        {
            return new Point(x, y);
        }

        public void reset()
        {
            x = initX;
            y = initY;
            walls = 10;
            movable = false;
        }
    }
}
