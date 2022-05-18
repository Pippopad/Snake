using SGEngine;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Snake
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public class Snake : SGEngine.SGEngine
    {
        Vector pixelSize = new Vector(25, 25);

        List<Shape> snake = new List<Shape>();
        Shape apple;
        UIText scoreText;

        List<Direction> dirs = new List<Direction>() { Direction.RIGHT };
        bool isAlive = true;

        public Snake() : base(800, 600, "Snake")
        {}

        public override void Start()
        {
            int sWidth = CurrentWindow.WindowWidth;
            int sHeight = CurrentWindow.WindowHeight;

            apple = new Shape(new Vector(Random.NextInt((int)((CurrentWindow.WindowWidth - pixelSize.X) / pixelSize.X)) * pixelSize.X + 1, Random.NextInt((int)((CurrentWindow.WindowHeight - pixelSize.Y) / pixelSize.Y)) * pixelSize.Y + 1), new Vector(pixelSize.X - 1, pixelSize.Y - 1), Color.Red);
            AppLogger.Info(apple.Position.ToString());

            snake.Add(new Shape(new Vector(sWidth / 2 + 1 + 4 * pixelSize.X, sHeight / 2 + 1), new Vector(pixelSize.X - 1, pixelSize.Y - 1), Color.Green));
            for (int i = 3; i >= 0; i--)
            {
                snake.Add(new Shape(new Vector(sWidth / 2 + i * pixelSize.X + 1, sHeight / 2 + 1), new Vector(pixelSize.X - 1, pixelSize.Y - 1), Color.Lime));
            }

            scoreText = new UIText("Score: 0", new Vector(10, 10));

            // test
            //for (int y = 0; y < CurrentWindow.WindowHeight / pixelSize.Y; y++)
            //{
            //    for (int x = 0; x < CurrentWindow.WindowWidth / pixelSize.X; x++)
            //    {
            //        new Shape(new Vector(pixelSize.X / 2 + x * pixelSize.X, pixelSize.Y / 2 + y * pixelSize.Y), pixelSize, Color.FromArgb((int)(x * 255 / (CurrentWindow.WindowWidth / pixelSize.X)), (int)(y * 255 / (CurrentWindow.WindowHeight/ pixelSize.Y)), 0));
            //    }
            //}
        }

        int tick = 0;
        int maxTick = 10;
        int appleAte = 0;
        public override void Update()
        {
            HandleInput();

            // Debug
            //snake[0].Position.Y = 0; snake[0].FillColor = Color.Red;
            //snake[1].Position.Y = 50; snake[1].FillColor = Color.Orange;
            //snake[2].Position.Y = 100; snake[2].FillColor = Color.Yellow;
            //snake[3].Position.Y = 150; snake[3].FillColor = Color.Green;
            //snake[4].Position.Y = 200; snake[4].FillColor = Color.Blue;

            if (tick == maxTick)
            {
                tick = 0;
                if (isAlive)
                {
                    while (dirs.Count > 0)
                    {
                        for (int i = snake.Count - 1; i > 0; i--)
                            snake[i].Position = snake[i - 1].Position.Copy();
                        switch (dirs[0])
                        {
                            case Direction.UP:
                                snake[0].Position.Y -= pixelSize.Y;
                                break;
                            case Direction.DOWN:
                                snake[0].Position.Y += pixelSize.Y;
                                break;
                            case Direction.LEFT:
                                snake[0].Position.X -= pixelSize.X;
                                break;
                            case Direction.RIGHT:
                                snake[0].Position.X += pixelSize.X;
                                break;
                        }
                        if (dirs.Count == 1) break;

                        dirs.RemoveAt(0);
                    }

                    List<Vector> coords = new List<Vector>();
                    foreach (var s in snake)
                    {
                        if (coords.Count > 0 && CheckCoords(s.Position, coords))
                        {
                            isAlive = false;
                            break;
                        }
                        coords.Add(s.Position);
                    }

                    if ((apple.Position.X >= snake[0].Position.X && apple.Position.X <= snake[0].Position.X + snake[0].Scale.X) &&
                        (apple.Position.Y >= snake[0].Position.Y && apple.Position.Y <= snake[0].Position.Y + snake[0].Scale.Y))
                    {
                        
                        Vector lastPos = snake[snake.Count - 1].Position;
                        Vector secondLastPos = snake[snake.Count - 2].Position;
                        snake.Add(new Shape(lastPos.Copy(), new Vector(pixelSize.X - 1, pixelSize.Y - 1), Color.Lime));
                        
                        if (lastPos.Y < secondLastPos.Y)
                        {
                            snake[snake.Count - 1].Position.Y -= pixelSize.Y;
                        }
                        else if (lastPos.Y > secondLastPos.Y)
                        {
                            snake[snake.Count - 1].Position.Y += pixelSize.Y;
                        }
                        else if (lastPos.X < secondLastPos.X)
                        {
                            snake[snake.Count - 1].Position.X -= pixelSize.X;
                        }
                        else if (lastPos.Y > secondLastPos.Y)
                        {
                            snake[snake.Count - 1].Position.X += pixelSize.X;
                        }

                        apple.Destroy();
                        appleAte++;
                        if (maxTick > 0 && appleAte % 10 == 0) maxTick--;
                        scoreText.Text = $"Score: {appleAte}";
                        apple = new Shape(new Vector(Random.NextInt((int)((CurrentWindow.WindowWidth - pixelSize.X) / pixelSize.X)) * pixelSize.X + 1, Random.NextInt((int)((CurrentWindow.WindowHeight - pixelSize.Y) / pixelSize.Y)) * pixelSize.Y + 1), new Vector(pixelSize.X - 1, pixelSize.Y - 1), Color.Red);
                        AppLogger.Info(apple.Position.ToString());
                    }
                }
                else
                {
                    AppLogger.Info("Loser!");
                }
            }
            tick++;

        }

        private void HandleInput()
        {
            if (dirs.Count >= 2) return;

            Direction last = dirs[dirs.Count - 1];

            if (Input.GetKey('W') && last != Direction.DOWN && last != Direction.UP)
            {
                dirs.Add(Direction.UP);
            }
            else if (Input.GetKey('S') && last != Direction.UP && last != Direction.DOWN)
            {
                dirs.Add(Direction.DOWN);
            }
            else if (Input.GetKey('A') && last != Direction.RIGHT && last != Direction.LEFT)
            {
                dirs.Add(Direction.LEFT);
            }
            else if (Input.GetKey('D') && last != Direction.LEFT && last != Direction.RIGHT)
            {
                dirs.Add(Direction.RIGHT);
            }
        }

        private bool CheckCoords(Vector coords, List<Vector> coordsList)
        {
            foreach (Vector c in coordsList)
            {
                if (coords == c) return true;
            }
            return false;
        }
    }
}
