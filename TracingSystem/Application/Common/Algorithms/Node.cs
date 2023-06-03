using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TracingSystem.Application.Common.Algorithms
{
    public class Node
    {
        readonly List<Edge> edges = new List<Edge>();
        public readonly int NodeNumber;
        public Color Color { get; set; }
        public List<Point> Coordinates { get; set; }

        public Node(int number)
        {
            NodeNumber = number;
            Coordinates = new List<Point>();
        }

        public IEnumerable<Node> IncidentNodes
        {
            get
            {
                return edges.Select(z => z.OtherNode(this));
            }
        }
        public IEnumerable<Edge> IncidentEdges
        {
            get
            {
                foreach (var e in edges) yield return e;
            }
        }
        public static Edge Connect(Node node1, Node node2, Graph graph)
        {
            if (!graph.Nodes.Contains(node1) || !graph.Nodes.Contains(node2)) throw new ArgumentException();

            var edge1 = graph.Edges.Where(edge => (edge.From.NodeNumber == node1.NodeNumber && edge.To.NodeNumber == node2.NodeNumber) || (edge.From.NodeNumber == node2.NodeNumber && edge.To.NodeNumber == node1.NodeNumber));
            if (edge1.Count() != 0) return new Edge(node1, node2);

            var edge = new Edge(node1, node2);
            node1.edges.Add(edge);
            node2.edges.Add(edge);
            return edge;
        }
        public static void Disconnect(Edge edge)
        {
            edge.From.edges.Remove(edge);
            edge.To.edges.Remove(edge);
        }
    }
}
