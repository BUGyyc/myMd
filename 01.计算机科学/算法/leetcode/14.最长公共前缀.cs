/*
 * @lc app=leetcode.cn id=14 lang=csharp
 *
 * [14] 最长公共前缀
 *
 * https://leetcode-cn.com/problems/longest-common-prefix/description/
 *
 * algorithms
 * Easy (38.82%)
 * Likes:    1305
 * Dislikes: 0
 * Total Accepted:    375.8K
 * Total Submissions: 967.9K
 * Testcase Example:  '["flower","flow","flight"]'
 *
 * 编写一个函数来查找字符串数组中的最长公共前缀。
 * 
 * 如果不存在公共前缀，返回空字符串 ""。
 * 
 * 示例 1:
 * 
 * 输入: ["flower","flow","flight"]
 * 输出: "fl"
 * 
 * 
 * 示例 2:
 * 
 * 输入: ["dog","racecar","car"]
 * 输出: ""
 * 解释: 输入不存在公共前缀。
 * 
 * 
 * 说明:
 * 
 * 所有输入只包含小写字母 a-z 。
 * 
 */

// @lc code=start
public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0) return "";
        if (strs.Length == 1) return strs[0];
        int len = 1;
        int max = strs[0].Length;
        while (len <= max)
        {
            string s = strs[0].Substring(0, len);
            if (Check(strs, s, len) == false) return strs[0].Substring(0, len - 1);
            len++;
        }
        return strs[0];
    }

    private bool Check(string[] strs, string s, int len)
    {
        for (int i = 1; i < strs.Length; i++)
        {
            string item = strs[i];
            if (item.Length < len) return false;
            if (item.Substring(0, len).Equals(s) == false) return false;
        }

        return true;
    }
}
// @lc code=end

