/*
 * @lc app=leetcode.cn id=103 lang=csharp
 *
 * [103] 二叉树的锯齿形层次遍历
 *
 * https://leetcode-cn.com/problems/binary-tree-zigzag-level-order-traversal/description/
 *
 * algorithms
 * Medium (55.11%)
 * Likes:    282
 * Dislikes: 0
 * Total Accepted:    75.4K
 * Total Submissions: 136.8K
 * Testcase Example:  '[3,9,20,null,null,15,7]'
 *
 * 给定一个二叉树，返回其节点值的锯齿形层次遍历。（即先从左往右，再从右往左进行下一层遍历，以此类推，层与层之间交替进行）。
 * 
 * 例如：
 * 给定二叉树 [3,9,20,null,null,15,7],
 * 
 * ⁠   3
 * ⁠  / \
 * ⁠ 9  20
 * ⁠   /  \
 * ⁠  15   7
 * 
 * 
 * 返回锯齿形层次遍历如下：
 * 
 * [
 * ⁠ [3],
 * ⁠ [20,9],
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
    public IList<IList<int>> ZigzagLevelOrder (TreeNode root) {
        List<IList<int>> result = new List<IList<int>> ();
        if (root == null) {
            return result;
        }

        DFS (root, 0, result);
        return result;
    }


    private void DFS (TreeNode treeNode, int level, List<IList<int>> result) {
        if (level >= result.Count) {
            List<int> list = new List<int> ();
            list.Add (treeNode.val);
            result.Add (list);
        } else {
            if (level % 2 == 0) {
                result[level].Add (treeNode.val);
            } else {
                result[level].Insert (0, treeNode.val);
            }
        }

        if (treeNode.left != null) DFS (treeNode.left, level + 1, result);
        if (treeNode.right != null) DFS (treeNode.right, level + 1, result);
    }

}
// @lc code=end