/*
 * @lc app=leetcode.cn id=143 lang=csharp
 *
 * [143] 重排链表
 *
 * https://leetcode-cn.com/problems/reorder-list/description/
 *
 * algorithms
 * Medium (59.65%)
 * Likes:    468
 * Dislikes: 0
 * Total Accepted:    72.7K
 * Total Submissions: 121.8K
 * Testcase Example:  '[1,2,3,4]'
 *
 * 给定一个单链表 L：L0→L1→…→Ln-1→Ln ，
 * 将其重新排列后变为： L0→Ln→L1→Ln-1→L2→Ln-2→…
 * 
 * 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
 * 
 * 示例 1:
 * 
 * 给定链表 1->2->3->4, 重新排列为 1->4->2->3.
 * 
 * 示例 2:
 * 
 * 给定链表 1->2->3->4->5, 重新排列为 1->5->2->4->3.
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
public class Solution
{
    //TODO:
    public void ReorderList(ListNode head)
    {
        if (head == null) return;
        ListNode p1 = head;
        ListNode p2 = ReverseList(head);
        int len = 0;
        ListNode p = head;
        while (p != null)
        {
            p = p.next;
            len++;
        }
        int step = 0;
        ListNode cur = new ListNode(-1);
        cur.next = head;
        while (step < len)
        {
            if (step % 2 == 0)
            {
                cur.next = p1;
                cur = cur.next;
                p1 = p1.next;
            }
            else
            {
                cur.next = p2;
                cur = cur.next;
                p2 = p2.next;
            }
            step++;
        }
    }

    public ListNode ReverseList(ListNode head)
    {
        ListNode cur = head;
        ListNode pre = null;
        while (cur != null)
        {
            ListNode tmp = cur.next;
            cur.next = pre;
            pre = cur;
            cur = tmp;
        }
        return pre;
    }
}
// @lc code=end

