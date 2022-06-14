using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    public enum PlayerClass
    {
        Human,
        Computer
    }

    public class Player
    {
        public int x;
        public int y;
        public int winLine;
        public uint walls = 10;
        public bool movable = false;
        public Color color;
        public PlayerClass playerClass;
        private int initX, initY;

        public List<Point> moves;

        public Player(int x, int y, int line, Color color, PlayerClass playerClass)
        {
            this.x = initX = x;
            this.y = initY = y;
            winLine = line;
            this.color = color;
            this.playerClass = playerClass;
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
