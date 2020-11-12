/*
 * @lc app=leetcode.cn id=219 lang=csharp
 *
 * [219] 存在重复元素 II
 *
 * https://leetcode-cn.com/problems/contains-duplicate-ii/description/
 *
 * algorithms
 * Easy (40.60%)
 * Likes:    213
 * Dislikes: 0
 * Total Accepted:    67.6K
 * Total Submissions: 166.6K
 * Testcase Example:  '[1,2,3,1]\n3'
 *
 * 给定一个整数数组和一个整数 k，判断数组中是否存在两个不同的索引 i 和 j，使得 nums [i] = nums [j]，并且 i 和 j 的差的
 * 绝对值 至多为 k。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: nums = [1,2,3,1], k = 3
 * 输出: true
 * 
 * 示例 2:
 * 
 * 输入: nums = [1,0,1,1], k = 1
 * 输出: true
 * 
 * 示例 3:
 * 
 * 输入: nums = [1,2,3,1,2,3], k = 2
 * 输出: false
 * 
 */

// @lc code=start
public class Solution
{
    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        if(nums.Length == 0)return false;
        if(k<=0)return false;
        Dictionary<int,int> dic = new Dictionary<int,int>();
        for(int i = 0;i<nums.Length;i++){
            if(dic.ContainsKey(nums[i])){
                int value = i - dic[nums[i]];
                if(value<= k){
                    return true;
                }else{
                    dic[nums[i]] = i;
                }
            }else{
                dic.Add(nums[i],i);
            }            
        }
        return false;
    }
}
// @lc code=end

