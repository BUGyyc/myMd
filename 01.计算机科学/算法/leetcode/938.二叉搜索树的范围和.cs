/*
 * @lc app=leetcode.cn id=938 lang=csharp
 *
 * [938] 二叉搜索树的范围和
 *
 * https://leetcode-cn.com/problems/range-sum-of-bst/description/
 *
 * algorithms
 * Easy (77.84%)
 * Likes:    152
 * Dislikes: 0
 * Total Accepted:    40.6K
 * Total Submissions: 52.1K
 * Testcase Example:  '[10,5,15,3,7,null,18]\n7\n15'
 *
 * 给定二叉搜索树的根结点 root，返回值位于范围 [low, high] 之间的所有结点的值的和。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：root = [10,5,15,3,7,null,18], low = 7, high = 15
 * 输出：32
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：root = [10,5,15,3,7,13,18,1,null,6], low = 6, high = 10
 * 输出：23
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 树中节点数目在范围 [1, 2 * 10^4] 内
 * 1 
 * 1 
 * 所有 Node.val 互不相同
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
    public int RangeSumBST (TreeNode root, int low, int high) {
        if (root == null) return 0;
        List<int> list = new List<int> ();
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        while (root != null || stack.Count > 0) {
            while (root != null) {
                stack.Push (root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop ();
            list.Add (tmp.val);
            root = tmp.right;
        }
        int sum = 0;
        foreach (var item in list) {
            if (item >= low && item <= high) {
                sum += item;
            }
        }
        return sum;
    }
}
// @lc code=end