/*
 * @lc app=leetcode.cn id=653 lang=csharp
 *
 * [653] 两数之和 IV - 输入 BST
 *
 * https://leetcode-cn.com/problems/two-sum-iv-input-is-a-bst/description/
 *
 * algorithms
 * Easy (57.60%)
 * Likes:    212
 * Dislikes: 0
 * Total Accepted:    26.2K
 * Total Submissions: 45.4K
 * Testcase Example:  '[5,3,6,2,4,null,7]\n9'
 *
 * 给定一个二叉搜索树和一个目标结果，如果 BST 中存在两个元素且它们的和等于给定的目标结果，则返回 true。
 * 
 * 案例 1:
 * 
 * 
 * 输入: 
 * ⁠   5
 * ⁠  / \
 * ⁠ 3   6
 * ⁠/ \   \
 * 2   4   7
 * 
 * Target = 9
 * 
 * 输出: True
 * 
 * 
 * 
 * 
 * 案例 2:
 * 
 * 
 * 输入: 
 * ⁠   5
 * ⁠  / \
 * ⁠ 3   6
 * ⁠/ \   \
 * 2   4   7
 * 
 * Target = 28
 * 
 * 输出: False
 * 
 * 
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
    public bool FindTarget (TreeNode root, int k) {
        if (root == null) return false;
        List<int> list = new List<int> ();
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        while (stack.Count > 0 || root != null) {
            while (root != null) {
                stack.Push (root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop ();
            list.Add (tmp.val);
            root = tmp.right;
        }
        int i = 0, j = list.Count - 1;
        while (i < j) {
            if (list[i] + list[j] == k) {
                return true;
            } else if (list[i] + list[j] > k) {
                j--;
            } else {
                i++;
            }
        }
        return false;
    }
}
// @lc code=end