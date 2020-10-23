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
public class Solution {
    public IList<IList<int>> ThreeSum (int[] nums) {
        List<IList<int>> result = new List<IList<int>> ();
        for (int i = 0; i < nums.Length - 1; i++) {
            int x = nums[i];
            int target = 0 - x;
            List<int> list = new List<int> ();
            for (int j = i + 1; j < nums.Length; j++) {
                
            }
        }
    }
}
// @lc code=end