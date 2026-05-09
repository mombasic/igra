using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Collision
    {
        int[,] mapa;

        public Collision(int[,] mapa)
        {
            this.mapa = mapa;
        }
        //public void Check(Rectangle C, int pos) 
        //{
        //    if (mapa[pos / 32, C.Y]) //return true;

        //    //return false;
        //}
    }
}
