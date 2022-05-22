using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGEngine
{
    public class UIText : UI
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public SolidBrush Foreground { get; set; }

        private StringFormat format;

        public UIText(string text, Vector position)
        {
            this.Text = text;
            this.Position = position;
            this.Font = new Font("Arial", 16);
            this.Foreground = new SolidBrush(Color.Black);

            SGEngine.RegisterUI(this);
        }

        public override void Draw(Graphics g)
        {
            g.DrawString(this.Text, this.Font, this.Foreground, new PointF(Position.X, Position.Y));
        }
    }
}
