using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGEngine
{
    public class Logger
    {
        public string Prefix { get; set; }

        private SGWindow Window;

        public Logger(string prefix, SGWindow window)
        {
            this.Prefix = prefix;
            this.Window = window;
        }

        public void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{Prefix}]: {msg}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void VisualInfo(string msg, string title="Info")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{Prefix}]: {msg}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void VisualWarn(string msg, string title = "Warning")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{Prefix}]: {msg}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void VisualError(string msg, string title = "Error")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
