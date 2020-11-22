/*
 * @lc app=leetcode.cn id=64 lang=csharp
 *
 * [64] 最小路径和
 *
 * https://leetcode-cn.com/problems/minimum-path-sum/description/
 *
 * algorithms
 * Medium (67.60%)
 * Likes:    703
 * Dislikes: 0
 * Total Accepted:    154.3K
 * Total Submissions: 228.3K
 * Testcase Example:  '[[1,3,1],[1,5,1],[4,2,1]]'
 *
 * 给定一个包含非负整数的 m x n 网格，请找出一条从左上角到右下角的路径，使得路径上的数字总和为最小。
 * 
 * 说明：每次只能向下或者向右移动一步。
 * 
 * 示例:
 * 
 * 输入:
 * [
 * [1,3,1],
 * ⁠ [1,5,1],
 * ⁠ [4,2,1]
 * ]
 * 输出: 7
 * 解释: 因为路径 1→3→1→1→1 的总和最小。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int MinPathSum (int[][] grid) {
               int row = grid.Length;
        if (row == 0) return 0;
        int col = grid[0].Length;
        int[, ] arr = new int[row, col];
        arr[0,0] = grid[0][0];
        for (int i = 1; i < row; i++) {
            arr[i,0] = grid[i][0] + arr[i - 1,0];
        }
        for (int j = 1; j < col; j++) {
            arr[0,j] = grid[0][j] + arr[0,j - 1];
        }
        for (int i = 1; i < row; i++) {
            for (int j = 1; j < col; j++) {
                arr[i,j] = Math.Min (arr[i - 1,j], arr[i,j - 1]) + grid[i][j];
            }
        }
        return arr[row - 1,col - 1];
    }
}
// @lc code=end