using igra.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Enemy
    {
        public Bitmap[,] enemy_sprite = new Bitmap[,]{ {Resources.burning_ghoul1, Resources.burning_ghoul2, Resources.burning_ghoul3, Resources.burning_ghoul4, Resources.burning_ghoul5 },{Resources.wizard_idle_1, Resources.wizard_idle_2 , Resources.wizard_idle_3 , Resources.wizard_idle_4 , Resources.wizard_idle_5 } };

        int health;
        int posx;
        int posy;
        int type;
        bool moveLast = true;

        int moveX = 0;
        //int moveY;

        int projx;
        int projy;
        int projTimer = 0;

        //posx - pos

        Load l;

        int aState = 0;

        public Enemy(int posx, int posy, int type) 
        {
            this.posx = posx;
            this.posy = posy;
            this.type = type;
            if (type == 0) this.health = 3;
            else this.health = 0;
        }

        public void Anim(Graphics g, bool moveLast, int pos) 
        {
            if (moveLast) g.DrawImage(enemy_sprite[type, aState], new Point(posx - pos, posy));
            else g.DrawImage(l.MirrorImage(enemy_sprite[type, aState]), new Point(posx - pos, posy));

            aState++;
            if (aState > 4) aState = 0; 
        }
        public bool Attack(int type, int mPosX, int mPosY, int pos) 
        {
            switch (type) 
            {
                case 0: 
                {
                    if (posx == mPosX && posy == mPosY) return true;
                    break;
                }
                case 1:
                {
                    if (posx == mPosX && posy == mPosY) return true;
                    break;
                }
            }
            return false;
        }
        public void Logic(int pos) 
        {
            if (posx - pos < 280) moveX = -3;
            else if (posx - pos > -280) moveX = 3;
            else moveX = 0;
        }
    }
}
