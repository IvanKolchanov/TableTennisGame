using System;
using System.Threading.Tasks;

namespace TableTennisGame
{
    class Program
    {
        public static int width = 120;
        public static int height = 30;
        public static char[,] screen = new char[width, height];
        public static bool game = false, ESC_PRESSED = false;

        private static void setup()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width;
            Console.Title = "Pong";
            for (int j = height - 1; j >= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    screen[i, j] = ' ';
                    DrawFigure.emptyArray[i, j] = ' ';
                }
            }
        }


        static void Main(string[] args)
        {
            setup();
            DrawFigure.startMenu();
            while (game)
            {
                DrawFigure.refresh();
                if (Console.KeyAvailable)
                {
                    Task.Factory.StartNew(() =>
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;
                        if (game)
                        {
                            switch (key)
                            {
                                case ConsoleKey.UpArrow:
                                    Body.platform2 -= 3;
                                    Body.platform2 = Math.Max(3, Body.platform2);
                                    break;
                                case ConsoleKey.DownArrow:
                                    Body.platform2 += 3;
                                    Body.platform2 = Math.Min(height - 5, Body.platform2);
                                    break;
                                case ConsoleKey.W:
                                    Body.platform1 -= 3;
                                    Body.platform1 = Math.Max(3, Body.platform1);
                                    break;
                                case ConsoleKey.S:
                                    Body.platform1 += 3;
                                    Body.platform1 = Math.Min(height - 5, Body.platform1);
                                    break;
                                case ConsoleKey.Escape:
                                    ESC_PRESSED = true;
                                    break;
                            }
                        }
                    });
                }
                
                if (ESC_PRESSED)
                {
                    DrawFigure.restartGame(0);
                    ESC_PRESSED = false;
                }
            }
            
        }
    }
}
