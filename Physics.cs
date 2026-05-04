using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace igra
{
    internal class Physics
    {
        const float slowdown_constant = 8.0f;

        const float topSpeedX = 10.0f;
        const float topSpeedY = 10.0f;

        const float walkingAcceleration = 0.1f;

        int x;
        int y;
        float velocityX = 0f;
        float velocityY = 0f;
        float accelerationX = 0f;
        float accelerationY = 0f;

        public Physics(int pos, int y) 
        {
            x = pos;
            this.y = y;
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
        public void update(float deltatime) 
        {
            x += Convert.ToInt32(velocityX * deltatime / 12);
            velocityX += accelerationX * deltatime / 12;

            if (accelerationX < 0.0f) velocityX = Math.Max(velocityX, -topSpeedX);
            else if (accelerationX > 0.0f) velocityX = Math.Max(velocityX, topSpeedX);
            else velocityX *= slowdown_constant;
        }

    }
}
