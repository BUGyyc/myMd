/*
 * @lc app=leetcode.cn id=23 lang=csharp
 *
 * [23] 合并K个升序链表
 *
 * https://leetcode-cn.com/problems/merge-k-sorted-lists/description/
 *
 * algorithms
 * Hard (53.34%)
 * Likes:    955
 * Dislikes: 0
 * Total Accepted:    178.8K
 * Total Submissions: 335K
 * Testcase Example:  '[[1,4,5],[1,3,4],[2,6]]'
 *
 * 给你一个链表数组，每个链表都已经按升序排列。
 * 
 * 请你将所有链表合并到一个升序链表中，返回合并后的链表。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：lists = [[1,4,5],[1,3,4],[2,6]]
 * 输出：[1,1,2,3,4,4,5,6]
 * 解释：链表数组如下：
 * [
 * ⁠ 1->4->5,
 * ⁠ 1->3->4,
 * ⁠ 2->6
 * ]
 * 将它们合并到一个有序链表中得到。
 * 1->1->2->3->4->4->5->6
 * 
 * 
 * 示例 2：
 * 
 * 输入：lists = []
 * 输出：[]
 * 
 * 
 * 示例 3：
 * 
 * 输入：lists = [[]]
 * 输出：[]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * k == lists.length
 * 0 <= k <= 10^4
 * 0 <= lists[i].length <= 500
 * -10^4 <= lists[i][j] <= 10^4
 * lists[i] 按 升序 排列
 * lists[i].length 的总和不超过 10^4
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
    public ListNode MergeKLists (ListNode[] lists) {
        ListNode head = null;
        ListNode p = null;
        while (HasNullListNode (lists) == false) {
            ListNode item = GetMinListNodeIndex (lists);
            if (head == null) {
                head = new ListNode (item.val);
                p = head;
            } else {
                p.next = new ListNode (item.val);
                p = p.next;
            }
            item = item.next;
        }
        return head;
    }

    public bool HasNullListNode (ListNode[] lists) {
        foreach (var item in lists) {
            if (item == null) {
                return true;
            }
        }
        return false;
    }

    public ListNode GetMinListNodeIndex (ListNode[] lists) {
        int minIndex = 0;
        int minValue = int.MaxValue;
        for (int i = 0; i < lists.Length; i++) {
            var item = lists[i];
            if (item != null && item.val < minValue) {
                minIndex = i;
                minValue = item.val;
            }
        }
        return lists[minIndex];
    }
}
// @lc code=end