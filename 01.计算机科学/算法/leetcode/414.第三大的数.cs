/*
 * @lc app=leetcode.cn id=414 lang=csharp
 *
 * [414] 第三大的数
 *
 * https://leetcode-cn.com/problems/third-maximum-number/description/
 *
 * algorithms
 * Easy (35.43%)
 * Likes:    176
 * Dislikes: 0
 * Total Accepted:    38K
 * Total Submissions: 107.3K
 * Testcase Example:  '[3,2,1]'
 *
 * 给定一个非空数组，返回此数组中第三大的数。如果不存在，则返回数组中最大的数。要求算法时间复杂度必须是O(n)。
 * 
 * 示例 1:
 * 
 * 
 * 输入: [3, 2, 1]
 * 
 * 输出: 1
 * 
 * 解释: 第三大的数是 1.
 * 
 * 
 * 示例 2:
 * 
 * 
 * 输入: [1, 2]
 * 
 * 输出: 2
 * 
 * 解释: 第三大的数不存在, 所以返回最大的数 2 .
 * 
 * 
 * 示例 3:
 * 
 * 
 * 输入: [2, 2, 3, 1]
 * 
 * 输出: 1
 * 
 * 解释: 注意，要求返回第三大的数，是指第三大且唯一出现的数。
 * 存在两个值为2的数，它们都排第二。
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int ThirdMax (int[] nums) {
        long f, s, t;
        int count = 0;
        f = long.MinValue;
        s = long.MinValue;
        t = long.MinValue;
        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] > f) {
                count++;
                t = s;
                s = f;
                f = nums[i];
            } else if (nums[i] > s && nums[i] < f) {
                count++;
                t = s;
                s = nums[i];
            } else if (nums[i] > t && nums[i] < s) {
                count++;
                t = nums[i];
            }
        }
        if (count < 3)
            return (int) f;
        else
            return (int) t;
    }
}
// @lc code=end