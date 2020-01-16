using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameofLife
{
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[10, 10];
        bool[,] sratchpad = new bool[10, 10];
        

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            int counting = 0;
            //y
            for (int i = 0; i < universe.GetLength(1); i++)
            {
                //x
                for (int j = 0; j < universe.GetLength(0); j++)
                {
                    sratchpad[j, i] = false;
                    counting = CountingNeighbors(j, i);

                   if (universe[j, i] == true)
                    {
                        if (counting < 2 )
                        {
                            sratchpad[j, i] = false;
                        }
                        if (counting > 3)
                        {
                            sratchpad[j, i] = false;
                        }
                        if (counting == 2 || counting == 3)
                        {
                            sratchpad[j, i] = true;
                        }
                    }
                    else
                    {
                        if (counting == 3)
                        {
                            sratchpad[j, i] = true;
                        }
                    }

                }
            }


            bool[,] temp = universe;
            universe = sratchpad;
            sratchpad = temp;


            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();


            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            //USE FLOATS!!!
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = Rectangle.Empty;
                    cellRect.X = (x * cellWidth);
                    cellRect.Y = (y * cellHeight);
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;


                    Font font = new Font("Arial", 20f);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    Rectangle rect = new Rectangle(0, 0, 100, 100);
                    int neighbors = CountingNeighbors(x, y);



                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    e.Graphics.DrawString(neighbors.ToString(), graphicsPanel1.Font, Brushes.Black, cellRect.X, cellRect.Y);
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = ((float)graphicsPanel1.ClientSize.Width) / universe.GetLength(0);
                float cellHeight = ((float)graphicsPanel1.ClientSize.Height) / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)(e.X / cellWidth);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)(e.Y / cellHeight);

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            graphicsPanel1.Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            NextGeneration();

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }


        private int CountingNeighbors(int x, int y)
        {
            int count = 0;


            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    // check if out of bounds universe[5,5]
                    if ((x + i >= 0 && x + i < universe.GetLength(0))&& (y + j >= 0 && y + j < universe.GetLength(1)))
                    {
                        // check if its the same universe position [x,y] = [i, j]
                        if ( i != 0 || j != 0)
                        {
                            // check if cell is Alive or Dead
                            if (universe[x + i, y + j] == true)
                            {
                                count++;
                            }

                        }

                    }
                   
                    
                }
            }

            return count;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            dlg.Color = gridColor;


            if(DialogResult.OK == dlg.ShowDialog())
            {
                gridColor = dlg.Color;
                graphicsPanel1.Invalidate();
            }
        }

        private void modalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modal_Dialog dlg = new Modal_Dialog();



            dlg.Number = timer.Interval;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                timer.Interval = dlg.Number;

                graphicsPanel1.Invalidate();
            }

        }
    }
}
