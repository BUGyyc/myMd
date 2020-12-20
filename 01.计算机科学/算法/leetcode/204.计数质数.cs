/*
 * @lc app=leetcode.cn id=204 lang=csharp
 *
 * [204] 计数质数
 *
 * https://leetcode-cn.com/problems/count-primes/description/
 *
 * algorithms
 * Easy (35.54%)
 * Likes:    467
 * Dislikes: 0
 * Total Accepted:    83K
 * Total Submissions: 233.4K
 * Testcase Example:  '10'
 *
 * 统计所有小于非负整数 n 的质数的数量。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：n = 10
 * 输出：4
 * 解释：小于 10 的质数一共有 4 个, 它们是 2, 3, 5, 7 。
 * 
 * 
 * 示例 2：
 * 
 * 输入：n = 0
 * 输出：0
 * 
 * 
 * 示例 3：
 * 
 * 输入：n = 1
 * 输出：0
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 0 <= n <= 5 * 10^6
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int CountPrimes (int n) {
        if (n < 3)
            return 0;;
        //从3开始验算，所以初始值为1（2为质数）。
        int count = 1;
        for (int i = 3; i < n; i++) {
            //当某个数为 2 的 n 次方时（n为自然数），其 & (n - 1) 所得值将等价于取余运算所得值
            //*如果 x = 2^n ，则 x & (n - 1) == x % n
            //if(i % 2 == 0)
            if ((i & 1) == 0)
                continue;;
            bool sign = true;
            //用 j * j <= i 代替 j <= √i 会更好。
            //因为我们已经排除了所有偶数，所以每次循环加二将规避偶数会减少循环次数
            for (int j = 3; j * j <= i; j += 2) {
                if (i % j == 0) {
                    sign = false;
                    break;
                }
            }
            if (sign)
                count++;;
        }
        return count;
    }
}
// @lc code=end