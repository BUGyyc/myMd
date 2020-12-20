/*
 * @lc app=leetcode.cn id=162 lang=csharp
 *
 * [162] 寻找峰值
 *
 * https://leetcode-cn.com/problems/find-peak-element/description/
 *
 * algorithms
 * Medium (47.79%)
 * Likes:    323
 * Dislikes: 0
 * Total Accepted:    63.8K
 * Total Submissions: 133.4K
 * Testcase Example:  '[1,2,3,1]'
 *
 * 峰值元素是指其值大于左右相邻值的元素。
 * 
 * 给定一个输入数组 nums，其中 nums[i] ≠ nums[i+1]，找到峰值元素并返回其索引。
 * 
 * 数组可能包含多个峰值，在这种情况下，返回任何一个峰值所在位置即可。
 * 
 * 你可以假设 nums[-1] = nums[n] = -∞。
 * 
 * 示例 1:
 * 
 * 输入: nums = [1,2,3,1]
 * 输出: 2
 * 解释: 3 是峰值元素，你的函数应该返回其索引 2。
 * 
 * 示例 2:
 * 
 * 输入: nums = [1,2,1,3,5,6,4]
 * 输出: 1 或 5 
 * 解释: 你的函数可以返回索引 1，其峰值元素为 2；
 * 或者返回索引 5， 其峰值元素为 6。
 * 
 * 
 * 说明:
 * 
 * 你的解法应该是 O(logN) 时间复杂度的。
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int FindPeakElement (int[] nums) {
        return Search (nums, 0, nums.Length - 1);
        // int len = nums.Length;
        // if (len <= 2) return nums[0];
        // int i = 0;
        // while (i < len) {
        //     bool hasUp = false;
        //     while (i < len - 1 && nums[i] < nums[i + 1]) {
        //         i++;
        //         hasUp = true;
        //     }
        //     bool hasDown = false;
        //     while (i < len - 1 && nums[i] > nums[i + 1]) {
        //         i++;
        //         hasDown = true;
        //     }
        //     i++;
        // }
        // return 
    }

    private int Search (int[] nums, int l, int r) {
        if (l == r) {
            return l;
        }
        int mid = (l + r) / 2;
        if (nums[mid] > nums[mid + 1]) {
            return Search (nums, l, mid);
        }
        return Search (nums, mid + 1, r);
    }
}
// @lc code=end