using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BobrGroup
{
    class Polygon
    {
        public List<Points> poly = new List<Points>();
        public Color color=Color.Coral;

        public Polygon(Points[] points,Color color)
        {
            for(var i=0; i<points.GetLength(0); i++)
                poly.Add(points[i]);
            this.color = color;
        }
        public Points CenterPoint()
        {
            return new Points((poly[0].X + poly[1].X + poly[2].X + poly[3].X) / 4,
                              (poly[0].Y + poly[1].Y + poly[2].Y + poly[3].Y) / 4,
                              (poly[0].Z + poly[1].Z + poly[2].Z + poly[3].Z) / 4);
        }
    }
}
