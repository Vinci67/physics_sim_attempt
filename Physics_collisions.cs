using System;
using System.Collections.Generic;
using System.Text;

namespace physics_sim_attempt
{
    public static class Physics_collisions
    {
        public static void checkCollisions(List<Ball> balls)
        {
            List<(int,int)> seenCollisions = new List<(int,int)>();

            foreach (Ball ballCurrent in balls)
            {
                foreach(Ball ballCheck in balls)
                {
                    if (!seenCollisions.Contains((ballCurrent.id, ballCheck.id)) && !seenCollisions.Contains((ballCheck.id, ballCurrent.id)))
                    {
                        if (Math.Pow((ballCurrent.position[0] - ballCheck.position[0]), 2) + Math.Pow((ballCurrent.position[1] - ballCheck.position[1]), 2) < Math.Pow(ballCurrent.radius + ballCheck.radius,2))
                        {
                            // Collision has occured
                            double totalMomentumBeforeX = ballCurrent.mass * ballCurrent.velocity[0] + ballCheck.mass * ballCheck.velocity[0];
                            double totalMomentumBeofreY =ballCurrent.mass * ballCurrent.velocity[1] + ballCheck.mass * ballCheck.velocity[1];

                            ballCurrent.velocity[0] = ballCurrent.coefficientOfRestitution * (ballCurrent.mass * ballCurrent.velocity[0] + 2 * ballCheck.mass * ballCheck.velocity[0] - ballCheck.mass * ballCurrent.velocity[0]) / (ballCurrent.mass + ballCheck.mass);
                            ballCurrent.velocity[1] = ballCurrent.coefficientOfRestitution * (ballCurrent.mass * ballCurrent.velocity[1] + 2 * ballCheck.mass * ballCheck.velocity[1] - ballCheck.mass * ballCurrent.velocity[1]) / (ballCurrent.mass + ballCheck.mass);
                            
                            ballCheck.velocity[0] = (totalMomentumBeforeX - (ballCurrent.mass*ballCurrent.velocity[0]))/ballCheck.mass;
                            ballCheck.velocity[1] = (totalMomentumBeofreY - (ballCurrent.mass * ballCurrent.velocity[1])) / ballCheck.mass;


                            seenCollisions.Add((ballCurrent.id, ballCheck.id));
                        }
                    }
                    
                }
            }
        }


    }
}
