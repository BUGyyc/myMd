/*
 * @lc app=leetcode.cn id=504 lang=csharp
 *
 * [504] 七进制数
 *
 * https://leetcode-cn.com/problems/base-7/description/
 *
 * algorithms
 * Easy (49.95%)
 * Likes:    75
 * Dislikes: 0
 * Total Accepted:    20K
 * Total Submissions: 40.1K
 * Testcase Example:  '100'
 *
 * 给定一个整数，将其转化为7进制，并以字符串形式输出。
 * 
 * 示例 1:
 * 
 * 
 * 输入: 100
 * 输出: "202"
 * 
 * 
 * 示例 2:
 * 
 * 
 * 输入: -7
 * 输出: "-10"
 * 
 * 
 * 注意: 输入范围是 [-1e7, 1e7] 。
 * 
 */

// @lc code=start
public class Solution {
    public string ConvertToBase7 (int num) {
        if (num == 0) return "0";
        bool f = (num < 0);
        if (f) num *= -1;
        StringBuilder sb = new StringBuilder ();
        while (num > 0) {
            int val = num % 7;
            num /= 7;
            sb.Append (val);
        }

        char[] cs = sb.ToString ().ToCharArray ();
        Array.Reverse (cs);
        string str = new string (cs);
        return f? "-" + str : str;
    }
}
// @lc code=end