using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TwitchBot
{
    public partial class ChatFish : Form
    {
        // Define grid points for fish and food positions
        Point[][] points = new Point[6][]
        {
            new Point[] { new Point(207, 207), new Point(309, 207), new Point(411, 207), new Point(513, 207), new Point(615, 207), new Point(717, 207) }, 
            new Point[] { new Point(207, 309), new Point(309, 309), new Point(411, 309), new Point(513, 309), new Point(615, 309), new Point(717, 309) },
            new Point[] { new Point(207, 411), new Point(309, 411), new Point(411, 411), new Point(513, 411), new Point(615, 411), new Point(717, 411) },
            new Point[] { new Point(207, 513), new Point(309, 513), new Point(411, 513), new Point(513, 513), new Point(615, 513), new Point(717, 513) },
            new Point[] { new Point(207, 615), new Point(309, 615), new Point(411, 615), new Point(513, 615), new Point(615, 615), new Point(717, 615) },
            new Point[] { new Point(207, 717), new Point(309, 717), new Point(411, 717), new Point(513, 717), new Point(615, 717), new Point(717, 717) }
        };

        // Current indices for fish and food positions
        Vector2 fishPointIndex = new Vector2(0, 0);
        Vector2 foodPointIndex = new Vector2(2, 2);

        // PictureBoxes for fish and food
        PictureBox fishPictureBox = new PictureBox();
        PictureBox foodPictureBox = new PictureBox();

        // Images for fish and food
        Image fishImage = null;
        Image foodImage = null;

        // Flag to track if food is present
        bool foodPresent = false;

        // Constructor to initialize the ChatFish form
        public ChatFish()
        {
            InitializeComponent();
        }

        // Setup the fish and food on form load
        private void ChatFish_Load(object sender, EventArgs e)
        {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            fishImage = Properties.Resources.ChatFishFishBig;
            foodImage = Properties.Resources.ChatFishFishfoodBig;

            Random rand = new Random();
            fishPointIndex = new Vector2(rand.Next(0, points[0].Length), rand.Next(0, points.Length));
            fishPictureBox.Location = points[(int)fishPointIndex.X][(int)fishPointIndex.Y];
            fishPictureBox.Size = new Size(100, 100);
            fishPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            fishPictureBox.Image = fishImage;
            fishPictureBox.BackColor = Color.Transparent;
            this.Controls.Add(fishPictureBox);

            foodPictureBox.Location = points[(int)foodPointIndex.X][(int)foodPointIndex.Y];
            foodPictureBox.Size = new Size(100, 100);
            foodPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            foodPictureBox.Image = foodImage;
            foodPictureBox.BackColor = Color.Transparent;
            foodPictureBox.Visible = false;
            this.Controls.Add(foodPictureBox);
        }

        // Method to spawn food at a random location
        public void Feed()
        {
            RunOnUiThread(() =>
            {
                if (foodPresent)
                {
                    return;
                }
                Random rand = new Random();
                foodPointIndex = new Vector2(rand.Next(0, points[0].Length), rand.Next(0, points.Length));
                while (foodPointIndex == fishPointIndex)
                {
                    foodPointIndex = new Vector2(rand.Next(0, points[0].Length), rand.Next(0, points.Length));
                }
                foodPictureBox.Location = points[(int)foodPointIndex.X][(int)foodPointIndex.Y];
                foodPresent = true;
                foodPictureBox.Visible = true;
                Console.WriteLine($"Food spawned at: {(int)foodPointIndex.X}, {(int)foodPointIndex.Y}");
            });
        }

        // Methods to move the fish up
        public void Up()
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine("Fish moved up!");
                if (fishPointIndex.X > 0)
                {
                    fishPointIndex.X -= 1;
                    fishPictureBox.Location = points[(int)fishPointIndex.X][(int)fishPointIndex.Y];
                    CheckIfFishAteFood();
                }
            });
        }

        // Methods to move the fish down
        public void Down()
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine("Fish moved down!");
                if (fishPointIndex.X < points.Length - 1)
                {
                    fishPointIndex.X += 1;
                    fishPictureBox.Location = points[(int)fishPointIndex.X][(int)fishPointIndex.Y];
                    CheckIfFishAteFood();
                }
            });
        }

        // Methods to move the fish left
        public new void Left()
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine("Fish moved left!");
                if (fishPointIndex.Y > 0)
                {
                    fishPointIndex.Y -= 1;
                    fishPictureBox.Location = points[(int)fishPointIndex.X][(int)fishPointIndex.Y];
                    CheckIfFishAteFood();
                }
            });
        }

        // Methods to move the fish right
        public new void Right()
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine("Fish moved right!");
                if (fishPointIndex.Y < points[0].Length - 1)
                {
                    fishPointIndex.Y += 1;
                    fishPictureBox.Location = points[(int)fishPointIndex.X][(int)fishPointIndex.Y];
                    CheckIfFishAteFood();
                }
            });
        }

        // Check if the fish has eaten the food => if positions match
        private void CheckIfFishAteFood()
        {
            RunOnUiThread(() =>
            {
                Console.WriteLine(fishPointIndex);
                Console.WriteLine(foodPointIndex);
                if (foodPresent && fishPointIndex == foodPointIndex)
                {
                    foodPresent = false;
                    foodPictureBox.Visible = false;
                    Console.WriteLine("Fish ate the food!");
                }
            });
        }

        // Helper method to run actions on the UI thread
        private void RunOnUiThread(Action action)
        {
            if (InvokeRequired)
                Invoke(action);
            else
                action();
        }
    }
}
