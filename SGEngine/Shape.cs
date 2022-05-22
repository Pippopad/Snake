using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public class Shape : Sprite2D
    {
        public Color FillColor { get; set; }

        public Shape(Vector position, Vector scale, Color fillColor) : base(position, scale)
        {
            FillColor = fillColor;

        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(this.FillColor), (int)this.Position.X, (int)this.Position.Y, this.Scale.X, this.Scale.Y);
        }
    }
}
