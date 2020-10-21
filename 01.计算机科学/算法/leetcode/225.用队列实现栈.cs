/*
 * @lc app=leetcode.cn id=225 lang=csharp
 *
 * [225] 用队列实现栈
 *
 * https://leetcode-cn.com/problems/implement-stack-using-queues/description/
 *
 * algorithms
 * Easy (65.76%)
 * Likes:    232
 * Dislikes: 0
 * Total Accepted:    76.9K
 * Total Submissions: 117K
 * Testcase Example:  '["MyStack","push","push","top","pop","empty"]\n[[],[1],[2],[],[],[]]'
 *
 * 使用队列实现栈的下列操作：
 * 
 * 
 * push(x) -- 元素 x 入栈
 * pop() -- 移除栈顶元素
 * top() -- 获取栈顶元素
 * empty() -- 返回栈是否为空
 * 
 * 
 * 注意:
 * 
 * 
 * 你只能使用队列的基本操作-- 也就是 push to back, peek/pop from front, size, 和 is empty
 * 这些操作是合法的。
 * 你所使用的语言也许不支持队列。 你可以使用 list 或者 deque（双端队列）来模拟一个队列 , 只要是标准的队列操作即可。
 * 你可以假设所有操作都是有效的（例如, 对一个空的栈不会调用 pop 或者 top 操作）。
 * 
 * 
 */

// @lc code=start
public class MyStack
{

    Queue<int> q1;
    Queue<int> q2;
    // int first;

    /** Initialize your data structure here. */
    public MyStack()
    {
        q1 = new Queue<int>();
        q2 = new Queue<int>();
    }

    /** Push element x onto stack. */
    public void Push(int x)
    {
        while (q1.Count > 0)
        {
            int n = q1.Dequeue();
            q2.Enqueue(n);
        }

        q1.Enqueue(x);
        while (q2.Count > 0)
        {
            int n = q2.Dequeue();
            q1.Enqueue(n);
        }
    }

    /** Removes the element on top of the stack and returns that element. */
    public int Pop()
    {
        // while (q1.Count > 1)
        // {
        //     int x = q1.Dequeue();
        //     q2.Enqueue(x);
        // }
        // int p = q1.Dequeue();
        // // if (q2.Count > 1)
        // // {
        // while (q2.Count > 1)
        // {
        //     int x = q2.Dequeue();
        //     q1.Enqueue(x);
        // }
        // int m = q2.Dequeue();
        // first = m;
        // q1.Enqueue(first);
        // // }
        // // else
        // // {
        // //     int m = q2.Dequeue();
        // //     first = m;
        // //     q1.Enqueue(first);
        // // }
        // return p;
        int p = q1.Dequeue();
        return p;
    }

    /** Get the top element. */
    public int Top()
    {
        return q1.Peek();
    }

    /** Returns whether the stack is empty. */
    public bool Empty()
    {
        return q1.Count == 0;
    }
}

/**
 * Your MyStack object will be instantiated and called as such:
 * MyStack obj = new MyStack();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Top();
 * bool param_4 = obj.Empty();
 */
// @lc code=end

