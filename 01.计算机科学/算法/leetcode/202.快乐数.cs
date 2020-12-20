/*
 * @lc app=leetcode.cn id=202 lang=csharp
 *
 * [202] 快乐数
 *
 * https://leetcode-cn.com/problems/happy-number/description/
 *
 * algorithms
 * Easy (60.79%)
 * Likes:    471
 * Dislikes: 0
 * Total Accepted:    105K
 * Total Submissions: 172.8K
 * Testcase Example:  '19'
 *
 * 编写一个算法来判断一个数 n 是不是快乐数。
 * 
 * 「快乐数」定义为：对于一个正整数，每一次将该数替换为它每个位置上的数字的平方和，然后重复这个过程直到这个数变为 1，也可能是 无限循环 但始终变不到
 * 1。如果 可以变为  1，那么这个数就是快乐数。
 * 
 * 如果 n 是快乐数就返回 True ；不是，则返回 False 。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：19
 * 输出：true
 * 解释：
 * 1^2 + 9^2 = 82
 * 8^2 + 2^2 = 68
 * 6^2 + 8^2 = 100
 * 1^2 + 0^2 + 0^2 = 1
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public bool IsHappy (int n) {
        int slow = n;
        int fast = n;
        do {
            slow = CheckInt (slow);
            fast = CheckInt (fast);
            fast = CheckInt (fast);
        } while (fast != slow);
        return fast == 1;
    }

    private int CheckInt (int n) {
        int result = 0;
        while (n != 0) {
            int x = n % 10;
            result += x * x;
            n = n / 10;
        }
        return result;
    }
}
// @lc code=end