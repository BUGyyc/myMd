/*
 * @lc app=leetcode.cn id=700 lang=csharp
 *
 * [700] 二叉搜索树中的搜索
 *
 * https://leetcode-cn.com/problems/search-in-a-binary-search-tree/description/
 *
 * algorithms
 * Easy (75.07%)
 * Likes:    105
 * Dislikes: 0
 * Total Accepted:    45.9K
 * Total Submissions: 61.1K
 * Testcase Example:  '[4,2,7,1,3]\n2'
 *
 * 给定二叉搜索树（BST）的根节点和一个值。 你需要在BST中找到节点值等于给定值的节点。 返回以该节点为根的子树。 如果节点不存在，则返回 NULL。
 * 
 * 例如，
 * 
 * 
 * 给定二叉搜索树:
 * 
 * ⁠       4
 * ⁠      / \
 * ⁠     2   7
 * ⁠    / \
 * ⁠   1   3
 * 
 * 和值: 2
 * 
 * 
 * 你应该返回如下子树:
 * 
 * 
 * ⁠     2     
 * ⁠    / \   
 * ⁠   1   3
 * 
 * 
 * 在上述示例中，如果要找的值是 5，但因为没有节点值为 5，我们应该返回 NULL。
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
    public TreeNode SearchBST (TreeNode root, int val) {
        if (root == null) return null;
        if (root.val == val) {
            return root;
        } else if (root.val > val && root.left != null) {
            return SearchBST (root.left, val);
        } else if (root.val < val && root.right != null) {
            return SearchBST (root.right, val);
        }
        return null;
    }
}
// @lc code=end