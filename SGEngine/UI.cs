using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGEngine
{
    public abstract class UI
    {
        public Vector Position { get; set; }

        public abstract void Draw(Graphics g);

        public void Destroy()
        {
            SGEngine.UnregisterUI(this);
        }
    }
}
