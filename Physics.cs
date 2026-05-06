using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Physics
    {
        const float slowdown_constant = 0.8f;

        const float topSpeedX = 0.325f;
        const float topSpeedY = 0.325f;

        public const float walkingAcceleration = 0.0012f;

        public int x, y;
        public float velocityX, velocityY;
        public float accelerationX, accelerationY;

        public Physics(int pos, int y) 
        {
            x = pos;
            this.y = y;

            velocityX = 0.0f;
            velocityY = 0.0f;
            accelerationX = 0.0f;
            accelerationY = 0.0f;
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
        public int update(float deltatime, int pos) 
        {
            pos += Convert.ToInt32(velocityX * deltatime);
            velocityX += accelerationX * deltatime;

            if (accelerationX < 0.0f) velocityX = Math.Max(velocityX, -topSpeedX);
            else if (accelerationX > 0.0f) velocityX = Math.Min(velocityX, topSpeedX);
            else velocityX *= slowdown_constant;

            //if()

            return pos;
        }

    }
}
