/*
 * @lc app=leetcode.cn id=1748 lang=csharp
 *
 * [1748] 唯一元素的和
 *
 * https://leetcode-cn.com/problems/sum-of-unique-elements/description/
 *
 * algorithms
 * Easy (80.85%)
 * Likes:    2
 * Dislikes: 0
 * Total Accepted:    3.2K
 * Total Submissions: 3.9K
 * Testcase Example:  '[1,2,3,2]'
 *
 * 给你一个整数数组 nums 。数组中唯一元素是那些只出现 恰好一次 的元素。
 * 
 * 请你返回 nums 中唯一元素的 和 。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：nums = [1,2,3,2]
 * 输出：4
 * 解释：唯一元素为 [1,3] ，和为 4 。
 * 
 * 
 * 示例 2：
 * 
 * 输入：nums = [1,1,1,1,1]
 * 输出：0
 * 解释：没有唯一元素，和为 0 。
 * 
 * 
 * 示例 3 ：
 * 
 * 输入：nums = [1,2,3,4,5]
 * 输出：15
 * 解释：唯一元素为 [1,2,3,4,5] ，和为 15 。
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= nums.length <= 100
 * 1 <= nums[i] <= 100
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int SumOfUnique (int[] nums) {
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        foreach (var item in nums) {
            if (dic.ContainsKey (item)) {
                dic[item]++;
            } else {
                dic.Add (item, 1);
            }
        }

        int sum = 0;
        foreach (var item in dic) {
            if (item.Value == 1) {
                sum += item.Key;
            }
        }
        return sum;
    }
}
// @lc code=end