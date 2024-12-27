using Microsoft.AspNetCore.Mvc;
using ShortRouteOptimizer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShortRouteOptimizer.Controllers
{
    /// <summary>
    /// Controller for finding shortest paths in a graph
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class GraphController : ControllerBase
    {
        private readonly GraphData _graphData;

        public GraphController()
        {
            _graphData = new GraphData();
            InitializeGraph();
        }

        private void InitializeGraph()
        {           
            AddNode("A", new Dictionary<string, int> { { "B", 4 }, { "C", 6 } });
            AddNode("B", new Dictionary<string, int> { { "F", 2 } });  // Note: B->E is not bidirectional
            AddNode("C", new Dictionary<string, int> { { "D", 8 } });
            AddNode("D", new Dictionary<string, int> { { "G", 1 } });
            AddNode("E", new Dictionary<string, int> { { "B", 2 }, { "F", 3 }, { "G", 4 } });
            AddNode("F", new Dictionary<string, int> { { "H", 6 } });
            AddNode("G", new Dictionary<string, int> { { "H", 5 }, { "I", 8 } });
            AddNode("H", new Dictionary<string, int> { { "I", 5 } });
            AddNode("I", new Dictionary<string, int> { });
        }

        private void AddNode(string name, Dictionary<string, int> connections)
        {
            _graphData.Nodes[name] = new Node
            {
                Name = name,
                Connections = connections
            };
        }

      
        [HttpGet("shortestPath")]
        public ActionResult<object> GetShortestPath(
            [Required][RegularExpression("[A-I]")] string from,
            [Required][RegularExpression("[A-I]")] string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                return BadRequest("Both 'from' and 'to' nodes must be specified");
            }

            if (!_graphData.Nodes.ContainsKey(from) || !_graphData.Nodes.ContainsKey(to))
            {
                return BadRequest("Invalid node names specified");
            }

            var result = _graphData.ShortestPath(from, to, _graphData.Nodes.Values.ToList());
            
            return Ok(new
            {
                Path = $"fromNodeName = \"{from}\", toNodeName = \"{to}\": {result.GetFormattedPath()}",
                TotalDistance = $"Total Distance: {result.Distance}"
            });
        }
    }
}
