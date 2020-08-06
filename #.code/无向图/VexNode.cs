/*
 * @Author: delevin.ying 
 * @Date: 2020-07-20 10:27:16 
 * @Last Modified by:   delevin.ying 
 * @Last Modified time: 2020-07-20 10:27:16 
 */
namespace GameCore
{
    /// <summary>
    /// 无向图邻接表的顶点结点类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class VexNode<T>
    {
        private MapNode<T> data; //图的顶点
        private AdjListNode<T> firstAdj; //邻接表的第1个结点

        public MapNode<T> Data
        {
            get { return data; }
            set { data = value; }
        }

        //邻接表的第1个结点属性
        public AdjListNode<T> FirstAdj
        {
            get { return firstAdj; }
            set { firstAdj = value; }
        }

        //构造器
        public VexNode()
        {
            data = null;
            firstAdj = null;
        }

        //构造器
        public VexNode(MapNode<T> nd)
        {
            data = nd;
            firstAdj = null;
        }

        //构造器
        public VexNode(MapNode<T> nd, AdjListNode<T> alNode)
        {
            data = nd;
            firstAdj = alNode;
        }
    }
}