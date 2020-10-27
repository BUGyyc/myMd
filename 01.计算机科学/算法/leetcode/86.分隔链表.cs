/*
 * @lc app=leetcode.cn id=86 lang=csharp
 *
 * [86] 分隔链表
 *
 * https://leetcode-cn.com/problems/partition-list/description/
 *
 * algorithms
 * Medium (59.98%)
 * Likes:    274
 * Dislikes: 0
 * Total Accepted:    56.7K
 * Total Submissions: 94.4K
 * Testcase Example:  '[1,4,3,2,5,2]\n3'
 *
 * 给定一个链表和一个特定值 x，对链表进行分隔，使得所有小于 x 的节点都在大于或等于 x 的节点之前。
 * 
 * 你应当保留两个分区中每个节点的初始相对位置。
 * 
 * 
 * 
 * 示例:
 * 
 * 输入: head = 1->4->3->2->5->2, x = 3
 * 输出: 1->2->2->4->3->5
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
public class Solution
{
    public ListNode Partition(ListNode head, int x)
    {
        if (head == null) return null;
        ListNode s = null;
        ListNode b = null;
        ListNode f = null;
        ListNode sec = null;
        ListNode p = head;
        while (p != null)
        {
            if (p.val < x)
            {
                if (s == null)
                {
                    s = new ListNode(p.val);
                    f = s;
                }
                else
                {
                    s.next = new ListNode(p.val);
                    s = s.next;
                }
            }
            else
            {
                if (b == null)
                {
                    b = new ListNode(p.val);
                    sec = b;
                }
                else
                {
                    b.next = new ListNode(p.val);
                    b = b.next;
                }
            }
            p = p.next;
        }
        s.next = sec;
        return f;
    }
}
// @lc code=end

