using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public partial class View : Form
    {
        private int Probel = 0,
                    Shirota = 0,
                    StoronaKvadrata = 0,
                    ShirotaStenki = 0;

        private Graphics gr;
        private Player[] players;
        private Player readyToMove = null;

        public delegate bool PlayerTurn(Point coords);
        public event PlayerTurn MoveAction;
        public event PlayerTurn WallAction;

        public View(Player[] players)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(-1600,100);
            this.players = players;
            newGame();
        }

        public void newGame()
        {
            Shirota = pictureBox1.Width;
            StoronaKvadrata = Shirota / 14;
            Probel = StoronaKvadrata / 2;
            pictureBox1.Image = new Bitmap(Shirota, Shirota);
            gr = Graphics.FromImage(pictureBox1.Image);
            gr.Clear(Color.FromArgb(192, 192, 255));
            refreshPlayers();
        }

        public void refreshPlayers()
        {
            GrafKvadratov();
            drawPlayers();
        }

        private Point getIndexesFromClick(MouseEventArgs me)
        {
            if (me.X < Probel || me.Y < Probel
                              || me.X > Shirota - Probel
                              || me.Y > Shirota - Probel)
                return new Point(-1,-1);
            int x = me.X / (StoronaKvadrata + Probel) * 2;
            if (me.X % (StoronaKvadrata + Probel) < Probel){x--;}
            int y = me.Y / (StoronaKvadrata + Probel) * 2;
            if (me.Y % (StoronaKvadrata + Probel) < Probel){y--;}
            return new Point(x,y);
        }

        private Point getCoordsFromIndexes(int col, int row)
        {
            int x = 0, y = 0;
            x = Probel + (Probel + StoronaKvadrata) * (col / 2) + (col % 2 == 0 ? 0 : StoronaKvadrata);
            y = Probel + (Probel + StoronaKvadrata) * (row / 2) + (row % 2 == 0 ? 0 : StoronaKvadrata);
            return new Point(x,y);
        }


        private void clickAnalizer(object sender, EventArgs e)
        {
            Point coords = getIndexesFromClick((MouseEventArgs)e);
            if (coords.X == -1 || coords.Y == -1)
            {
                return;
            }
            if (readyToMove != null)
            {
                MoveAction?.Invoke(coords);
                readyToMove = null;
                refreshPlayers();
                return;
            }
            foreach (var p in players)
            {
                if (p.movable && coords.X == p.x && coords.Y == p.y)
                {
                    drawPossibleMoves(p.moves);
                    readyToMove = p;
                    return;
                }
            }
            if (coords.X % 2 == 1 && coords.Y % 2 == 0)
            {
                if (WallAction != null && !WallAction.Invoke(coords)) return;
                StenkaVertik(coords.X, coords.Y);
                return;

            }
            if (coords.X % 2 == 0 && coords.Y % 2 == 1)
            {
                if (WallAction != null && !WallAction.Invoke(coords)) return;
                StenkaGorizont(coords.X, coords.Y);
                return;
            }
            label4.Text = coords.X + "; " + coords.Y;
        }

        void GrafKvadrat(Point pnt, Brush brush)
        {
            gr.FillRectangle(brush,
                pnt.X, pnt.Y, StoronaKvadrata, StoronaKvadrata);
        }

        void GrafKvadratov()
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    GrafKvadrat(getCoordsFromIndexes(i*2, j*2),
                        Brushes.White);
                }
            }
            pictureBox1.Refresh();
        }

        void drawPlayers()
        {
            foreach (var p in players)
            {
                drawPlayer(p);
            }
            pictureBox1.Refresh();
        }

        void drawPlayer(Player player)
        {
            int size = (int)(StoronaKvadrata * 0.8);
            int delta = (int) (StoronaKvadrata * 0.1);
            var coor = getCoordsFromIndexes(player.x, player.y);
            gr.FillEllipse(new SolidBrush(player.color),
                coor.X + delta, coor.Y+ delta,
                size, size);
        }

        void StenkaVertik(int x, int y, Brush brush = null)
        {
            if (brush == null) brush = Brushes.Gray;
            var coor = getCoordsFromIndexes(x, y);
            gr.FillRectangle(brush,
                coor.X, coor.Y,
                Probel,
                StoronaKvadrata * 2 + Probel
                );
            pictureBox1.Refresh();
        }

        void StenkaGorizont(int x, int y, Brush brush = null)
        {
            if (brush == null) brush = Brushes.Gray;
            gr.FillRectangle(brush,
                Probel + (StoronaKvadrata + Probel) * x / 2,
                (StoronaKvadrata + Probel) * (y / 2 + 1),
                StoronaKvadrata * 2 + Probel,
                Probel
            );
            pictureBox1.Refresh();
        }

        private void drawPossibleMoves(List<Point> cells)
        {
            foreach (var cell in cells)
            {
                GrafKvadrat(getCoordsFromIndexes(cell.X, cell.Y),
                Brushes.Beige);
            }
            pictureBox1.Refresh();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
