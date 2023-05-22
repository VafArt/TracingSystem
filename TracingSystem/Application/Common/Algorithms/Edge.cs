using System;
using System.Collections.Generic;
using System.Text;

namespace TracingSystem.Application.Common.Algorithms
{
    public class Edge
    {
        public readonly Node From;
        public readonly Node To;
        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
        public bool IsIncident(Node node)
        {
            return From == node || To == node;
        }
        public Node OtherNode(Node node)
        {
            if (!IsIncident(node)) throw new ArgumentException();
            if (From == node) return To;
            return From;
        }
    }
}
