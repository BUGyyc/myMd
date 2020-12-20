/*
 * @lc app=leetcode.cn id=78 lang=csharp
 *
 * [78] 子集
 *
 * https://leetcode-cn.com/problems/subsets/description/
 *
 * algorithms
 * Medium (79.13%)
 * Likes:    842
 * Dislikes: 0
 * Total Accepted:    162K
 * Total Submissions: 204.7K
 * Testcase Example:  '[1,2,3]'
 *
 * 给定一组不含重复元素的整数数组 nums，返回该数组所有可能的子集（幂集）。
 * 
 * 说明：解集不能包含重复的子集。
 * 
 * 示例:
 * 
 * 输入: nums = [1,2,3]
 * 输出:
 * [
 * ⁠ [3],
 * [1],
 * [2],
 * [1,2,3],
 * [1,3],
 * [2,3],
 * [1,2],
 * []
 * ]
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<IList<int>> Subsets (int[] nums) {
        List<IList<int>> result = new List<IList<int>> ();
        int len = nums.Length;
        if (len == 0) {
            result.Add (new List<int> ());
            return result;
        }
        // while (len > 0) {
        //     BackTrack (nums, result, new List<int> (), new bool[len], 0, len);
        //     len--;
        // }
        List<int> list = new List<int> ();
        BackTrack (nums, result, list, 0);
        return result;
    }

    private void BackTrack (int[] nums, List<IList<int>> result, List<int> list, int start) {
        result.Add (list.ToList ());
        for (int i = start; i < nums.Length; i++) {
            list.Add (nums[i]);
            BackTrack (nums, result, list, i + 1);
            list.RemoveAt (list.Count - 1);
        }
    }

    // private void BackTrack (int[] nums, List<IList<int>> result, List<int> list, bool[] visit, int count, int target) {
    //     if (count == target) {
    //         result.Add (list);
    //         return;
    //     }
    //     for (int i = 0; i < nums.Length; i++) {
    //         if (visit[i]) continue;
    //         visit[i] = true;
    //         list.Add (nums[i]);
    //         BackTrack (nums, result, list, visit, count + 1, target);
    //         list.RemoveAt (list.Count - 1);
    //         visit[i] = false;
    //     }
    // }
}
// @lc code=end