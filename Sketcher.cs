using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing.Drawing2D;

namespace EtchaSketch
{
    /// <summary>
    /// Unit 7 Project: Basic Project & Extension Work 1 to 3
    /// Author: Shan Ahmed
    /// This application is a digital version of the etchasketcha
    /// </summary>
    public enum ClearScreen
    {
        CLEAR,
        NOTHING
    }

    public partial class Sketcher : Form
    {
        int x = 50, y = 50;     // starting position
        int size = 20, difference = 5;       

        private const int MAXCOLOUR = 256;
        private ClearScreen clearing = ClearScreen.NOTHING;

        private Random generate = new Random();
        Bitmap bm;

        private Color randomColor()
        {
            int r, g, b;

            r = generate.Next(MAXCOLOUR);
            g = generate.Next(MAXCOLOUR);
            b = generate.Next(MAXCOLOUR);

            return Color.FromArgb(r, g, b);
        }

        public Sketcher()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this creates the etchasketcher appearance
        /// it also makes the marker appear on the opposite border when
        /// a border is breached
        /// </summary>

        private void Sketcher_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromImage(bm);        // get a graphic object for the bitmap
            if (clearing == ClearScreen.CLEAR)
            {
                g.Clear(BackColor);
            }
            
            g.FillEllipse(Brushes.ForestGreen, x, y, size, size);  // put a circle in the bitmap
            g.FillRectangle(Brushes.Maroon, 0, 0, 30, 400);          
            g.FillRectangle(Brushes.Maroon, 420, 0, 30, 400);
            g.FillRectangle(Brushes.Maroon, 0, 250, 600, 100);     // creates border
            g.FillRectangle(Brushes.Maroon, 0, 0, 600, 30);
            g.FillEllipse(Brushes.White, 75, 260, 50, 50);
            g.FillEllipse(Brushes.White, 300, 260, 50, 50);        // two circles to imitate rollers
        
            if (y + size >= 250)
            {
                y = 30;
            }
            if (y + size <= 30)
            {
                y = 250;
            }
            if (x + size >= 420)
            {
                x = 30;
            }
            if (x + size <= 30)
            {
                x = 420;
            }
        }
        private void Sketcher_Load(object sender, EventArgs e)
        { 
            splashScreen SScreen = new splashScreen();
            SScreen.ShowDialog();
            bm = new Bitmap(this.Width, this.Height);   // create a form-size bitmap
            this.BackgroundImage = bm;                  // use the bitmap as the form background
        }

        /// <summary>
        /// This creates the necessary keys for the user to make changes and
        /// use the sketcher
        /// </summary>

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            string input;
            input = keyData.ToString();

            if (input == "Down")
            {
                y += 10;
                Refresh();
                return true;
            }
            if (input == "Up")
            {
                y -= 10;
                Refresh();
                return true;
            }
            if (input == "Right")
            {
                x += 10;
                Refresh();
                return true;
            }
            if (input == "Left")
            {
                x -= 10;
                Refresh();
                return true;
            }
            if (input == "C")
            {
                clearing = ClearScreen.CLEAR;
                Refresh();
                return true;
            }
            if (input == "Escape")
            {
                Finish confirm = new Finish();
                confirm.Show();
            }
            if (input == "B" && size <= 100)
            {
                size = size + difference;
            }
            if (input == "S" && size >= 20)
            {
                size = size - difference;
            }
            if (input == "Up" && input == "Right" || input == "E")
            {
                y -= 10;
                x += 10;
                Refresh();
                return true;
            }
            if (input == "Up" && input == "Left" || input == "Q")
            {
                y -= 10;
                x -= 10;
                Refresh();
                return true;
            }
            if (input == "Down" && input == "Right" || input == "X")
            {
                y += 10;
                x += 10;
                Refresh();
                return true;
            }
            if (input == "Down" && input == "Left" || input == "Z")
            {
                y += 10;
                x -= 10;
                Refresh();
                return true;
            }
            if (input == "F2")
            {
                this.BackColor = randomColor();
                Refresh();
                return true;
            }
            Refresh();
            return false;
        }

    }
}