using System.Collections.Generic;

namespace ShortRouteOptimizer.Models
{
    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; } = new List<string>();
        public int Distance { get; set; }

        public string GetFormattedPath()
        {
            return string.Join(", ", NodeNames);
        }
    }
}
