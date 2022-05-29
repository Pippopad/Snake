using System;
using System.Drawing;
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
            this.WindowWidth = width;
            this.WindowHeight = height;
            this.Size = new Size(width + 17, height + 40);
            this.Title = this.Text = title;

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        public void SetBackground(string path)
        {
            bool ok = false;
            do
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackgroundImage = Image.FromFile(path);
                    });
                    ok = true;
                }
                catch (Exception)
                {
                }
            } while (!ok);
        }
    }
}
