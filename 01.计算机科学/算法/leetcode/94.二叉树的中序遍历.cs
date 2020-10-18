/*
 * @lc app=leetcode.cn id=94 lang=csharp
 *
 * [94] 二叉树的中序遍历
 *
 * https://leetcode-cn.com/problems/binary-tree-inorder-traversal/description/
 *
 * algorithms
 * Medium (73.78%)
 * Likes:    749
 * Dislikes: 0
 * Total Accepted:    283K
 * Total Submissions: 383.3K
 * Testcase Example:  '[1,null,2,3]'
 *
 * 给定一个二叉树，返回它的中序 遍历。
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
 * 输出: [1,3,2]
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

using System.Collections.Generic;
public class Solution {
    public IList<int> InorderTraversal (TreeNode root) {
        List<int> list = new List<int> ();
        Stack<TreeNode> stack = new Stack<TreeNode> ();

        while (root != null || stack.Count > 0) {
            while (root != null) {
                stack.Push (root);
                root = root.left;
            }
            root = stack.Pop ();
            list.Add (root.val);
            root = root.right;
        }
        return list;
    }

    private IList<int> func1 (TreeNode root) {
        List<int> list = new List<int> ();
        InOrder (ref list, root);
        return list;
    }

    private void InOrder (ref List<int> list, TreeNode root) {
        if (root == null) {
            return;
        }
        InOrder (ref list, root.left);
        list.Add (root.val);
        InOrder (ref list, root.right);
    }
}
// @lc code=end