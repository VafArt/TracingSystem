﻿using OriginalCircuit.AltiumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Domain
{
    public class Element
    {
        public int Id { get; set; }

        public int LayerId { get; set; }

        public string Name { get; set; }

        public ElementPoint Location { get; set; }

        public ICollection<ElementPoint>? PadsCoords { get; set; }

        public byte[]? Image { get; set; }

        public Layer Layer { get; set; }
    }
}
