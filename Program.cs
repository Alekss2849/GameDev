using System;
using System.Collections.Generic;

namespace Quoridor
{
    class Coords
    {
        public int x, y;

        public Coords(int x, int y)
        {
           setCoords(x,y); 
        }

        public void setCoords(int x, int y)
        {
            this.y = y;
            this.x = x;
        }
    }

    class Model
    {
        private char[,] mainField = new char[17,17];
        private Coords player1, player2;
        private Coords curPlayer;

        public Model()
        {
            player1 = new Coords(8, 16);
            player2 = new Coords(8, 0);
            curPlayer = player1;
        }

        private bool findWay(Coords player, int line)
        {
            for (int i = 0; i < 15; i++)
            {
                mainField[15, i] = (char)3;
            }
            Queue<Coords> q = new Queue<Coords>();
            char[,] checkField = (char[,])mainField.Clone();
            q.Enqueue(player);
            checkField[player.y, player.x] = (char)1;
            while (q.Count != 0)
            {
                
                Coords cur = q.Dequeue();
                Console.WriteLine(cur.x + " " + cur.y);
                if (cur.y == line)
                {
                    printField(checkField);
                    return true;
                    
                }
                if (cur.y > 0 && checkField[cur.y - 1, cur.x] == 0
                              && checkField[cur.y - 2, cur.x] != 1)
                {
                    checkField[cur.y - 2, cur.x] = (char)1;
                    q.Enqueue(new Coords(cur.x, cur.y - 2));
                }
                if (cur.y < 16 && checkField[cur.y + 1, cur.x] == 0
                               && checkField[cur.y + 2, cur.x] != 1)
                {
                    checkField[cur.y + 2, cur.x] = (char)1;
                    q.Enqueue(new Coords(cur.x, cur.y + 2));
                }
                if (cur.x > 0 && checkField[cur.y, cur.x-1] == 0
                              && checkField[cur.y, cur.x-2] != 1)
                {
                    checkField[cur.y, cur.x-2] = (char)1;
                    q.Enqueue(new Coords(cur.x-2, cur.y));
                }
                if (cur.x < 16 && checkField[cur.y, cur.x + 1] == 0
                               && checkField[cur.y, cur.x + 2] != 1)
                {
                    checkField[cur.y, cur.x + 2] = (char)1;
                    q.Enqueue(new Coords(cur.x + 2, cur.y));
                }
            }
            printField(checkField);
            return false;
        }

        private bool isNotCheckedCell(char[,] field)
        {
            for (int i = 0; i < 17; i+=2)
            {
                for (int j = 0; j < 17; j+=2)
                {
                    if (field[i, j] == 0) return true;
                }
            }
            return false;
        }

        public bool move(int x, int y)
        {
            if (x % 2 == 1 || y % 2 == 1) return false;
            if (mainField[(y + curPlayer.y) / 2, (x + curPlayer.x) / 2] == 0)
            {
                curPlayer.setCoords(x,y);
                curPlayer = curPlayer == player1 ? player2 : player1;
                return true;
            }
            return false;
        }

        public void printField(char[,] field = null)
        {
            if (field == null)
            {
                field = mainField;
            }
            Console.WriteLine();
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if(i == player1.y && j == player1.x)
                        Console.Write("G ");
                    else if (i == player2.y && j == player2.x)
                        Console.Write("R ");
                    else
                        Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public bool placeWall(int x, int y)
        {
            return false;
        }


        static void Main(string[] args)
        {
            Model model = new Model();
            model.move(8, 14);
            model.move(5, 4);
            model.printField();
            if (model.findWay(new Coords(4, 4), 16))
            {
                Console.WriteLine("Was Found!");
            }
            else
            {
                Console.WriteLine("No way!");
            }
           
        }
    }
}


/*
    . #
    . #
    0 


    */