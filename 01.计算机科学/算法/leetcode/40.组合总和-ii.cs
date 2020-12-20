/*
 * @lc app=leetcode.cn id=40 lang=csharp
 *
 * [40] 组合总和 II
 *
 * https://leetcode-cn.com/problems/combination-sum-ii/description/
 *
 * algorithms
 * Medium (64.51%)
 * Likes:    427
 * Dislikes: 0
 * Total Accepted:    114K
 * Total Submissions: 176.8K
 * Testcase Example:  '[10,1,2,7,6,1,5]\n8'
 *
 * 给定一个数组 candidates 和一个目标数 target ，找出 candidates 中所有可以使数字和为 target 的组合。
 * 
 * candidates 中的每个数字在每个组合中只能使用一次。
 * 
 * 说明：
 * 
 * 
 * 所有数字（包括目标数）都是正整数。
 * 解集不能包含重复的组合。 
 * 
 * 
 * 示例 1:
 * 
 * 输入: candidates = [10,1,2,7,6,1,5], target = 8,
 * 所求解集为:
 * [
 * ⁠ [1, 7],
 * ⁠ [1, 2, 5],
 * ⁠ [2, 6],
 * ⁠ [1, 1, 6]
 * ]
 * 
 * 
 * 示例 2:
 * 
 * 输入: candidates = [2,5,2,1,2], target = 5,
 * 所求解集为:
 * [
 * [1,2,2],
 * [5]
 * ]
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<IList<int>> CombinationSum2 (int[] candidates, int target) {
        Array.Sort (candidates);
        List<IList<int>> result = new List<IList<int>> ();
        List<int> list = new List<int> ();
        BackTrack (result, list, 0, 0, target, candidates);
        return result;
    }

    private void BackTrack (List<IList<int>> result, List<int> list, int step, int sum, int target, int[] candidates) {
        if (sum == target) {
            result.Add (list.ToList ());
        } else if (sum < target) {
            for (int i = step; i < candidates.Length; i++) {
                if (i > step && candidates[i - 1] == candidates[i]) {
                    continue;
                }
                list.Add (candidates[i]);
                BackTrack (result, list, i + 1, sum + candidates[i], target, candidates);
                list.RemoveAt (list.Count - 1);
            }
        }
    }
}
// @lc code=end