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
        public Rectangle r = new Rectangle(355, 640 - 120 - 64, 164, 120);


        Bitmap[] main_sprite = { Resources.idle1, Resources.walk1, Resources.walk2};
    }
}
