/*
 * @lc app=leetcode.cn id=908 lang=csharp
 *
 * [908] 最小差值 I
 *
 * https://leetcode-cn.com/problems/smallest-range-i/description/
 *
 * algorithms
 * Easy (68.94%)
 * Likes:    57
 * Dislikes: 0
 * Total Accepted:    15.2K
 * Total Submissions: 22K
 * Testcase Example:  '[1]\n0'
 *
 * 给你一个整数数组 A，请你给数组中的每个元素 A[i] 都加上一个任意数字 x （-K <= x <= K），从而得到一个新数组 B 。
 * 
 * 返回数组 B 的最大值和最小值之间可能存在的最小差值。
 * 
 * 
 * 
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：A = [1], K = 0
 * 输出：0
 * 解释：B = [1]
 * 
 * 
 * 示例 2：
 * 
 * 输入：A = [0,10], K = 2
 * 输出：6
 * 解释：B = [2,8]
 * 
 * 
 * 示例 3：
 * 
 * 输入：A = [1,3,6], K = 3
 * 输出：0
 * 解释：B = [3,3,3] 或 B = [4,4,4]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= A.length <= 10000
 * 0 <= A[i] <= 10000
 * 0 <= K <= 10000
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int SmallestRangeI (int[] A, int K) {
        if (A.Length <= 1) return 0;
        Array.Sort (A);
        int a = A[0];
        int b = A[A.Length - 1];
        a += K;
        b -= K;
        return a >= b?0 : b - a;
    }
}
// @lc code=end