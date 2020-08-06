/*
 * @Author: delevin.ying 
 * @Date: 2020-07-15 19:21:23 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2020-07-16 16:45:54
 */
using System.Collections.Generic;
namespace GameCore
{
    public class Vertex
    {
        public VertexStruct Data;
        public bool isVisited;
        public List<Vertex> neighbors;
        public Vertex()
        {
            neighbors = new List<Vertex>();
        }

        public void AddNeighbor(Vertex item)
        {
            if (HasNeighbor(item) == false)
            {
                neighbors.Add(item);
            }
        }

        public bool HasNeighbor(Vertex item)
        {
            return neighbors.Contains(item);
        }

        public List<Vertex> GetNeighbors()
        {
            return neighbors;
        }
    }
}