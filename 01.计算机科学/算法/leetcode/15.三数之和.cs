/*
 * @lc app=leetcode.cn id=15 lang=csharp
 *
 * [15] 三数之和
 *
 * https://leetcode-cn.com/problems/3sum/description/
 *
 * algorithms
 * Medium (29.77%)
 * Likes:    2676
 * Dislikes: 0
 * Total Accepted:    345.3K
 * Total Submissions: 1.2M
 * Testcase Example:  '[-1,0,1,2,-1,-4]'
 *
 * 给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，使得 a + b + c = 0
 * ？请你找出所有满足条件且不重复的三元组。
 * 
 * 注意：答案中不可以包含重复的三元组。
 * 
 * 
 * 
 * 示例：
 * 
 * 给定数组 nums = [-1, 0, 1, 2, -1, -4]，
 * 
 * 满足要求的三元组集合为：
 * [
 * ⁠ [-1, 0, 1],
 * ⁠ [-1, -1, 2]
 * ]
 * 
 * 
 */

// @lc code=start
public class Solution
{
    //排序数组，然后从第一位开始，再来取首尾指针
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        List<IList<int>> result = new List<IList<int>>();
        if (nums.Length <= 2) return result;
        Array.Sort(nums);
        int start = 0;
        while (start < nums.Length - 2)
        {
            int left = start + 1;
            int right = nums.Length - 1;
            int target = 0 - nums[start];
            while (left < right)
            {
                if (target > nums[left] + nums[right])
                {
                    left++;
                }
                else if (target < nums[left] + nums[right])
                {
                    right--;
                }
                else
                {
                    var list = new List<int>();
                    list.Add(nums[start]);
                    list.Add(nums[left]);
                    list.Add(nums[right]);
                    result.Add(list);

                    //去除重复元素
                    while (left < right && nums[left] == list[1])
                        ++left;
                    while (left < right && nums[right] == list[2])
                        --right;
                }
            }
            //去除重复元素
            int currentStartNumber = nums[start];
            while (start < nums.Length - 2 && nums[start] == currentStartNumber)
                ++start;
        }
        return result;
    }
}
// @lc code=end