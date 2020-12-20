/*
 * @lc app=leetcode.cn id=46 lang=csharp
 *
 * [46] 全排列
 *
 * https://leetcode-cn.com/problems/permutations/description/
 *
 * algorithms
 * Medium (77.03%)
 * Likes:    956
 * Dislikes: 0
 * Total Accepted:    209.4K
 * Total Submissions: 271.9K
 * Testcase Example:  '[1,2,3]'
 *
 * 给定一个 没有重复 数字的序列，返回其所有可能的全排列。
 * 
 * 示例:
 * 
 * 输入: [1,2,3]
 * 输出:
 * [
 * ⁠ [1,2,3],
 * ⁠ [1,3,2],
 * ⁠ [2,1,3],
 * ⁠ [2,3,1],
 * ⁠ [3,1,2],
 * ⁠ [3,2,1]
 * ]
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<IList<int>> Permute (int[] nums) {
        List<IList<int>> result = new List<IList<int>> ();
        LinkedList<int> list = new LinkedList<int> ();
        BackTrack (nums, list, result);
        return result;
    }

    private void BackTrack (int[] nums, LinkedList<int> list, List<IList<int>> result) {
        if (list.Count == nums.Length) {
            result.Add (new List<int> (list));
            return;
        }
        for (int i = 0; i < nums.Length; i++) {
            if (list.Contains (nums[i])) {
                //
            } else {
                list.AddLast (nums[i]);
                BackTrack (nums, list, result);
                list.RemoveLast ();
            }
        }
    }
}
// @lc code=end