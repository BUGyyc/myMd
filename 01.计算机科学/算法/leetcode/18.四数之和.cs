/*
 * @lc app=leetcode.cn id=18 lang=csharp
 *
 * [18] 四数之和
 *
 * https://leetcode-cn.com/problems/4sum/description/
 *
 * algorithms
 * Medium (39.17%)
 * Likes:    652
 * Dislikes: 0
 * Total Accepted:    130.2K
 * Total Submissions: 332.3K
 * Testcase Example:  '[1,0,-1,0,-2,2]\n0'
 *
 * 给定一个包含 n 个整数的数组 nums 和一个目标值 target，判断 nums 中是否存在四个元素 a，b，c 和 d ，使得 a + b + c
 * + d 的值与 target 相等？找出所有满足条件且不重复的四元组。
 * 
 * 注意：
 * 
 * 答案中不可以包含重复的四元组。
 * 
 * 示例：
 * 
 * 给定数组 nums = [1, 0, -1, 0, -2, 2]，和 target = 0。
 * 
 * 满足要求的四元组集合为：
 * [
 * ⁠ [-1,  0, 0, 1],
 * ⁠ [-2, -1, 1, 2],
 * ⁠ [-2,  0, 0, 2]
 * ]
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public IList<IList<int>> FourSum(int[] nums, int target) {
                List<IList<int>> result = new List<IList<int>>();
        int len = nums.Length;
        if (len < 4) return result;
        Array.Sort (nums);
        for (int i = 0; i < len - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) continue;
            if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target)
            {
                break;
            }
            if (nums[i] + nums[len - 3] + nums[len - 2] + nums[len - 1] < target
            )
            {
                continue;
            }
            for (int j = i + 1; j < len - 2; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1]) continue;
                if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target)
                {
                    break;
                }
                if (nums[i] + nums[j] + nums[len - 2] + nums[len - 1] < target)
                {
                    continue;
                }
                int left = j + 1;
                int right = len - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[j] + nums[left] + nums[right];
                    if (sum == target)
                    {
                        List<int> list = new List<int>();
                        list.Add(nums[i]);
                        list.Add(nums[j]);
                        list.Add(nums[left]);
                        list.Add(nums[right]);
                        result.Add(list.ToList());
                        while(left < right && nums[left] == nums[left+1]){
                            left++;
                        }
                        left++;
                        while(left < right && nums[right] == nums[right-1]){
                            right--;
                        }
                        right--;
                    }else if(sum > target){
                        right--;
                    }else{
                        left++;
                    }
                }
            }
        }
        return result;
    }
}
// @lc code=end

