/*
 * @lc app=leetcode.cn id=120 lang=csharp
 *
 * [120] 三角形最小路径和
 *
 * https://leetcode-cn.com/problems/triangle/description/
 *
 * algorithms
 * Medium (66.78%)
 * Likes:    629
 * Dislikes: 0
 * Total Accepted:    114K
 * Total Submissions: 170.8K
 * Testcase Example:  '[[2],[3,4],[6,5,7],[4,1,8,3]]'
 *
 * 给定一个三角形，找出自顶向下的最小路径和。每一步只能移动到下一行中相邻的结点上。
 * 
 * 相邻的结点 在这里指的是 下标 与 上一层结点下标 相同或者等于 上一层结点下标 + 1 的两个结点。
 * 
 * 
 * 
 * 例如，给定三角形：
 * 
 * [
 * ⁠    [2],
 * ⁠   [3,4],
 * ⁠  [6,5,7],
 * ⁠ [4,1,8,3]
 * ]
 * 
 * 
 * 自顶向下的最小路径和为 11（即，2 + 3 + 5 + 1 = 11）。
 * 
 * 
 * 
 * 说明：
 * 
 * 如果你可以只使用 O(n) 的额外空间（n 为三角形的总行数）来解决这个问题，那么你的算法会很加分。
 * 
 */

// @lc code=start
public class Solution {
    //动态规划
    public int MinimumTotal (IList<IList<int>> triangle) {
        int row = triangle.Count;
        if (row == 0) return 0;
        int[, ] result = new int[row, row];

        result[0, 0] = triangle[0][0];

        for (int i = 1; i < row; i++) {
            result[i, 0] = result[i - 1, 0] + triangle[i][0];
            for (int j = 1; j < i; j++) {
                result[i, j] = Math.Min (result[i - 1, j - 1], result[i - 1, j]) + triangle[i][j];
            }
            result[i, i] = result[i - 1, i - 1] + triangle[i][i];
        }
        int min = result[row - 1, 0];
        for (int i = 0; i < row; i++) {
            min = Math.Min (min, result[row - 1, i]);
        }
        return min;
    }
}
// @lc code=end