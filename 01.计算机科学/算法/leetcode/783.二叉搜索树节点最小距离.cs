/*
 * @lc app=leetcode.cn id=783 lang=csharp
 *
 * [783] 二叉搜索树节点最小距离
 *
 * https://leetcode-cn.com/problems/minimum-distance-between-bst-nodes/description/
 *
 * algorithms
 * Easy (56.09%)
 * Likes:    107
 * Dislikes: 0
 * Total Accepted:    23.9K
 * Total Submissions: 42.6K
 * Testcase Example:  '[4,2,6,1,3,null,null]'
 *
 * 给定一个二叉搜索树的根节点 root，返回树中任意两节点的差的最小值。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入: root = [4,2,6,1,3,null,null]
 * 输出: 1
 * 解释:
 * 注意，root是树节点对象(TreeNode object)，而不是数组。
 * 
 * 给定的树 [4,2,6,1,3,null,null] 可表示为下图:
 * 
 * ⁠         4
 * ⁠       /   \
 * ⁠     2      6
 * ⁠    / \    
 * ⁠   1   3  
 * 
 * 最小的差值是 1, 它是节点1和节点2的差值, 也是节点3和节点2的差值。
 * 
 * 
 * 
 * 注意：
 * 
 * 
 * 二叉树的大小范围在 2 到 100。
 * 二叉树总是有效的，每个节点的值都是整数，且不重复。
 * 本题与 530：https://leetcode-cn.com/problems/minimum-absolute-difference-in-bst/
 * 相同
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

    // int minValue = int.MaxValue;

    public int MinDiffInBST (TreeNode root) {
        int minValue = int.MaxValue;
        if (root == null) return 0;
        List<int> list = new List<int>();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        while(stack.Count > 0 || root!=null){
            while(root!=null){
                stack.Push(root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop();
            list.Add(tmp.val);
            root = tmp.right;
        }

        for(int i = 0;i<list.Count-1;i++){
            int v = Math.Abs(list[i] - list[i+1]);
            minValue = Math.Min(v,minValue);
        }
        return minValue;
    }

    // private void FindMin(TreeNode root){
    //     if(root == null)return;
    //     if(root.left == null && root.right == null){
    //         return;
    //     }

    //     if(root.left!=null){
    //         int val = Math.Abs(root.val - root.left.val);
    //         minValue = Math.Min(minValue,val);
    //         FindMin(root.left);
    //     }

    //     if(root.right!=null){
    //         int val = Math.Abs(root.val - root.right.val);
    //         minValue = Math.Min(minValue,val);
    //         FindMin(root.right);
    //     }
    // }

}
// @lc code=end