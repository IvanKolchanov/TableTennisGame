using System;

namespace TableTennisGame
{
    public class Vector
    {
        public double x, y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void multiply(double k)
        {
            x *= k;
            y *= k;
        }

        public double getLength()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public void add(Vector vector1)
        {
            x += vector1.x;
            y += vector1.y;
        }

        public Vector copy()
        {
            return new Vector(x, y);
        }

    }
}
