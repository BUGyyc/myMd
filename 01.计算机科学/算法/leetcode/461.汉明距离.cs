/*
 * @lc app=leetcode.cn id=461 lang=csharp
 *
 * [461] 汉明距离
 *
 * https://leetcode-cn.com/problems/hamming-distance/description/
 *
 * algorithms
 * Easy (78.51%)
 * Likes:    364
 * Dislikes: 0
 * Total Accepted:    89.2K
 * Total Submissions: 113.7K
 * Testcase Example:  '1\n4'
 *
 * 两个整数之间的汉明距离指的是这两个数字对应二进制位不同的位置的数目。
 * 
 * 给出两个整数 x 和 y，计算它们之间的汉明距离。
 * 
 * 注意：
 * 0 ≤ x, y < 2^31.
 * 
 * 示例:
 * 
 * 
 * 输入: x = 1, y = 4
 * 
 * 输出: 2
 * 
 * 解释:
 * 1   (0 0 0 1)
 * 4   (0 1 0 0)
 * ⁠      ↑   ↑
 * 
 * 上面的箭头指出了对应二进制位不同的位置。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int HammingDistance(int x, int y) {
        int z = x ^ y;
        int result = 0;
        while (z != 0)
        {
            if (z % 2 == 1)
            {
                result++;
            }
            z >>= 1;
        }
        return result;
    }
}
// @lc code=end

