using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private static List<Sprite2D> Sprites;
        private static List<UI> UIs;

        public List<string> Args { get; private set; }

        public SGEngine(int width, int height, string title, List<string> args)
        {
            CurrentWindow = new SGWindow(width, height, title);
            CurrentWindow.Paint += Renderer;
            CurrentWindow.KeyDown += KeyDown;
            CurrentWindow.KeyUp += KeyUp;
            CurrentWindow.FormClosed += FormClosed;

            CoreLogger = new Logger("Core", CurrentWindow);
            AppLogger = new Logger("Application", CurrentWindow);

            Sprites = new List<Sprite2D>();
            UIs = new List<UI>();

            this.Args = args;

            Random.Init();

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.SetApartmentState(ApartmentState.STA);
            GameLoopThread.Start();

            Application.Run(CurrentWindow);
        }

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            Terminate();
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

            List<Sprite2D> ToRender = new List<Sprite2D>(Sprites);

            foreach (var sprite in ToRender)
            {
                sprite.Draw(g);
            }

            foreach (var ui in UIs)
            {
                ui.Draw(g);
            }
        }

        public static bool RegisterSprite(Sprite2D p)
        {
            if (p == null || Sprites.Any(x => x.Id == p.Id)) return false;

            Sprites.Add(p);
            CoreLogger.Info($"Registered new sprite!");
            if (p is SGImage) CoreLogger.Info($"\t- {((SGImage)p).Img}");

            return true;
        }

        public static bool UnregisterSprite(Sprite2D p)
        {
            if (p == null) return false;

            for (int i = 0; i < Sprites.Count; i++)
            {
                if (Sprites[i].Id == p.Id)
                {
                    Sprites.RemoveAt(i);
                    CoreLogger.Info("Unregistered sprite!");
                    return true;
                }
            }

            return false;
        }

        public static bool RegisterUI(UI ui)
        {
            if (ui == null) return false;

            UIs.Add(ui);
            CoreLogger.Info("Registered new UI!");

            return true;
        }

        public static bool UnregisterUI(UI ui)
        {
            if (ui == null) return false;

            CoreLogger.Info("Unregistered UI!");
            return UIs.Remove(ui);
        }

        public void Terminate()
        {
            try
            {
                GameLoopThread.Abort();
                Application.Exit();
            }
            catch
            {}
        }

        public void Exit()
        {
            CurrentWindow.Invoke((MethodInvoker)delegate
            {
                CurrentWindow.Close();
            });
        }

        public abstract void Start();
        public abstract void Update();
    }
}
