/*
 * @Author: delevin.ying 
 * @Date: 2020-07-15 19:22:13 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2020-07-17 15:39:15
 */
using UnityEngine;

namespace GameCore
{
    public struct VertexStruct
    {
        public uint id;
        public uint mapId;//所属mapId，区分类型
        public float value;
        public string flag;
        public Vector3 position;
    }
}