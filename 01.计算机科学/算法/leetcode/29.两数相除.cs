/*
 * @lc app=leetcode.cn id=29 lang=csharp
 *
 * [29] 两数相除
 *
 * https://leetcode-cn.com/problems/divide-two-integers/description/
 *
 * algorithms
 * Medium (20.17%)
 * Likes:    441
 * Dislikes: 0
 * Total Accepted:    66.1K
 * Total Submissions: 327.9K
 * Testcase Example:  '10\n3'
 *
 * 给定两个整数，被除数 dividend 和除数 divisor。将两数相除，要求不使用乘法、除法和 mod 运算符。
 * 
 * 返回被除数 dividend 除以除数 divisor 得到的商。
 * 
 * 整数除法的结果应当截去（truncate）其小数部分，例如：truncate(8.345) = 8 以及 truncate(-2.7335) =
 * -2
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: dividend = 10, divisor = 3
 * 输出: 3
 * 解释: 10/3 = truncate(3.33333..) = truncate(3) = 3
 * 
 * 示例 2:
 * 
 * 输入: dividend = 7, divisor = -3
 * 输出: -2
 * 解释: 7/-3 = truncate(-2.33333..) = -2
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 被除数和除数均为 32 位有符号整数。
 * 除数不为 0。
 * 假设我们的环境只能存储 32 位有符号整数，其数值范围是 [−2^31,  2^31 − 1]。本题中，如果除法结果溢出，则返回 2^31 − 1。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int Divide(int dividend, int divisor) {
        int sign = (dividend^divisor)>>31;
        if(dividend > 0)dividend = -dividend;
        if(divisor > 0)divisor = -divisor;
        long lDividend = (long)dividend;
        long lDivisor = (long)divisor;
        long res = 0;
        while(lDividend <= lDivisor){
            long temp = lDivisor;
            long i = 1;
            while(lDividend <= temp){
                lDividend -= temp;
                res+=i;
                i<<=1;
                temp<<=1;
            }
        }
        if(sign == -1)res = -res;
        if(res > int.MaxValue){
            return int.MaxValue;
        }else if(res < int.MinValue){
            return int.MinValue;
        }else{
            return (int)res;
        }
    }
}
// @lc code=end

