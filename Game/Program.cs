using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Player[] players = {
                new Player(8, 0, 16, Color.Red),
                new Player(8, 16, 0, Color.Green)
            };
            View view = new View(players);
            Controller controller = new Controller(view, new Model(players));
            Application.Run(view);
        }
    }
}
