/*
 * @lc app=leetcode.cn id=9 lang=csharp
 *
 * [9] 回文数
 */

// @lc code=start
public class Solution {
    //首尾比较
    public bool IsPalindrome (int x) {
        string s = x.ToString ();
        char[] chs = s.ToCharArray ();
        int i = 0;
        int j = chs.Length - 1;
        while (i < j) {
            if (chs[i] != chs[j]) {
                return false;
            } else {
                i++;
                j--;
            }
        }
        return true;
    }
}
// @lc code=end