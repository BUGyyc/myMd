/*
 * @lc app=leetcode.cn id=6 lang=csharp
 *
 * [6] Z 字形变换
 *
 * https://leetcode-cn.com/problems/zigzag-conversion/description/
 *
 * algorithms
 * Medium (48.89%)
 * Likes:    867
 * Dislikes: 0
 * Total Accepted:    179.1K
 * Total Submissions: 366.3K
 * Testcase Example:  '"PAYPALISHIRING"\n3'
 *
 * 将一个给定字符串根据给定的行数，以从上往下、从左到右进行 Z 字形排列。
 * 
 * 比如输入字符串为 "LEETCODEISHIRING" 行数为 3 时，排列如下：
 * 
 * L   C   I   R
 * E T O E S I I G
 * E   D   H   N
 * 
 * 
 * 之后，你的输出需要从左往右逐行读取，产生出一个新的字符串，比如："LCIRETOESIIGEDHN"。
 * 
 * 请你实现这个将字符串进行指定行数变换的函数：
 * 
 * string convert(string s, int numRows);
 * 
 * 示例 1:
 * 
 * 输入: s = "LEETCODEISHIRING", numRows = 3
 * 输出: "LCIRETOESIIGEDHN"
 * 
 * 
 * 示例 2:
 * 
 * 输入: s = "LEETCODEISHIRING", numRows = 4
 * 输出: "LDREOEIIECIHNTSG"
 * 解释:
 * 
 * L     D     R
 * E   O E   I I
 * E C   I H   N
 * T     S     G
 * 
 */

// @lc code=start
public class Solution {
    //!  这里其实是表示 字符间隔，不用太在意图形表象
    //TODO:
    public string Convert (string s, int numRows) {
        if (numRows == 1) return s;
        int len = s.Length;
        int cycleLen = 2 * numRows - 2;
        StringBuilder sb = new StringBuilder ();
        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j + i < len; j += cycleLen) {
                sb.Append (s[j + i]);
                if (i != 0 && i != numRows - 1 && j + cycleLen - i < len)
                    sb.Append (s[j + cycleLen - i]);
            }
        }
        return sb.ToString ();
    }
}
// @lc code=end