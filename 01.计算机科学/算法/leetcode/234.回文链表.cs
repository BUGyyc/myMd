/*
 * @lc app=leetcode.cn id=234 lang=csharp
 *
 * [234] 回文链表
 *
 * https://leetcode-cn.com/problems/palindrome-linked-list/description/
 *
 * algorithms
 * Easy (44.89%)
 * Likes:    748
 * Dislikes: 0
 * Total Accepted:    165.7K
 * Total Submissions: 369K
 * Testcase Example:  '[1,2]'
 *
 * 请判断一个链表是否为回文链表。
 * 
 * 示例 1:
 * 
 * 输入: 1->2
 * 输出: false
 * 
 * 示例 2:
 * 
 * 输入: 1->2->2->1
 * 输出: true
 * 
 * 
 * 进阶：
 * 你能否用 O(n) 时间复杂度和 O(1) 空间复杂度解决此题？
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
    public bool IsPalindrome (ListNode head) {
        // if (head == null) return true;
        // if (head.next == null) return true;

        // ListNode halfNode = HalfNode (head);
        // ListNode newHead = ReverseList (halfNode.next);
        // while (newHead != null) {
        //     if (head.val != newHead.val) {
        //         return false;
        //     }
        //     head = head.next;
        //     newHead = newHead.next;
        // }
        // return true;
        ListNode p = head;
        Stack<int> stack = new Stack<int> ();
        while (p != null) {
            stack.Push (p.val);
            p = p.next;
        }

        while (head != null) {
            if (head.val != stack.Pop ()) {
                return false;
            }
            head = head.next;
        }

        return true;
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

    private ListNode HalfNode (ListNode head) {
        ListNode fast = head;
        ListNode slow = head;
        while (fast != null && fast.next != null) {
            fast = fast.next.next;
            slow = slow.next;
        }
        return slow;
    }
}
// @lc code=end