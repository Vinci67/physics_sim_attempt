using System;
using System.Collections.Generic;
using System.Text;

namespace physics_sim_attempt
{
    public class Physics_collisions_attempt2_with_cleaner
    {
        public static void Physics_cols(List<Ball> balls)
        {
            List<(int, int)> seenCollisions = new List<(int, int)>();
            seenCollisions.Clear();
            foreach (Ball ballCurrent in balls)
            {
                foreach (Ball ballCheck in balls)
                {
                    if (ballCurrent == ballCheck)
                    {
                        continue;
                    }
                    if (Math.Pow((ballCurrent.position[0] - ballCheck.position[0]), 2) + Math.Pow((ballCurrent.position[1] - ballCheck.position[1]), 2) < Math.Pow(ballCurrent.radius + ballCheck.radius, 2))
                    {
                        //Console.WriteLine(Math.Pow((ballCurrent.position[0] - ballCheck.position[0]), 2) + Math.Pow((ballCurrent.position[1] - ballCheck.position[1]), 2));
                        //Console.WriteLine(Math.Pow(ballCurrent.radius + ballCheck.radius, 2));
                        if (!seenCollisions.Contains((ballCurrent.id, ballCheck.id)) && !seenCollisions.Contains((ballCheck.id, ballCurrent.id)))
                        { 

                            //Console.WriteLine("Collision");
                            // Vector from ballCheck to ballCurrent
                            double[] vector = [ballCheck.position[0] - ballCurrent.position[0], ballCheck.position[1] - ballCurrent.position[1]];
                            double distance = Math.Sqrt(vector[0] * vector[0] + vector[1] * vector[1]);
                            double[] normalVector = [vector[0] / distance, vector[1] / distance];
                            double[] relativeVelocity = [ballCheck.velocity[0] - ballCurrent.velocity[0], ballCheck.velocity[1] - ballCurrent.velocity[1]];
                            double scalarVelocityAlongNorm = relativeVelocity[0] * normalVector[0] + relativeVelocity[1] * normalVector[1];

                            if (scalarVelocityAlongNorm > 0)
                            {
                                //Console.WriteLine("Continued");
                                continue;  // Means separating so shouldn't calc
                            }
                            
                            double bounceScalarV = -(1 + ballCurrent.coefficientOfRestitution) * scalarVelocityAlongNorm;
                            double impulseScalar = bounceScalarV / ((1 / ballCurrent.mass) + (1 / ballCheck.mass));
                            double impulseX = impulseScalar * normalVector[0];
                            double impulseY = impulseScalar * normalVector[1];

                            ballCurrent.velocity[0] -= impulseX / ballCurrent.mass;
                            ballCurrent.velocity[1] -= impulseY / ballCurrent.mass;
                            ballCheck.velocity[0] += impulseX / ballCheck.mass;
                            ballCheck.velocity[1] += impulseY / ballCheck.mass;


                            double overlap = (ballCurrent.radius + ballCheck.radius) - distance;
                            double moveX = normalVector[0] * overlap / 2;
                            double moveY = normalVector[1] * overlap / 2;

                            ballCurrent.position[0] -= moveX;
                            ballCurrent.position[1] -= moveY;
                            ballCheck.position[0] += moveX;
                            ballCheck.position[1] += moveY;


                            seenCollisions.Add((ballCurrent.id, ballCheck.id));
                        }
                    }
                }
            }
        }
    }
}