using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BobrGroup
{
    public partial class Form1 : Form
    {
        Spher spher;

        // 1-2D;
         // 2-профильная 
         // 3-горизонтальная
         // 3-горизонтальная
         // 4-аксонометрическая
         // 5-косоугольная
         // 6-перспективная
    

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) { }
        private void Button_Click(object sender, EventArgs e)
        {
            DataUpdate();
            var button = (ButtonBase)sender;
            switch (button.Text)
            {
                case "Draw":
                    spher = new Spher(Data.Radius, Data.Latitude, Data.Longitude);
                    spher.RenderFigure(picture);
                    break;
                case "Update":
                    spher.RenderFigure(picture);
                    break;
                case "Rotate":
                    spher.Rotate(Data.Alpha, Data.Beta, Data.Gama);
                    spher.RenderFigure(picture);
                    break;
                case "Move":
                    spher.Move(Data.Dx, Data.Dy, Data.Dz);
                    spher.RenderFigure(picture);
                    break;
                case "Scale":
                    spher.Scale(Data.Sx, Data.Sy, Data.Sz);
                    spher.RenderFigure(picture);
                    break;
                case "Lines":
                    Data.FDraw = 1;
                    break;
                case "Polygon":
                    Data.FDraw = 2;
                    break;
                case "Nothing":
                    Data.FProj = 1;
                    break;
                case "Horizontal":
                    Data.FProj = 2;
                    break;
                case "Profile":
                    Data.FProj = 3;
                    break;
                case "Axonometric":
                    Data.FProj = 4;
                    break;
                case "Oblique":
                    Data.FProj = 5;
                    break;
                case "Perspective":
                    Data.FProj = 6;
                    break;
            };
        }

        private void DataUpdate()
        {
            Data.Radius = double.Parse(Radius.Text);
            Data.Longitude = int.Parse(longitude.Text);
            Data.Latitude = int.Parse(latitude.Text);
            Data.Alpha = double.Parse(alpha.Text);
            Data.Beta = double.Parse(beta.Text);
            Data.Gama = double.Parse(gama.Text);
            Data.Dx = double.Parse(Dx.Text);
            Data.Dy = double.Parse(Dy.Text);
            Data.Dz = double.Parse(Dz.Text);
            Data.Sx = double.Parse(Sx.Text);
            Data.Sy = double.Parse(Sy.Text);
            Data.Sz = double.Parse(Sz.Text);
            Data.Psi = double.Parse(Psi.Text);
            Data.Fi = double.Parse(Fi.Text);
            Data.L = double.Parse(l.Text);
            Data.A = double.Parse(a.Text);
            Data.D = double.Parse(d.Text);
            Data.Teta = double.Parse(teta.Text);
            Data.F = double.Parse(f.Text);
            Data.Ro = double.Parse(ro.Text);
            Data.X = double.Parse(X.Text);
            Data.Y = double.Parse(Y.Text);
            Data.Z = double.Parse(Z.Text);
            Data.kd = double.Parse(kd.Text);
            Data.Il = double.Parse(Il.Text);
            Data.Ia = double.Parse(Ia.Text);
        }
    }
}
