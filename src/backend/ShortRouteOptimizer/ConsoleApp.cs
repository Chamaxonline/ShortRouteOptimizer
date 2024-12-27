using ShortRouteOptimizer.Models;
using System;

namespace ShortRouteOptimizer
{
    public class ConsoleApp
    {
        public static void RunConsoleApp()
        {
            var graphData = new GraphData();
            InitializeGraph(graphData);

            while (true)
            {
                Console.WriteLine("\nShortest Path Calculator");
                Console.WriteLine("----------------------");
                Console.Write("Enter FROM node (or 'exit' to quit): ");
                string fromNode = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (fromNode.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    break;

                Console.Write("Enter TO node: ");
                string toNode = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (!graphData.Nodes.ContainsKey(fromNode) || !graphData.Nodes.ContainsKey(toNode))
                {
                    Console.WriteLine("Invalid node names. Please try again.");
                    continue;
                }

                var result = graphData.ShortestPath(fromNode, toNode, graphData.Nodes.Values.ToList());

                // Display results in the required format
                Console.WriteLine($"\n> fromNodeName = \"{fromNode}\", toNodeName = \"{toNode}\": {string.Join(", ", result.NodeNames)}");
                Console.WriteLine($"> Total Distance: {result.Distance}");
            }
        }

        private static void InitializeGraph(GraphData graphData)
        {
            // Initialize with directional connections as per the image
            AddNode(graphData, "A", new Dictionary<string, int> { { "B", 4 }, { "C", 6 } });
            AddNode(graphData, "B", new Dictionary<string, int> { { "F", 2 } });  // Note: B->E is not bidirectional
            AddNode(graphData, "C", new Dictionary<string, int> { { "D", 8 } });
            AddNode(graphData, "D", new Dictionary<string, int> { { "G", 1 } });
            AddNode(graphData, "E", new Dictionary<string, int> { { "B", 2 }, { "F", 3 }, { "G", 4 } });
            AddNode(graphData, "F", new Dictionary<string, int> { { "H", 6 } });
            AddNode(graphData, "G", new Dictionary<string, int> { { "H", 5 }, { "I", 8 } });
            AddNode(graphData, "H", new Dictionary<string, int> { { "I", 5 } });
            AddNode(graphData, "I", new Dictionary<string, int> { });
        }

        private static void AddNode(GraphData graphData, string name, Dictionary<string, int> connections)
        {
            graphData.Nodes[name] = new Node
            {
                Name = name,
                Connections = connections
            };
        }
    }
}
