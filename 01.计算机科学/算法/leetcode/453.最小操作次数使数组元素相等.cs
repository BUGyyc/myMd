/*
 * @lc app=leetcode.cn id=453 lang=csharp
 *
 * [453] 最小移动次数使数组元素相等
 *
 * https://leetcode-cn.com/problems/minimum-moves-to-equal-array-elements/description/
 *
 * algorithms
 * Easy (54.96%)
 * Likes:    154
 * Dislikes: 0
 * Total Accepted:    16.6K
 * Total Submissions: 30.2K
 * Testcase Example:  '[1,2,3]'
 *
 * 给定一个长度为 n 的非空整数数组，找到让数组所有元素相等的最小移动次数。每次移动将会使 n - 1 个元素增加 1。
 * 
 * 
 * 
 * 示例:
 * 
 * 输入:
 * [1,2,3]
 * 
 * 输出:
 * 3
 * 
 * 解释:
 * 只需要3次移动（注意每次移动会增加两个元素的值）：
 * 
 * [1,2,3]  =>  [2,3,3]  =>  [3,4,3]  =>  [4,4,4]
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int MinMoves (int[] nums) {
        Array.Sort (nums);
        int count = 0;
        for (int i = nums.Length - 1; i > 0; i--) {
            count += nums[i] - nums[0];
        }
        return count;
    }
}
// @lc code=end