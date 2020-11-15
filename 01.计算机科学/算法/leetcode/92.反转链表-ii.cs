/*
 * @lc app=leetcode.cn id=92 lang=csharp
 *
 * [92] 反转链表 II
 *
 * https://leetcode-cn.com/problems/reverse-linked-list-ii/description/
 *
 * algorithms
 * Medium (51.63%)
 * Likes:    560
 * Dislikes: 0
 * Total Accepted:    82K
 * Total Submissions: 158.9K
 * Testcase Example:  '[1,2,3,4,5]\n2\n4'
 *
 * 反转从位置 m 到 n 的链表。请使用一趟扫描完成反转。
 * 
 * 说明:
 * 1 ≤ m ≤ n ≤ 链表长度。
 * 
 * 示例:
 * 
 * 输入: 1->2->3->4->5->NULL, m = 2, n = 4
 * 输出: 1->4->3->2->5->NULL
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
    public ListNode ReverseBetween (ListNode head, int m, int n) {
        if (head == null || head.next == null) return head;
        Stack<int> stack = new Stack<int> ();
        int step = 0;
        ListNode p = head;
        while (p != null) {
            step++;
            if (step >= m && step <= n) {
                stack.Push (p.val);
            }
            p = p.next;
        }
        p = head;
        step = 0;
        while (p != null) {
            step++;
            if (step >= m && step <= n) {
                p.val = stack.Pop ();
            }
            p = p.next;
        }
        return head;
    }
}
// @lc code=end