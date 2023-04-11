using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Domain
{
    public class TracePoint : DbPoint
    {
        public int TraceId { get; set; }

        public Trace Trace { get; set; }
    }
}
