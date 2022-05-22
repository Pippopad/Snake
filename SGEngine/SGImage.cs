using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public class SGImage : Sprite2D
    {
        public System.Drawing.Image Img { get; set; }

        public SGImage(Vector position, Vector scale, string path) : base(position, scale)
        {
            this.Img = System.Drawing.Image.FromFile(path);

            SGEngine.RegisterSprite(this);
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(this.Img, new Rectangle(this.Position.ToPoint(), this.Scale.ToSize()));
        }
    }
}
