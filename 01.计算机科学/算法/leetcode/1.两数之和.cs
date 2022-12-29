/*
 * @lc app=leetcode.cn id=1 lang=csharp
 *
 * [1] 两数之和
 */

// @lc code=start
public class Solution
{
    //主要是利用Dic ,Key 存储 nums[i]  Value 存储 索引i
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            //! target - nums[i] 是比较巧妙的，用差来查找，Dic 是否已经存在 差。然后 Dic 的 value 存储的是 Index
            if (dic.ContainsKey(target - nums[i]) && dic[target - nums[i]] != i)
            {
                return new int[2] { dic[target - nums[i]], i };
            }
            if (dic.ContainsKey(nums[i]) == false)
            {
                dic.Add(nums[i], i);
            }
        }
        return new int[0];
        // return test (nums, target);
    }

    private int[] test(int[] nums, int target)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (dic.ContainsKey(target - nums[i]) && dic[target - nums[i]] != i)
            {
                return new int[2] { dic[target - nums[i]], i };
            }
            if (dic.ContainsKey(nums[i]) == false)
            {
                dic.Add(nums[i], i);
            }
        }
        return new int[0];
    }
}
// @lc code=end