/*
 * @lc app=leetcode.cn id=645 lang=csharp
 *
 * [645] 错误的集合
 *
 * https://leetcode-cn.com/problems/set-mismatch/description/
 *
 * algorithms
 * Easy (42.48%)
 * Likes:    150
 * Dislikes: 0
 * Total Accepted:    30.1K
 * Total Submissions: 70.8K
 * Testcase Example:  '[1,2,2,4]'
 *
 * 集合 s 包含从 1 到 n 的整数。不幸的是，因为数据错误，导致集合里面某一个数字复制了成了集合里面的另外一个数字的值，导致集合 丢失了一个数字 并且
 * 有一个数字重复 。
 * 
 * 给定一个数组 nums 代表了集合 S 发生错误后的结果。
 * 
 * 请你找出重复出现的整数，再找到丢失的整数，将它们以数组的形式返回。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：nums = [1,2,2,4]
 * 输出：[2,3]
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：nums = [1,1]
 * 输出：[1,2]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 2 
 * 1 
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int[] FindErrorNums(int[] nums) {
        int len = nums.Length;
        int sum = (1 + len) * len / 2;
        List<int> list = new List<int>();
        int target = -1;
        foreach (var item in nums)
        {
            if (list.Contains(item) == false)
            {
                sum -= item;
                list.Add(item);
            }
            else
            {
                target = item;
            }
        }
        return new int[2] { target, sum };
    }
}
// @lc code=end

