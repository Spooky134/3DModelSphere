using System;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Drawing.Drawing2D;
using static System.Math;

namespace BobrGroup
{
    public class Spher
    {
        Rendering r=new Rendering();
        //массив для точек
        private Points[,] Sph;
        private Points[,] Buffer;
        //private List<Polygon> polygon = new List<Polygon>();
        private Points viewpoint = new Points(0, 0, 10000);
        private Points lightpoint = new Points(5000, 0, 0);
        private List<Points[]> polygonList = new List<Points[]>();
        List<Color> colorlist = new List<Color>(); 
        double ia = 127; 
        double ka = 1;
        double il = 127; 
        double kd = 1;


        //входные данные 
        public int Lati { get; set; }
        public int Longi { get; set; }
        public double Rad { get; set; }

        public Spher(double radius, int latitude, int longitude)
        {
            Rad = radius;
            Lati = latitude;
            Longi = longitude;
            Init();
        }

        //заполнение массива точками
        public void Init()
        {
            Buffer=new Points[Lati,Longi];
            Sph = new Points[Lati, Longi];
            for (var i = 0; i < Lati; i++)
                for (var j = 0; j < Longi; j++)
                    Sph[i, j] = Expansion(Rad, i * 360 / Lati, j * 360 / Longi);
        }
        //парметрическая формула шара
        public Points Expansion(double radius, double teta, double fi)
        {
            var point = new Points
            {
                X = (radius * Sin(teta * PI / 180)) * Cos(fi * PI / 180),
                Y = (radius * Sin(teta * PI / 180)) * Sin(fi * PI / 180),
                Z = radius * Cos(teta * PI / 180)
            };

            return point;
        }

