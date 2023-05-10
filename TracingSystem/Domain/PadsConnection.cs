using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracingSystem.Domain
{
    public class PadsConnection
    {
        public int Id { get; set; }

        public int PcbId { get; set; }

        public int PadFromId { get; set; }

        public int PadToId { get; set; }

        public Pad PadFrom { get; set; }

        public Pad PadTo { get; set; }

        public Pcb Pcb { get; set; }
    }
}
