/*
 * @lc app=leetcode.cn id=105 lang=csharp
 *
 * [105] 从前序与中序遍历序列构造二叉树
 *
 * https://leetcode-cn.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/
 *
 * algorithms
 * Medium (68.31%)
 * Likes:    718
 * Dislikes: 0
 * Total Accepted:    122.3K
 * Total Submissions: 179K
 * Testcase Example:  '[3,9,20,15,7]\n[9,3,15,20,7]'
 *
 * 根据一棵树的前序遍历与中序遍历构造二叉树。
 * 
 * 注意:
 * 你可以假设树中没有重复的元素。
 * 
 * 例如，给出
 * 
 * 前序遍历 preorder = [3,9,20,15,7]
 * 中序遍历 inorder = [9,3,15,20,7]
 * 
 * 返回如下的二叉树：
 * 
 * ⁠   3
 * ⁠  / \
 * ⁠ 9  20
 * ⁠   /  \
 * ⁠  15   7
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
    //TODO:
    public TreeNode BuildTree (int[] preorder, int[] inorder) {
        if(preorder.Length == 0 || inorder.Length == 0)return null;
        TreeNode root = new TreeNode(preorder[0]);
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        int inorderIndex = 0;
        for(int i = 1;i<preorder.Length;i++){
            int val = preorder[i];
            TreeNode node = stack.Peek();
            if(node.val != inorder[inorderIndex]){
                node.left = new TreeNode(val);
                stack.Push(node.left);
            }else{
                while(stack.Count > 0 && stack.Peek().val == inorderIndex[inorderIndex]){
                    inorderIndex++;
                    node = stack.Pop();
                }
                node.right = new TreeNode(val);
                stack.Push(node.right);
            }
        }
        return root;
    }
}
// @lc code=end