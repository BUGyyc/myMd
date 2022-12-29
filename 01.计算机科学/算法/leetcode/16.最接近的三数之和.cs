/*
 * @lc app=leetcode.cn id=16 lang=csharp
 *
 * [16] 最接近的三数之和
 *
 * https://leetcode-cn.com/problems/3sum-closest/description/
 *
 * algorithms
 * Medium (45.87%)
 * Likes:    603
 * Dislikes: 0
 * Total Accepted:    160.7K
 * Total Submissions: 350.4K
 * Testcase Example:  '[-1,2,1,-4]\n1'
 *
 * 给定一个包括 n 个整数的数组 nums 和 一个目标值 target。找出 nums 中的三个整数，使得它们的和与 target
 * 最接近。返回这三个数的和。假定每组输入只存在唯一答案。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：nums = [-1,2,1,-4], target = 1
 * 输出：2
 * 解释：与 target 最接近的和是 2 (-1 + 2 + 1 = 2) 。
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 3 <= nums.length <= 10^3
 * -10^3 <= nums[i] <= 10^3
 * -10^4 <= target <= 10^4
 * 
 * 
 */

// @lc code=start
public class Solution
{
    //先排序，再start,left,right
    public int ThreeSumClosest(int[] nums, int target)
    {
        Array.Sort(nums);
        int result = nums[0] + nums[1] + nums[2];
        for (int i = 0; i < nums.Length; i++)
        {
            int l = i + 1;
            int r = nums.Length - 1;
            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                if (target == sum)
                {
                    return target;
                }
                else if (Math.Abs(target - sum) < Math.Abs(target - result))
                {
                    result = sum;
                }
                else if (target > sum)
                {
                    l++;
                }
                else if (target < sum)
                {
                    r--;
                }
            }
        }
        return result;
    }
}
// @lc code=end