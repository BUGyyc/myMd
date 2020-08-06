/*
 * @Author: delevin.ying 
 * @Date: 2020-08-06 15:50:13 
 * @Last Modified by:   delevin.ying 
 * @Last Modified time: 2020-08-06 15:50:13 
 */

namespace GameCore
{
    public interface IMap<T>
    {
        int GetNumOfVertex();
        int GetNumOfEdge();
        void SetEdge(MapNode<T> node1, MapNode<T> node2, float v);
        void DelEdge(MapNode<T> node1, MapNode<T> node2);
        bool IsEdge(MapNode<T> node1, MapNode<T> node2);
    }
}