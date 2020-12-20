/*
 * @lc app=leetcode.cn id=47 lang=csharp
 *
 * [47] 全排列 II
 *
 * https://leetcode-cn.com/problems/permutations-ii/description/
 *
 * algorithms
 * Medium (62.09%)
 * Likes:    501
 * Dislikes: 0
 * Total Accepted:    116.3K
 * Total Submissions: 187.3K
 * Testcase Example:  '[1,1,2]'
 *
 * 给定一个可包含重复数字的序列，返回所有不重复的全排列。
 * 
 * 示例:
 * 
 * 输入: [1,1,2]
 * 输出:
 * [
 * ⁠ [1,1,2],
 * ⁠ [1,2,1],
 * ⁠ [2,1,1]
 * ]
 * 
 */

// @lc code=start
public class Solution {
    //回溯。需要一个记录是否访问的数组
    public IList<IList<int>> PermuteUnique (int[] nums) {
        List<IList<int>> result = new List<IList<int>> ();
        LinkedList<int> list = new LinkedList<int> ();
        bool[] visit = new bool[nums.Length];
        Array.Sort (nums);
        BackTrack (nums, list, result, visit, 0);
        return result;
    }

    private void BackTrack (int[] nums, LinkedList<int> list, List<IList<int>> result, bool[] visit, int deep) {
        if (deep == nums.Length) {
            result.Add (new List<int> (list));
            return;
        }

        for (int i = 0; i < nums.Length; i++) {
            if (visit[i] || (i > 0) && nums[i] == nums[i - 1] && visit[i - 1] == false) {
                continue;
            }
            visit[i] = true;
            list.AddLast (nums[i]);
            BackTrack (nums, list, result, visit, deep + 1);
            list.RemoveLast ();
            visit[i] = false;
        }
    }
}
// @lc code=end