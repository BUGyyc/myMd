/*
 * @lc app=leetcode.cn id=2 lang=csharp
 *
 * [2] 两数相加
 *
 * https://leetcode-cn.com/problems/add-two-numbers/description/
 *
 * algorithms
 * Medium (38.68%)
 * Likes:    5069
 * Dislikes: 0
 * Total Accepted:    590.2K
 * Total Submissions: 1.5M
 * Testcase Example:  '[2,4,3]\n[5,6,4]'
 *
 * 给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
 * 
 * 如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
 * 
 * 您可以假设除了数字 0 之外，这两个数都不会以 0 开头。
 * 
 * 示例：
 * 
 * 输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
 * 输出：7 -> 0 -> 8
 * 原因：342 + 465 = 807
 * 
 * 
 */

// @lc code=start
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    //将节点逐个取下来，然后相互比较,carry进位
    public ListNode AddTwoNumbers (ListNode l1, ListNode l2) {
        ListNode p = null;
        ListNode head = null;
        int carry = 0;
        while (l1 != null || l2 != null) {
            int a = l1 == null?0 : l1.val;
            int b = l2 == null?0 : l2.val;
            int sum = carry + a + b;
            if (head == null) {
                head = new ListNode (sum % 10);
                p = head;
            } else {
                p.next = new ListNode (sum % 10);
                p = p.next;
            }
            carry = sum / 10;
            if (l1 != null) l1 = l1.next;
            if (l2 != null) l2 = l2.next;
        }
        if (carry > 0) {
            p.next = new ListNode (1);
        }
        return head;
    }
}
// @lc code=end