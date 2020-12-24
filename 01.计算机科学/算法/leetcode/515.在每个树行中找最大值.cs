/*
 * @lc app=leetcode.cn id=515 lang=csharp
 *
 * [515] 在每个树行中找最大值
 *
 * https://leetcode-cn.com/problems/find-largest-value-in-each-tree-row/description/
 *
 * algorithms
 * Medium (62.96%)
 * Likes:    116
 * Dislikes: 0
 * Total Accepted:    22.3K
 * Total Submissions: 35.4K
 * Testcase Example:  '[1,3,2,5,3,null,9]'
 *
 * 您需要在二叉树的每一行中找到最大的值。
 * 
 * 示例：
 * 
 * 
 * 输入: 
 * 
 * ⁠         1
 * ⁠        / \
 * ⁠       3   2
 * ⁠      / \   \  
 * ⁠     5   3   9 
 * 
 * 输出: [1, 3, 9]
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
    public IList<int> LargestValues (TreeNode root) {
        List<int> list = new List<int> ();
        if (root == null) return list;
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);
        while (queue.Count > 0) {
            int count = queue.Count;
            int max = int.MinValue;
            for (int i = 0; i < count; i++) {
                TreeNode tmp = queue.Dequeue ();
                max = Math.Max (tmp.val, max);
                if (tmp.left != null) queue.Enqueue (tmp.left);
                if (tmp.right != null) queue.Enqueue (tmp.right);
            }
            list.Add (max);
        }
        return list;
    }
}
// @lc code=end