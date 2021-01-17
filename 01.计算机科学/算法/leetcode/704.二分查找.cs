/*
 * @lc app=leetcode.cn id=704 lang=csharp
 *
 * [704] 二分查找
 *
 * https://leetcode-cn.com/problems/binary-search/description/
 *
 * algorithms
 * Easy (55.76%)
 * Likes:    188
 * Dislikes: 0
 * Total Accepted:    88K
 * Total Submissions: 157.8K
 * Testcase Example:  '[-1,0,3,5,9,12]\n9'
 *
 * 给定一个 n 个元素有序的（升序）整型数组 nums 和一个目标值 target  ，写一个函数搜索 nums 中的
 * target，如果目标值存在返回下标，否则返回 -1。
 * 
 * 
 * 示例 1:
 * 
 * 输入: nums = [-1,0,3,5,9,12], target = 9
 * 输出: 4
 * 解释: 9 出现在 nums 中并且下标为 4
 * 
 * 
 * 示例 2:
 * 
 * 输入: nums = [-1,0,3,5,9,12], target = 2
 * 输出: -1
 * 解释: 2 不存在 nums 中因此返回 -1
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 你可以假设 nums 中的所有元素是不重复的。
 * n 将在 [1, 10000]之间。
 * nums 的每个元素都将在 [-9999, 9999]之间。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int Search (int[] nums, int target) {
        int i = 0, j = nums.Length - 1;
        while (i <= j) {
            int mid = (i + j) / 2;
            if (nums[mid] > target) {
                j = mid - 1;
            } else if (nums[mid] < target) {
                i = mid + 1;
            } else {
                return mid;
            }
        }
        return -1;
    }
}
// @lc code=end