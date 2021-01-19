/*
 * @lc app=leetcode.cn id=543 lang=csharp
 *
 * [543] 二叉树的直径
 *
 * https://leetcode-cn.com/problems/diameter-of-binary-tree/description/
 *
 * algorithms
 * Easy (52.06%)
 * Likes:    588
 * Dislikes: 0
 * Total Accepted:    88K
 * Total Submissions: 169K
 * Testcase Example:  '[1,2,3,4,5]'
 *
 * 给定一棵二叉树，你需要计算它的直径长度。一棵二叉树的直径长度是任意两个结点路径长度中的最大值。这条路径可能穿过也可能不穿过根结点。
 * 
 * 
 * 
 * 示例 :
 * 给定二叉树
 * 
 * ⁠         1
 * ⁠        / \
 * ⁠       2   3
 * ⁠      / \     
 * ⁠     4   5    
 * 
 * 
 * 返回 3, 它的长度是路径 [4,2,1,3] 或者 [5,2,1,3]。
 * 
 * 
 * 
 * 注意：两结点之间的路径长度是以它们之间边的数目表示。
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
    public int DiameterOfBinaryTree (TreeNode root) {
        if (root == null) return 0;
        int left = GetDepth (root.left);
        int right = GetDepth (root.right);

        int l_max = DiameterOfBinaryTree (root.left);
        int r_max = DiameterOfBinaryTree (root.right);

        left += right;
        return Math.Max (left, Math.Max (l_max, r_max));
    }

    private int GetDepth (TreeNode root) {
        if (root == null) return 0;
        int left = GetDepth (root.left);
        int right = GetDepth (root.right);
        return Math.Max (left, right) + 1;
    }
}
// @lc code=end