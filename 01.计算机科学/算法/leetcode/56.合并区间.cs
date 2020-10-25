/*
 * @lc app=leetcode.cn id=56 lang=csharp
 *
 * [56] 合并区间
 *
 * https://leetcode-cn.com/problems/merge-intervals/description/
 *
 * algorithms
 * Medium (43.22%)
 * Likes:    662
 * Dislikes: 0
 * Total Accepted:    156.3K
 * Total Submissions: 361.7K
 * Testcase Example:  '[[1,3],[2,6],[8,10],[15,18]]'
 *
 * 给出一个区间的集合，请合并所有重叠的区间。
 * 
 * 
 * 
 * 示例 1:
 * 
 * 输入: intervals = [[1,3],[2,6],[8,10],[15,18]]
 * 输出: [[1,6],[8,10],[15,18]]
 * 解释: 区间 [1,3] 和 [2,6] 重叠, 将它们合并为 [1,6].
 * 
 * 
 * 示例 2:
 * 
 * 输入: intervals = [[1,4],[4,5]]
 * 输出: [[1,5]]
 * 解释: 区间 [1,4] 和 [4,5] 可被视为重叠区间。
 * 
 * 注意：输入类型已于2019年4月15日更改。 请重置默认代码定义以获取新方法签名。
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * intervals[i][0] <= intervals[i][1]
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int[][] Merge (int[][] intervals) {
        List<int[]> result = new List<int[]> ();
        var a = intervals[0];
        for (int i = 1; i < intervals.Length; i++) {
            var b = intervals[i];
            if (CanMerge (a, b)) {
                a = MergeFunc1 (a, b);
                result.Add (a);
            } else {
                result.Add (a);
                a = b;
            }
        }
        
    }

    private bool CanMerge (int[] a, int[] b) {
        if (a == null || b == null) {
            return true;
        } else if (a[1] < b[0]) {
            return false;
        } else if (b[1] < a[0]) {
            return false;
        }
        return true;
    }

    private int[] MergeFunc1 (int[] a, int[] b) {
        int min = Mathf.Min (a[0], b[0]);
        int max = Mathf.Max (a[1], b[1]);
        return new int[2] { min, max };
    }
}
// @lc code=end