/*
 * @lc app=leetcode.cn id=39 lang=csharp
 *
 * [39] 组合总和
 *
 * https://leetcode-cn.com/problems/combination-sum/description/
 *
 * algorithms
 * Medium (71.48%)
 * Likes:    1001
 * Dislikes: 0
 * Total Accepted:    172.5K
 * Total Submissions: 241.3K
 * Testcase Example:  '[2,3,6,7]\n7'
 *
 * 给定一个无重复元素的数组 candidates 和一个目标数 target ，找出 candidates 中所有可以使数字和为 target 的组合。
 * 
 * candidates 中的数字可以无限制重复被选取。
 * 
 * 说明：
 * 
 * 
 * 所有数字（包括 target）都是正整数。
 * 解集不能包含重复的组合。 
 * 
 * 
 * 示例 1：
 * 
 * 输入：candidates = [2,3,6,7], target = 7,
 * 所求解集为：
 * [
 * ⁠ [7],
 * ⁠ [2,2,3]
 * ]
 * 
 * 
 * 示例 2：
 * 
 * 输入：candidates = [2,3,5], target = 8,
 * 所求解集为：
 * [
 * [2,2,2,2],
 * [2,3,3],
 * [3,5]
 * ]
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= candidates.length <= 30
 * 1 <= candidates[i] <= 200
 * candidate 中的每个元素都是独一无二的。
 * 1 <= target <= 500
 * 
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<IList<int>> CombinationSum (int[] candidates, int target) {
        List<IList<int>> result = new List<IList<int>> ();
        List<int> list = new List<int> ();
        BackTrack (result, target, 0, 0, list, candidates);
        return result;
    }

    private void BackTrack (List<IList<int>> result, int target, int step, int sum, List<int> list, int[] candidates) {
        if (sum > target) {
            return;
        }
        if (sum == target) {
            result.Add (list.ToList ());
            return;
        }

        while (step < candidates.Length) {
            sum += candidates[step];
            list.Add (candidates[step]);
            BackTrack (result, target, step, sum, list, candidates);
            sum -= candidates[step];
            list.RemoveAt (list.Count - 1);
            step++;
        }
    }
}
// @lc code=end