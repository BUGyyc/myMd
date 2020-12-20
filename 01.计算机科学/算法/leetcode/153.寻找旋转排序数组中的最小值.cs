/*
 * @lc app=leetcode.cn id=153 lang=csharp
 *
 * [153] 寻找旋转排序数组中的最小值
 *
 * https://leetcode-cn.com/problems/find-minimum-in-rotated-sorted-array/description/
 *
 * algorithms
 * Medium (51.77%)
 * Likes:    284
 * Dislikes: 0
 * Total Accepted:    85.5K
 * Total Submissions: 165.2K
 * Testcase Example:  '[3,4,5,1,2]'
 *
 * 假设按照升序排序的数组在预先未知的某个点上进行了旋转。
 * 
 * ( 例如，数组 [0,1,2,4,5,6,7]  可能变为 [4,5,6,7,0,1,2] )。
 * 
 * 请找出其中最小的元素。
 * 
 * 你可以假设数组中不存在重复元素。
 * 
 * 示例 1:
 * 
 * 输入: [3,4,5,1,2]
 * 输出: 1
 * 
 * 示例 2:
 * 
 * 输入: [4,5,6,7,0,1,2]
 * 输出: 0
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int FindMin (int[] nums) {
        int len = nums.Length;
        if (len == 0) return 0;
        if (len == 1) return nums[0];
        int min = nums[0];
        for (int i = 0; i < len - 1; i++) {
            if (nums[i] > nums[i + 1]) {
                min = nums[i + 1];
                break;
            }
        }
        return min;
    }
}
// @lc code=end