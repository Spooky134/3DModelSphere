using System;
using static System.Math;

namespace BobrGroup
{
    static class Transformation
    {
        //поворот
        public static Points Rotate(this Points point, double alpha, double beta, double gama)
        {
            alpha = DegtoRad(alpha);
            beta = DegtoRad(beta);
            gama = DegtoRad(gama);
            double[,] rot = { { Cos(beta) * Cos(gama),
                    Cos(beta) * Sin(gama),
                    -Sin(beta), 0 },

                { Sin(alpha) * Sin(beta) * Cos(gama) - Cos(alpha) * Sin(gama),
                    Sin(alpha) * Sin(beta) * Sin(gama) + Cos(alpha) * Cos(gama),
                    Sin(alpha) * Cos(beta), 0 },

                { Cos(alpha) * Sin(beta) * Cos(gama) + Sin(alpha) * Sin(gama),
                    Cos(alpha) * Sin(beta) * Sin(gama) - Sin(alpha)* Cos(gama),
                    Cos(alpha)* Cos(beta),0 },

                { 0,0,0,1 } };

            return Points.MultiMatr1(point.p, rot);
        }
        //масштаб
        public static Points Scale(this Points point, double sx, double sy, double sz)
        {
            double[,] scal = { { sx, 0, 0, 0 },
                { 0, sy, 0, 0 },
                { 0, 0, sz, 0 },
                { 0, 0, 0, 1 } };
            return Points.MultiMatr1(point.p, scal);
        }
        //перемещение
        public static Points Move(this Points point, double dx, double dy, double dz)
        {
            double[,] mov = { { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { dx, dy, dz, 1 } };

            return Points.MultiMatr1(point.p, mov);
        }


        //профильная
        public static Points ProfileProjection(this Points point)
        {
            return new Points(point.Z, point.Y, point.X);
        }
        //горизонтальная
        public static Points HorizontalProjection(this Points point)
        {
            return new Points(point.X, point.Z, point.Y);
        }
        //аксонометрическая проекция
        public static Points AxonometricProjection(this Points point, double psi, double fi)
        {
            psi = DegtoRad(psi);
            fi = DegtoRad(fi);
            double[,] matrix = { { Cos(psi), Sin(psi)* Sin(fi), 0, 0 },
                { 0, Cos(fi), 0, 0 },
                { Sin(psi), -Sin(fi)* Cos(psi), 1, 0 },
                { 0, 0, 0, 1 } };

            return Points.MultiMatr1(point.p, matrix);
        }
        //косоугольная проекция
        public static Points ObliqueProjection(this Points point, double l, double alpha)
        {
            alpha = DegtoRad(alpha);
            double[,] oblique ={ { 1,0, 0, 0 },
                                { 0, 1, 0, 0 },
                                { l*Cos(alpha), l*Sin(alpha), 1, 0 },
                                { 0, 0, 0, 1 } };

            return Points.MultiMatr1(point.p, oblique); ;
        }
        //Перспективная
        public static Points PerspectiveProjection(this Points point, double d)
        {
            if ((0 <= point.Z) && (point.Z < 0.1))
            {
                point.Z = 0.1;
            }
            else if (point.Z < 0 && point.Z > -0.1)
            {
                point.Z = -0.1;
            }

            return new Points(point.X / (point.p[0, 2] / d), point.Y / (point.p[0, 2] / d), d);
        }
        //видовое преобразование
        public static Points SpeciesTransformation(this Points point, double teta, double fi, double ro)
        {
            teta = DegtoRad(teta);
            fi = DegtoRad(fi);
            double[,] spe ={ { -Sin(teta),-Cos(fi)*Cos(teta),-Sin(fi)*Cos(teta), 0 },
                { Cos(teta), -Cos(fi)*Sin(teta), -Sin(fi)*Sin(teta), 0 },
                { 0, Sin(fi), -Cos(fi), 0 },
                { 0, 0, ro, 1 } };

            return Points.MultiMatr1(point.p, spe); ;
        }
        //градусы в радианы
        public static double DegtoRad(double grad)
        {
            grad = grad * PI / 180;
            return grad;
        }

       
    }
}
