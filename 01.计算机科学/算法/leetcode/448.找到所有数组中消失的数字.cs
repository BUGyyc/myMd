/*
 * @lc app=leetcode.cn id=448 lang=csharp
 *
 * [448] 找到所有数组中消失的数字
 *
 * https://leetcode-cn.com/problems/find-all-numbers-disappeared-in-an-array/description/
 *
 * algorithms
 * Easy (61.12%)
 * Likes:    526
 * Dislikes: 0
 * Total Accepted:    68K
 * Total Submissions: 111.2K
 * Testcase Example:  '[4,3,2,7,8,2,3,1]'
 *
 * 给定一个范围在  1 ≤ a[i] ≤ n ( n = 数组大小 ) 的 整型数组，数组中的元素一些出现了两次，另一些只出现一次。
 * 
 * 找到所有在 [1, n] 范围之间没有出现在数组中的数字。
 * 
 * 您能在不使用额外空间且时间复杂度为O(n)的情况下完成这个任务吗? 你可以假定返回的数组不算在额外空间内。
 * 
 * 示例:
 * 
 * 
 * 输入:
 * [4,3,2,7,8,2,3,1]
 * 
 * 输出:
 * [5,6]
 * 
 * 
 */

// @lc code=start
public class Solution {
    public IList<int> FindDisappearedNumbers(int[] nums) {
            int len = nums.Length;
            for(int i = 0;i<len;i++){
                int val = nums[i];
                if(val < 0)val *=-1;
                if(nums[val - 1]>0){
                    nums[val - 1] *= -1;
                }
            }
            List<int> list = new List<int>();
            for(int i = 0;i<len;i++){
                if(nums[i]>0){
                    list.Add(i+1);
                }
            }
            return list.ToArray();
    }
}
// @lc code=end

