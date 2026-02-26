using System.Security.Cryptography;

namespace physics_sim_attempt
{
    public partial class Form1 : Form
    {
        private int numBalls = 20;
        public List<Ball> balls = new List<Ball>();
        public float timeStep = (float) 0.05;
        
        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int minV = 0;
            int maxV = 200;
            double radius = 20;
            double mass = 5;
            Random getRandom = new Random();
            for (int i = 0; i < numBalls; i++)
                {
                int randV = getRandom.Next(minV,maxV);
                int randV2 = getRandom.Next(minV,maxV);
                int randX = getRandom.Next(0, this.ClientSize.Width);
                int randY = getRandom.Next(0, this.ClientSize.Height);
                int randId = getRandom.Next(0, 1000000);
                mass = getRandom.Next(0, 50);
                balls.Add(new Ball(mass, mass, [randV, randV2], [randX, randY], 1,randId));
                }
        }

        private void timer_tick_Tick(object sender, EventArgs e)
        {
            foreach(Ball ball in balls)
            {
                ball.updatePositions(timeStep, this.ClientSize.Width, this.ClientSize.Height);

            }
            //Physics_collisions.checkCollisions(balls);
            Physics_collisions_attempt2_with_cleaner.Physics_cols(balls);
            this.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ball ball in balls)
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath(); // May be able to move out and clear instead
                gp.AddEllipse((float)ball.position[0] - (float)ball.radius, (float) ball.position[1] - (float)ball.radius, (float)(ball.radius * 2), (float)(ball.radius*2));
                System.Drawing.Region r = new System.Drawing.Region(gp);
                Graphics gr = e.Graphics;
                gr.FillRegion(Brushes.LawnGreen, r);
            }
        }
    }
}
