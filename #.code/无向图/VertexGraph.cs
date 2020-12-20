/*
 * @Author: delevin.ying 
 * @Date: 2020-07-15 19:25:25 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2020-07-17 16:28:13
 */
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class VertexGraph
    {
        //所有的顶点
        private List<Vertex> vertices;
        //所有的边 包含权重值
        private Dictionary<string, float> edges;
        //存储已知的最短路径
        private Dictionary<string, List<Vertex>> paths = null;

        private Dictionary<string, float> pathLen = null;//路径长度

        public static readonly Color[] PointColors = new Color[]{
            Color.yellow,
            Color.blue,
            Color.cyan,
            Color.gray,
            Color.green,
            Color.red,
            Color.black,
            Color.white,
        };

        public VertexGraph()
        {
            vertices = new List<Vertex>();
            edges = new Dictionary<string, float>();
            paths = new Dictionary<string, List<Vertex>>();
            pathLen = new Dictionary<string, float>();
        }

        public void AddVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public List<Vertex> GetVertices()
        {
            return vertices;
        }

        public Vertex GetVertexById(uint id)
        {
            foreach (var item in vertices)
            {
                if (item.Data.id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Vertex GetVertexByTransform(Transform tf)
        {
            foreach (var item in vertices)
            {
                if (item.Data.position == tf.position)
                {
                    return item;
                }
            }
            return null;
        }

        public void AddEdge(Vertex v1, Vertex v2)
        {
            //无向图
            string key1 = GetKey(v1, v2);
            string key2 = GetKey(v2, v1);
            if (HasEdge(key1) || HasEdge(key2))
            {
                Debug.LogError("图中已经包含边");
            }
            else
            {
                edges[key1] = (v1.Data.position - v2.Data.position).magnitude;
            }
        }

        public void DeleteEdge(Vertex v1, Vertex v2)
        {
            string key1 = GetKey(v1, v2);
            string key2 = GetKey(v2, v1);
            if (HasEdge(key1))
            {
                edges[key1] = float.MaxValue;
            }
            else if (HasEdge(key2))
            {
                edges[key2] = float.MaxValue;
            }
            else
            {
                Debug.Log("图中不包含边  " + key1 + "  or " + key2);
            }
        }

        public bool HasEdge(Vertex v1, Vertex v2)
        {
            string key1 = GetKey(v1, v2);
            string key2 = GetKey(v2, v1);
            return HasEdge(key1) || HasEdge(key2);
        }

        public bool HasEdge(string key)
        {
            return edges.ContainsKey(key);
        }

        /// <summary>
        /// 两点的权重值
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public float GetVertexLineValue(Vertex origin, Vertex target)
        {
            string key1 = GetKey(origin, target);
            string key2 = GetKey(target, origin);
            if (edges.ContainsKey(key1))
            {
                return edges[key1];
            }
            else if (edges.ContainsKey(key2))
            {
                return edges[key2];
            }
            else
            {
                return float.MaxValue;
            }
        }

        public float GetMoveCost(Vertex origin, Vertex target)
        {
            string key1 = GetKey(origin, target);
            string key2 = GetKey(target, origin);
            if (pathLen.ContainsKey(key1))
            {
                return pathLen[key1];
            }
            else if (pathLen.ContainsKey(key2))
            {
                return pathLen[key2];
            }
            else
            {
                return float.MaxValue;
            }
        }

        private string GetKey(Vertex v1, Vertex v2)
        {
            return v1.Data.id + "_" + v2.Data.id;
        }

        /// <summary>
        /// 设置两点的权重值
        /// https://www.itread01.com/content/1508047085.html
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetVertexsValue(Vertex origin, Vertex target, float value)
        {
            string key1 = GetKey(origin, target);
            string key2 = GetKey(target, origin);
            if (edges.ContainsKey(key1))
            {
                edges[key1] = value;
            }
            else if (edges.ContainsKey(key2))
            {
                edges[key2] = value;
            }
            else
            {
                edges[key1] = value;
            }
        }

        public void SetMoveCostValue(Vertex origin, Vertex target, float value)
        {
            string key1 = GetKey(origin, target);
            string key2 = GetKey(target, origin);
            if (pathLen.ContainsKey(key1))
            {
                pathLen[key1] = value;
            }
            else if (pathLen.ContainsKey(key2))
            {
                pathLen[key2] = value;
            }
            else
            {
                pathLen[key1] = value;
            }
        }

        public int FindPath(ref List<Vertex> path, Vertex origin, Vertex target)
        {
            string key1 = GetKey(origin, target);
            string key2 = GetKey(target, origin);
            if (paths.ContainsKey(key1))
            {
                foreach (var item in paths[key1])
                {
                    path.Add(item);
                }
                return 1;
            }
            else if (paths.ContainsKey(key2))
            {
                //需要深度拷贝 ， 翻转一下
                foreach (var item in paths[key2])
                {
                    path.Add(item);
                }
                path.Reverse();
                return 2;
            }
            else
            {
                //先进行搜索一次
                //暂存搜索结果
                Dictionary<string, List<Vertex>> tempPaths = new Dictionary<string, List<Vertex>>();
                Dijsktra(ref tempPaths, origin);
                string key = GetKey(origin, target);
                if (tempPaths.ContainsKey(key))
                {
                    foreach (var item in tempPaths[key])
                    {
                        path.Add(item);
                    }
                    //存储已经找过的路径
                    paths.Add(key, path);
                }
                else
                {
                    Debug.LogError("未找到可走路径");
                }
                return 0;
            }
        }

        public void Dijsktra(ref Dictionary<string, List<Vertex>> tempPaths, Vertex start, bool isTest = false)
        {
            ResetVertexList();
            int length = vertices.Count;
            foreach (var item in vertices)
            {
                string key = GetKey(start, item);
                if (tempPaths.ContainsKey(key) == false || tempPaths[key].Count < 1)
                {
                    List<Vertex> path = new List<Vertex>();
                    path.Add(start);
                    tempPaths.Add(key, path);
                }
            }
            start.isVisited = true;
            for (int count = 1; count < length; count++)
            {
                int k = -1;
                float dmin = float.MaxValue;
                for (int i = 0; i < vertices.Count; i++)
                {
                    var item = vertices[i];
                    //GetVertexLineValue
                    if (item.isVisited == false && GetMoveCost(start, item) < dmin)
                    {
                        dmin = GetMoveCost(start, item);
                        k = i;
                    }
                }
                Vertex vk = vertices[k];
                vk.isVisited = true;
                string key = start.Data.id + "_" + vk.Data.id;
                tempPaths[key].Add(vk);

                for (int i = 0; i < length; i++)
                {
                    Vertex vi = vertices[i];
                    if (vi.isVisited == false)
                    {
                        // GetVertexLineValue
                        float compare = GetMoveCost(start, vk) + GetMoveCost(vk, vi);
                        if (compare < GetMoveCost(start, vi))
                        {
                            //跟新最小值  SetVertexsValue
                            SetMoveCostValue(start, vi, compare);
                            string keySI = GetKey(start, vi);
                            string keySK = GetKey(start, vk);
                            tempPaths[keySI].Clear();
                            foreach (var item in tempPaths[keySK])
                            {
                                if (tempPaths[keySI].Contains(item) == false)
                                {
                                    tempPaths[keySI].Add(item);
                                }
                            }

                            if (tempPaths[keySI].Contains(vi) == false)
                            {
                                tempPaths[keySI].Add(vi);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < length; i++)
            {
                string key = GetKey(start, vertices[i]);//start.Data.id + "_" + vertices[i].Data.id;
                List<Vertex> currPath = tempPaths[key];
                List<Vertex> shortPath = new List<Vertex>();
                foreach (var item in currPath)
                {
                    bool has = false;
                    foreach (var v in shortPath)
                    {
                        if (v.Data.id == item.Data.id)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (has == false)
                    {
                        shortPath.Add(item);
                    }
                }

                tempPaths[key] = shortPath;
                if (isTest)
                {
                    string result = "";
                    foreach (var item in shortPath)
                    {
                        result += "->" + item.Data.id;
                    }
                    Debug.Log("从" + start.Data.id + "出发到" + vertices[i].Data.id + "的最短路径为：" + result);
                }
            }
        }

        private void ResetVertexList()
        {
            foreach (var item in vertices)
            {
                item.isVisited = false;
            }
            //清理掉所有
            pathLen.Clear();
            //只存储图的初始边
            foreach (var item in edges)
            {
                pathLen.Add(item.Key, item.Value);
            }
        }

        // private bool hasInitFloyd = false;
        // public bool FindPathByFloyd()
        // {
        //     if (hasInitFloyd == false)
        //     {
        //         hasInitFloyd = true;
        //         InitDataByFloyd();
        //     }
        //     int i, j, k;
        //     for (k = 0; k < vertices.Count; k++)
        //     {
        //         for (i = 0; i < vertices.Count; i++)
        //         {
        //             for (j = 0; j < vertices.Count; j++)
        //             {
        //                 Vertex vi = vertices[i];//GetVertexById(i + 1);
        //                 Vertex vj = vertices[j];//GetVertexById(j + 1);
        //                 Vertex vk = vertices[k];// GetVertexById(k + 1);
        //                 string keyIK = GetKey(vi, vk);
        //                 string keyKj = GetKey(vk, vj);
        //                 string keyIJ = GetKey(vi, vj);
        //                 if (pathLen[keyIK] + pathLen[keyKj] < pathLen[keyIJ])
        //                 {
        //                     if (i == j && pathLen[keyIK] + pathLen[keyKj] == float.MaxValue)
        //                     {
        //                         return false;
        //                     }
        //                     pathLen[keyIJ] = pathLen[keyIK] + pathLen[keyKj];
        //                     paths[keyIJ].Clear();
        //                     foreach (var item in paths[keyIK])
        //                     {
        //                         paths[keyIJ].Add(item);
        //                     }
        //                     foreach (var item in paths[keyKj])
        //                     {
        //                         paths[keyIJ].Add(item);
        //                     }
        //                 }
        //             }
        //         }
        //     }

        //     foreach (var item in paths)
        //     {
        //         string key = item.Key;
        //         string res = "";
        //         foreach (var it in item.Value)
        //         {
        //             res += it.Data.id + " -> ";
        //         }

        //         Debug.LogError("路径搜索 ------》 " + key + "  path " + res);
        //     }

        //     return true;
        // }

        // private void InitDataByFloyd()
        // {
        //     for (int m = 0; m < vertices.Count - 1; m++)
        //     {
        //         Vertex vm = vertices[m];
        //         for (int n = m + 1; n < vertices.Count; n++)
        //         {
        //             Vertex vn = vertices[n];
        //             string key = GetKey(vm, vn);
        //             List<Vertex> list = new List<Vertex>();
        //             list.Add(vm);
        //             paths.Add(key, list);
        //         }
        //     }
        //     for (int i = 0; i < vertices.Count; i++)
        //     {
        //         float value = float.MaxValue;
        //         Vertex vi = vertices[i];
        //         for (int j = 0; j < vertices.Count; j++)
        //         {
        //             Vertex vj = vertices[j];
        //             //是否存在边
        //             if (HasEdge(vi, vj))
        //             {
        //                 value = GetVertexLineValue(vi, vj);
        //             }
        //             else
        //             {
        //                 value = float.MaxValue;
        //             }
        //             string key = GetKey(vi, vj);
        //             pathLen.Add(key, value);
        //         }
        //     }
        // }
    }
}