/*
 * @lc app=leetcode.cn id=5 lang=csharp
 *
 * [5] 最长回文子串
 *
 * https://leetcode-cn.com/problems/longest-palindromic-substring/description/
 *
 * algorithms
 * Medium (32.06%)
 * Likes:    2791
 * Dislikes: 0
 * Total Accepted:    395.1K
 * Total Submissions: 1.2M
 * Testcase Example:  '"babad"'
 *
 * 给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为 1000。
 * 
 * 示例 1：
 * 
 * 输入: "babad"
 * 输出: "bab"
 * 注意: "aba" 也是一个有效答案。
 * 
 * 
 * 示例 2：
 * 
 * 输入: "cbbd"
 * 输出: "bb"
 * 
 * 
 */

// @lc code=start
public class Solution {
    //动态规划判断 是否是 回文子串
    public string LongestPalindrome (string s) {
        //动态规划
        int n = s.Length;
        if (n < 2) return s;
        bool[, ] dp = new bool[n, n];
        string str = "";
        for (int i = 0; i < n; i++) {
            dp[i, i] = true;
        }
        int maxLen = 1;
        int begin = 0;
        //从回文长度为1开始判断，到字符长度n为止
        for (int len = 1; len <= n; len++) {
            for (int start = 0; start < n; start++) {
                //终点(end) = 起点（start） + 间隔（len - 1）
                int end = start + len - 1;

                //结尾点下标超出字符串长度
                if (end >= n)
                    break;
                //回文字符串的公式就是  子字符串也是回文，并且当前起点和重点的字符相等，所以得出公式
                //p[start+1,end-1]  && s[start] == s[end]
                //有回文长度为1和2两种特殊情况，其特殊性在于他们不存在子字符串
                //回文长度为1时，自身当然等于自身
                //回文长度为2时，起点和终点是相邻的，只要相邻的字符相等就可以
                dp[start, end] = (len == 1 || len == 2 || dp[start + 1, end - 1]) && s[start] == s[end];

                //当前字符串是回文，并且当前回文长度大于最长回文长度时，修改result
                if (dp[start, end] && len > str.Length)
                    str = s.Substring (start, len);
            }
        }
        return str;
    }

    private string Func (string s) {
        int len = s.Length ();
        bool[, ] dp = new bool[len, len];
        string str = "";
        for (int l = 0; l < len; l++) {
            for (int i = 0; i + l < len; i++) {
                int j = i + l;
                if (l == 0) {
                    dp[i, j] = true;
                } else if (l == 1) {
                    dp[i, j] = s[i] == s[j];
                } else {
                    dp[i, j] = s[i] == s[j] && dp[i + 1, i - 1];
                }

                //最后看长度是否超过之前的
                if (dp[i, j] && l + 1 > str.Length ()) {
                    str = s.Substring (i, i + l + 1);
                }
            }
        }
        return str;
    }
}
// @lc code=end