using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Domain
{
    public class Pad
    {
        public int Id { get; set; }

        public int ElementId { get; set; }

        public int? ConnectedPadId { get; set; }

        public float CenterX { get; set; }

        public float CenterY { get; set; }

        public float SizeX { get; set; }

        public float SizeY { get; set; }

        public Pad? ConnectedPad { get; set; }

        public Element Element { get; set; }
    }
}
