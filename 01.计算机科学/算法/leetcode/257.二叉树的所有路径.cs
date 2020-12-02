/*
 * @lc app=leetcode.cn id=257 lang=csharp
 *
 * [257] 二叉树的所有路径
 *
 * https://leetcode-cn.com/problems/binary-tree-paths/description/
 *
 * algorithms
 * Easy (66.17%)
 * Likes:    392
 * Dislikes: 0
 * Total Accepted:    82.6K
 * Total Submissions: 124.8K
 * Testcase Example:  '[1,2,3,null,5]'
 *
 * 给定一个二叉树，返回所有从根节点到叶子节点的路径。
 * 
 * 说明: 叶子节点是指没有子节点的节点。
 * 
 * 示例:
 * 
 * 输入:
 * 
 * ⁠  1
 * ⁠/   \
 * 2     3
 * ⁠\
 * ⁠ 5
 * 
 * 输出: ["1->2->5", "1->3"]
 * 
 * 解释: 所有根节点到叶子节点的路径为: 1->2->5, 1->3
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
    public IList<string> BinaryTreePaths (TreeNode root) {
        List<string> result = new List<string> ();
        Func1 (ref result, root, "");
        return result;
    }

    private void Func1 (ref List<string> result, TreeNode root, string path) {
        if (root == null) {
            return;
        }

        StringBuilder sb = new StringBuilder (path);
        sb.Append (root.val.ToString ());

        if (root.left == null && root.right == null) {
            result.Add (sb.ToString ());
            return;
        }
        sb.Append ("->");
        if (root.left != null) {
            Func1 (ref result, root.left, sb.ToString ());
        }

        if (root.right != null) {
            Func1 (ref result, root.right, sb.ToString ());
        }

    }
}
// @lc code=end