/*
 * @lc app=leetcode.cn id=300 lang=csharp
 *
 * [300] 最长上升子序列
 *
 * https://leetcode-cn.com/problems/longest-increasing-subsequence/description/
 *
 * algorithms
 * Medium (45.29%)
 * Likes:    1122
 * Dislikes: 0
 * Total Accepted:    163K
 * Total Submissions: 359.8K
 * Testcase Example:  '[10,9,2,5,3,7,101,18]'
 *
 * 给定一个无序的整数数组，找到其中最长上升子序列的长度。
 * 
 * 示例:
 * 
 * 输入: [10,9,2,5,3,7,101,18]
 * 输出: 4 
 * 解释: 最长的上升子序列是 [2,3,7,101]，它的长度是 4。
 * 
 * 说明:
 * 
 * 
 * 可能会有多种最长上升子序列的组合，你只需要输出对应的长度即可。
 * 你算法的时间复杂度应该为 O(n^2) 。
 * 
 * 
 * 进阶: 你能将算法的时间复杂度降低到 O(n log n) 吗?
 * 
 */

// @lc code=start
public class Solution {
    public int LengthOfLIS (int[] nums) {
        int len = nums.Length;
        int i = 0;
        if (len == 0) return 0;
        if (len == 1) return 1;
        if (len == 2) {
            if (nums[0] < nums[1]) {
                return 2;
            } else {
                return 0;
            }
        }
        int max = 0;
        while (i < len) {
            int l = 0;
            while (i < len - 1 && nums[i] < nums[i + 1]) {
                i++;
                l++;
            }
            max = Math.Max (max, l + 1);
            i++;
        }
        return max;
    }
}
// @lc code=end