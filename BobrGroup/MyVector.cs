using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BobrGroup
{
    static class MyVector
    {
        public static double Angle(Points p1, Points p2, Points p3, Points p4, Points lightpoints)
        {
            var avector = Avector(p1, p2, p3);
            var bvector = Bvector(p1, p2, p3, p4, lightpoints);
            var angle = Acos(VectorMultiplication(avector, bvector) / (VectorLenght(avector) * VectorLenght(bvector)));

            return angle;
        }
        public static double Angle(Points p1, Points p2, Points p3, Points lightpoints)
        {
            Points avector = Avector(p1, p2, p3);
            Points bvector = Bvector(p1, p2, p3, lightpoints);
            double angle = Acos(VectorMultiplication(avector, bvector) / (VectorLenght(avector) * VectorLenght(bvector)));

            return angle;
        }
        public static Points Bvector(Points p1, Points p2, Points p3, Points lightpoint)
        {
            return new Points(lightpoint.X - CenterPoint(p1, p2, p3).X,
                lightpoint.Y - CenterPoint(p1, p2, p3).Y,
                lightpoint.Z - CenterPoint(p1, p2, p3).Z);
        }
        public static Points CenterPoint(Points p1, Points p2, Points p3)
        {
            return new Points((p1.X + p2.X + p3.X) / 3, (p1.Y + p2.Y + p3.Y) / 3, (p1.Z + p2.Z + p3.Z) / 3);
        }
        public static Points Avector(Points p1, Points p2, Points p3)
        {
            return new Points(p1.Y * p2.Z + p2.Y * p3.Z + p3.Y * p1.Z - p2.Y * p1.Z - p3.Y * p2.Z - p1.Y * p3.Z,
                p1.Z * p2.X + p2.Z * p3.X + p3.Z * p1.X - p2.Z * p1.X - p3.Z * p2.X - p1.Z * p3.X,
                p1.X * p2.Y + p2.X * p3.Y + p3.X * p1.Y - p2.X * p1.Y - p3.X * p2.Y - p1.X * p3.Y);
        }
        public static Points Bvector(Points p1, Points p2, Points p3, Points p4, Points lightpoint)
        {
            return new Points(lightpoint.X - CenterPoint(p1, p2, p3, p4).X,
                lightpoint.Y - CenterPoint(p1, p2, p3, p4).Y,
                lightpoint.Z - CenterPoint(p1, p2, p3, p4).Z);
        }
        public static Points CenterPoint(Points p1, Points p2, Points p3, Points p4)
        {
            return new Points((p1.X + p2.X + p3.X + p4.X) / 4, (p1.Y + p2.Y + p3.Y + p4.Y) / 4, (p1.Z + p2.Z + p3.Z + p4.Z) / 4);
        }
        //public static Points CenterPoint(this Points[] p)
        //{
        //    return new Points((p[1].X + p[2].X + p[3].X) / 4, (p.Y + p2.Y + p3.Y + p4.Y) / 4, (p1.Z + p2.Z + p3.Z + p4.Z) / 4);
        //}

        public static double VectorMultiplication(Points vectora, Points vectorb)
        {
            return vectora.X * vectorb.X + vectora.Y * vectorb.Y + vectora.Z * vectorb.Z;
        }
        public static double VectorLenght(Points vector)
        {
            return Sqrt(Pow(vector.X, 2) + Pow(vector.Y, 2) + Pow(vector.Z, 2));
        }
        public static double RadtoDeg(double grad)
        {
            return grad * 180 / Math.PI;
        }
    }
}
