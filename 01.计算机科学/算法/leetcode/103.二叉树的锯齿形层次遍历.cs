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
    //TODO:
    public IList<IList<int>> ZigzagLevelOrder (TreeNode root) {
        // List<IList<int>> result = new List<IList<int>> ();
        // if (root == null) {
        //     return result;
        // }
        // DFS (root, 0, result);
        // return result;
        return PrintZigzagLevelOrder (root);
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

    //迭代写法
    private IList<IList<int>> PrintZigzagLevelOrder (TreeNode root) {
        List<IList<int>> result = new List<IList<int>> ();
        if (root == null) {
            return result;
        }
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);
        int level = 0;
        while (queue.Count > 0) {
            List<int> list = new List<int> ();
            int count = queue.Count;
            for (int i = 0; i < count; i++) {
                TreeNode tmp = queue.Dequeue ();
                list.Add (tmp.val);
                if (tmp.left != null) {
                    queue.Enqueue (tmp.left);
                }
                if (tmp.right != null) {
                    queue.Enqueue (tmp.right);
                }
            }
            if (level % 2 == 1) list.Reverse ();
            level++;
            result.Add (list);
        }
        return result;
    }

}
// @lc code=end