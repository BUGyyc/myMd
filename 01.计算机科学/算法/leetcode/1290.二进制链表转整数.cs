/*
 * @lc app=leetcode.cn id=1290 lang=csharp
 *
 * [1290] 二进制链表转整数
 *
 * https://leetcode-cn.com/problems/convert-binary-number-in-a-linked-list-to-integer/description/
 *
 * algorithms
 * Easy (81.00%)
 * Likes:    67
 * Dislikes: 0
 * Total Accepted:    40K
 * Total Submissions: 49.4K
 * Testcase Example:  '[1,0,1]'
 *
 * 给你一个单链表的引用结点 head。链表中每个结点的值不是 0 就是 1。已知此链表是一个整数数字的二进制表示形式。
 * 
 * 请你返回该链表所表示数字的 十进制值 。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 
 * 输入：head = [1,0,1]
 * 输出：5
 * 解释：二进制数 (101) 转化为十进制数 (5)
 * 
 * 
 * 示例 2：
 * 
 * 输入：head = [0]
 * 输出：0
 * 
 * 
 * 示例 3：
 * 
 * 输入：head = [1]
 * 输出：1
 * 
 * 
 * 示例 4：
 * 
 * 输入：head = [1,0,0,1,0,0,1,1,1,0,0,0,0,0,0]
 * 输出：18880
 * 
 * 
 * 示例 5：
 * 
 * 输入：head = [0,0]
 * 输出：0
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 链表不为空。
 * 链表的结点总数不超过 30。
 * 每个结点的值不是 0 就是 1。
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
    public int GetDecimalValue (ListNode head) {
        if (head == null) return 0;
        int result = 0;
        while (head != null) {
            if (head.val == 1) {
                result *= 2;
                result++;
            } else {
                result *= 2;
            }
            head = head.next;
        }
        return result;
    }
}
// @lc code=end