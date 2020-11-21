/*
 * @lc app=leetcode.cn id=31 lang=csharp
 *
 * [31] 下一个排列
 *
 * https://leetcode-cn.com/problems/next-permutation/description/
 *
 * algorithms
 * Medium (34.57%)
 * Likes:    694
 * Dislikes: 0
 * Total Accepted:    94K
 * Total Submissions: 271.8K
 * Testcase Example:  '[1,2,3]'
 *
 * 实现获取下一个排列的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。
 * 
 * 如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。
 * 
 * 必须原地修改，只允许使用额外常数空间。
 * 
 * 以下是一些例子，输入位于左侧列，其相应输出位于右侧列。
 * 1,2,3 → 1,3,2
 * 3,2,1 → 1,2,3
 * 1,1,5 → 1,5,1
 * 
 */

// @lc code=start
public class Solution {
    public void NextPermutation(int[] nums) {
        int len = nums.Length;
        if (len <= 1) return;
        bool find = false;
        int i = 0;
        int j = 0;
        for (i = len - 1; i >= 0; i--)
        {
            for (j = i - 1; j >= 0; j--)
            {
                if (nums[i] > nums[j])
                {
                    find = true;
                    break;
                }
            }
        }
        if (find)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        else
        {
            List<int> list = new List<int> (nums);
            list.Reverse ();
            nums = list.ToArray ();
        }
    }
}
// @lc code=end

