/*
 * @lc app=leetcode.cn id=137 lang=csharp
 *
 * [137] 只出现一次的数字 II
 *
 * https://leetcode-cn.com/problems/single-number-ii/description/
 *
 * algorithms
 * Medium (68.18%)
 * Likes:    452
 * Dislikes: 0
 * Total Accepted:    44.1K
 * Total Submissions: 64.7K
 * Testcase Example:  '[2,2,3,2]'
 *
 * 给定一个非空整数数组，除了某个元素只出现一次以外，其余每个元素均出现了三次。找出那个只出现了一次的元素。
 * 
 * 说明：
 * 
 * 你的算法应该具有线性时间复杂度。 你可以不使用额外空间来实现吗？
 * 
 * 示例 1:
 * 
 * 输入: [2,2,3,2]
 * 输出: 3
 * 
 * 
 * 示例 2:
 * 
 * 输入: [0,1,0,1,0,1,99]
 * 输出: 99
 * 
 */

// @lc code=start
public class Solution {
    public int SingleNumber(int[] nums) {
        Dictionary<int,int> result = new Dictionary<int,int>();
        int all = 0;
        int sum = 0;
        for(int i = 0;i<nums.Length;i++){
            all += nums[i];
            if(result.ContainsKey(nums[i]) == false){
                sum += nums[i];
                result.Add(nums[i],nums[i]);
            }
        }
        return (3*sum - all)/2;
    }
}
// @lc code=end

