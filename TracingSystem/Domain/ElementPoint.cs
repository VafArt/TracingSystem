using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Domain
{
    public class ElementPoint : DbPoint
    {
        public int ElementId { get; set; }

        public Element Element { get; set; }

        public ElementPoint() : base() { }

        public ElementPoint(float x, float y) : base(x, y) { }

        public ElementPoint(PointF point) : base(point) { }
    }
}
