/*
 * @lc app=leetcode.cn id=209 lang=csharp
 *
 * [209] 长度最小的子数组
 *
 * https://leetcode-cn.com/problems/minimum-size-subarray-sum/description/
 *
 * algorithms
 * Medium (44.42%)
 * Likes:    475
 * Dislikes: 0
 * Total Accepted:    93.4K
 * Total Submissions: 210.2K
 * Testcase Example:  '7\n[2,3,1,2,4,3]'
 *
 * 给定一个含有 n 个正整数的数组和一个正整数 s ，找出该数组中满足其和 ≥ s 的长度最小的 连续
 * 子数组，并返回其长度。如果不存在符合条件的子数组，返回 0。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：s = 7, nums = [2,3,1,2,4,3]
 * 输出：2
 * 解释：子数组 [4,3] 是该条件下的长度最小的子数组。
 * 
 * 
 * 
 * 
 * 进阶：
 * 
 * 
 * 如果你已经完成了 O(n) 时间复杂度的解法, 请尝试 O(n log n) 时间复杂度的解法。
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int MinSubArrayLen(int s, int[] nums) {
        int min = int.MaxValue;
        int start = 0;
        int len = nums.Length;
        if(len == 0)return 0;
        while(start<len){
            int sum = 0;
            for(int i = start;i<len;i++){
                if(sum + nums[i]>=s){
                    min = Math.Min(i-start+1,min);
                    break;
                }else{
                    sum += nums[i];
                }
            }
            start++;
        }
        return min == int.MaxValue?0:min;
    }
}
// @lc code=end

