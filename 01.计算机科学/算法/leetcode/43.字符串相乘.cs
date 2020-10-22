/*
 * @lc app=leetcode.cn id=43 lang=csharp
 *
 * [43] 字符串相乘
 *
 * https://leetcode-cn.com/problems/multiply-strings/description/
 *
 * algorithms
 * Medium (44.53%)
 * Likes:    498
 * Dislikes: 0
 * Total Accepted:    110.1K
 * Total Submissions: 247.1K
 * Testcase Example:  '"2"\n"3"'
 *
 * 给定两个以字符串形式表示的非负整数 num1 和 num2，返回 num1 和 num2 的乘积，它们的乘积也表示为字符串形式。
 * 
 * 示例 1:
 * 
 * 输入: num1 = "2", num2 = "3"
 * 输出: "6"
 * 
 * 示例 2:
 * 
 * 输入: num1 = "123", num2 = "456"
 * 输出: "56088"
 * 
 * 说明：
 * 
 * 
 * num1 和 num2 的长度小于110。
 * num1 和 num2 只包含数字 0-9。
 * num1 和 num2 均不以零开头，除非是数字 0 本身。
 * 不能使用任何标准库的大数类型（比如 BigInteger）或直接将输入转换为整数来处理。
 * 
 * 
 */

// @lc code=start
public class Solution
{
    //     "123"
    // "456"
    public string Multiply(string num1, string num2)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = num1.Length - 1; i >= 0; i--)
        {
            int a = int.Parse(num1[i].ToString());
            int carry = 0;
            for (int j = num2.Length - 1; j >= 0; j--)
            {
                int b = int.Parse(num2[j].ToString());
                int result = a * b;
                result += carry;
                int s = result % 10;
                carry = result / 10;
                sb.Append(s);
            }
        }

        if (carry > 0)
        {
            sb.Append(carry);
        }

        StringBuilder sb2 = new StringBuilder();
        string s1 = sb.ToString();
        for (int i = s1.Length - 1; i >= 0; i--)
        {
            sb2.Append(s1[i]);
        }

        return sb.ToString();
    }
}
// @lc code=end

