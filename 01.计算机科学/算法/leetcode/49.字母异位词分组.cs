/*
 * @lc app=leetcode.cn id=49 lang=csharp
 *
 * [49] 字母异位词分组
 *
 * https://leetcode-cn.com/problems/group-anagrams/description/
 *
 * algorithms
 * Medium (63.90%)
 * Likes:    496
 * Dislikes: 0
 * Total Accepted:    114.5K
 * Total Submissions: 179.2K
 * Testcase Example:  '["eat","tea","tan","ate","nat","bat"]'
 *
 * 给定一个字符串数组，将字母异位词组合在一起。字母异位词指字母相同，但排列不同的字符串。
 * 
 * 示例:
 * 
 * 输入: ["eat", "tea", "tan", "ate", "nat", "bat"]
 * 输出:
 * [
 * ⁠ ["ate","eat","tea"],
 * ⁠ ["nat","tan"],
 * ⁠ ["bat"]
 * ]
 * 
 * 说明：
 * 
 * 
 * 所有输入均为小写字母。
 * 不考虑答案输出的顺序。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public IList<IList<string>> GroupAnagrams (string[] strs) {
        Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>> ();
        foreach (var str in strs) {
            int[] cs = new int[26];
            foreach (var c in str) {
                cs[c - 'a']++;
            }
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < 26; i++) {
                if (cs[i] > 0) {
                    sb.Append ((char) ('a' + i));
                    sb.Append (cs[i]);
                }
            }
            string s = sb.ToString ();
            if (dic.ContainsKey (s)) {
                dic[s].Add (str);
            } else {
                List<string> list = new List<string> ();
                list.Add (str);
                dic.Add (s, list.ToList ());
            }
        }
        IList<IList<string>> res = new List<IList<string>> ();
        foreach (var item in dic) {
            var list = item.Value;
            res.Add (list);
        }
        return res;
    }
}
// @lc code=end