using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace physics_sim_attempt
{
    public class Ball
    {
        public int id;
        public double mass;
        public double radius;
        public double[] velocity;
        public double[] position;
        public double coefficientOfRestitution;


        public Ball(double mass, double radius, double[] velocity, double[] position, double coefficientOfRestitution, int id)
        {   
            this.mass = mass;
            this.radius = radius;
            this.velocity = velocity;
            this.position = position;
            this.coefficientOfRestitution = coefficientOfRestitution;
            this.id = id;
        }

        public void updatePositions(double timeStep, int boundX, int boundY)
        {
            this.position[0] += this.velocity[0] * timeStep;
            this.position[1] += this.velocity[1] * timeStep;

            if (this.position[0] - this.radius < 0)
            {
                position[0] = 0 + this.radius;
                velocity[0] = -velocity[0];
            }
            else if (this.position[0] + this.radius > boundX)
            {
                position[0] = boundX - this.radius;
                velocity[0] = -velocity[0];
            }
            if (this.position[1] - this.radius < 0)
            {
                position[1] = 0 + this.radius;
                velocity[1] = -velocity[1];

            }
            else if (this.position[1] + this.radius > boundY)
            {
                this.position[1] = boundY - this.radius;
                this.velocity[1] = -velocity[1];
            }


        }



    }
}
