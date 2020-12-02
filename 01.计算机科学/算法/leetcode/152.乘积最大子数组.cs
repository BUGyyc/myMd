/*
 * @lc app=leetcode.cn id=152 lang=csharp
 *
 * [152] 乘积最大子数组
 *
 * https://leetcode-cn.com/problems/maximum-product-subarray/description/
 *
 * algorithms
 * Medium (40.53%)
 * Likes:    816
 * Dislikes: 0
 * Total Accepted:    101K
 * Total Submissions: 249.2K
 * Testcase Example:  '[2,3,-2,4]'
 *
 * 给你一个整数数组 nums ，请你找出数组中乘积最大的连续子数组（该子数组中至少包含一个数字），并返回该子数组所对应的乘积。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: [2,3,-2,4]
 * 输出: 6
 * 解释: 子数组 [2,3] 有最大乘积 6。
 * 
 * 
 * 示例 2:
 * 
 * 输入: [-2,0,-1]
 * 输出: 0
 * 解释: 结果不能为 2, 因为 [-2,-1] 不是子数组。
 * 
 */

// @lc code=start
public class Solution {
    public int MaxProduct (int[] nums) {
        int max = int.MinValue;
        int[] result = new int[nums.Length];
        int[] mins = new int[nums.Length];
        result[0] = nums[0];
        for(int i = 1;i< nums.Length; i++){
            result[i] = Math.Max(Math.Max(result[i-1]*nums[i],nums[i]) , Math.Max(mins[i-1]*nums[i],nums[i]));
            mins[i] = Math.Min(Math.Min(result[i-1]*nums[i],nums[i]),Math.Min(mins[i-1]*nums[i],nums[i]));
        }
        for(int i = 0;i<result.Length;i++){
            max = Math.Max(max, result[i]);
        }
        return max;
    }
}
// @lc code=end