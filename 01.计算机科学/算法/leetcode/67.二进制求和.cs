/*
 * @lc app=leetcode.cn id=67 lang=csharp
 *
 * [67] 二进制求和
 *
 * https://leetcode-cn.com/problems/add-binary/description/
 *
 * algorithms
 * Easy (54.38%)
 * Likes:    498
 * Dislikes: 0
 * Total Accepted:    131K
 * Total Submissions: 240.9K
 * Testcase Example:  '"11"\n"1"'
 *
 * 给你两个二进制字符串，返回它们的和（用二进制表示）。
 * 
 * 输入为 非空 字符串且只包含数字 1 和 0。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: a = "11", b = "1"
 * 输出: "100"
 * 
 * 示例 2:
 * 
 * 输入: a = "1010", b = "1011"
 * 输出: "10101"
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 每个字符串仅由字符 '0' 或 '1' 组成。
 * 1 <= a.length, b.length <= 10^4
 * 字符串如果不是 "0" ，就都不含前导零。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string AddBinary (string a, string b) {
        List<char> list = new List<char> ();
        int aLen = a.Length;
        int bLen = b.Length;
        int i = 0;
        int j = 0;
        int carry;
        while (i < aLen || j < bLen) {
            char x = (i < aLen) ? a[i] : '0';
            char y = (j < bLen) ? b[j] : '0';
            string s = x.ToString () + y.ToString ();
            if (s.Equals ("10") || s.Equals ("01") && carry != 1) {
                list.Insert (0, "1");
            } else if (s.Equals ("10") || s.Equals ("01") && carry == 1) {
                list.Insert (0, "0");
                carry = 1;
            } else if (s.Equals ("11") && carry != 1) {
                list.Insert (0, "0");
                carry = 1;
            } else if (s.Equals ("11") && carry == 1) {
                list.Insert (0, "1");
                carry = 1;
            } else if (carry == 1) {
                list.Insert (0, "1");
                carry = 0;
            } else if (carry != 1) {
                list.Insert (0, "0");
            }
        }

        if (carry > 0) {
            list.Insert (0, "1");
        }
        StringBuild sb = new StringBuild ();
        foreach (var item in list) {
            sb.Append (item);
        }

        return sb.ToString ();

    }
}
// @lc code=end