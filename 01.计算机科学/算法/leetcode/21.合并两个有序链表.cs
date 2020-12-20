/*
 * @lc app=leetcode.cn id=21 lang=csharp
 *
 * [21] 合并两个有序链表
 *
 * https://leetcode-cn.com/problems/merge-two-sorted-lists/description/
 *
 * algorithms
 * Easy (64.51%)
 * Likes:    1319
 * Dislikes: 0
 * Total Accepted:    388.6K
 * Total Submissions: 602.2K
 * Testcase Example:  '[1,2,4]\n[1,3,4]'
 *
 * 将两个升序链表合并为一个新的 升序 链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：1->2->4, 1->3->4
 * 输出：1->1->2->3->4->4
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
    //取节点逐个比较
    public ListNode MergeTwoLists (ListNode l1, ListNode l2) {
        ListNode p = null;
        ListNode head = null;
        while (l1 != null || l2 != null) {
            int result = 0;
            if (l1 != null && l2 != null) {
                if (l1.val < l2.val) {
                    result = l1.val;
                    l1 = l1.next;
                } else {
                    result = l2.val;
                    l2 = l2.next;
                }
            } else {
                if (l1 == null) {
                    result = l2.val;
                    l2 = l2.next;
                } else {
                    result = l1.val;
                    l1 = l1.next;
                }
            }

            if (head == null) {
                head = new ListNode (result);
                p = head;
            } else {
                p.next = new ListNode (result);
                p = p.next;
            }
        }
        return head;
    }
}
// @lc code=end