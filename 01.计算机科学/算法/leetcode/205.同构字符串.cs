/*
 * @lc app=leetcode.cn id=205 lang=csharp
 *
 * [205] 同构字符串
 *
 * https://leetcode-cn.com/problems/isomorphic-strings/description/
 *
 * algorithms
 * Easy (49.68%)
 * Likes:    319
 * Dislikes: 0
 * Total Accepted:    85.2K
 * Total Submissions: 171.6K
 * Testcase Example:  '"egg"\n"add"'
 *
 * 给定两个字符串 s 和 t，判断它们是否是同构的。
 * 
 * 如果 s 中的字符可以按某种映射关系替换得到 t ，那么这两个字符串是同构的。
 * 
 * 
 * 每个出现的字符都应当映射到另一个字符，同时不改变字符的顺序。不同字符不能映射到同一个字符上，相同字符只能映射到同一个字符上，字符可以映射到自己本身。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 
 * 输入：s = "egg", t = "add"
 * 输出：true
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：s = "foo", t = "bar"
 * 输出：false
 * 
 * 示例 3：
 * 
 * 
 * 输入：s = "paper", t = "title"
 * 输出：true
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 可以假设 s 和 t 长度相同。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public bool IsIsomorphic (string s, string t) {
        if (s.Length != t.Length) return false;
        if (s.Length == 0 && t.Length == 0) return true;
        Dictionary<char, char> dic = new Dictionary<char, char> ();
        Dictionary<char, char> dic2 = new Dictionary<char, char> ();
        int i = 0;
        while (i < s.Length) {
            char a = s[i];
            char b = t[i];
            if((dic.ContainsKey (a) && dic[a] != b) || (dic2.ContainsKey (b) && dic2[b] != a)) {
                return false;
            } else if(dic.ContainsKey(a) == false && dic2.ContainsKey(b) == false){
                dic.Add (a, b);
                dic2.Add (b, a);
            }
            i++;
        }
        return true;
    }
}
// @lc code=end