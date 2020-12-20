/*
 * @lc app=leetcode.cn id=199 lang=csharp
 *
 * [199] 二叉树的右视图
 *
 * https://leetcode-cn.com/problems/binary-tree-right-side-view/description/
 *
 * algorithms
 * Medium (64.41%)
 * Likes:    347
 * Dislikes: 0
 * Total Accepted:    71.8K
 * Total Submissions: 111.4K
 * Testcase Example:  '[1,2,3,null,5,null,4]'
 *
 * 给定一棵二叉树，想象自己站在它的右侧，按照从顶部到底部的顺序，返回从右侧所能看到的节点值。
 * 
 * 示例:
 * 
 * 输入: [1,2,3,null,5,null,4]
 * 输出: [1, 3, 4]
 * 解释:
 * 
 * ⁠  1            <---
 * ⁠/   \
 * 2     3         <---
 * ⁠\     \
 * ⁠ 5     4       <---
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
    //迭代
    public IList<int> RightSideView (TreeNode root) {
        List<int> result = new List<int> ();
        if (root == null) {
            return result;
        }
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        queue.Enqueue (root);
        while (queue.Count > 0) {
            int size = queue.Count;
            for (int i = 0; i < size; i++) {
                TreeNode node = queue.Dequeue ();
                if (node.left != null) {
                    queue.Enqueue (node.left);
                }
                if (node.right != null) {
                    queue.Enqueue (node.right);
                }
                //每一层的最后一个就是最右
                if (i == size - 1) {
                    result.Add (node.val);
                }
            }
        }
        return result;
    }
}
// @lc code=end