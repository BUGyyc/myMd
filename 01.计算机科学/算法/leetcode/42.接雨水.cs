/*
 * @lc app=leetcode.cn id=42 lang=csharp
 *
 * [42] 接雨水
 *
 * https://leetcode-cn.com/problems/trapping-rain-water/description/
 *
 * algorithms
 * Hard (53.77%)
 * Likes:    1916
 * Dislikes: 0
 * Total Accepted:    180.4K
 * Total Submissions: 335.5K
 * Testcase Example:  '[0,1,0,2,1,0,1,3,2,1,2,1]'
 *
 * 给定 n 个非负整数表示每个宽度为 1 的柱子的高度图，计算按此排列的柱子，下雨之后能接多少雨水。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 
 * 
 * 输入：height = [0,1,0,2,1,0,1,3,2,1,2,1]
 * 输出：6
 * 解释：上面是由数组 [0,1,0,2,1,0,1,3,2,1,2,1] 表示的高度图，在这种情况下，可以接 6 个单位的雨水（蓝色部分表示雨水）。 
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：height = [4,2,0,3,2,5]
 * 输出：9
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * n == height.length
 * 0 
 * 0 
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int Trap(int[] height) {
        int len = height.Length;
        if(len == 0)return 0;
        int[] leftMax = new int[len];
        int[] rightMax = new int[len];
        leftMax[0] = height[0];
        for(int i = 1;i<len;i++){
            leftMax[i] = Math.Max(leftMax[i-1],height[i]);
        }
        rightMax[len-1] = height[len-1];
        for(int i = len-2;i>=0;i--){
            rightMax[i] = Math.Max(rightMax[i+1],height[i]);
        }
        int result = 0;
        for(int i = 0;i<len;i++){
            result += Math.Min(rightMax[i],leftMax[i]) - height[i];
        }
        return result;
    }
}
// @lc code=end

