using igra.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Character
    {
        public Rectangle r = new Rectangle(318, 640 - 120 - 64, 164, 120);//PROMJENITI SIRINU SPRITA I OVDJE

        public Bitmap[] main_sprite = { Resources.idle1, Resources.walk1, Resources.walk2, Resources.jump1, Resources.fall1 };

        public Bitmap MirrorImage(Bitmap source)
        {
            Bitmap mirrored = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
                for (int j = 0; j < source.Height; j++)
                    mirrored.SetPixel(i, j, source.GetPixel(source.Width - i - 1, j));
            return mirrored;
        }
    }
}
