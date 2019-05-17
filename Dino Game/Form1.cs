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

        public canvas()
        {
            InitializeComponent();

            _x = 200;
            _y = 400;
            _objPostition = positon.Down;

        }


       


        

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up && _y >=410)
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
            if (_objPostition == positon.Up)
            {
              
                
                while(_y >= 200)
                {
                    _y -= 20;
                }
                _objPostition = positon.Down;

            }
            if(_objPostition == positon.Down)
            {

                _y += 20;
            }

            if (sneak == true)
            {
                höhe = 100;
                _y = _y + 50;

            }
            else
                höhe = 150;
            Invalidate();
        }

        private int breite = 80;
        private int höhe = 150;
        private Obstacles obst;
        private int _xobst= 1350;

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            
            
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, 0, 0, 1400, 600);
            e.Graphics.FillRectangle(Brushes.Black, _x, (float)_y, breite, höhe);
            e.Graphics.FillRectangle(Brushes.Black, _xobst, 500, 50,100);


        }

        

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                sneak = false;
            }
        }

        
    }
    // chrissi stinkt !



   
}
