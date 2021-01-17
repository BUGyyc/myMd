/*
 * @lc app=leetcode.cn id=709 lang=csharp
 *
 * [709] 转换成小写字母
 *
 * https://leetcode-cn.com/problems/to-lower-case/description/
 *
 * algorithms
 * Easy (76.04%)
 * Likes:    140
 * Dislikes: 0
 * Total Accepted:    59.7K
 * Total Submissions: 78.5K
 * Testcase Example:  '"Hello"'
 *
 * 实现函数 ToLowerCase()，该函数接收一个字符串参数 str，并将该字符串中的大写字母转换成小写字母，之后返回新的字符串。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入: "Hello"
 * 输出: "hello"
 * 
 * 示例 2：
 * 
 * 
 * 输入: "here"
 * 输出: "here"
 * 
 * 示例 3：
 * 
 * 
 * 输入: "LOVELY"
 * 输出: "lovely"
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string ToLowerCase (string str) {
        StringBuilder sb = new StringBuilder ();
        int step = 'A' - 'a';
        for (int i = 0; i < str.Length; i++) {
            if (str[i] >= 'A' && str[i] <= 'Z') {
                char c = (char)(str[i] - step);
                sb.Append (c);
            } else {
                sb.Append (str[i]);
            }
        }
        return sb.ToString ();
    }
}
// @lc code=end