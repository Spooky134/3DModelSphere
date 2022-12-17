namespace BobrGroup
{
    public class Points
    {
        public double[,] p = new double[1, 4] { { 0, 0, 0, 1 } };

        public double X
        {
            get => p[0, 0];
            set => p[0, 0] = value;
        }
        public double Y
        {
            get => p[0, 1];
            set => p[0, 1] = value;
        }
        public double Z
        {
            get => p[0, 2];
            set => p[0, 2] = value;
        }

        public Points(double x, double y, double z)
        {
            p = new double[1, 4] { { x, y, z, 1 } };
        }

        public Points(int x, int y, int z)
        {
            p = new double[1, 4] { { x, y, z, 1 } };
        }
        public static Points operator -(Points point)
        {
            return new Points(-point.X, -point.Y, -point.Z);
        }
        public Points(float x, float y, float z)
        {
            p = new double[1, 4] { { x, y, z, 1 } };
        }

        public Points()
        {
            //p = new double[1, 4] { { X, Y, Z, 1 } };
        }

        //умножение матрицы 4x4  x  4x4
        public static double[,] MultiMatr4(double[,] matrixA, double[,] matrixB)
        {
            var matrixC = new double[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < 4; k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return matrixC;
        }
        //умножение матрицы 1x4  x  4x4
        public static Points MultiMatr1(double[,] matrixA, double[,] matrixB)
        {
            var matrixC = new double[matrixA.GetLength(0), 4];
            for (var i = 0; i < 1; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < 4; k++)
                    {
                        matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return new Points(matrixC[0, 0], matrixC[0, 1], matrixC[0, 2]);
        }
    }
}
