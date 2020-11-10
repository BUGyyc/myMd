/*
 * @lc app=leetcode.cn id=219 lang=csharp
 *
 * [219] 存在重复元素 II
 *
 * https://leetcode-cn.com/problems/contains-duplicate-ii/description/
 *
 * algorithms
 * Easy (40.57%)
 * Likes:    209
 * Dislikes: 0
 * Total Accepted:    66.2K
 * Total Submissions: 163.1K
 * Testcase Example:  '[1,2,3,1]\n3'
 *
 * 给定一个整数数组和一个整数 k，判断数组中是否存在两个不同的索引 i 和 j，使得 nums [i] = nums [j]，并且 i 和 j 的差的
 * 绝对值 至多为 k。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: nums = [1,2,3,1], k = 3
 * 输出: true
 * 
 * 示例 2:
 * 
 * 输入: nums = [1,0,1,1], k = 1
 * 输出: true
 * 
 * 示例 3:
 * 
 * 输入: nums = [1,2,3,1,2,3], k = 2
 * 输出: false
 * 
 */

// @lc code=start
public class Solution {
    public bool ContainsNearbyDuplicate (int[] nums, int k) {
        if (k > nums.Length - 1) return false;
        for (int i = 0; i < nums.Length - k; i++) {
            int a = nums[i];
            int j = 0;
            while (j < k) {
                if (nums[j + i] == a) {
                    return true;
                }
                j++;
            }
        }
        return false;
    }
}
// @lc code=end