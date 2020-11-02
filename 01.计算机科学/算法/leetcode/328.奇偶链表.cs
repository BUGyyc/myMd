/*
 * @lc app=leetcode.cn id=328 lang=csharp
 *
 * [328] 奇偶链表
 *
 * https://leetcode-cn.com/problems/odd-even-linked-list/description/
 *
 * algorithms
 * Medium (63.49%)
 * Likes:    264
 * Dislikes: 0
 * Total Accepted:    60.9K
 * Total Submissions: 95.9K
 * Testcase Example:  '[1,2,3,4,5]'
 *
 * 给定一个单链表，把所有的奇数节点和偶数节点分别排在一起。请注意，这里的奇数节点和偶数节点指的是节点编号的奇偶性，而不是节点的值的奇偶性。
 * 
 * 请尝试使用原地算法完成。你的算法的空间复杂度应为 O(1)，时间复杂度应为 O(nodes)，nodes 为节点总数。
 * 
 * 示例 1:
 * 
 * 输入: 1->2->3->4->5->NULL
 * 输出: 1->3->5->2->4->NULL
 * 
 * 
 * 示例 2:
 * 
 * 输入: 2->1->3->5->6->4->7->NULL 
 * 输出: 2->3->6->7->1->5->4->NULL
 * 
 * 说明:
 * 
 * 
 * 应当保持奇数节点和偶数节点的相对顺序。
 * 链表的第一个节点视为奇数节点，第二个节点视为偶数节点，以此类推。
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
    public ListNode OddEvenList (ListNode head) {
        if (head == null || head.next == null) return head;
        ListNode l1 = null;
        ListNode l2 = null;
        ListNode head1 = null;
        ListNode head2 = null;
        int step = 1;
        ListNode p = head;
        while (p != null) {
            if (step % 2 == 1) {
                if (head1 == null) {
                    head1 = new ListNode (p.val);
                    l1 = head1;
                } else {
                    l1.next = new ListNode (p.val);
                    l1 = l1.next;
                }
            } else {
                if (head2 == null) {
                    head2 = new ListNode (p.val);
                    l2 = head2;
                } else {
                    l2.next = new ListNode (p.val);
                    l2 = l2.next;
                }
            }
            p = p.next;
            step++;
        }
        l1.next = head2;
        return head1;
    }
}
// @lc code=end