/*
 * @lc app=leetcode.cn id=9 lang=csharp
 *
 * [9] 回文数
 */

// @lc code=start
public class Solution {
    //除10 与乘10 比较
    public bool IsPalindrome (int x) {
        if (x < 0 || (x % 10 == 0 && x != 0)) return false;
        if (x == 0) return true;
        int y = 0;
        while (x > y) {
            int tmp = x % 10;
            y = y * 10 + tmp;
            x /= 10;
        }
        return x == y || (x == (y / 10));
    }

    //转字符后判断
    private bool Func (int x) {
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