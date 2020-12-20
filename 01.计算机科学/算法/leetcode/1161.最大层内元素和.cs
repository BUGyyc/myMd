/*
 * @lc app=leetcode.cn id=1161 lang=csharp
 *
 * [1161] 最大层内元素和
 *
 * https://leetcode-cn.com/problems/maximum-level-sum-of-a-binary-tree/description/
 *
 * algorithms
 * Medium (66.83%)
 * Likes:    33
 * Dislikes: 0
 * Total Accepted:    7.3K
 * Total Submissions: 11K
 * Testcase Example:  '[1,7,0,7,-8,null,null]'
 *
 * 给你一个二叉树的根节点 root。设根节点位于二叉树的第 1 层，而根节点的子节点位于第 2 层，依此类推。
 * 
 * 请你找出层内元素之和 最大 的那几层（可能只有一层）的层号，并返回其中 最小 的那个。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 
 * 输入：root = [1,7,0,7,-8,null,null]
 * 输出：2
 * 解释：
 * 第 1 层各元素之和为 1，
 * 第 2 层各元素之和为 7 + 0 = 7，
 * 第 3 层各元素之和为 7 + -8 = -1，
 * 所以我们返回第 2 层的层号，它的层内元素之和最大。
 * 
 * 
 * 示例 2：
 * 
 * 输入：root = [989,null,10250,98693,-89388,null,null,null,-32127]
 * 输出：2
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 树中的节点数介于 1 和 10^4 之间
 * -10^5 <= node.val <= 10^5
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
    //TODO:
    public int MaxLevelSum (TreeNode root) {
        if (root == null) return 0;
        //层序遍历
        Queue<TreeNode> queue = new Queue<TreeNode> ();
        Queue<TreeNode> tmp = new Queue<TreeNode> ();
        queue.Enqueue (root);
        int max = int.MinValue;
        int curValue = 0;
        int maxLevel = 0;
        int level = 1;
        while (queue.Count > 0) {

            curValue = 0;

            while (queue.Count > 0) {
                TreeNode cur = queue.Dequeue ();
                curValue += cur.val;
                tmp.Enqueue (cur);
            }

            while (tmp.Count > 0) {
                TreeNode cur = tmp.Dequeue ();
                if (cur.left != null) {
                    queue.Enqueue (cur.left);
                }
                if (cur.right != null) {
                    queue.Enqueue (cur.right);
                }
            }

            //max = Math.Max(max,curValue);
            if (curValue > max) {
                max = curValue;
                maxLevel = level;
            }
            level++;
        }
        return maxLevel;
    }
}
// @lc code=end