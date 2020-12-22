/*
 * @lc app=leetcode.cn id=55 lang=csharp
 *
 * [55] 跳跃游戏
 *
 * https://leetcode-cn.com/problems/jump-game/description/
 *
 * algorithms
 * Medium (41.21%)
 * Likes:    883
 * Dislikes: 0
 * Total Accepted:    163.3K
 * Total Submissions: 396.2K
 * Testcase Example:  '[2,3,1,1,4]'
 *
 * 给定一个非负整数数组，你最初位于数组的第一个位置。
 * 
 * 数组中的每个元素代表你在该位置可以跳跃的最大长度。
 * 
 * 判断你是否能够到达最后一个位置。
 * 
 * 示例 1:
 * 
 * 输入: [2,3,1,1,4]
 * 输出: true
 * 解释: 我们可以先跳 1 步，从位置 0 到达 位置 1, 然后再从位置 1 跳 3 步到达最后一个位置。
 * 
 * 
 * 示例 2:
 * 
 * 输入: [3,2,1,0,4]
 * 输出: false
 * 解释: 无论怎样，你总会到达索引为 3 的位置。但该位置的最大跳跃长度是 0 ， 所以你永远不可能到达最后一个位置。
 * 
 * 
 */

// @lc code=start
public class Solution {
    //遍历，刷新记录的最大跳跃步子，如果大于等于数组长度，则说明可以
    public bool CanJump (int[] nums) {
        int max = 0;
        for(int i = 0;i<nums.Length;i++){
            if(i<=max){
                max = Math.Max(i+nums[i],max);
                if(max>=nums.Length-1)return true;
            }
        }
        return max>=nums.Length-1;
    }
}
// @lc code=end