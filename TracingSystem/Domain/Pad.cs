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
        public Guid Id { get; set; }

        public int ElementId { get; set; }

        public Guid ConnectedPadId { get; set; }

        public float CenterX { get; set; }

        public float CenterY { get; set; }

        public float SizeX { get; set; }

        public float SizeY { get; set; }

        public Element Element { get; set; }
    }
}
