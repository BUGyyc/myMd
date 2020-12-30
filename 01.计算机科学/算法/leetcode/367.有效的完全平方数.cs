/*
 * @lc app=leetcode.cn id=367 lang=csharp
 *
 * [367] 有效的完全平方数
 *
 * https://leetcode-cn.com/problems/valid-perfect-square/description/
 *
 * algorithms
 * Easy (43.52%)
 * Likes:    173
 * Dislikes: 0
 * Total Accepted:    46.9K
 * Total Submissions: 107.8K
 * Testcase Example:  '16'
 *
 * 给定一个正整数 num，编写一个函数，如果 num 是一个完全平方数，则返回 True，否则返回 False。
 * 
 * 说明：不要使用任何内置的库函数，如  sqrt。
 * 
 * 示例 1：
 * 
 * 输入：16
 * 输出：True
 * 
 * 示例 2：
 * 
 * 输入：14
 * 输出：False
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public bool IsPerfectSquare (int num) {
        if(num < 0)return false;
        if(num == 1 || num == 0)return true;
        long left = 2;
        long right = num/2;
        long x = 0;
        long g = 0;
        while(left <= right){
            x = left + (right-left)/2;
            g = x * x;
            if(g == num){
                return true;
            }else if(g > num){
                right = x - 1;
            }else{
                left = x + 1;
            }
        }
        return false;
    }
}
// @lc code=end