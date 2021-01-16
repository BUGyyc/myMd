/*
 * @lc app=leetcode.cn id=594 lang=csharp
 *
 * [594] 最长和谐子序列
 *
 * https://leetcode-cn.com/problems/longest-harmonious-subsequence/description/
 *
 * algorithms
 * Easy (49.50%)
 * Likes:    146
 * Dislikes: 0
 * Total Accepted:    19.4K
 * Total Submissions: 39.2K
 * Testcase Example:  '[1,3,2,2,5,2,3,7]'
 *
 * 和谐数组是指一个数组里元素的最大值和最小值之间的差别正好是1。
 * 
 * 现在，给定一个整数数组，你需要在所有可能的子序列中找到最长的和谐子序列的长度。
 * 
 * 示例 1:
 * 
 * 
 * 输入: [1,3,2,2,5,2,3,7]
 * 输出: 5
 * 原因: 最长的和谐数组是：[3,2,2,2,3].
 * 
 * 
 * 说明: 输入的数组长度最大不超过20,000.
 * 
 */

// @lc code=start
public class Solution {
    public int FindLHS (int[] nums) {
        if (nums == null || nums.Length < 1) return 0;
        if (nums.Length == 1) return 1;
        Array.Sort (nums);
        int count = 0;
        int max = 0;
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] - nums[i - 1] <= 1) {
                if (count == 0) {
                    count = 1;
                } else {
                    count++;
                }
            } else {
                max = Math.Max (max, count);
                count = 0;
            }
        }
        max = Math.Max (max, count);
        return max;
    }
}
// @lc code=end