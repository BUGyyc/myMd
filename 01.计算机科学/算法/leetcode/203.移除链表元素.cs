/*
 * @lc app=leetcode.cn id=203 lang=csharp
 *
 * [203] 移除链表元素
 *
 * https://leetcode-cn.com/problems/remove-linked-list-elements/description/
 *
 * algorithms
 * Easy (46.46%)
 * Likes:    470
 * Dislikes: 0
 * Total Accepted:    110.6K
 * Total Submissions: 238.1K
 * Testcase Example:  '[1,2,6,3,4,5,6]\n6'
 *
 * 删除链表中等于给定值 val 的所有节点。
 * 
 * 示例:
 * 
 * 输入: 1->2->6->3->4->5->6, val = 6
 * 输出: 1->2->3->4->5
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
    //指针操作
    public ListNode RemoveElements (ListNode head, int val) {
        if (head == null) return null;
        ListNode p = head;
        ListNode newHead = null;
        ListNode n = null;
        while (p != null) {
            if (p.val == val) {
                //
            } else {
                if (newHead == null) {
                    newHead = new ListNode (p.val);
                    n = newHead;
                } else {
                    n.next = new ListNode (p.val);
                    n = n.next;
                }
            }
            p = p.next;
        }
        return newHead;
    }
}
// @lc code=end