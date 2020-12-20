/*
 * @lc app=leetcode.cn id=445 lang=csharp
 *
 * [445] 两数相加 II
 *
 * https://leetcode-cn.com/problems/add-two-numbers-ii/description/
 *
 * algorithms
 * Medium (58.18%)
 * Likes:    294
 * Dislikes: 0
 * Total Accepted:    53.9K
 * Total Submissions: 92.7K
 * Testcase Example:  '[7,2,4,3]\n[5,6,4]'
 *
 * 给你两个 非空 链表来代表两个非负整数。数字最高位位于链表开始位置。它们的每个节点只存储一位数字。将这两数相加会返回一个新的链表。
 * 
 * 你可以假设除了数字 0 之外，这两个数字都不会以零开头。
 * 
 * 
 * 
 * 进阶：
 * 
 * 如果输入链表不能修改该如何处理？换句话说，你不能对列表中的节点进行翻转。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：(7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
 * 输出：7 -> 8 -> 0 -> 7
 * 
 * 
 */

// @lc code=start
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution {
    //TODO:
    public ListNode AddTwoNumbers (ListNode l1, ListNode l2) {
        if (l1 == null && l2 == null) {
            return null;
        } else if (l1 == null) {
            return l2;
        } else if (l2 == null) {
            return l1;
        }
        ListNode a = ReverseList (l1);
        ListNode b = ReverseList (l2);
        int sum = 0;
        int carry = 0;
        ListNode head = null;
        ListNode p = null;
        while (a != null || b != null) {
            int x = (a == null) ? 0 : a.val;
            int y = (b == null) ? 0 : b.val;
            sum = x + y + carry;
            carry = sum / 10;
            sum = sum % 10;
            if (head == null) {
                head = new ListNode (sum);
                p = head;
            } else {
                p.next = new ListNode (sum);
                p = p.next;
            }
            if (a != null) a = a.next;
            if (b != null) b = b.next;
        }
        if (carry > 0) {
            p.next = new ListNode (carry);
        }
        ListNode newHead = ReverseList (head);
        return newHead;
    }

    private ListNode ReverseList (ListNode head) {
        ListNode pre = null;
        ListNode p = head;
        while (p != null) {
            ListNode tmp = p.next;
            p.next = pre;
            pre = p;
            p = tmp;
        }
        return pre;
    }
}
// @lc code=end