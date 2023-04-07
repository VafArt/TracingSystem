using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;

namespace TracingSystem.Domain
{
    public class Layer
    {
        public int Id { get; set; }

        public int PcbId { get;set; }

        public int Number { get; set; }

        public ICollection<Trace>? Traces { get; set; }

        public Pcb Pcb { get; set; }
    }
}
