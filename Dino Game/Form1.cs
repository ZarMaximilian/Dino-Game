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

        // Array zum Handeln der Eigenschaften (x- und y-Koordinate, Breite und Höhe und Nummer) aller Wolken
        // @maxi: zum Nachlesen --> https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/arrays/
        // das Array wolken_data soll wiederum Arrays mit den Daten der einzelnen Wolken enthalten
        // @maxi: zum Nachlesen --> https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/arrays/jagged-arrays
        int[][] wolken_data = new int[100][];
       
        
        // mit der Variablen counter_keine_wolke wird gezählt wie lange schon pro Durchgang keine Wolke mehr erzeugt wurde
        private int counter_keine_wolke;
        // die Dauer bis die nächste Wolke kommen soll, wird zufällig bestimmt
        private int dauer_bis_nächste_wolke;
        // Anzahl aller Vorhandenen Wolken
        private int anzahl_wolken;

        public canvas()
        {
            InitializeComponent();

            anzahl_wolken = 0;
            counter_keine_wolke = 0;

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
                sneak = false;
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


            if (counter_keine_wolke < dauer_bis_nächste_wolke)
            {
                counter_keine_wolke++;
            }
            else
            {
                Erzeuge_Wolke();
                counter_keine_wolke = 0;
            }


            int anzahl = anzahl_wolken;
            // Pro Durchlauf werden alle x-Koordinaten aller Wolken um 20 nach links verschoben
            for (int i=0; i< anzahl; i++)
            {
                int [] aktuelleWolke = wolken_data[i];

                // Wenn die Wolke nicht mehr sichtbar ist, wird sie gelöscht
                if (aktuelleWolke[0] < -300)
                {
                    Lösche_Wolke(i);
                    anzahl--;
                }
                else
                    aktuelleWolke[0] -= 20;

            }
            

            

            // Aktualisiert die Oberfläche
            Invalidate();
        }

        // Methode zum Erzeugen der Wolken
        private void Erzeuge_Wolke()
        {
            // Es wird zufällig eine der 6 Wolken ausgewählt
            rng = new Random();
            int nummer_wolke = rng.Next(1, 6);

            int w_x = 1400;
            int w_y = rng.Next(0, 100);
            int w_breite = rng.Next(100, 200);
            int w_hoehe = (2* w_breite)/3;

            // Ein Array mit Daten der aktuellen Wolke wird zum Array wolken_data hinzugefügt
            wolken_data[anzahl_wolken] = new int[] { w_x, w_y, w_breite, w_hoehe, nummer_wolke };

            
            anzahl_wolken++;
            dauer_bis_nächste_wolke = rng.Next(10, 50);
            Console.WriteLine(anzahl_wolken);
            Console.WriteLine(dauer_bis_nächste_wolke);

        }

        // Methode zum Löschen einer Wolke
        private void Lösche_Wolke(int index)
        {
            if (anzahl_wolken > 1)
            {
                // die hinterste Wolke kommt an die erste Stelle
                int[] letzte_Wolke = wolken_data[anzahl_wolken-1];

                // Wo sich die zulöschende Wolke sich befunden hat, ist jetzt die letzte Wolke
                wolken_data[index] = letzte_Wolke;

                // Der Ort, an dem die letzte Wolke, wird auf null gesetzt
                wolken_data[anzahl_wolken-1] = null;

                anzahl_wolken--;
            }           
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



        // diese Methode wird nach dem Verschieben der Elemente aufgerufen
        // Verschoben werden die Elemente in der Methode 'Timer1_Tick'

        /*
         * @maxi:   die Methode Canvas_Paint wird über 'Invalidate()' von der 'Timer1_Tick'-Methode aufgerufen
         *          in der Datei 'Form1.Designer.cs' siehst du die Methode
         *             -->  'this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint)'
         *          die wird mit 'Invalidate()' aufgerufen                  
         */

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            
            // blaue Hintergrund
            e.Graphics.FillRectangle(Brushes.LightSkyBlue, 0, 0, 1400, 600);

            // Mensch (Heimisch)
            //e.Graphics.FillRectangle(Brushes.Black, _x, (float)_y, breite, höhe);
            Bitmap heimisch = new Bitmap("Heimisch.png");
            e.Graphics.DrawImage(heimisch, _x, (float)_y, breite, höhe);
            // Kakteen (Hindernisse)
            e.Graphics.FillRectangle(Brushes.Transparent, _xobst , _yobst, hitboxbreite, hitboxhöhe );
            e.Graphics.DrawImage(new Bitmap("Bild.png"), _xobst, _yobst, _obstbreite,_obsthöhe);

            for (int i = 0; i < anzahl_wolken; i++)
            {
                int[] aktuelle_wolke = wolken_data[i];
                int x_wolke = aktuelle_wolke[0];
                int y_wolke = aktuelle_wolke[1];
                int breite = aktuelle_wolke[2];
                int hoehe = aktuelle_wolke[3];
                int nummer_wolke = aktuelle_wolke[4];

                string wolke_name;
                // weist den entsprechenden Dateinamen der Wolke hinzu
                switch (nummer_wolke)
                {
                    case 1:
                        wolke_name = "wolke1.png";
                        break;

                    case 2:
                        wolke_name = "wolke2.png";
                        break;

                    case 3:
                        wolke_name = "wolke3.png";
                        break;

                    case 4:
                        wolke_name = "wolke4.png";
                        break;

                    case 5:
                        wolke_name = "wolke5.png";
                        break;

                    case 6:
                        wolke_name = "wolke6.png";
                        break;

                    default:
                        wolke_name = "";
                        break;
                }

                // Die aktuelle Wolke wird gezeichnet
                e.Graphics.DrawImage(new Bitmap(wolke_name), x_wolke, y_wolke, breite, hoehe);
            }
            // Testwolke
            //e.Graphics.DrawImage(new Bitmap("wolke1.png"), 1000, 200, 300, 200);
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
    // chrissi stinkt ! Nicht!



   
}
