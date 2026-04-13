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

namespace igra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int map(int x, int in_min, int in_max, int out_min, int out_max) 
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        int x;
        int y;
        bool stanje;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(800, 600);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //string a = serialPort1.ReadLine();
            //x = Convert.ToInt32(a.Substring(0, a.IndexOf(',')));
            //y = Convert.ToInt32(a.Substring(a.IndexOf(','), a.LastIndexOf(',')));
            //stanje = Convert.ToBoolean(a.Substring(a.LastIndexOf(','), a.Length - a.LastIndexOf(',')));


            //if (x > 64) label1.Location(label1.Location.X++, label1.Location.Y);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            mc mc = new mc();
            e.Graphics.DrawImage(Resources.zulfo, mc.r);
        }
    }
}
