using System.Collections.Generic;

public struct TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
}

public struct ListNode {
    public int val;
    public ListNode next;
}

public class LocalTry {
    public void Dived (int dividend, int divisor) {
        bool sign = (dividend ^ divisor) >> 31;
        if (dividend > 0) dividend = -dividend;
        if (divisor > 0) divisor = -divisor;
        long lDividend = (long) dividend;
        long lDivisor = (long) divisor;
        long res = 0;
        while (lDividend >= lDivisor) {
            long temp = lDivisor;
            long i = 1;
            while (lDividend >= temp) {
                lDividend -= temp;
                res += i;
                i <<= 1;
                temp <<= 1;
            }
        }
        if (sign == -1) res = -res;
        if (res > int.MaxValue) {
            return int.MaxValue;
        } else if (res < int.MinValue) {
            return int.MinValue;
        } else {
            return res;
        }
    }

    /// <summary>
    /// 144
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PreorderTraversal (TreeNode root) {
        List<int> result = new List<int> ();
        PreorderTraversalFunc1 (ref result, root);
    }

    private void PreorderTraversalFunc1 (ref List<int> result, TreeNode root) {
        if (root == null) return;
        result.Add (root.val);
        if (root.left != null) PreorderTraversalFunc1 (ref result, root.left);
        if (root.right != null) PreorderTraversalFunc1 (ref result, root.right);
    }

    private void PreorderTraversalFunc2 (ref List<int> result, TreeNode root) {
        Stack<TreeNode> stack = new Stack<TreeNode> ();
        if (root != null) {
            stack.Push (root);
        }
        while (stack.Count > 0) {
            TreeNode node = stack.Pop ();
            result.Add (node.val);
            if (node.left != null) {
                stack.Push (node.left);
            }
            if (node.right != null) {
                stack.Push (node.right);
            }
        }
    }

    private void StrMulti (string a, string b) {

    }
    /// <summary>
    /// 82
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    private ListNode DeleteMultiNode (ListNode head) {
        if (head == null || head.next == null) return head;
        ListNode pre = new ListNode (-1);
        pre.next = head;
        ListNode newHead = pre;
        ListNode l = null;
        ListNode r = null;
        while (pre.next != null) {
            l = pre.next;
            r = pre.next;
            while (r.next != null && r.next.val == l.val) {
                r = r.next;
            }
            if (l == r) {
                pre = pre.next;
            } else {
                pre.next = r.next;
            }
        }
        return newHead.next;
    }

    private ListNode ChangeList (ListNode list, int m, int n) {
        if (list == null || list.next == null) return list;
        int step = 0;
        ListNode p = list;
        ListNode pre1 = null;
        ListNode pre2 = null;
        while (p != null) {
            step++;
            if (step == m - 1) {
                pre1 = p;
            }
            if (step == n - 1) {
                pre2 = p;
            }
        }

        ListNode mNode = pre1.next;
        ListNode nNode = pre2.next;
        pre1.next = nNode;
        nNode.next = mNode.next;
        pre2.next = mNode.next;
        mNode.next = pre2.next.next;
    }
    /// <summary>
    /// [99,99]  [1,0,1,1]1
    /// 2
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    private bool FindTarget (int[] nums, int k) {
        if (nums.Length == 0) return false;
        if (k <= 0) return false;
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        for (int i = 0; i < nums.Length; i++) {
            if (dic.ContainsKey (nums[i])) {
                int value = i - dic[nums[i]];
                if (value <= k) {
                    return true;
                } else {
                    dic[nums[i]] = i;
                }
            } else {
                dic.Add (nums[i], i);
            }
        }
        return false;
    }

    private TreeNode Exchange (TreeNode root) {
        if (root == null) {
            return null;
        }
        root.left = Exchange (root.right);
        root.right = Exchange (root.left);
        return root;
    }

    private IList<string> MergeList (int[] nums) {
        List<string> result = new List<string> ();
        if (nums.Length == 0) {
            return result;
        }
        if (nums.Lenght == 1) {
            result.Add (nums[0].ToString ());
            return result;
        }
        int pre = nums[0];
        // StringBuilder sb = new StringBuilder();
        // for(int i = 1;i<nums.Length;i++){
        //     if(nums)
        // }
    }

    public int FurthestBuilding (int[] heights, int bricks, int ladders) {
        
    }

    public void NextArray(int[] nums){
        int len = nums.Length;
        if(len <= 1)return;
    }
}