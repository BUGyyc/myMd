using System.Collections.Generic;

public class LocalTry
{
    public void Dived(int dividend, int divisor)
    {
        bool sign = (dividend ^ divisor) >> 31;
        if (dividend > 0) dividend = -dividend;
        if (divisor > 0) divisor = -divisor;
        long lDividend = (long)dividend;
        long lDivisor = (long)divisor;
        long res = 0;
        while (lDividend >= lDivisor)
        {
            long temp = lDivisor;
            long i = 1;
            while (lDividend >= temp)
            {
                lDividend -= temp;
                res += i;
                i <<= 1;
                temp <<= 1;
            }
        }
        if (sign == -1) res = -res;
        if (res > int.MaxValue)
        {
            return int.MaxValue;
        }
        else if (res < int.MinValue)
        {
            return int.MinValue;
        }
        else
        {
            return res;
        }
    }

    /// <summary>
    /// 144
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PreorderTraversal(TreeNode root)
    {
        List<int> result = new List<int>();
        PreorderTraversalFunc1(ref result, root);
    }

    private void PreorderTraversalFunc1(ref List<int> result, TreeNode root)
    {
        if (root == null) return;
        result.Add(root.val);
        if (root.left != null) PreorderTraversalFunc1(ref result, root.left);
        if (root.right != null) PreorderTraversalFunc1(ref result, root.right);
    }

    private void PreorderTraversalFunc2(ref List<int> result,TreeNode root){
        Stack<TreeNode> stack = new Stack<TreeNode>();
        if(root!=null){
            stack.Push(root);
        }
        while(stack.Count>0){
            TreeNode node = stack.Pop();
            result.Add(node.val);
            if(node.left!=null){
                stack.Push(node.left);
            }
            if(node.right!=null){
                stack.Push(node.right);
            }
        }
    }



}

public struct TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
}

public struct ListNode
{
    public int val;
    public ListNode next;
}