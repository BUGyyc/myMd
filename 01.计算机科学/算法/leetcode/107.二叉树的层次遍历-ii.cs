/*
 * @lc app=leetcode.cn id=107 lang=csharp
 *
 * [107] 二叉树的层次遍历 II
 *
 * https://leetcode-cn.com/problems/binary-tree-level-order-traversal-ii/description/
 *
 * algorithms
 * Easy (67.65%)
 * Likes:    349
 * Dislikes: 0
 * Total Accepted:    104.9K
 * Total Submissions: 155K
 * Testcase Example:  '[3,9,20,null,null,15,7]'
 *
 * 给定一个二叉树，返回其节点值自底向上的层次遍历。 （即按从叶子节点所在层到根节点所在的层，逐层从左向右遍历）
 * 
 * 例如：
 * 给定二叉树 [3,9,20,null,null,15,7],
 * 
 * ⁠   3
 * ⁠  / \
 * ⁠ 9  20
 * ⁠   /  \
 * ⁠  15   7
 * 
 * 
 * 返回其自底向上的层次遍历为：
 * 
 * [
 * ⁠ [15,7],
 * ⁠ [9,20],
 * ⁠ [3]
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
    //list 旋转
    public IList<IList<int>> LevelOrderBottom (TreeNode root) {
        return PrintLevelOrderBottom (root);
    }

    //双队列解法
    public IList<IList<int>> LevelOrderBottom2 (TreeNode root) {
        IList<IList<int>> result = new List<IList<int>> ();
        //队列用来放每一层不为null的节点
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        //用来临时存放每次更新queue前的上一层所有节点
        Queue<TreeNode> tempQueue = new Queue<TreeNode> ();

        if (root != null) {
            queue.Enqueue (root);
        }
        while (queue.Count > 0) {
            var list = new List<int> ();
            while (queue.Count > 0) {
                var element = queue.Dequeue ();
                //逐个添加该层节点
                list.Add (element.val);
                tempQueue.Enqueue (element);
            }
            result.Add (list);
            while (tempQueue.Count > 0) {
                //放入下一层不为空的节点
                var element = tempQueue.Dequeue ();
                if (element.left != null) {
                    queue.Enqueue (element.left);
                }
                if (element.right != null) {
                    queue.Enqueue (element.right);
                }
            }
        }
        return result.Reverse ().ToList ();
    }

    public IList<IList<int>> Func1 (TreeNode root) {
        List<IList<int>> result = new List<IList<int>> ();
        if (root == null) {
            return result;
        }
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        Queue<TreeNode> tempQueue = new Queue<TreeNode> ();
        queue.Enqueue (root);
        while (queue.Count > 0) {
            List<int> list = new List<int> ();
            for (int i = 0; i < queue.Count; i++) {
                TreeNode treeNode = queue.Dequeue ();
                list.Add (treeNode.val);
                tempQueue.Enqueue (treeNode);
            }
            result.Add (list);
            while (tempQueue.Count > 0) {
                TreeNode treeNode = tempQueue.Dequeue ();
                if (treeNode.left != null) {
                    queue.Enqueue (treeNode.left);
                }
                if (treeNode.right != null) {
                    queue.Enqueue (treeNode.right);
                }
            }
        }
        result.Reverse ();
        return result;
    }

    public IList<IList<int>> PrintLevelOrderBottom (TreeNode root) {
        List<IList<int>> result = new List<IList<int>> ();
        if (root == null) return result;
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);
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
            result.Add (list);
        }
        result.Reverse ();
        return result;
    }
}
// @lc code=end