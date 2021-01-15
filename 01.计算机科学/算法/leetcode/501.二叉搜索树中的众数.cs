/*
 * @lc app=leetcode.cn id=501 lang=csharp
 *
 * [501] 二叉搜索树中的众数
 *
 * https://leetcode-cn.com/problems/find-mode-in-binary-search-tree/description/
 *
 * algorithms
 * Easy (49.84%)
 * Likes:    258
 * Dislikes: 0
 * Total Accepted:    45.9K
 * Total Submissions: 92.1K
 * Testcase Example:  '[1,null,2,2]'
 *
 * 给定一个有相同值的二叉搜索树（BST），找出 BST 中的所有众数（出现频率最高的元素）。
 * 
 * 假定 BST 有如下定义：
 * 
 * 
 * 结点左子树中所含结点的值小于等于当前结点的值
 * 结点右子树中所含结点的值大于等于当前结点的值
 * 左子树和右子树都是二叉搜索树
 * 
 * 
 * 例如：
 * 给定 BST [1,null,2,2],
 * 
 * ⁠  1
 * ⁠   \
 * ⁠    2
 * ⁠   /
 * ⁠  2
 * 
 * 
 * 返回[2].
 * 
 * 提示：如果众数超过1个，不需考虑输出顺序
 * 
 * 进阶：你可以不使用额外的空间吗？（假设由递归产生的隐式调用栈的开销不被计算在内）
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
    public int[] FindMode(TreeNode root) {
        List<int> list = new List<int>();
        if (root == null) return list.ToArray();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        Dictionary<int, int> dic = new Dictionary<int, int>();
        int max = 0;
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop();
            if (dic.ContainsKey(tmp.val))
            {
                dic[tmp.val]++;
            }
            else
            {
                dic[tmp.val] = 1;
            }
            max = Math.Max(max, dic[tmp.val]);
            root = tmp.right;
        }
        
        foreach (var item in dic)
        {
            if (item.Value == max)
            {
                list.Add(item.Key);
            }
        }
        return list.ToArray();
    }
}
// @lc code=end

