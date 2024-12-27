using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortRouteOptimizer.Models
{
    public class GraphData
    {
        public Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();

        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes)
        {
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var unvisited = new HashSet<string>();

            // Initialize distances
            foreach (var node in graphNodes)
            {
                distances[node.Name] = int.MaxValue;
                unvisited.Add(node.Name);
            }
            distances[fromNodeName] = 0;

            while (unvisited.Count > 0)
            {
                string current = GetMinimumDistanceVertex(distances, unvisited);
                if (current == toNodeName)
                    break;

                unvisited.Remove(current);

                var currentNode = graphNodes.First(n => n.Name == current);
                foreach (var neighbor in currentNode.Connections)
                {
                    if (!unvisited.Contains(neighbor.Key))
                        continue;

                    var alt = distances[current] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = current;
                    }
                }
            }

            // Create result
            var result = new ShortestPathData();
            if (distances[toNodeName] == int.MaxValue)
            {
                return result; // No path found
            }

            // Reconstruct path
            var path = new List<string>();
            var currentVertex = toNodeName;

            while (previous.ContainsKey(currentVertex))
            {
                path.Insert(0, currentVertex);
                currentVertex = previous[currentVertex];
            }
            path.Insert(0, fromNodeName);

            result.NodeNames = path;
            result.Distance = distances[toNodeName];
            return result;
        }

        private string GetMinimumDistanceVertex(Dictionary<string, int> distances, HashSet<string> unvisited)
        {
            int minDistance = int.MaxValue;
            string minVertex = null;

            foreach (var vertex in unvisited)
            {
                if (distances[vertex] < minDistance)
                {
                    minDistance = distances[vertex];
                    minVertex = vertex;
                }
            }

            return minVertex;
        }
    }
}
