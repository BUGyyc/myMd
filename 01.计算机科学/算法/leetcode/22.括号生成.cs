/*
 * @lc app=leetcode.cn id=22 lang=csharp
 *
 * [22] 括号生成
 *
 * https://leetcode-cn.com/problems/generate-parentheses/description/
 *
 * algorithms
 * Medium (76.38%)
 * Likes:    1369
 * Dislikes: 0
 * Total Accepted:    189.3K
 * Total Submissions: 247.8K
 * Testcase Example:  '3'
 *
 * 数字 n 代表生成括号的对数，请你设计一个函数，用于能够生成所有可能的并且 有效的 括号组合。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：n = 3
 * 输出：[
 * ⁠      "((()))",
 * ⁠      "(()())",
 * ⁠      "(())()",
 * ⁠      "()(())",
 * ⁠      "()()()"
 * ⁠    ]
 * 
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<string> GenerateParenthesis (int n) {
        List<string> list = new List<string> ();
        BackTrack (list, 0, 0, n, new StringBuilder ());
        return list;
    }

    private void BackTrack (List<string> list, int open, int close, int n, StringBuilder sb) {
        if (sb.Length == 2 * n) {
            list.Add (sb.ToString ());
            return;
        }
        if (open < n) {
            sb.Append ('(');
            BackTrack (list, open + 1, close, n, sb);
            // str.Remove(str.Length-1);
            sb.Remove (sb.Length - 1, 1);
        }
        if (close < open) {
            sb.Append (')');
            BackTrack (list, open, close + 1, n, sb);
            // str.Remove(str.Length-1);
            sb.Remove (sb.Length - 1, 1);
        }

    }
}
// @lc code=end