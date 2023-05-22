using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TracingSystem.Application.Common.Algorithms
{
    public class Graph
    {
        private Node[] nodes;


        public Graph(int nodesCount)
        {
            nodes = Enumerable.Range(0, nodesCount).Select(z => new Node(z)).ToArray();
        }

        public int Length { get { return nodes.Length; } }

        public bool[,] AdjacencyMatrix
        {
            get
            {
                var matrix = new bool[nodes.Length, nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    var incidentNodes = nodes[i].IncidentNodes.ToArray();
                    for (int j = 0; j < incidentNodes.Count(); j++)
                    {
                        matrix[i, incidentNodes[j].NodeNumber] = true;
                    }
                }
                return matrix;
            }
        }

        public Node this[int index] { get { return nodes[index]; } }

        public Node[] Nodes
        {
            get
            {
                return nodes;
            }
        }

        public void Connect(int index1, int index2)
        {
            Node.Connect(nodes[index1], nodes[index2], this);
        }

        public void Delete(Edge edge)
        {
            Node.Disconnect(edge);
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                return nodes.SelectMany(z => z.IncidentEdges).Distinct();
            }
        }

        public static Graph MakeGraph(params int[] incidentNodes)
        {
            var graph = new Graph(incidentNodes.Max() + 1);
            for (int i = 0; i < incidentNodes.Length - 1; i += 2)
                graph.Connect(incidentNodes[i], incidentNodes[i + 1]);
            return graph;
        }
    }
}
