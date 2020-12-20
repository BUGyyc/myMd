/*
 * @lc app=leetcode.cn id=169 lang=csharp
 *
 * [169] 多数元素
 *
 * https://leetcode-cn.com/problems/majority-element/description/
 *
 * algorithms
 * Easy (64.85%)
 * Likes:    777
 * Dislikes: 0
 * Total Accepted:    228.2K
 * Total Submissions: 351.9K
 * Testcase Example:  '[3,2,3]'
 *
 * 给定一个大小为 n 的数组，找到其中的多数元素。多数元素是指在数组中出现次数大于 ⌊ n/2 ⌋ 的元素。
 * 
 * 你可以假设数组是非空的，并且给定的数组总是存在多数元素。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: [3,2,3]
 * 输出: 3
 * 
 * 示例 2:
 * 
 * 输入: [2,2,1,1,1,2,2]
 * 输出: 2
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int MajorityElement (int[] nums) {
        int len = nums.Length;
        int half = len / 2;
        Array.Sort (nums);
        if (len <= 2) {
            return nums[0];
        } else if (len == 3) {
            return (nums[0] == nums[1]) ? nums[0] : nums[2];
        }
        if (nums[half] == nums[half + 1]) {
            return nums[half];
        } else if (nums[half] == nums[half - 1]) {
            return nums[half];
        }
        return nums[half];
    }
}
// @lc code=end