/*
 * @lc app=leetcode.cn id=7 lang=csharp
 *
 * [7] 整数反转
 */

// @lc code=start
public class Solution
{
    //注意溢出的比较
    public int Reverse(int x)
    {
        int result = 0;
        while (x != 0)
        {
            int n = x % 10;
            x = x / 10;
            //! 判断一些特殊的溢出情况即可
            if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && n > 7))
            {
                return 0;
            }
            if (result < int.MinValue / 10 || (result == int.MinValue / 10 && n < -8))
            {
                return 0;
            }
            result = result * 10 + n;
        }
        return result;
    }
}
// @lc code=end