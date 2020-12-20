/*
 * @lc app=leetcode.cn id=415 lang=csharp
 *
 * [415] 字符串相加
 *
 * https://leetcode-cn.com/problems/add-strings/description/
 *
 * algorithms
 * Easy (51.87%)
 * Likes:    274
 * Dislikes: 0
 * Total Accepted:    82.8K
 * Total Submissions: 159.6K
 * Testcase Example:  '"0"\n"0"'
 *
 * 给定两个字符串形式的非负整数 num1 和num2 ，计算它们的和。
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * num1 和num2 的长度都小于 5100
 * num1 和num2 都只包含数字 0-9
 * num1 和num2 都不包含任何前导零
 * 你不能使用任何內建 BigInteger 库， 也不能直接将输入的字符串转换为整数形式
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public string AddStrings (string num1, string num2) {
        int a = num1.Length;
        int b = num2.Length;
        if (a == 0 && b == 0) {
            return "";
        } else if (a == 0) {
            return num2;
        } else if (b == 0) {
            return num1;
        }
        int carry = 0;
        int i = a - 1;
        int j = b - 1;
        List<int> list = new List<int> ();
        while (i >= 0 || j >= 0) {
            int x = (i < 0) ? 0 : num1[i] - '0';
            int y = (j < 0) ? 0 : num2[j] - '0';
            int sum = x + y + carry;
            carry = sum / 10;
            int val = sum % 10;
            list.Insert (0, val);
            i--;
            j--;
        }
        if (carry > 0) list.Insert (0, carry);
        StringBuilder sb = new StringBuilder ();
        foreach (var item in list) {
            sb.Append (item.ToString ());
        }
        return sb.ToString ();
    }
}
// @lc code=end