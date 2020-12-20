/*
 * @lc app=leetcode.cn id=58 lang=csharp
 *
 * [58] 最后一个单词的长度
 *
 * https://leetcode-cn.com/problems/length-of-last-word/description/
 *
 * algorithms
 * Easy (33.74%)
 * Likes:    246
 * Dislikes: 0
 * Total Accepted:    132K
 * Total Submissions: 390.9K
 * Testcase Example:  '"Hello World"'
 *
 * 给定一个仅包含大小写字母和空格 ' ' 的字符串 s，返回其最后一个单词的长度。如果字符串从左向右滚动显示，那么最后一个单词就是最后出现的单词。
 * 
 * 如果不存在最后一个单词，请返回 0 。
 * 
 * 说明：一个单词是指仅由字母组成、不包含任何空格字符的 最大子字符串。
 * 
 * 
 * 
 * 示例:
 * 
 * 输入: "Hello World"
 * 输出: 5
 * 
 * 
 */

// @lc code=start
public class Solution {
    //从后往前数
    public int LengthOfLastWord (string s) {
        if (s.Length == 0) return 0;
        s = s.Trim ();
        s = s.ToLower ();
        int i = s.Length - 1;
        for (; i >= 0; i--) {
            char c = s[i];
            if (c == ' ') {
                break;
            }
        }
        return s.Length - 1 - i;
    }
}
// @lc code=end