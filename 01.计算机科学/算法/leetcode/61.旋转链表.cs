/*
 * @lc app=leetcode.cn id=61 lang=csharp
 *
 * [61] 旋转链表
 *
 * https://leetcode-cn.com/problems/rotate-list/description/
 *
 * algorithms
 * Medium (40.49%)
 * Likes:    356
 * Dislikes: 0
 * Total Accepted:    92.4K
 * Total Submissions: 228.2K
 * Testcase Example:  '[1,2,3,4,5]\n2'
 *
 * 给定一个链表，旋转链表，将链表每个节点向右移动 k 个位置，其中 k 是非负数。
 * 
 * 示例 1:
 * 
 * 输入: 1->2->3->4->5->NULL, k = 2
 * 输出: 4->5->1->2->3->NULL
 * 解释:
 * 向右旋转 1 步: 5->1->2->3->4->NULL
 * 向右旋转 2 步: 4->5->1->2->3->NULL
 * 
 * 
 * 示例 2:
 * 
 * 输入: 0->1->2->NULL, k = 4
 * 输出: 2->0->1->NULL
 * 解释:
 * 向右旋转 1 步: 2->0->1->NULL
 * 向右旋转 2 步: 1->2->0->NULL
 * 向右旋转 3 步: 0->1->2->NULL
 * 向右旋转 4 步: 2->0->1->NULL
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
    public ListNode RotateRight (ListNode head, int k) {
        if (head == null) return null;
        ListNode fast = head;
        ListNode last = head;
        int len = 1;
        while (fast.next != null) {
            fast = fast.next;
            len++;
        }

        fast.next = head;
        int step = len - k % len;
        while (step > 1) {
            step--;
            last = last.next;
        }
        ListNode newHead = last.next;
        last.next = null;
        return newHead;
    }
}
// @lc code=end