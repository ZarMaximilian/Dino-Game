
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

        public Obstacles(int x, int y,int breite, int höhe)
        {
           
            
        }
    }
}
