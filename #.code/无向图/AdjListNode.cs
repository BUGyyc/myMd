/*
 * @Author: delevin.ying 
 * @Date: 2020-07-03 14:21:12 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2020-07-03 15:03:17
 */
namespace GameCore
{
    /// <summary>
    /// 无向图邻接表类的实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdjListNode<T>
    {
        private int adjvex;//邻接顶点
        private AdjListNode<T> next;//下一个邻接表结点

        //邻接顶点属性
        public int Adjvex
        {
            get { return adjvex; }
            set { adjvex = value; }
        }

        //下一个邻接表结点属性
        public AdjListNode<T> Next
        {
            get { return next; }
            set { next = value; }
        }

        public AdjListNode(int vex)
        {
            adjvex = vex;
            next = null;
        }
    }
}