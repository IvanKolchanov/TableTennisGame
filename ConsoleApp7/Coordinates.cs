

namespace TableTennisGame
{
    class Coordinates
    {
        private static int width = Program.width;
        private static int height = Program.height;
        private static double aspect = (double)width / height;
        private static double pixelAspect = (double)8 / 16;
        public static double normalizeX(double z)
        {
            return (double)((int)(z * (height * aspect * pixelAspect / 2))) / (height * aspect * pixelAspect / 2);
        }

        public static double normalizeY(double z)
        {
            return (double)((int)(z * (height / 2))) / (height / 2);
        }

        public static double iToX(int i)
        {
            double x = ((double)i / width) * 2 - 1;
            x *= aspect * pixelAspect;
            return x;
        }

        public static double jToY(int j)
        {
            double y = ((double)(height - 1 - j) / height) * 2 - 1;
            return y;
        }
    }
}
