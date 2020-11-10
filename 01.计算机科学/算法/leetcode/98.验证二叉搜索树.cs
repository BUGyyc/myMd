/*
 * @lc app=leetcode.cn id=98 lang=csharp
 *
 * [98] 验证二叉搜索树
 *
 * https://leetcode-cn.com/problems/validate-binary-search-tree/description/
 *
 * algorithms
 * Medium (32.64%)
 * Likes:    817
 * Dislikes: 0
 * Total Accepted:    184.1K
 * Total Submissions: 564.1K
 * Testcase Example:  '[2,1,3]'
 *
 * 给定一个二叉树，判断其是否是一个有效的二叉搜索树。
 * 
 * 假设一个二叉搜索树具有如下特征：
 * 
 * 
 * 节点的左子树只包含小于当前节点的数。
 * 节点的右子树只包含大于当前节点的数。
 * 所有左子树和右子树自身必须也是二叉搜索树。
 * 
 * 
 * 示例 1:
 * 
 * 输入:
 * ⁠   2
 * ⁠  / \
 * ⁠ 1   3
 * 输出: true
 * 
 * 
 * 示例 2:
 * 
 * 输入:
 * ⁠   5
 * ⁠  / \
 * ⁠ 1   4
 * / \
 * 3   6
 * 输出: false
 * 解释: 输入为: [5,1,4,null,null,3,6]。
 * 根节点的值为 5 ，但是其右子节点值为 4 。
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
    public bool IsValidBST (TreeNode root) {
        return CompareFunc (root, 0, 0, false, false);
    }

    private bool CompareFunc (TreeNode root, int low, int up, bool hasLow, bool hasUp) {
        if (root == null) {
            return true;
        }

        int val = root.val;
        if (hasLow && val <= low) {
            return false;
        }

        if (hasUp && val >= up) {
            return false;
        }

        if (CompareFunc (root.left, low, val, hasLow, true) == false) {
            return false;
        }

        if (CompareFunc (root.right, val, up, true, hasUp) == false) {
            return false;
        }

        return true;
    }
}
// @lc code=end