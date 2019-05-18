
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dino_Game
{
    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new canvas());
        }

    }

    public class Obstacles : Form
    {
        public int x;
        public int y;

        public enum bewegung
        {
            Links
        }
        public float speed;
        public int obstspeed;
        private int breite, höhe;
        private canvas canvas;

        public Obstacles(int _x, int _y, int breite, int höhe)
        {
            canvas canvas = new canvas();
            x = _x;
            y = _y;
            breite = this.breite;
            höhe = this.höhe;
            

        }



    }
}