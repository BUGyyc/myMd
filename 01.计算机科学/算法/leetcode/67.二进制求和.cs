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
        List<string> list = new List<string> ();
        int aLen = a.Length;
        int bLen = b.Length;
        int i = aLen - 1;
        int j = bLen - 1;
        int carry = 0;
        while (i >= 0 || j >= 0) {
            int x = (i >= 0) ? a[i] - '0' : 0;
            int y = (j >= 0) ? b[j] - '0' : 0;
            int sum = x + y + carry;
            int result = sum % 2;
            carry = sum / 2;
            list.Insert (0, result.ToString ());
            i--;
            j--;
        }
        if (carry > 0) {
            list.Insert (0, carry.ToString ());
        }
        string str = "";
        foreach (var item in list) {

            str += item;
        }
        return str;
    }
}
// @lc code=end