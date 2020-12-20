/*
 * @lc app=leetcode.cn id=404 lang=csharp
 *
 * [404] 左叶子之和
 *
 * https://leetcode-cn.com/problems/sum-of-left-leaves/description/
 *
 * algorithms
 * Easy (56.18%)
 * Likes:    249
 * Dislikes: 0
 * Total Accepted:    60.5K
 * Total Submissions: 107.7K
 * Testcase Example:  '[3,9,20,null,null,15,7]'
 *
 * 计算给定二叉树的所有左叶子之和。
 * 
 * 示例：
 * 
 * 
 * ⁠   3
 * ⁠  / \
 * ⁠ 9  20
 * ⁠   /  \
 * ⁠  15   7
 * 
 * 在这个二叉树中，有两个左叶子，分别是 9 和 15，所以返回 24
 * 
 * 
 * 
 */

// @lc code=start
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    //TODO:
    private int result = 0;
    public int SumOfLeftLeaves (TreeNode root) {
        if (root == null) {
            return 0;
        }
        if (root.left != null) {
            GetLeftSum (root.left, true);
        }

        if (root.right != null) {
            GetLeftSum (root.right, false);
        }

        return result;
    }

    private void GetLeftSum (TreeNode root, bool isLeft) {
        if (root == null) {
            return;
        }
        if (isLeft && root.left == null && root.right == null) {
            result += root.val;
        }
        if (root.left != null) {
            GetLeftSum (root.left, true);
        }
        if (root.right != null) {
            GetLeftSum (root.right, false);
        }
    }
}
// @lc code=end