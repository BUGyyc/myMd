/*
 * @lc app=leetcode.cn id=102 lang=csharp
 *
 * [102] 二叉树的层序遍历
 *
 * https://leetcode-cn.com/problems/binary-tree-level-order-traversal/description/
 *
 * algorithms
 * Medium (63.54%)
 * Likes:    669
 * Dislikes: 0
 * Total Accepted:    206.5K
 * Total Submissions: 325K
 * Testcase Example:  '[3,9,20,null,null,15,7]'
 *
 * 给你一个二叉树，请你返回其按 层序遍历 得到的节点值。 （即逐层地，从左到右访问所有节点）。
 * 
 * 
 * 
 * 示例：
 * 二叉树：[3,9,20,null,null,15,7],
 * 
 * ⁠   3
 * ⁠  / \
 * ⁠ 9  20
 * ⁠   /  \
 * ⁠  15   7
 * 
 * 
 * 返回其层次遍历结果：
 * 
 * [
 * ⁠ [3],
 * ⁠ [9,20],
 * ⁠ [15,7]
 * ]
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
    public IList<IList<int>> LevelOrder (TreeNode root) {
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        List<IList<int>> result = new List<IList<int>> ();
        if (root == null) {
            return result;
        }
        queue.Enqueue (root);
        while (queue.Count > 0) {
            List<int> list = new List<int> ();
            int count = queue.Count;
            for (int i = 0; i < count; i++) {
                TreeNode treeNode = queue.Dequeue ();
                list.Add (treeNode.val);
                if (treeNode.left != null) {
                    queue.Enqueue (treeNode.left);
                }
                if (treeNode.right != null) {
                    queue.Enqueue (treeNode.right);
                }
            }
            result.Add (list);
        }
        return result;
    }
}
// @lc code=end