using igra.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Character
    {
        int X;
        int Y;

        public Rectangle hitbox;
        public Rectangle sprite;

        public Character() 
        {
            this.X = 318;
            this.Y = 640 - 64 - 120;
            hitbox = new Rectangle(X, Y, 34, 88);
            sprite = new Rectangle(X - 82, Y, 164, 120);

        }

        public void xChange(int X) 
        {
            this.X = X;
            hitbox.X = X;
            sprite.X = X;
        }

        public void yChange(int Y)
        {
            this.Y = Y;
            hitbox.Y = Y;
            sprite.Y = Y;
        }

        public Bitmap[] main_sprite = { Resources.idle1, Resources.walk1, Resources.walk2, Resources.jump1, Resources.fall1 };

        public int health = 3;
    }
}
