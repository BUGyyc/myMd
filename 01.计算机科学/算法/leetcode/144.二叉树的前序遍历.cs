/*
 * @lc app=leetcode.cn id=144 lang=csharp
 *
 * [144] 二叉树的前序遍历
 *
 * https://leetcode-cn.com/problems/binary-tree-preorder-traversal/description/
 *
 * algorithms
 * Medium (68.41%)
 * Likes:    451
 * Dislikes: 0
 * Total Accepted:    222.6K
 * Total Submissions: 325.4K
 * Testcase Example:  '[1,null,2,3]'
 *
 * 给你二叉树的根节点 root ，返回它节点值的 前序 遍历。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：root = [1,null,2,3]
 * 输出：[1,2,3]
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：root = []
 * 输出：[]
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：root = [1]
 * 输出：[1]
 * 
 * 
 * 示例 4：
 * 
 * 
 * 输入：root = [1,2]
 * 输出：[1,2]
 * 
 * 
 * 示例 5：
 * 
 * 
 * 输入：root = [1,null,2]
 * 输出：[1,2]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 树中节点数目在范围 [0, 100] 内
 * -100 
 * 
 * 
 * 
 * 
 * 进阶：递归算法很简单，你可以通过迭代算法完成吗？
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
    public IList<int> PreorderTraversal (TreeNode root) {
        List<int> result = new List<int> ();
        // PreorderTraversalFunc1(ref result, root);
        // PreorderTraversalFunc2(ref result, root);
        if (root == null) {
            return result;
        }
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        // stack.Push (root);
        while (root != null || stack.Count > 0) {
            while (root != null) {
                result.Add (root.val);
                stack.Push (root);
                root = root.left;
            }
            TreeNode curr = stack.Pop ();
            root = curr.right;
        }

        return result;
    }

    private void PreorderTraversalFunc1 (ref List<int> result, TreeNode root) {
        if (root == null) return;
        result.Add (root.val);
        if (root.left != null) PreorderTraversalFunc1 (ref result, root.left);
        if (root.right != null) PreorderTraversalFunc1 (ref result, root.right);
    }

    private void PreorderTraversalFunc2 (ref List<int> result, TreeNode root) {
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        if (root != null) {
            stack.Push (root);
        }
        while (stack.Count > 0) {
            TreeNode node = stack.Pop ();
            result.Add (node.val);
            if (node.right != null) {
                stack.Push (node.right);
            }
            if (node.left != null) {
                stack.Push (node.left);
            }
        }
    }

}
// @lc code=end