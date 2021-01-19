/*
 * @lc app=leetcode.cn id=476 lang=csharp
 *
 * [476] 数字的补数
 *
 * https://leetcode-cn.com/problems/number-complement/description/
 *
 * algorithms
 * Easy (69.28%)
 * Likes:    189
 * Dislikes: 0
 * Total Accepted:    27.6K
 * Total Submissions: 39.8K
 * Testcase Example:  '5'
 *
 * 给定一个正整数，输出它的补数。补数是对该数的二进制表示取反。
 * 
 * 
 * 
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: 5
 * 输出: 2
 * 解释: 5 的二进制表示为 101（没有前导零位），其补数为 010。所以你需要输出 2 。
 * 
 * 
 * 示例 2:
 * 
 * 输入: 1
 * 输出: 0
 * 解释: 1 的二进制表示为 1（没有前导零位），其补数为 0。所以你需要输出 0 。
 * 
 * 
 * 
 * 
 * 注意:
 * 
 * 
 * 给定的整数保证在 32 位带符号整数的范围内。
 * 你可以假定二进制数不包含前导零位。
 * 本题与 1009 https://leetcode-cn.com/problems/complement-of-base-10-integer/ 相同
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int FindComplement (int num) {
        int maxBit = 0;
        int tmp = num;
        while (tmp > 0) {
            tmp >>= 1;
            maxBit++;
        }
        int val = num ^ ((1 << maxBit) - 1);
        return val;
    }
}
// @lc code=end