using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public class Shape
    {
        public Vector Position { get; set; }
        public Vector Scale { get; set; }
        public Color FillColor { get; set; }

        public Shape(Vector position, Vector scale, Color fillColor)
        {
            Position = position;
            Scale = scale;
            FillColor = fillColor;

            SGEngine.RegisterShape(this);
        }

        public void Destroy()
        {
            SGEngine.UnregisterShape(this);
        }
    }
}
