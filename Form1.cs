using igra.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            SerialPort mySerialPort = new SerialPort("COM6");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            C = new Character();

            PlayerPhysics = new Physics(pos, C.r.Y);

            //mySerialPort.Open();
        }

        //private static void DataReceivedHandler(
        //                object sender,
        //                SerialDataReceivedEventArgs e)
        //{
        //    SerialPort sp = (SerialPort)sender;
        //    string indata = sp.ReadExisting();
        //    //Console.WriteLine("Data Received:");
        //    //Console.Write(indata);
        //}

        //int map(int x, int in_min, int in_max, int out_min, int out_max) 
        //{
        //    return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        //}
        //private static System.Timers.Timer atimer;

        int x;
        int y;
        bool stanje;
        Load l;
        Character C;
        Physics PlayerPhysics;
        int aState = 0;
        double aTimer = 0;
        bool moveLast = true;
        int pos = 400;
        //int jValue = 0;
        int jTimer = 0;

        int moveX = 0;

        List<Enemy> enemies = new List<Enemy>();

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void DataReceivedHandler(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string a = serialPort1.ReadLine();
            Console.WriteLine(a);
            x = Convert.ToInt32(a.Substring(0, a.IndexOf(',')));
            y = Convert.ToInt32(a.Substring(a.IndexOf(','), a.LastIndexOf(',')));
            stanje = Convert.ToBoolean(a.Substring(a.LastIndexOf(','), a.Length - a.LastIndexOf(',')));

            if (x > 112) { moveX = 3; moveLast = true; aState = 1; }
            else if (x > 96) { moveX = 1; moveLast = true; aState = 1; }
            else if (x < 32) { moveX = -1; moveLast = false; aState = 1; }
            else if (x < 16) { moveX = -3; moveLast = false; aState = 1; }

            if (y > 64 && jTimer == 0) 
            {
                jTimer = 1;
            }
            //if (x > 64) label1.Location(label1.Location.X++, label1.Location.Y);
        }

        void jump() 
        {
            Rectangle newP = C.r;

            if (jTimer < 20)
            {
                newP.Y = newP.Y - 8;
                jTimer++;
            }
            else if (jTimer < 30) jTimer++;
            else if (jTimer < 49)
            {
                newP.Y = newP.Y + 8; //jcheck preuzeo
                jTimer++;
            }
            else
            {
                jTimer = 0;
                //if ((640 - C.r.Y - C.r.Height) % 16 != 0) C.r.Y = C.r.Y + 8;
            }
            C.r = newP;
            //this.Refresh();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if(moveLast)e.Graphics.DrawImage(C.main_sprite[aState], C.r);
            else e.Graphics.DrawImage(l.MirrorImage(C.main_sprite[aState]), C.r);

            l.render(e.Graphics, pos, enemies);
            for (int i = 0; i < enemies.Count; i++) enemies[i].Anim(e.Graphics, true, pos);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { }
            if (e.KeyCode == Keys.D) 
            {
                //aState = 1;
                //moveX = 3;
                //moveLast = true;
                PlayerPhysics.startMovingRight();
                moveLast = true;
                //this.Refresh();
            }
            if (e.KeyCode == Keys.A)
            {
                //aState = 1;
                //moveX = -3;
                //moveLast = false;
                PlayerPhysics.startMovingLeft();
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
                //moveX = 0;
                PlayerPhysics.stopMoving();
                if (aState == 1 || aState == 2) aState = 0;
            }
            if (e.KeyCode == Keys.A)
            {
                //moveX = 0;
                PlayerPhysics.stopMoving();
                if (aState == 1 || aState == 2) aState = 0;
            }
            //if (e.KeyCode == Keys.J && jTimer == 0)
            //{
            //    jTimer = 1;
            //    //jump();
            //}
        }

        //private double animToggle(double aTimer)
        private double animToggle()
        {
            //aTimer = 0;
            if (jTimer > 20) { aState = 4;return 0; }
            if (jTimer > 0) { aState = 3; return 0; }
            //if (moveX != 0)
            if(PlayerPhysics.accelerationX == Physics.walkingAcceleration || PlayerPhysics.accelerationX == -Physics.walkingAcceleration)
            {
                if (aState != 1 && aTimer > 300)
                {
                    aState = 1;
                    aTimer = 0;
                    return 0;
                }
                else if (aState != 2 && aTimer > 300)
                {
                    aState = 2;
                    aTimer = 0;
                    return 0;
                }
            }
            else aState = 0;
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
            int visina = (int)Math.Floor(Convert.ToDouble(640 - C.r.Y - C.r.Height));
            //int visina = 640 - C.r.Y - C.r.Height;
            //Console.WriteLine("j" + jTimer);
            if ((l.mapa[(pos + C.r.X) / 32 + 2, visina / 32 - 1] == 0 && l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, visina / 32 - 1] == 0 && l.mapa[(pos + C.r.X + C.r.Width / 2) / 32, visina / 32 - 1] == 0) && (jTimer == 0)) { jTimer = 30; }    //jTimer > 29 ||       //rupa
            else if ((l.mapa[(pos + C.r.X) / 32 + 2, visina / 32] != 0 || l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, visina / 32] != 0 || l.mapa[(pos + C.r.X + C.r.Width / 2) / 32, visina / 32] != 0) && jTimer > 29) {
                //Console.WriteLine(visina);
                //Console.WriteLine("j" + jTimer);
                jTimer = 0;
                //if (visina % 16 != 0) { C.r = new Rectangle(C.r.X, C.r.Y - 8, C.r.Width, C.r.Height); }
            }  //prekid rupe

            //Console.WriteLine("y:" + C.r.Y);
            //Console.WriteLine("X: " + C.r.X + " POS: " + pos);
            //Console.WriteLine("jTimer: " + jTimer);
            if (l.mapa[(pos + C.r.X) / 32 + 2, (640 - C.r.Y) / 32 - 1] != 0 || l.mapa[(pos + C.r.X + C.r.Width) / 32 - 2, (640 - C.r.Y) / 32 - 1] != 0 || l.mapa[(pos + C.r.X + C.r.Width / 2) / 32, (640 - C.r.Y) / 32 - 1] != 0) //plafon
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
            //if(xCheck()) pos = pos + (moveX * (int)deltatime / 12);
            pos = PlayerPhysics.update((float)deltatime, pos);

            Console.WriteLine(pos);
            Console.WriteLine(PlayerPhysics.x);

            yCheck();
            //36 2 3 4

            //Console.WriteLine("X:   pos: " + pos + "  " + C.r.X + " " + C.r.Width + " " + ((pos + C.r.X + C.r.Width) / 32 - 2));
            //Console.WriteLine("Y:   " + C.r.Y + " " + ((640 - C.r.Y) / 32 - 1));
            //Console.WriteLine(((pos + C.r.X + C.r.Width) / 32 - 2) + " " + ((640 - C.r.Y) / 32 - 1));
            //Console.WriteLine(l.mapa[34, 1]);

            aTimer = aTimer + deltatime;
            //if (aTimer > 300) 
            //{
            //    if (aState == 1) aState = 2;
            //    else if (aState == 2) aState = 1;
            //    aTimer = 0;
            //}
            //aTimer = animToggle(aTimer);
            animToggle();
        }
    }
}

