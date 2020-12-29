/*
 * @lc app=leetcode.cn id=213 lang=csharp
 *
 * [213] 打家劫舍 II
 *
 * https://leetcode-cn.com/problems/house-robber-ii/description/
 *
 * algorithms
 * Medium (40.30%)
 * Likes:    436
 * Dislikes: 0
 * Total Accepted:    69.1K
 * Total Submissions: 171.5K
 * Testcase Example:  '[2,3,2]'
 *
 * 你是一个专业的小偷，计划偷窃沿街的房屋，每间房内都藏有一定的现金。这个地方所有的房屋都 围成一圈
 * ，这意味着第一个房屋和最后一个房屋是紧挨着的。同时，相邻的房屋装有相互连通的防盗系统，如果两间相邻的房屋在同一晚上被小偷闯入，系统会自动报警 。
 * 
 * 给定一个代表每个房屋存放金额的非负整数数组，计算你 在不触动警报装置的情况下 ，能够偷窃到的最高金额。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：nums = [2,3,2]
 * 输出：3
 * 解释：你不能先偷窃 1 号房屋（金额 = 2），然后偷窃 3 号房屋（金额 = 2）, 因为他们是相邻的。
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：nums = [1,2,3,1]
 * 输出：4
 * 解释：你可以先偷窃 1 号房屋（金额 = 1），然后偷窃 3 号房屋（金额 = 3）。
 * 偷窃到的最高金额 = 1 + 3 = 4 。
 * 
 * 示例 3：
 * 
 * 
 * 输入：nums = [0]
 * 输出：0
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 
 * 0 
 * 
 * 
 */
// @lc code=start
public class Solution
{
    public int Rob(int[] nums)
    {
        int len = nums.Length;
        if (len == 0) return 0;
        if (len == 1) return nums[0];
        if (len == 2) return Math.Max(nums[0], nums[1]);
        return Math.Max(MyRob(nums, 0, len - 2), MyRob(nums, 1, len - 1));
    }

    private int MyRob(int[] arr, int left, int right)
    {
        if (left == right) return arr[left];

        int[] dp = new int[arr.Length];
        dp[left] = arr[left];
        dp[left + 1] = Math.Max(arr[left], arr[left + 1]);

        for (int i = left + 2; i <= right; i++)
        dp[i] = Math.Max(arr[i] + dp[i - 2], dp[i - 1]);

        return dp[right];
    }
}
// @lc code=end
