/*
 * @lc app=leetcode.cn id=263 lang=csharp
 *
 * [263] 丑数
 *
 * https://leetcode-cn.com/problems/ugly-number/description/
 *
 * algorithms
 * Easy (49.95%)
 * Likes:    168
 * Dislikes: 0
 * Total Accepted:    49.7K
 * Total Submissions: 99.5K
 * Testcase Example:  '6'
 *
 * 编写一个程序判断给定的数是否为丑数。
 * 
 * 丑数就是只包含质因数 2, 3, 5 的正整数。
 * 
 * 示例 1:
 * 
 * 输入: 6
 * 输出: true
 * 解释: 6 = 2 × 3
 * 
 * 示例 2:
 * 
 * 输入: 8
 * 输出: true
 * 解释: 8 = 2 × 2 × 2
 * 
 * 
 * 示例 3:
 * 
 * 输入: 14
 * 输出: false 
 * 解释: 14 不是丑数，因为它包含了另外一个质因数 7。
 * 
 * 说明：
 * 
 * 
 * 1 是丑数。
 * 输入不会超过 32 位有符号整数的范围: [−2^31,  2^31 − 1]。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public bool IsUgly(int num) {
        int n = num;
        if(n == 1 || n == 2|| n == 3 || n == 4 || n == 5 || n == 6 || n == 8 || n == 9)return true;
        if(n == 7)return false;
        if(n == 0)return false;
        if(n%2 == 0){
            return IsUgly(n/2);
        }else if(n%3 == 0){
            return IsUgly(n/3);
        }else if(n%5 == 0){
            return IsUgly(n/5);
        }
        return false;
    }
}
// @lc code=end

