/*
 * @lc app=leetcode.cn id=993 lang=csharp
 *
 * [993] 二叉树的堂兄弟节点
 *
 * https://leetcode-cn.com/problems/cousins-in-binary-tree/description/
 *
 * algorithms
 * Easy (52.18%)
 * Likes:    115
 * Dislikes: 0
 * Total Accepted:    15.8K
 * Total Submissions: 30.3K
 * Testcase Example:  '[1,2,3,4]\n4\n3'
 *
 * 在二叉树中，根节点位于深度 0 处，每个深度为 k 的节点的子节点位于深度 k+1 处。
 * 
 * 如果二叉树的两个节点深度相同，但父节点不同，则它们是一对堂兄弟节点。
 * 
 * 我们给出了具有唯一值的二叉树的根节点 root，以及树中两个不同节点的值 x 和 y。
 * 
 * 只有与值 x 和 y 对应的节点是堂兄弟节点时，才返回 true。否则，返回 false。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：root = [1,2,3,4], x = 4, y = 3
 * 输出：false
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：root = [1,2,3,null,4,null,5], x = 5, y = 4
 * 输出：true
 * 
 * 
 * 示例 3：
 * 
 * 
 * 
 * 输入：root = [1,2,3,null,4], x = 2, y = 3
 * 输出：false
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 二叉树的节点数介于 2 到 100 之间。
 * 每个节点的值都是唯一的、范围为 1 到 100 的整数。
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
    Dictionary<int, int> levelDic = new Dictionary<int, int> ();
    Dictionary<int, TreeNode> parentDic = new Dictionary<int, TreeNode> ();
    public bool IsCousins (TreeNode root, int x, int y) {
        Dfs (root, null);
        return (levelDic[x] == levelDic[y]) && (parentDic[x] != parentDic[y]);
    }

    private void Dfs (TreeNode root, TreeNode parent) {
        if (root != null) {
            levelDic.Add (root.val, parent == null?0 : levelDic[parent.val] + 1);
            parentDic.Add (root.val, parent);
            Dfs (root.left, root);
            Dfs (root.right, root);
        }
    }
}
// @lc code=end