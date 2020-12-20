/*
 * @lc app=leetcode.cn id=113 lang=csharp
 *
 * [113] 路径总和 II
 *
 * https://leetcode-cn.com/problems/path-sum-ii/description/
 *
 * algorithms
 * Medium (61.06%)
 * Likes:    370
 * Dislikes: 0
 * Total Accepted:    99.3K
 * Total Submissions: 162.5K
 * Testcase Example:  '[5,4,8,11,null,13,4,7,2,null,null,5,1]\n22'
 *
 * 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
 * 
 * 说明: 叶子节点是指没有子节点的节点。
 * 
 * 示例:
 * 给定如下二叉树，以及目标和 sum = 22，
 * 
 * ⁠             5
 * ⁠            / \
 * ⁠           4   8
 * ⁠          /   / \
 * ⁠         11  13  4
 * ⁠        /  \    / \
 * ⁠       7    2  5   1
 * 
 * 
 * 返回:
 * 
 * [
 * ⁠  [5,4,11,2],
 * ⁠  [5,8,4,5]
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
    //递归存储起来
    List<IList<int>> result = new List<IList<int>> ();
    public IList<IList<int>> PathSum (TreeNode root, int sum) {
        List<int> path = new List<int> ();
        Func1 (path, root, sum);
        return result;
    }

    private void Func1 (List<int> path, TreeNode root, int sum) {
        if (root == null) return;
        sum -= root.val;

        List<int> list = new List<int> (path);
        list.Add (root.val);

        if (root.left == null && root.right == null) {
            if (sum == 0) {
                result.Add (list);
                return;
            }
        }

        if (root.left != null) {
            Func1 (list, root.left, sum);
        }

        if (root.right != null) {
            Func1 (list, root.right, sum);
        }
    }
}
// @lc code=end