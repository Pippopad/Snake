using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SGEngine
{
    public abstract class SGEngine
    {
        public static SGWindow CurrentWindow = null;
        private Thread GameLoopThread = null;

        public static Logger CoreLogger { get; private set; }
        public static Logger AppLogger { get; private set; }

        private static List<Shape> Shapes;
        private static List<UI> UIs = new List<UI>();

        public SGEngine(int width, int height, string title)
        {
            CurrentWindow = new SGWindow(width, height, title);
            CurrentWindow.Paint += Renderer;
            CurrentWindow.KeyDown += KeyDown;
            CurrentWindow.KeyUp += KeyUp;

            CoreLogger = new Logger("Core", CurrentWindow);
            AppLogger = new Logger("Application", CurrentWindow);

            Shapes = new List<Shape>();

            Random.Init();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.SetApartmentState(ApartmentState.STA);
            GameLoopThread.Start();

            Application.Run(CurrentWindow);
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            Input.UpdateKey(e.KeyCode, false);
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            Input.UpdateKey(e.KeyCode, true);
        }

        private void GameLoop()
        {
            Start();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    Update();
                    CurrentWindow.BeginInvoke((MethodInvoker)delegate { CurrentWindow.Refresh(); });
                    Thread.Sleep(1);
                }
                catch
                {}
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Cyan);

            List<Shape> ToRender = new List<Shape>(Shapes);

            foreach (var shape in ToRender)
            {
                g.FillRectangle(new SolidBrush(shape.FillColor), (int)shape.Position.X, (int)shape.Position.Y, shape.Scale.X, shape.Scale.Y);
            }

            foreach (var ui in UIs)
            {
                ui.Draw(g);
            }
        }

        public static bool RegisterShape(Shape p)
        {
            if (p == null) return false;

            Shapes.Add(p);
            CoreLogger.Info("Registered new shape!");

            return true;
        }

        public static bool UnregisterShape(Shape p)
        {
            if (p == null) return false;

            CoreLogger.Info("Unregistered shape!");
            return Shapes.Remove(p);
        }

        public static bool RegisterUI(UI ui)
        {
            if (ui == null) return false;

            UIs.Add(ui);

            return true;
        }

        public static bool UnregisterUI(UI ui)
        {
            if (ui == null) return false;

            return UIs.Remove(ui);
        }

        public void Terminate()
        {
            try
            {
                Thread.CurrentThread.Abort();
                Application.Exit();
            }
            catch
            {}
        }

        public abstract void Start();
        public abstract void Update();
    }
}
