/*
 * @lc app=leetcode.cn id=82 lang=csharp
 *
 * [82] 删除排序链表中的重复元素 II
 *
 * https://leetcode-cn.com/problems/remove-duplicates-from-sorted-list-ii/description/
 *
 * algorithms
 * Medium (49.63%)
 * Likes:    396
 * Dislikes: 0
 * Total Accepted:    72.8K
 * Total Submissions: 146.7K
 * Testcase Example:  '[1,2,3,3,4,4,5]'
 *
 * 给定一个排序链表，删除所有含有重复数字的节点，只保留原始链表中 没有重复出现 的数字。
 * 
 * 示例 1:
 * 
 * 输入: 1->2->3->3->4->4->5
 * 输出: 1->2->5
 * 
 * 
 * 示例 2:
 * 
 * 输入: 1->1->1->2->3
 * 输出: 2->3
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
public class Solution
{
    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null || head.next == null) return head;
        ListNode pre = new ListNode(-1);
        pre.next = head;
        ListNode newHead = pre;
        ListNode l = null;
        ListNode r = null;
        while (pre.next != null)
        {
            l = pre.next;
            r = pre.next;
            while (r.next != null && r.next.val == l.val)
            {
                r = r.next;
            }
            if (l == r)
            {
                pre = pre.next;
            }
            else
            {
                pre.next = r.next;
            }
        }
        return newHead.next;
    }
}
// @lc code=end

