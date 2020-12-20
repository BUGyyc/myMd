/*
 * @lc app=leetcode.cn id=114 lang=csharp
 *
 * [114] 二叉树展开为链表
 *
 * https://leetcode-cn.com/problems/flatten-binary-tree-to-linked-list/description/
 *
 * algorithms
 * Medium (71.19%)
 * Likes:    593
 * Dislikes: 0
 * Total Accepted:    87.7K
 * Total Submissions: 123.3K
 * Testcase Example:  '[1,2,5,3,4,null,6]'
 *
 * 给定一个二叉树，原地将它展开为一个单链表。
 * 
 * 
 * 
 * 例如，给定二叉树
 * 
 * ⁠   1
 * ⁠  / \
 * ⁠ 2   5
 * ⁠/ \   \
 * 3   4   6
 * 
 * 将其展开为：
 * 
 * 1
 * ⁠\
 * ⁠ 2
 * ⁠  \
 * ⁠   3
 * ⁠    \
 * ⁠     4
 * ⁠      \
 * ⁠       5
 * ⁠        \
 * ⁠         6
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
    //栈迭代
    public void Flatten (TreeNode root) {
        List<TreeNode> list = new List<TreeNode> ();
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        TreeNode node = root;
        while (stack.Count > 0 || node != null) {
            while (node != null) {
                list.Add (node);
                stack.Push (node);
                node = node.left;
            }
            node = stack.Pop ();
            node = node.right;
        }
        if (list.Count > 0) {
            TreeNode pre = list[0];
            for (int i = 1; i < list.Count; i++) {
                TreeNode curr = list[i];
                pre.left = null;
                pre.right = curr;
                pre = curr;
            }
        }
    }
}
// @lc code=end