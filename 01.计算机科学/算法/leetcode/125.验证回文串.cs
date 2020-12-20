/*
 * @lc app=leetcode.cn id=125 lang=csharp
 *
 * [125] 验证回文串
 *
 * https://leetcode-cn.com/problems/valid-palindrome/description/
 *
 * algorithms
 * Easy (46.56%)
 * Likes:    290
 * Dislikes: 0
 * Total Accepted:    175.5K
 * Total Submissions: 376.9K
 * Testcase Example:  '"A man, a plan, a canal: Panama"'
 *
 * 给定一个字符串，验证它是否是回文串，只考虑字母和数字字符，可以忽略字母的大小写。
 * 
 * 说明：本题中，我们将空字符串定义为有效的回文串。
 * 
 * 示例 1:
 * 
 * 输入: "A man, a plan, a canal: Panama"
 * 输出: true
 * 
 * 
 * 示例 2:
 * 
 * 输入: "race a car"
 * 输出: false
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public bool IsPalindrome (string s) {
        if (s.Length == 0) return true;
        string str = s.ToLower ();
        int l = 0;
        int r = str.Length - 1;
        while (l < r) {
            if (CheckValue (str[l]) == false) {
                l++;
            } else if (CheckValue (str[r]) == false) {
                r--;
            } else if (str[l] != str[r]) {
                return false;
            } else {
                l++;
                r--;
            }
        }
        return true;
    }

    private bool CheckValue (char c) {
        if ('0' <= c && c <= '9') {
            return true;
        } else if ('a' <= c && c <= 'z') {
            return true;
        } else {
            return false;
        }
    }
}
// @lc code=end