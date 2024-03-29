﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;

namespace TracingSystem.Domain
{
    public class Pcb
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int TraceWidth { get; set; }

        public int TracePadding { get; set; }

        public ICollection<PadsConnection>? PadsConnections { get; set; }

        public ICollection<Layer>? Layers { get; set; }

        public Project Project { get; set; }
    }
}
