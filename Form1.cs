using igra.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Timers;

namespace igra
{
    public partial class Form1 : Form
    {
        Stopwatch stopwatch = new Stopwatch();
        double lastTime = 0, currentTime, deltaTime;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 640);
            DoubleBuffered = true;
            this.BackgroundImage = Resources.background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;

            l = new Load();

            l.begin();

            //l.begin();

            stopwatch.Start();

            Application.Idle += gameLoop;
        }

        int map(int x, int in_min, int in_max, int out_min, int out_max) 
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        //private static System.Timers.Timer atimer;

        int x;
        int y;
        bool stanje;
        Load l;
        state s;
        Character C = new Character();
        int aState = 0;
        double aTimer = 0;
        bool moveLast = true;
        int pos = 400;
        //int jValue = 0;
        int jTimer = 0;

        int moveX = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            

            
        }
        
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string a = serialPort1.ReadLine();
            x = Convert.ToInt32(a.Substring(0, a.IndexOf(',')));
            y = Convert.ToInt32(a.Substring(a.IndexOf(','), a.LastIndexOf(',')));
            stanje = Convert.ToBoolean(a.Substring(a.LastIndexOf(','), a.Length - a.LastIndexOf(',')));

            if (x > 112) moveX = 3;
            else if (x > 96) moveX = 1;
            else if (x > 32) moveX = -1;
            else moveX = -3;

            if (y > 64 && jTimer == 0) jTimer++;
            //if (x > 64) label1.Location(label1.Location.X++, label1.Location.Y);
        }

        void jump() 
        {
            Rectangle newP = C.r;

            if (jTimer < 20)
            {
                newP.Y = newP.Y - 7;
                jTimer++;
            }
            else if (jTimer < 30) jTimer++;
            else if (jTimer < 49)
            {
                newP.Y = newP.Y + 7; //jcheck preuzeo
                jTimer++;
            }
            else jTimer = 0;
            C.r = newP;
            //this.Refresh();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(moveLast)e.Graphics.DrawImage(C.main_sprite[aState], C.r);
            else e.Graphics.DrawImage(C.MirrorImage(C.main_sprite[aState]), C.r);
            l.render(e.Graphics, pos);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) 
            {
                if(moveX == 0 && jTimer == 0)  aState = 1;
                moveX = 3;
                moveLast = true;
                //this.Refresh();
            }
            if (e.KeyCode == Keys.A)
            {                
                if (moveX == 0 && jTimer == 0) aState = 1;
                moveX = -3;
                moveLast = false;
                //this.Refresh();
            }
            if (e.KeyCode == Keys.W && jTimer == 0) 
            {
                jTimer = 1;
                //jump();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                moveX = 0;
                if (aState == 1 || aState == 2) aState = 0;
            }
            if (e.KeyCode == Keys.A)
            {
                moveX = 0;
                if (aState == 1 || aState == 2) aState = 0;
            }
            //if (e.KeyCode == Keys.J && jTimer == 0)
            //{
            //    jTimer = 1;
            //    //jump();
            //}
        }

        private double animToggle(double aTimer) 
        {
            //aTimer = 0;
            if(jTimer > 20) { aState = 4;return 0; }
            if (jTimer > 0) { aState = 3; return 0; }
            if (moveX != 0)
            {
                if (aState == 2)
                {
                    aState = 1;
                    return 0;
                }
                else
                {
                    aState = 2;
                    return 0;
                }
            }
            aState = 0;
            return 0;
            //if (aState < 4) { aState++; return; }
            //if (aState == 4) { aState = 0; return; }
        }

        private void gameLoop(object sender, EventArgs e)
        {
            currentTime = stopwatch.Elapsed.TotalMilliseconds;
            deltaTime = currentTime - lastTime;
            lastTime = currentTime;
            update(deltaTime);

            this.Invalidate();
        }

        bool xCheck() 
        {
            if (l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y - C.r.Height) / 32] != 0 && moveX < 0) return false;
            if (l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y - (C.r.Height / 2)) / 32] != 0 && moveX < 0) return false;
            if (l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y) / 32 - 1] != 0 && moveX < 0) return false;

            if (l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y - C.r.Height) / 32] != 0 && moveX > 0) return false;
            if (l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y - (C.r.Height / 2)) / 32] != 0 && moveX > 0) return false;
            if (l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y) / 32 - 1] != 0 && moveX > 0) return false;

            return true;
        }

        void yCheck() 
        {
            if ((l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y - C.r.Height) / 32 - 1] == 0 || l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y - C.r.Height) / 32 - 1] == 0) && (jTimer == 0)) { jTimer = 30; }    //jTimer > 29 ||       //rupa
            else if ((l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y - C.r.Height) / 32] != 0 || l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y - C.r.Height) / 32] != 0) && jTimer > 29) { jTimer = 0; C.r.Y = C.r.Y - 7; }  //prekid rupe
            Console.WriteLine("OVO GLEDAJ " + C.r.Y + " " + C.r.Height + " = " + ((640 - C.r.Y - C.r.Height) / 32));
            if (l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y) / 32] != 0) //plafon
            {
                if (jTimer > 0) jTimer = 30;
                else jTimer = 0;
            }
            //Console.WriteLine(((pos + C.r.X) / 32 + 2) + " " + ((640 - C.r.Y - C.r.Height) / 32 - 1) + " " + l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y) / 32 - 2]);
        }

        void update(double deltatime) 
        {
            //bool running = true;
            //Stopwatch sw = Stopwatch.StartNew();
            if (jTimer > 0 || l.mapa[pos / 32, C.r.Y / 32] != 0) jump();
            if(xCheck()) pos = pos + (moveX * (int)deltatime / 12);
            yCheck();
            //36 2 3 4

            //Console.WriteLine("X:   pos: " + pos + "  " + C.r.X + " " + C.r.Width + " " + ((pos + C.r.X + C.r.Width) / 32 - 2));
            //Console.WriteLine("Y:   " + C.r.Y + " " + ((640 - C.r.Y) / 32 - 1));
            //Console.WriteLine(((pos + C.r.X + C.r.Width) / 32 - 2) + " " + ((640 - C.r.Y) / 32 - 1));
            //Console.WriteLine(l.mapa[34, 1]);

            aTimer = aTimer + deltatime;
            if(aTimer > 300) aTimer = animToggle(aTimer);

            //ChangeWorld();


            //s.userInput();
            //this.Refresh();
        }
    }
}