        public void Rotate(double alpha,double beta,double gama)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Sph[i, j] = Sph[i, j].Rotate(alpha, beta, gama);
        }
        public void Scale(double sx, double sy, double sz)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Sph[i, j] = Sph[i, j].Scale(sx, sy, sz);
        }
        public void Move(double dx, double dy, double dz)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Sph[i, j] = Sph[i,j].Move(dx, dy, dz);
        }

        public void RenderFigure(PictureBox picture)
        {
            Projection();
            switch (Data.FDraw)
            {
                case 1:
                    Render(picture);
                    break;
                case 2:
                    RenderPolygon(picture);
                    break;
            };
        }
        public void Render(PictureBox picture)
        {
            r.Gener(picture);
            for (var i = 1; i < Sph.GetLength(0) / 2 + 1; i++)
            {
                for (var j = 1; j < Sph.GetLength(1); j++)
                {
                    r.RenderPolygon(Buffer[i, j - 1], Buffer[i - 1, j - 1], Buffer[i - 1, j], Buffer[i, j], Color.DodgerBlue);
                }
                r.RenderPolygon(Buffer[i, Sph.GetLength(1) - 1], Buffer[i - 1, Sph.GetLength(1) - 1], Buffer[i - 1, 0], Buffer[i, 0], Color.DodgerBlue);
            }
        }
        public void RenderPolygon(PictureBox picture)
        {
            r.Gener(picture);
             for (var i=0; i < polygonList.Count; i++)
             {
                 if (polygonList[i].Length == 3)
                 {
                     r.PolygonFill(polygonList[i][0], polygonList[i][1], polygonList[i][2],colorlist[i]);
                 }
                 else
                 {
                     r.PolygonFill(polygonList[i][0], polygonList[i][1], polygonList[i][2], polygonList[i][3], colorlist[i]);
                 }
             }
        }

     

        public void Projection()
        {
            lightpoint= new Points(Data.X,Data.Y,Data.Z);
            kd = Data.kd;
            il = Data.Il;
            ia = Data.Ia;
            switch (Data.FProj)
            {
                case 1:
                    Array.Copy(Sph, Buffer, Sph.Length);
                    CreatePolygon();
                    Painting();
                    RobertsAlgorithm();
                    break;
                case 2:
                    Horizontal();
                    break;
                case 3:
                    Profile();
                    break;
                case 4:
                    Axonometric(Data.Psi, Data.Fi);
                    break;
                case 5:
                    Oblique(Data.L, Data.A);
                    break;
                case 6:
                    Array.Copy(Sph, Buffer, Sph.Length);
                    Perspective(Data.D,Data.Teta,Data.F,Data.Ro);
                    break;
            };
        }
        public void Profile()
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Sph[i, j] = Sph[i, j].Rotate(0, 0, 0);

            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Sph[i,j].ProfileProjection();
            lightpoint = lightpoint.ProfileProjection();
            CreatePolygon();
            Painting();
            RobertsAlgorithm();
        }
        public void Horizontal()
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Sph[i, j].HorizontalProjection();
            lightpoint = lightpoint.HorizontalProjection();
            CreatePolygon();
            Painting();
            RobertsAlgorithm();
        }
        public void Axonometric(double psi, double fi)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Sph[i, j].AxonometricProjection(psi,fi);
            CreatePolygon();
            Painting();
            RobertsAlgorithm();
        }
        public void Oblique(double l, double alpha)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Sph[i, j].ObliqueProjection(l, alpha);
            CreatePolygon();
            Painting();
            RobertsAlgorithm();
        }
        //public void Perspective(double d, double teta, double fi, double ro)
        //{
        //    Species(teta, fi, ro);
        //    CreatePolygon();
        //    Painting();
        //    for (var i = 0; i < polygonList.Count; i++)
        //    {
        //        if (polygonList[i].GetLength(0) == 3)
        //        {
        //            polygonList[i][0] = polygonList[i][0].PerspectiveProjection(d);
        //            polygonList[i][1] = polygonList[i][1].PerspectiveProjection(d);
        //            polygonList[i][2] = polygonList[i][2].PerspectiveProjection(d);
        //        }
        //        else
        //        {
        //            polygonList[i][0] = polygonList[i][0].PerspectiveProjection(d);
        //            polygonList[i][1] = polygonList[i][1].PerspectiveProjection(d);
        //            polygonList[i][2] = polygonList[i][2].PerspectiveProjection(d);
        //            polygonList[i][3] = polygonList[i][3].PerspectiveProjection(d);
        //        }
        //    }
        //    RobertsAlgorithm();
            
        //    for (var i = 0; i < Sph.GetLength(0); i++)
        //    for (var j = 0; j < Sph.GetLength(1); j++)
        //        Buffer[i, j] = Buffer[i, j].PerspectiveProjection(d);
        //}
        public void Perspective(double d, double teta, double fi, double ro)
        {
           
            CreatePolygon();
            Painting();
            for (var i = 0; i < polygonList.Count; i++)
            {
                if (polygonList[i].GetLength(0) == 3)
                {
                    polygonList[i][0] = polygonList[i][0].SpeciesTransformation(teta, fi, ro);
                    polygonList[i][1] = polygonList[i][1].SpeciesTransformation(teta, fi, ro);
                    polygonList[i][2] = polygonList[i][2].SpeciesTransformation(teta, fi, ro);
                }
                else
                {
                    polygonList[i][0] = polygonList[i][0].SpeciesTransformation(teta, fi, ro);
                    polygonList[i][1] = polygonList[i][1].SpeciesTransformation(teta, fi, ro);
                    polygonList[i][2] = polygonList[i][2].SpeciesTransformation(teta, fi, ro);
                    polygonList[i][3] = polygonList[i][3].SpeciesTransformation(teta, fi, ro);
                }
            }
            for (var i = 0; i < polygonList.Count; i++)
            {
                if (polygonList[i].GetLength(0) == 3)
                {
                    polygonList[i][0] = polygonList[i][0].PerspectiveProjection(d);
                    polygonList[i][1] = polygonList[i][1].PerspectiveProjection(d);
                    polygonList[i][2] = polygonList[i][2].PerspectiveProjection(d);
                }
                else
                {
                    polygonList[i][0] = polygonList[i][0].PerspectiveProjection(d);
                    polygonList[i][1] = polygonList[i][1].PerspectiveProjection(d);
                    polygonList[i][2] = polygonList[i][2].PerspectiveProjection(d);
                    polygonList[i][3] = polygonList[i][3].PerspectiveProjection(d);
                }
            }
            RobertsAlgorithm();

            Species(teta, fi, ro);
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Buffer[i, j].PerspectiveProjection(d);
        }
        public void Species(double teta,double fi, double ro)
        {
            for (var i = 0; i < Sph.GetLength(0); i++)
            for (var j = 0; j < Sph.GetLength(1); j++)
                Buffer[i, j] = Sph[i, j].SpeciesTransformation(teta, fi, ro);
             //lightpoint = -lightpoint;
        }

        public Points PolarToDecart(double ro, double teta, double fi)
        {
            return new Points(ro * Sin(teta) * Cos(fi), ro * Sin(teta) * Sin(fi), ro * Cos(teta));
        }

        private static Color Light(Color color, float factor)
        {
            byte r = (byte)((color.R * factor));
            byte g = (byte)((color.G * factor));
            byte b = (byte)((color.B * factor));
            return Color.FromArgb( r, g, b);
        }

        public void Painting()
        {
            for (var i = 0; i < polygonList.Count; i++)
            {
                var cosLight = Cos(MyVector.Angle(polygonList[i][0], polygonList[i][1], polygonList[i][2], lightpoint));
                var intensity = ((float)(ia * ka + (il * kd * cosLight) / 255));
                colorlist.Add( Light(Color.Blue, intensity));
            }
        }
        public void RobertsAlgorithm()
        {
            double cosView;
            for (var i = polygonList.Count - 1; i >= 0; i--)
            {
                cosView = MyVector.Angle(polygonList[i][0], polygonList[i][1], polygonList[i][2], viewpoint);
                if (RadtoDeg(cosView) > 90 || RadtoDeg(cosView) < 0)
                {
                    polygonList.RemoveAt(i);
                    colorlist.RemoveAt(i);
                }
            }
        }
        public void CreatePolygon()
        {
            polygonList.Clear();
            colorlist.Clear();
            //шапка
            for (var j = 1; j < Sph.GetLength(1); j++)
            {
                polygonList.Add(new[] { Buffer[1, j],Buffer[1 - 1, j - 1],  Buffer[1, j - 1]});
            }
            polygonList.Add(new[] { Buffer[1, 0], Buffer[1 - 1, Sph.GetLength(1) - 1], Buffer[1, Sph.GetLength(1) - 1]});
            //остальное
            for (var i = 2; i < Sph.GetLength(0) / 2 + 1; i++)
            {
                for (var j = 1; j < Sph.GetLength(1); j++)
                {
                    polygonList.Add(new[] { Buffer[i, j], Buffer[i - 1, j], Buffer[i - 1, j - 1], Buffer[i, j - 1]   });
                }
                polygonList.Add(new[] { Buffer[i, 0], Buffer[i - 1, 0], Buffer[i - 1, Sph.GetLength(1) - 1], Buffer[i, Sph.GetLength(1) - 1]});
            }
        }
        public static double RadtoDeg(double grad)
        {
            return grad * 180 / Math.PI;
        }

    }
}
