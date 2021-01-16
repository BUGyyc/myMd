/*
 * @lc app=leetcode.cn id=590 lang=csharp
 *
 * [590] N叉树的后序遍历
 *
 * https://leetcode-cn.com/problems/n-ary-tree-postorder-traversal/description/
 *
 * algorithms
 * Easy (75.20%)
 * Likes:    121
 * Dislikes: 0
 * Total Accepted:    44.4K
 * Total Submissions: 59.1K
 * Testcase Example:  '[1,null,3,2,4,null,5,6]'
 *
 * 给定一个 N 叉树，返回其节点值的后序遍历。
 * 
 * 例如，给定一个 3叉树 :
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 返回其后序遍历: [5,6,3,2,4,1].
 * 
 * 
 * 
 * 说明: 递归法很简单，你可以使用迭代法完成此题吗?
 */

// @lc code=start
/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> children;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, IList<Node> _children) {
        val = _val;
        children = _children;
    }
}
*/

public class Solution {
    public IList<int> Postorder (Node root) {
        List<int> result = new List<int> ();
        if (root == null) return result;
        Stack<Node> stack = new Stack<Node> ();
        stack.Push (root);
        while (stack.Count > 0) {
            Node tmp = stack.Pop ();
            result.Add (tmp.val);
            foreach (var item in tmp.children) {
                stack.Push (item);
            }
        }
        result.Reverse();
        return result;
    }
}
// @lc code=end