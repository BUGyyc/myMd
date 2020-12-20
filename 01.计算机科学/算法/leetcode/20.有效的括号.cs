/*
 * @lc app=leetcode.cn id=20 lang=csharp
 *
 * [20] 有效的括号
 *
 * https://leetcode-cn.com/problems/valid-parentheses/description/
 *
 * algorithms
 * Easy (43.12%)
 * Likes:    1912
 * Dislikes: 0
 * Total Accepted:    432.8K
 * Total Submissions: 1M
 * Testcase Example:  '"()"'
 *
 * 给定一个只包括 '('，')'，'{'，'}'，'['，']' 的字符串，判断字符串是否有效。
 * 
 * 有效字符串需满足：
 * 
 * 
 * 左括号必须用相同类型的右括号闭合。
 * 左括号必须以正确的顺序闭合。
 * 
 * 
 * 注意空字符串可被认为是有效字符串。
 * 
 * 示例 1:
 * 
 * 输入: "()"
 * 输出: true
 * 
 * 
 * 示例 2:
 * 
 * 输入: "()[]{}"
 * 输出: true
 * 
 * 
 * 示例 3:
 * 
 * 输入: "(]"
 * 输出: false
 * 
 * 
 * 示例 4:
 * 
 * 输入: "([)]"
 * 输出: false
 * 
 * 
 * 示例 5:
 * 
 * 输入: "{[]}"
 * 输出: true
 * 
 */

// @lc code=start
public class Solution {
    //TODO：利用栈
    public bool IsValid (string s) {
        Stack st = new Stack ();
        for (int i = 0; i < s.Length; i++) {
            string item = s[i].ToString ();
            if (st.Count > 0) {
                string x = st.Peek ().ToString ();
                if (check (item, x)) //x.Equals(item))
                {
                    st.Pop ();
                } else {
                    st.Push (item);
                }
            } else {
                st.Push (item);
            }
        }

        return st.Count == 0;
    }

    private bool check (string a, string b) {
        if (a.Equals (")") && b.Equals ("(")) {
            return true;
        } else if (a.Equals ("}") && b.Equals ("{")) {
            return true;
        } else if (a.Equals ("]") && b.Equals ("[")) {
            return true;
        }
        return false;
    }
}
// @lc code=end