/*
 * @lc app=leetcode.cn id=669 lang=csharp
 *
 * [669] 修剪二叉搜索树
 *
 * https://leetcode-cn.com/problems/trim-a-binary-search-tree/description/
 *
 * algorithms
 * Easy (67.12%)
 * Likes:    337
 * Dislikes: 0
 * Total Accepted:    22.3K
 * Total Submissions: 33.3K
 * Testcase Example:  '[1,0,2]\n1\n2'
 *
 * 给你二叉搜索树的根节点 root ，同时给定最小边界low 和最大边界 high。通过修剪二叉搜索树，使得所有节点的值在[low,
 * high]中。修剪树不应该改变保留在树中的元素的相对结构（即，如果没有被移除，原有的父代子代关系都应当保留）。 可以证明，存在唯一的答案。
 * 
 * 所以结果应当返回修剪好的二叉搜索树的新的根节点。注意，根节点可能会根据给定的边界发生改变。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：root = [1,0,2], low = 1, high = 2
 * 输出：[1,null,2]
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：root = [3,0,4,null,2,null,null,1], low = 1, high = 3
 * 输出：[3,2,null,1]
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：root = [1], low = 1, high = 2
 * 输出：[1]
 * 
 * 
 * 示例 4：
 * 
 * 
 * 输入：root = [1,null,2], low = 1, high = 3
 * 输出：[1,null,2]
 * 
 * 
 * 示例 5：
 * 
 * 
 * 输入：root = [1,null,2], low = 2, high = 4
 * 输出：[2]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 树中节点数在范围 [1, 10^4] 内
 * 0 
 * 树中每个节点的值都是唯一的
 * 题目数据保证输入是一棵有效的二叉搜索树
 * 0 
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
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public TreeNode TrimBST (TreeNode root, int low, int high) {
        if (root == null) return root;
        if (root.val < low) return TrimBST (root.right, low, high);
        if (root.val > high) return TrimBST (root.left, low, high);
        root.left = TrimBST (root.left, low, high);
        root.right = TrimBST (root.right, low, high);
        return root;
    }
}
// @lc code=end