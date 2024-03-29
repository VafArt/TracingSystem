﻿using OriginalCircuit.AltiumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;

namespace TracingSystem.Application.Controls
{
    public class ElementControl : PictureBox
    {
        public PcbComponent PcbComponent { get; set; }

        public Element Element { get; set; }
    }
}
