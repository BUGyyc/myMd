/*
 * @lc app=leetcode.cn id=628 lang=csharp
 *
 * [628] 三个数的最大乘积
 *
 * https://leetcode-cn.com/problems/maximum-product-of-three-numbers/description/
 *
 * algorithms
 * Easy (49.98%)
 * Likes:    213
 * Dislikes: 0
 * Total Accepted:    36.9K
 * Total Submissions: 73.9K
 * Testcase Example:  '[1,2,3]'
 *
 * 给定一个整型数组，在数组中找出由三个数组成的最大乘积，并输出这个乘积。
 * 
 * 示例 1:
 * 
 * 
 * 输入: [1,2,3]
 * 输出: 6
 * 
 * 
 * 示例 2:
 * 
 * 
 * 输入: [1,2,3,4]
 * 输出: 24
 * 
 * 
 * 注意:
 * 
 * 
 * 给定的整型数组长度范围是[3,10^4]，数组中所有的元素范围是[-1000, 1000]。
 * 输入的数组中任意三个数的乘积不会超出32位有符号整数的范围。
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int MaximumProduct (int[] nums) {
        Array.Sort (nums);
        int len = nums.Length;
        int a = nums[0] * nums[1] * nums[len - 1];
        int b = nums[len - 3] * nums[len - 2] * nums[len - 1];
        return Math.Max (a, b);
    }
}
// @lc code=end