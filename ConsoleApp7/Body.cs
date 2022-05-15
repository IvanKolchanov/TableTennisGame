using System;

namespace TableTennisGame
{
    public class Body
    {
        private static int width = Program.width;
        private static int height = Program.height;
        private static DateTime dateTimeWas = DateTime.Now;
        public static int platform1 = 14, platform2 = 14;

        public Vector vector = new Vector(0, 0);
        private double x0, y0, radius;
        public double X0 { get { return x0; } private set { x0 = Coordinates.normalizeX(value); } }
        public double Y0 { get { return y0; } set { y0 = Coordinates.normalizeY(value); } }
        public double Radius { get { return radius; } set { radius = Coordinates.normalizeX(value); radiusNormToY = Coordinates.normalizeY(value); } }

        private double radiusNormToY;

        public char[,] bodyScreen = new char[width, height];
        public Body(double x0, double y0, double radius, Vector vector)
        {
            this.vector.x = vector.x;
            this.vector.y = vector.y;
            X0 = x0;
            Y0 = y0;
            Radius = radius;
            if (radiusNormToY == 0)
            {
                radiusNormToY = (double)2 / height;
            }
            for (int j = height - 1; j >= 0; j--)
            {
                double y = Coordinates.jToY(j);
                for (int i = 0; i < width; i++)
                {
                    double x = Coordinates.iToX(i);
                    if (Math.Pow(x - this.x0, 2) + Math.Pow(y - this.y0, 2) <= this.radius) bodyScreen[i, j] = '@';
                    else bodyScreen[i, j] = ' ';
                }
            }
        }

        public void applyVector()
        {
            double deltaTime = (DateTime.Now - dateTimeWas).TotalMilliseconds / 45;
            dateTimeWas = DateTime.Now;
            if (DrawFigure.firstTry)
            {
                deltaTime = 1;
                DrawFigure.firstTry = false;
            }
            if (Math.Abs(vector.x) < 0.17)
            {
                vector.x *= 1.0003;
            }
            if (Math.Abs(vector.y) < 0.17)
            {
                vector.y *= 1.0003;
            }
            X0 += vector.x * deltaTime;
            Y0 += vector.y * deltaTime;


            double leftPlatformX = Coordinates.iToX(8);
            double RightPlatformX = Coordinates.iToX(width - 8);
            if (Y0 <= -1)
            {
                vector.y *= -1;
                double travel = -1 - Y0;
                Y0 = -1 + travel + radiusNormToY;
            }
            else if (Y0 >= 1)
            {
                vector.y *= -1;
                double travel = 1 - Y0;
                Y0 = 1 + travel - radiusNormToY;
            }
            if (X0 - Radius <= leftPlatformX)
            {
                if (Y0 + radiusNormToY >= Coordinates.jToY(platform1 + 4) && Y0 - radiusNormToY <= Coordinates.jToY(platform1 - 3))
                {
                    double travel = leftPlatformX - X0;
                    X0 = leftPlatformX + travel + Radius;
                    vector.x *= -1;
                }
                else
                {
                    DrawFigure.restartGame(2);
                    return;
                }
            }
            if (X0 + Radius >= RightPlatformX)
            {
                if (Y0 + radiusNormToY >= Coordinates.jToY(platform2 + 4) && Y0 - radiusNormToY <= Coordinates.jToY(platform2 - 3))
                {
                    double travel = RightPlatformX - X0;
                    X0 = RightPlatformX + travel - Radius;
                    vector.x *= -1;
                }
                else
                {
                    DrawFigure.restartGame(1);
                    return;
                }
            }
            updateScreen();
        }

        public void updateScreen()
        {
            for (int j = height - 1; j >= 0; j--)
            {
                double y = Coordinates.jToY(j);
                for (int i = 0; i < width; i++)
                {
                    double x = Coordinates.iToX(i);
                    if (Math.Pow(x - X0, 2) + Math.Pow(y - Y0, 2) <= radius) bodyScreen[i, j] = '@';
                    else bodyScreen[i, j] = ' ';
                }
            }
            Console.SetCursorPosition(59, 2);
            Console.Write(DrawFigure.firstPlayer + " " + DrawFigure.secondPlayer);
        }
    }
}
