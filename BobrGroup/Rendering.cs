using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BobrGroup
{
    public class Rendering
    {
        private static Bitmap bmp;
        private  Graphics _graph;

        public void Render(Points point1, Points point2)
        {
            _graph.DrawLine(new Pen(Color.Black,1), (float)point1.X, (float)point1.Y, (float)point2.X, (float)point2.Y);
        }
        //полигоны
        public void PolygonFill(Points point1, Points point2, Points point3,Points point4,Color color)
        {
            var points1 = new Point((int)point1.X, (int)point1.Y);
            var points2 = new Point((int)point2.X, (int)point2.Y);
            var points3 = new Point((int)point3.X, (int)point3.Y);
            var points4 = new Point((int)point4.X, (int)point4.Y);
            Point[] curvePoints = { points1, points2, points3,points4 };
            _graph.FillPolygon(new SolidBrush(color), curvePoints);
        }
        public void PolygonFill(Points point1, Points point2, Points point3, Color color)
        {
            var points1 = new Point((int)point1.X, (int)point1.Y);
            var points2 = new Point((int)point2.X, (int)point2.Y);
            var points3 = new Point((int)point3.X, (int)point3.Y);
            Point[] curvePoints = { points1, points2, points3};
            _graph.FillPolygon(new SolidBrush(color), curvePoints);
        }
        public void RenderPolygon(Points point1, Points point2, Points point3, Points point4,Color color)
        {
            var points1 = new Point((int)point1.X, (int)point1.Y);
            var points2 = new Point((int)point2.X, (int)point2.Y);
            var points3 = new Point((int)point3.X, (int)point3.Y);
            var points4 = new Point((int)point4.X, (int)point4.Y);
            Point[] curvePoints = { points1, points2, points3, points4 };
            _graph.DrawPolygon(new Pen(color,1), curvePoints);
        }
        public void RenderPolygon(Points point1, Points point2, Points point3, Color color)
        {
            var points1 = new Point((int)point1.X, (int)point1.Y);
            var points2 = new Point((int)point2.X, (int)point2.Y);
            var points3 = new Point((int)point3.X, (int)point3.Y);
            Point[] curvePoints = { points1, points2, points3 };
            _graph.DrawPolygon(new Pen(color, 1), curvePoints);
        }

        public void Gener(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            _graph = Graphics.FromImage(bmp);
            _graph.SmoothingMode = SmoothingMode.AntiAlias;
            _graph.Clear(Color.Black);
            //расположение фигуры по центру bmp
            _graph.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
            _graph.ScaleTransform(1, -1);
            pictureBox.Image = bmp;
            //_graph = pictureBox.CreateGraphics();
            //_graph.SmoothingMode = SmoothingMode.AntiAlias;
            //_graph.Clear(Color.White);

            //_graph.TranslateTransform(pictureBox.Width / 2, pictureBox.Height / 2);
            //_graph.ScaleTransform(1, -1);
        }
    }
}
