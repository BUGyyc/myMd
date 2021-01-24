/*
 * @lc app=leetcode.cn id=965 lang=csharp
 *
 * [965] 单值二叉树
 *
 * https://leetcode-cn.com/problems/univalued-binary-tree/description/
 *
 * algorithms
 * Easy (68.54%)
 * Likes:    69
 * Dislikes: 0
 * Total Accepted:    24K
 * Total Submissions: 35K
 * Testcase Example:  '[1,1,1,1,1,null,1]'
 *
 * 如果二叉树每个节点都具有相同的值，那么该二叉树就是单值二叉树。
 * 
 * 只有给定的树是单值二叉树时，才返回 true；否则返回 false。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 
 * 输入：[1,1,1,1,1,null,1]
 * 输出：true
 * 
 * 
 * 示例 2：
 * 
 * 
 * 
 * 输入：[2,2,2,5,2]
 * 输出：false
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 给定树的节点数范围是 [1, 100]。
 * 每个节点的值都是整数，范围为 [0, 99] 。
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
    public bool IsUnivalTree (TreeNode root) {
        if (root == null) return true;
        int val = root.val;
        return Check (root.left, val) && Check (root.right, val);
    }

    private bool Check (TreeNode root, int val) {
        if(root == null)return true;
        if (root.val != val) {
            return false;
        }
        bool checkLeft = true;
        bool checkRight = true;
        if (root.left != null) {
            checkLeft = Check (root.left, val);
        }

        if (root.right != null) {
            checkRight = Check (root.right, val);
        }
        return checkLeft && checkRight;
    }
}
// @lc code=end