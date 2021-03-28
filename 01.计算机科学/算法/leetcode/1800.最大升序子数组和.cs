/*
 * @lc app=leetcode.cn id=1800 lang=csharp
 *
 * [1800] 最大升序子数组和
 *
 * https://leetcode-cn.com/problems/maximum-ascending-subarray-sum/description/
 *
 * algorithms
 * Easy (69.89%)
 * Likes:    7
 * Dislikes: 0
 * Total Accepted:    6.1K
 * Total Submissions: 8.7K
 * Testcase Example:  '[10,20,30,5,10,50]'
 *
 * 给你一个正整数组成的数组 nums ，返回 nums 中一个 升序 子数组的最大可能元素和。
 * 
 * 子数组是数组中的一个连续数字序列。
 * 
 * 已知子数组 [numsl, numsl+1, ..., numsr-1, numsr] ，若对所有 i（l ），numsi  < numsi+1
 * 都成立，则称这一子数组为 升序 子数组。注意，大小为 1 的子数组也视作 升序 子数组。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：nums = [10,20,30,5,10,50]
 * 输出：65
 * 解释：[5,10,50] 是元素和最大的升序子数组，最大元素和为 65 。
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：nums = [10,20,30,40,50]
 * 输出：150
 * 解释：[10,20,30,40,50] 是元素和最大的升序子数组，最大元素和为 150 。 
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：nums = [12,17,15,13,10,11,12]
 * 输出：33
 * 解释：[10,11,12] 是元素和最大的升序子数组，最大元素和为 33 。 
 * 
 * 
 * 示例 4：
 * 
 * 
 * 输入：nums = [100,10,1]
 * 输出：100
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 
 * 1 
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int MaxAscendingSum (int[] nums) {
        if (nums.Length == 0) return 0;
        if (nums.Length == 1) return nums[0];
        int max = int.MinValue;
        int sum = nums[0];
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] > nums[i - 1]) {
                sum += nums[i];
            } else {
                max = Math.Max (sum, max);
                sum = nums[i];
            }
            max = Math.Max (sum, max);
        }
        return max;
    }
}
// @lc code=end