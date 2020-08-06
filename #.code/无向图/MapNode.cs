/*
 * @Author: delevin.ying 
 * @Date: 2020-06-29 16:09:50 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2020-06-29 16:10:52
 */


namespace GameCore
{
    public class MapNode<T>
    {
        private T data;
        private int id;

        public MapNode(T value,int _id)
        {
            data = value;
            id = _id;
        }

        public T Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
    }
}