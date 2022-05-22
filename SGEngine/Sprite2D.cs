using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public abstract class Sprite2D
    {
        public Vector Position { get; set; }
        public Vector Scale { get; set; }

        public Sprite2D(Vector position, Vector scale)
        {
            this.Position = position;
            this.Scale = scale;
            
            SGEngine.RegisterSprite(this);
        }

        public abstract void Draw(Graphics g);

        public void Destroy()
        {
            SGEngine.UnregisterSprite(this);
        }
    }
}
