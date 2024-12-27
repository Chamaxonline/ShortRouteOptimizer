using System.Collections.Generic;

namespace ShortRouteOptimizer.Models
{
    public class Node
    {
        public string Name { get; set; }
        public Dictionary<string, int> Connections { get; set; } = new Dictionary<string, int>();
    }
}
