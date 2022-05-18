using System;
using System.Windows.Forms;

namespace SGEngine
{
    public class SGWindow : Form
    {
        public string Title { get; private set; }
        public int WindowWidth { get; private set; }
        public int WindowHeight{ get; private set; }

        public SGWindow(int width, int height, string title)
        {
            this.Width = this.WindowWidth = width;
            this.Height = this.WindowHeight = height;
            this.Title = this.Text = title;

            this.DoubleBuffered = true;
        }
    }
}
