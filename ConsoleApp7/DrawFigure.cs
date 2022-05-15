using System;
using System.Collections.Generic;
using System.Threading;

namespace TableTennisGame
{
    class DrawFigure
    {
        private static int width = Program.width;
        private static int height = Program.height;
        public static char[,] emptyArray = new char[width, height];
        private static int FIRST_PLAYER_WON = 1, ESC_WAS_PRESSED = 0;
        public static bool firstTry = true;
        public static int firstPlayer = 0, secondPlayer = 0;

        public static List<PointOnScreen> pointsToDelete = new List<PointOnScreen>();
        public static List<PointOnScreen> pointsToDeleteNow = new List<PointOnScreen>();
        private static Random rand = new Random();
        public static Body ball = new Body(0, 0, 0.035, new Vector((double)rand.Next(8, 12) * getRandMinus() / 100, (double)rand.Next(8, 12) * getRandMinus() / 100));

        public static void startMenu()
        {
            Console.WriteLine("The game is for 2 players");
            Console.WriteLine("*** Player on the left moves his platform by pressing W for up or S for down");
            Console.WriteLine("*** Player on the right moves his platform by pressing ArrowUp for up or ArrowDown for down");
            Console.WriteLine("*** To return back to the main menu press ESC");
            Console.WriteLine("To start the game press any key");
            Console.ReadKey();
            Console.Clear();
            Program.game = true;
        }

        private static int getRandMinus()
        {
            int a = rand.Next(0, 2);
            if (a == 1)
            {
                return -1;
            } else
            {
                return 1;
            }
        }

        public static void refresh()
        {
            ball.applyVector();
            putFiguresTogether();
            Thread.Sleep(40);
        }

        public static void restartGame(int reason)
        {
            
            Program.game = false;
            ball = new Body(0, 0, 0.035, new Vector((double)rand.Next(8, 12) * getRandMinus() / 100, (double)rand.Next(8, 12) * getRandMinus() / 100));
            Body.platform1 = 14; Body.platform2 = 14;
            firstTry = true;
            pointsToDelete.Clear();
            pointsToDeleteNow.Clear();
            Console.Clear();
            if (reason != ESC_WAS_PRESSED)
            {
                if (reason == FIRST_PLAYER_WON) { Console.WriteLine("GAME OVER" + "\n" + "FIRST PLAYER WON"); firstPlayer++; }
                else { Console.WriteLine("GAME OVER" + "\n" + "SECOND PLAYER WON"); secondPlayer++; }
                Console.WriteLine("PRESS ANY KEY TO PLAY AGAIN");
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    restartGame(0);
                    return;
                }
                Console.Clear();
                Program.game = true;
            }else
            {
                startMenu();
            }
        }

        public static void putFiguresTogether()
        {
            pointsToDeleteNow = new List<PointOnScreen>(pointsToDelete);
            pointsToDelete.Clear();
            char[,] figure = ball.bodyScreen;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (figure[i, j] == '@')
                    {
                        pointsToDelete.Add(new PointOnScreen(i, j));
                        Console.SetCursorPosition(i, j);
                        Console.Write('@');
                        continue;
                    }
                    else if (i <= 8 && i >= 5)
                    {
                        if (j <= Body.platform1 + 4 && j >= Body.platform1 - 3)
                        {
                            pointsToDelete.Add(new PointOnScreen(i, j));
                            Console.SetCursorPosition(i, j);
                            Console.Write('@');
                        }
                        continue;
                    }
                    else if (i <= width - 5 && i >= width - 8)
                    {
                        if (j <= Body.platform2 + 4 && j >= Body.platform2 - 3)
                        {
                            pointsToDelete.Add(new PointOnScreen(i, j));
                            Console.SetCursorPosition(i, j);
                            Console.Write('@');
                        }
                        continue;
                    }
                }
            }
            for (int i = 0; i < pointsToDeleteNow.Count; i++)
            {
                PointOnScreen cursor = pointsToDeleteNow[i];
                if (!pointsToDelete.Exists(pointOnScreen => pointOnScreen.i == cursor.i && pointOnScreen.j == cursor.j))
                {
                    Console.SetCursorPosition(cursor.i, cursor.j);
                    Console.Write(' ');
                }
            }
        }
    }
}
