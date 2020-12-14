/*
 * @lc app=leetcode.cn id=155 lang=csharp
 *
 * [155] 最小栈
 *
 * https://leetcode-cn.com/problems/min-stack/description/
 *
 * algorithms
 * Easy (55.58%)
 * Likes:    709
 * Dislikes: 0
 * Total Accepted:    173.1K
 * Total Submissions: 311.5K
 * Testcase Example:  '["MinStack","push","push","push","getMin","pop","top","getMin"]\n' +
  '[[],[-2],[0],[-3],[],[],[],[]]'
 *
 * 设计一个支持 push ，pop ，top 操作，并能在常数时间内检索到最小元素的栈。
 * 
 * 
 * push(x) —— 将元素 x 推入栈中。
 * pop() —— 删除栈顶的元素。
 * top() —— 获取栈顶元素。
 * getMin() —— 检索栈中的最小元素。
 * 
 * 
 * 
 * 
 * 示例:
 * 
 * 输入：
 * ["MinStack","push","push","push","getMin","pop","top","getMin"]
 * [[],[-2],[0],[-3],[],[],[],[]]
 * 
 * 输出：
 * [null,null,null,null,-3,null,0,-2]
 * 
 * 解释：
 * MinStack minStack = new MinStack();
 * minStack.push(-2);
 * minStack.push(0);
 * minStack.push(-3);
 * minStack.getMin();   --> 返回 -3.
 * minStack.pop();
 * minStack.top();      --> 返回 0.
 * minStack.getMin();   --> 返回 -2.
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * pop、top 和 getMin 操作总是在 非空栈 上调用。
 * 
 * 
 */

// @lc code=start
public class MinStack {
    Stack stack = null;
    public struct Item {
        public int val;
        public int min;
    }
    /** initialize your data structure here. */
    public MinStack () {
        stack = new Stack ();
    }

    public void Push (int x) {
        int min = GetMin ();
        int n = (x > min) ? min : x;
        Item item = new Item ();
        item.val = x;
        item.min = n;
        stack.Push (item);
    }

    public void Pop () {
        stack.Pop ();
    }

    public int Top () {
        Item item = (Item) stack.Peek ();
        return item.val;
    }

    public int GetMin () {
        if (stack.Count == 0) {
            return int.MaxValue;
        } else {
            Item item = (Item) stack.Peek ();
            return item.min;
        }
    }
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(x);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */
// @lc code=end