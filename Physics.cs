using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Physics
    {
        int[,] mapa;

        const float slowdown_constant = 0.8f;

        const float topSpeedX = 0.325f;
        const float topSpeedY = 0.325f;
        public const float jumpSpeed = 1.0f;

        public const float walkingAcceleration = 0.0012f;
        const float gravity = 0.0012f;

        //public int x, y;
        public int pos, posy;
        public float velocityX, velocityY;
        public float accelerationX, accelerationY;

        bool on_ground = false; //temp
        public bool jumpActive;
        int jumpTime = 275;
        int jumpRemaining;

        float deltatime;

        public Physics(int y, int[,] mapa) 
        {
            pos = 400;
            posy = y;

            velocityX = 0.0f;
            velocityY = 0.0f;
            accelerationX = 0.0f;
            accelerationY = 0.0f;

            jumpActive = false;

            this.mapa = mapa;
        }

        public void startMovingRight() 
        {
            accelerationX = walkingAcceleration;
        }

        public void startMovingLeft() 
        {
            accelerationX = -walkingAcceleration;
        }
        public void stopMoving() 
        {
            accelerationX = 0.0f;
        }
        public void startJump() 
        {
            if (on_ground) 
            {
                velocityY = -jumpSpeed;
                jumpActive = true;
                jumpRemaining = 0;
            }
            
        }
        public void jumpUpdate()
        {
            //Console.WriteLine(velocityY);
            posy += Convert.ToInt32(velocityY * deltatime);

            if (!jumpActive) velocityY = Math.Min((velocityY + gravity) * deltatime, topSpeedY);
            else if (jumpRemaining >= jumpTime)
            {
                jumpActive = false;
                velocityY = 0;
            }
            else jumpRemaining += (int)deltatime;
            
            int deltaY = (int)Math.Round(velocityY * deltatime);
            Console.WriteLine((640 - posy - 120 + deltaY) / 32);
            Console.WriteLine(mapa[pos / 32, (640 - posy - 120 + deltaY) / 32]);
            if (deltaY > 0) 
            {
                if (mapa[pos / 32, (640 - posy - 120 + deltaY) / 32] == 1)
                {
                    posy = ((640 - posy - 120 + deltaY) / 32) * 32 + 1;

                    on_ground = true;
                }
                else on_ground = false;
            }
            
            //return y;
        }
        public void update(float deltatime) 
        {
            this.deltatime = deltatime;
            
            //this.on_ground = on_ground;
            updateX();
            jumpUpdate();
        }
        void updateX( ) 
        {
            //Console.WriteLine(velocityX);
            pos += Convert.ToInt32(velocityX * deltatime);
            velocityX += accelerationX * deltatime;

            if (accelerationX < 0.0f) velocityX = Math.Max(velocityX, -topSpeedX);
            else if (accelerationX > 0.0f) velocityX = Math.Min(velocityX, topSpeedX);
            else if(!jumpActive) velocityX *= slowdown_constant;

            int deltaX = (int)Math.Round(velocityX * deltatime);
            //if(deltaX > 0) 
            //{
            if (mapa[(pos + deltaX) / 32, posy / 32] == 1) pos = (int)Math.Round((double)(pos + deltaX) / 32) * 32;
            //}
            //else if(deltaX < 0)
            //{
            //    if (mapa[(pos + deltaX) / 32, y] == 1) pos = (int)Math.Round((double)(pos + deltaX) / 32) * 32;
            //}
            //else if(on_ground) velocityX *= slowdown_constant;

            //if()

            //return pos;
        }
        //void updateY() 
        //{
            
        //}
        //bool CollideX(float deltatime, int[,] mapa, int pos) 
        //{
        //    float deltaX = velocityX * deltatime;
        //    if (mapa[(pos + Convert.ToInt32(deltaX)) / 32, y] == 1) return true;
        //    return false;
        //}
    }
}
