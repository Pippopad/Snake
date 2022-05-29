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
        public int Id { get; private set; }
        public Vector Position { get; set; }
        public Vector Scale { get; set; }

        private static int ENTITY_ID = 0;

        public Sprite2D(Vector position, Vector scale)
        {
            this.Position = position;
            this.Scale = scale;
                
            this.Id = ENTITY_ID++;

            SGEngine.RegisterSprite(this);
        }

        public abstract void Draw(Graphics g);

        public void Destroy()
        {
            SGEngine.UnregisterSprite(this);
        }
    }
}
