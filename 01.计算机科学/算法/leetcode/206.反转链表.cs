/*
 * @lc app=leetcode.cn id=206 lang=csharp
 *
 * [206] 反转链表
 *
 * https://leetcode-cn.com/problems/reverse-linked-list/description/
 *
 * algorithms
 * Easy (70.83%)
 * Likes:    1310
 * Dislikes: 0
 * Total Accepted:    361.9K
 * Total Submissions: 511K
 * Testcase Example:  '[1,2,3,4,5]'
 *
 * 反转一个单链表。
 * 
 * 示例:
 * 
 * 输入: 1->2->3->4->5->NULL
 * 输出: 5->4->3->2->1->NULL
 * 
 * 进阶:
 * 你可以迭代或递归地反转链表。你能否用两种方法解决这道题？
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
    //递归和迭代写法
    public ListNode ReverseList (ListNode head) {
        return ReverseListFunc2 (head);
    }

    private ListNode ReverseListFunc2 (ListNode head) {
        if (head == null || head.next == null) return head;
        ListNode p = ReverseListFunc2 (head.next);
        head.next.next = head;
        head.next = null;
        return p;
    }

    private ListNode Func1 (ListNode head) {
        if (head == null) return null;
        //上一个节点
        ListNode pre = null;
        //当前节点
        ListNode p = head;
        while (p != null) {
            //取下下一个节点
            ListNode tmp = p.next;
            //当前节点的next 指向 上一个节点
            p.next = pre;
            //刷新pre 所指向的节点，方便下一次迭代使用
            pre = p;
            //当前指针往前移
            p = tmp;
        }
        return pre;
    }

    private ListNode ReverseListFunc (ListNode head) {
        if (head == null || head.next == null) return head;
        ListNode cur = head;
        ListNode pre = null;
        while (cur != null) {
            ListNode tmp = cur.next;
            cur.next = pre;
            pre = cur;
            cur = tmp;
        }
        return pre;
    }
}
// @lc code=end