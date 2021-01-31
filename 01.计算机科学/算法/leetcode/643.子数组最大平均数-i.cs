/*
 * @lc app=leetcode.cn id=643 lang=csharp
 *
 * [643] 子数组最大平均数 I
 *
 * https://leetcode-cn.com/problems/maximum-average-subarray-i/description/
 *
 * algorithms
 * Easy (39.59%)
 * Likes:    120
 * Dislikes: 0
 * Total Accepted:    23.1K
 * Total Submissions: 58.5K
 * Testcase Example:  '[1,12,-5,-6,50,3]\n4'
 *
 * 给定 n 个整数，找出平均数最大且长度为 k 的连续子数组，并输出该最大平均数。
 * 
 * 
 * 
 * 示例：
 * 
 * 
 * 输入：[1,12,-5,-6,50,3], k = 4
 * 输出：12.75
 * 解释：最大平均数 (12-5-6+50)/4 = 51/4 = 12.75
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 k n 
 * 所给数据范围 [-10,000，10,000]。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public double FindMaxAverage (int[] nums, int k) {
        int len = nums.Length;
        if (len == 0) return 0;
        int[] res = new int[len];
        int Max = nums[0];
        for (int i = 0; i < len; i++) {
            int step = i + 1;
            int max = nums[i];
            int sum = nums[i];
            while (step <= k && step < len) {
                sum += nums[step++];
                max = Math.Max (sum, max);
            }
            res[i] = max;
            Max = Math.Max (Max, max);
        }
        return Max * 1f / k * 1f;
    }
}
// @lc code=end