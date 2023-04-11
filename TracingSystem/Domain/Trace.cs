using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Domain
{
    public class Trace
    {
        public int Id { get; set; }

        public int LayerId { get; set; }

        public int Length { get; set; }

        public ICollection<TracePoint>? DirectionChangingCoords { get; set; }

        public Layer Layer { get; set; }
    }
}
