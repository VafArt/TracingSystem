using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;

namespace TracingSystem.Domain
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public byte[]? PcbLib { get; set; }

        public ICollection<Pcb>? Pcbs { get; set; }
    }
}
