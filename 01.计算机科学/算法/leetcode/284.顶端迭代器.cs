/*
 * @lc app=leetcode.cn id=284 lang=csharp
 *
 * [284] 顶端迭代器
 *
 * https://leetcode-cn.com/problems/peeking-iterator/description/
 *
 * algorithms
 * Medium (72.15%)
 * Likes:    62
 * Dislikes: 0
 * Total Accepted:    6.1K
 * Total Submissions: 8.4K
 * Testcase Example:  '["PeekingIterator","next","peek","next","next","hasNext"]\n' +
  '[[[1,2,3]],[],[],[],[],[]]'
 *
 * 给定一个迭代器类的接口，接口包含两个方法： next() 和 hasNext()。设计并实现一个支持 peek() 操作的顶端迭代器 --
 * 其本质就是把原本应由 next() 方法返回的元素 peek() 出来。
 * 
 * 示例:
 * 
 * 假设迭代器被初始化为列表 [1,2,3]。
 * 
 * 调用 next() 返回 1，得到列表中的第一个元素。
 * 现在调用 peek() 返回 2，下一个元素。在此之后调用 next() 仍然返回 2。
 * 最后一次调用 next() 返回 3，末尾元素。在此之后调用 hasNext() 应该返回 false。
 * 
 * 
 * 进阶：你将如何拓展你的设计？使之变得通用化，从而适应所有的类型，而不只是整数型？
 * 
 */

// @lc code=start
// C# IEnumerator interface reference:
// https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=netframework-4.8

class PeekingIterator {
    //TODO:
    List<int> list;
    // iterators refers to the first element of the array.
    public PeekingIterator (IEnumerator<int> iterator) {
        // initialize any member here.
        list = new List<int> ();
        while (iterator.HasNext ()) {
            list.Add (iterator.Next ());
        }
    }

    // Returns the next element in the iteration without advancing the iterator.
    public int Peek () {
        return list.Count > 0 ? list[0] : -1;
    }

    // Returns the next element in the iteration and advances the iterator.
    public int Next () {
        if (list.Count > 0) {
            int val = list[0];
            list.RemoveAt (0);
            return val;
        } else {
            return -1;
        }
    }

    // Returns false if the iterator is refering to the end of the array of true otherwise.
    public bool HasNext () {
        return list.Count > 0;
    }
}
// @lc code=end