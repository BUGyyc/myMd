/*
 * @lc app=leetcode.cn id=145 lang=csharp
 *
 * [145] 二叉树的后序遍历
 *
 * https://leetcode-cn.com/problems/binary-tree-postorder-traversal/description/
 *
 * algorithms
 * Medium (73.44%)
 * Likes:    470
 * Dislikes: 0
 * Total Accepted:    158.1K
 * Total Submissions: 215.2K
 * Testcase Example:  '[1,null,2,3]'
 *
 * 给定一个二叉树，返回它的 后序 遍历。
 * 
 * 示例:
 * 
 * 输入: [1,null,2,3]  
 * ⁠  1
 * ⁠   \
 * ⁠    2
 * ⁠   /
 * ⁠  3 
 * 
 * 输出: [3,2,1]
 * 
 * 进阶: 递归算法很简单，你可以通过迭代算法完成吗？
 * 
 */

// @lc code=start
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    //Stack迭代
    public IList<int> PostorderTraversal (TreeNode root) {
        return PrintPostorderTraversal (root);
    }

    public IList<int> PrintPostorderTraversal (TreeNode root) {
        List<int> result = new List<int> ();
        if (root == null) {
            return result;
        }
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        while (stack.Count > 0 || root != null) {
            while (root != null) {
                result.Add (root.val);
                stack.Push (root);
                root = root.right;
            }
            root = stack.Pop ();
            root = root.left;
        }
        result.Reverse ();
        return result;
    }
}
// @lc code=end