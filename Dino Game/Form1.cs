using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Dino_Game
{
    public partial class canvas : Form
    {
        enum positon
        {
            Up, Down, Nothing
        }
        private positon _objPostition;
        private int _x;
        private double _y;

        private int max_hoehe;

        public canvas()
        {
            InitializeComponent();

            _x = 200;
            _y = 400;
            _objPostition = positon.Down;
            max_hoehe = 200;

        }


       


        

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up && _y >= 410)
            {
                _objPostition = positon.Up;
            }

            if (e.KeyCode == Keys.W)
            {
                _objPostition = positon.Up;
            }

            if(e.KeyCode == Keys.Down)
            {
                sneak = true;
            }


        }


        private Boolean sneak = false;
        private Random rng;
      
        // Ticker der jede Sekunde durchläuft wird
        private void Timer1_Tick(object sender, EventArgs e)
        {
            
            _xobst -= 30;
            if(_xobst <= 0)
            {
                rng = new Random();
                _xobst = rng.Next(1400, 1700);
            }

            if (_y +höhe >= 550)
            {
                _y =400;
               
            }
            
            if (_x < _xobst +20 + hitboxbreite && _x + breite > _xobst +20 && _y < _yobst + hitboxhöhe && _y + höhe > _yobst)
            {
                Application.Exit();
                
            }

            // Mensch wird hochgesetzt
            if (_objPostition == positon.Up)
            {
                if (_y >= max_hoehe + 20)
                {
                    _y -= 30;
                }
                else
                {
                    _objPostition = positon.Down;
                }
                

            }

            // Mensch wird runtergesetzt
            if(_objPostition == positon.Down)
            {
                _y += 30;
            }

            if (sneak == true)
            {
                höhe = 100;
                _y = _y + 50;

            }
            else
                höhe = 150;

            // Aktualisiert die Oberfläche
            Invalidate();
        }



        private int breite = 80;
        private int höhe = 150;
        private Obstacles obst;
        private int _xobst= 1350;
        public Bitmap heimisch;
        private int _obstbreite = 100;
        private int _obsthöhe = 150;
        private int _yobst = 450;
        private int hitboxbreite = 80;
        private int hitboxhöhe = 130;


        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            
            // blaue Hintergrund
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, 0, 0, 1400, 600);

            // Mensch (Heimisch)
            //e.Graphics.FillRectangle(Brushes.Black, _x, (float)_y, breite, höhe);
            Bitmap heimisch = new Bitmap("Heimisch ausgeschnitten.png");
            e.Graphics.DrawImage(heimisch, _x, (float)_y, breite, höhe);
            // Kakteen (Hindernisse)
            e.Graphics.FillRectangle(Brushes.Transparent, _xobst , _yobst, hitboxbreite, hitboxhöhe );
            e.Graphics.DrawImage(new Bitmap("Bild.png"), _xobst, _yobst, _obstbreite,_obsthöhe);

        }

        

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                sneak = false;
            }
        }

        private void canvas_Load(object sender, EventArgs e)
        {

        }

        
    }
    // chrissi stinkt !



   
}
