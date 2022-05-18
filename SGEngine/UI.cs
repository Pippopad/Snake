using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public interface UI
    {
        void Draw(Graphics g);
        void Destroy();
    }
}
