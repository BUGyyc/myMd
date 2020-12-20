/*
 * @lc app=leetcode.cn id=387 lang=csharp
 *
 * [387] 字符串中的第一个唯一字符
 *
 * https://leetcode-cn.com/problems/first-unique-character-in-a-string/description/
 *
 * algorithms
 * Easy (47.52%)
 * Likes:    282
 * Dislikes: 0
 * Total Accepted:    108K
 * Total Submissions: 227.3K
 * Testcase Example:  '"leetcode"'
 *
 * 给定一个字符串，找到它的第一个不重复的字符，并返回它的索引。如果不存在，则返回 -1。
 * 
 * 
 * 
 * 示例：
 * 
 * s = "leetcode"
 * 返回 0
 * 
 * s = "loveleetcode"
 * 返回 2
 * 
 * 
 * 
 * 
 * 提示：你可以假定该字符串只包含小写字母。
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int FirstUniqChar (string s) {
        Dictionary<char, int> dic = new Dictionary<char, int> ();
        for (int i = 0; i < s.Length; i++) {
            if (dic.ContainsKey (s[i])) {
                int val = dic[s[i]];
                dic[s[i]] = val + 1;
            } else {
                dic[s[i]] = 1;
            }
        }

        foreach (var item in dic) {
            if (item.Value == 1) {
                return s.IndexOf (item.Key);
            }
        }
        return -1;
    }
}
// @lc code=end