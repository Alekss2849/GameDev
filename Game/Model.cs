using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    public class Model
    {
        private char[,] mainField = new char[17, 17];
        private Player[] players;
        private Point[] walls = new Point[20];
        private int wallsSize = 0;

        public Player curPlayer;

        public Model(Player []players)
        {
            this.players = players;
            curPlayer = players[0];
            switchPlayer();
            updatePossibleMoves();
        }

        private bool findWay(Player player)
        {
            Queue<Point> q = new Queue<Point>();
            char[,] checkField = (char[,])mainField.Clone();
            q.Enqueue(player.getPoint());
            checkField[player.y, player.x] = (char)1;
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };
            while (q.Count != 0)
            {
                Point cur = q.Dequeue();
                if (cur.Y == player.winLine)
                {
                    return true;
                }
                for (int k = 0; k < 4; k++)
                {
                    if (cur.Y + dy[k] >= 0 && cur.Y + dy[k] <= 16
                        && cur.X + dx[k] >= 0 && cur.X + dx[k] <= 16
                        && checkField[cur.Y + dy[k], cur.X + dx[k]] == 0
                        && checkField[cur.Y + dy[k] * 2, cur.X + dx[k] * 2] != 1)
                    {
                        checkField[cur.Y + dy[k] * 2, cur.X + dx[k] * 2] = (char)1;
                        q.Enqueue(new Point(cur.X + dx[k] * 2, cur.Y + +dy[k] * 2));
                    }
                }
            }
            return false;
        }

        public void getPossibleMoves(Player player)
        {
            Player oponent = player == players[0] ? players[1] : players[0];
            player.moves.Clear();
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };
            for (int k = 0; k < 4; k++)
            {
                if (player.x + dx[k] >= 0 && player.x + dx[k] <= 16 &&
                    player.y + dy[k] >= 0 && player.y + dy[k] <= 16 &&
                    mainField[player.y + dy[k], player.x + dx[k]] == 0)
                {
                    if (player.y + dy[k]*2 == oponent.y && player.x + dx[k] * 2 == oponent.x)
                    {
                        if (oponent.x + dx[k] > 0 && oponent.x + dx[k] < 16 &&
                            oponent.y + dy[k] > 0 && oponent.y + dy[k] < 16 &&
                            mainField[oponent.y + dy[k], oponent.x + dx[k]] == 0)
                        {
                            player.moves.Add(new Point(player.x + dx[k]*4, player.y + dy[k] * 4));
                        }
                        else
                        {   
                            continue;
                            if (oponent.y > 0 && mainField[oponent.y - 1, oponent.x] == 0)
                            {
                                player.moves.Add(new Point(player.x - 2, player.y - 2));
                            }
                            if (oponent.y < 16 && mainField[oponent.y + 1, oponent.x] == 0)
                            {
                                player.moves.Add(new Point(player.x - 2, player.y + 2));
                            }
                        }
                    }
                    else
                    {
                        player.moves.Add(new Point(player.x + dx[k]*2, player.y + dy[k]*2));
                    }
                }
            }
        }

        private void fillWallCell(char ch, char dir, int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (dir == 'v') mainField[y + i, x] = ch;
                else mainField[y, x + i] = ch;
            }
        }

        public bool placeWall(Point coords)
        {
            int x = coords.X, y = coords.Y;
            if (x % 2 == 1 && y % 2 == 0 && y < 16 &&
                mainField[y, x] == 0 && mainField[y + 1, x] == 0 && mainField[y + 2, x] == 0)
            {
                fillWallCell('#', 'v', x, y);
                if (!checkAllWays())
                {
                    fillWallCell('\0', 'v', x, y);
                    return false;
                }
                walls[wallsSize++] = new Point(x, y);
                return true;
            }
            if (x % 2 == 0 && y % 2 == 1 && x < 16 &&
                mainField[y, x] == 0 && mainField[y, x + 1] == 0 && mainField[y, x + 2] == 0)
            {
                fillWallCell('#', 'h', x, y);
                if (!checkAllWays())
                {
                    fillWallCell('\0', 'h', x, y);
                    return false;
                }
                walls[wallsSize++] = new Point(x, y);
                return true;
            }
            return false;
        }

        private bool checkAllWays()
        {
            foreach (var player in players)
            {
                if (!findWay(player))
                {
                    return false;
                }
            }
            return true;
        }

        private void switchPlayer()
        {
            curPlayer.movable = false;
            curPlayer = curPlayer == players[0] ? players[1] : players[0];
            curPlayer.movable = true;
            updatePossibleMoves();
        }

        public void updatePossibleMoves()
        {
            foreach (var player in players)
            {
                getPossibleMoves(player);
            }
        }

        public bool moveAction(Point moveCoor)
        {
            foreach (var coor in curPlayer.moves)
            {
                if (coor.Equals(moveCoor))
                {
                    curPlayer.x = moveCoor.X;
                    curPlayer.y = moveCoor.Y;
                    switchPlayer();
                    //updatePossibleMoves();
                    return true;
                }
            }
            return false;
        }
    }
}
