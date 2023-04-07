using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem.Domain
{
    public class DbPoint
    {
        public int Id { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public PointF GetPointF { get { return new PointF(X, Y); } }

        public DbPoint()
        {
            X = 0;
            Y = 0;
        }

        public DbPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public DbPoint(PointF point)
        {
            X = point.X;
            Y = point.Y;
        }
    }
}
