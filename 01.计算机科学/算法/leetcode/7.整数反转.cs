/*
 * @lc app=leetcode.cn id=7 lang=csharp
 *
 * [7] 整数反转
 */

// @lc code=start
public class Solution {
    public int Reverse (int x) {
        int result = 0;
        while (x != 0) {
            int n = x % 10;
            x = x / 10;
            if (result > int.MaxValue/10 || (result == int.MaxValue/10 && n > 7)) {
                return 0;
            }
            if (result < int.MinValue/10 || (result == int.MinValue/10 && n < -8)) {
                return 0;
            }
            result = result * 10 + n;
        }
        return result;
    }
}
// @lc code=end