/*
 * @lc app=leetcode.cn id=77 lang=csharp
 *
 * [77] 组合
 *
 * https://leetcode-cn.com/problems/combinations/description/
 *
 * algorithms
 * Medium (75.79%)
 * Likes:    416
 * Dislikes: 0
 * Total Accepted:    109.7K
 * Total Submissions: 144.8K
 * Testcase Example:  '4\n2'
 *
 * 给定两个整数 n 和 k，返回 1 ... n 中所有可能的 k 个数的组合。
 * 
 * 示例:
 * 
 * 输入: n = 4, k = 2
 * 输出:
 * [
 * ⁠ [2,4],
 * ⁠ [3,4],
 * ⁠ [2,3],
 * ⁠ [1,2],
 * ⁠ [1,3],
 * ⁠ [1,4],
 * ]
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    public IList<IList<int>> Combine (int n, int k) {
        List<IList<int>> result = new List<IList<int>> ();
        List<int> list = new List<int> ();
        BackTrack (result, list, n, k, 1);
        return result;
    }

    private void BackTrack (List<IList<int>> result, List<int> list, int n, int k, int start) {
        if (k == 0) {
            result.Add (list.ToList ());
        } else if (k > 0) {
            for (int i = start; i <= n; i++) {
                list.Add (i);
                BackTrack (result, list, n, k - 1, i + 1);
                list.RemoveAt (list.Count - 1);
            }
        }
    }
}
// @lc code=end