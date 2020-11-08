/*
 * @lc app=leetcode.cn id=146 lang=csharp
 *
 * [146] LRU缓存机制
 *
 * https://leetcode-cn.com/problems/lru-cache/description/
 *
 * algorithms
 * Medium (51.06%)
 * Likes:    967
 * Dislikes: 0
 * Total Accepted:    108.7K
 * Total Submissions: 212.8K
 * Testcase Example:  '["LRUCache","put","put","get","put","get","put","get","get","get"]\n' +
  '[[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]'
 *
 * 运用你所掌握的数据结构，设计和实现一个  LRU (最近最少使用) 缓存机制。它应该支持以下操作： 获取数据 get 和 写入数据 put 。
 * 
 * 获取数据 get(key) - 如果关键字 (key) 存在于缓存中，则获取关键字的值（总是正数），否则返回 -1。
 * 写入数据 put(key, value) -
 * 如果关键字已经存在，则变更其数据值；如果关键字不存在，则插入该组「关键字/值」。当缓存容量达到上限时，它应该在写入新数据之前删除最久未使用的数据值，从而为新的数据值留出空间。
 * 
 * 
 * 
 * 进阶:
 * 
 * 你是否可以在 O(1) 时间复杂度内完成这两种操作？
 * 
 * 
 * 
 * 示例:
 * 
 * LRUCache cache = new LRUCache( 2 /* 缓存容量 */ //);
//  * 
//  * cache.put(1, 1);
//  * cache.put(2, 2);
//  * cache.get(1);       // 返回  1
//  * cache.put(3, 3);    // 该操作会使得关键字 2 作废
//  * cache.get(2);       // 返回 -1 (未找到)
//  * cache.put(4, 4);    // 该操作会使得关键字 1 作废
//  * cache.get(1);       // 返回 -1 (未找到)
//  * cache.get(3);       // 返回  3
//  * cache.get(4);       // 返回  4
//  * 
//  * 
//  */

// @lc code=start
public class LRUCache {
    Dictionary<int, int> proDic = null;
    Dictionary<int, int> dic = null;
    int Max = 0;
    int pro = 1;
    int count = 0;
    public LRUCache (int capacity) {
        Max = capacity;
        count = 0;
        proDic = new Dictionary<int, int> ();
        dic = new Dictionary<int, int> ();
    }

    public int Get (int key) {
        if (dic.ContainsKey (key)) {
            proDic[key] = pro++;
            return dic[key];
        }
        return -1;
    }

    public void Put (int key, int value) {
        if (dic.ContainsKey (key)) {
            //修改值
            dic[key] = value;
            proDic[key] = pro++;
        } else {
            //不存在需要添加
            dic.Add (key, value);
            proDic.Add (key, pro++);
            if (count < Max) {
                //未达到最大数量
                count++;
            } else {
                //已经达到最大数量
                //需要移除多余的
                CheckRemoveLast ();
            }
        }
    }

    private void CheckRemoveLast () {
        int min = int.MaxValue;
        int minKey = -1;
        foreach (var item in proDic) {
            if (min >= item.Value) {
                min = item.Value;
                minKey = item.Key;
            }
        }
        dic.Remove (minKey);
        proDic.Remove (minKey);
    }
}

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */
// @lc code=end