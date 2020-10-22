/*
 * @lc app=leetcode.cn id=34 lang=csharp
 *
 * [34] 在排序数组中查找元素的第一个和最后一个位置
 *
 * https://leetcode-cn.com/problems/find-first-and-last-position-of-element-in-sorted-array/description/
 *
 * algorithms
 * Medium (40.45%)
 * Likes:    627
 * Dislikes: 0
 * Total Accepted:    140.9K
 * Total Submissions: 348K
 * Testcase Example:  '[5,7,7,8,8,10]\n8'
 *
 * 给定一个按照升序排列的整数数组 nums，和一个目标值 target。找出给定目标值在数组中的开始位置和结束位置。
 * 
 * 你的算法时间复杂度必须是 O(log n) 级别。
 * 
 * 如果数组中不存在目标值，返回 [-1, -1]。
 * 
 * 示例 1:
 * 
 * 输入: nums = [5,7,7,8,8,10], target = 8
 * 输出: [3,4]
 * 
 * 示例 2:
 * 
 * 输入: nums = [5,7,7,8,8,10], target = 6
 * 输出: [-1,-1]
 * 
 */

// @lc code=start
public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return new int[2] { -1, -1 };
        }
        else if (nums.Length == 1)
        {
            if (nums[0] != target)
            {
                return new int[2] { -1, -1 };
            }
            else
            {
                return new int[2] { 0, 0 };
            }
        }

        int s = 0;
        int e = nums.Length - 1;
        int start = -1;
        int end = -1;
        while (s <= e)
        {

            if (nums[s] < target)
            {
                s++;
            }
            else if (nums[s] == target)
            {
                if (start == -1) start = s;
            }



            if (nums[e] > target)
            {
                e--;
            }
            else if (nums[e] == target)
            {
                if (end == -1) end = e;
            }

            if (start != -1 && end != -1)
            {
                break;
            }
        }

        return new int[2] { start, end };

    }
}
// @lc code=end

