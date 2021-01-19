/*
 * @lc app=leetcode.cn id=557 lang=csharp
 *
 * [557] 反转字符串中的单词 III
 *
 * https://leetcode-cn.com/problems/reverse-words-in-a-string-iii/description/
 *
 * algorithms
 * Easy (73.72%)
 * Likes:    265
 * Dislikes: 0
 * Total Accepted:    107.7K
 * Total Submissions: 146K
 * Testcase Example:  `"Let's take LeetCode contest"`
 *
 * 给定一个字符串，你需要反转字符串中每个单词的字符顺序，同时仍保留空格和单词的初始顺序。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入："Let's take LeetCode contest"
 * 输出："s'teL ekat edoCteeL tsetnoc"
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 在字符串中，每个单词由单个空格分隔，并且字符串中不会有任何额外的空格。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string ReverseWords (string s) {
        if (s.Length == 0) return "";
        Stack<char> stack = new Stack<char> ();
        char[] cs = s.ToCharArray ();
        StringBuilder sb = new StringBuilder ();
        for (int i = 0; i < cs.Length; i++) {
            char c = cs[i];
            if (i == cs.Length - 1) {
                stack.Push (c);
                while (stack.Count > 0) {
                    char a = stack.Pop ();
                    sb.Append (a);
                }
            } else if (c != ' ') {
                stack.Push (c);
            } else {
                while (stack.Count > 0) {
                    char a = stack.Pop ();
                    sb.Append (a);
                }
                sb.Append (' ');
            }
        }
        return sb.ToString ();
    }
}
// @lc code=end