/*
 * @lc app=leetcode.cn id=73 lang=csharp
 *
 * [73] 矩阵置零
 *
 * https://leetcode-cn.com/problems/set-matrix-zeroes/description/
 *
 * algorithms
 * Medium (55.84%)
 * Likes:    311
 * Dislikes: 0
 * Total Accepted:    57K
 * Total Submissions: 102K
 * Testcase Example:  '[[1,1,1],[1,0,1],[1,1,1]]'
 *
 * 给定一个 m x n 的矩阵，如果一个元素为 0，则将其所在行和列的所有元素都设为 0。请使用原地算法。
 * 
 * 示例 1:
 * 
 * 输入: 
 * [
 * [1,1,1],
 * [1,0,1],
 * [1,1,1]
 * ]
 * 输出: 
 * [
 * [1,0,1],
 * [0,0,0],
 * [1,0,1]
 * ]
 * 
 * 
 * 示例 2:
 * 
 * 输入: 
 * [
 * [0,1,2,0],
 * [3,4,5,2],
 * [1,3,1,5]
 * ]
 * 输出: 
 * [
 * [0,0,0,0],
 * [0,4,5,0],
 * [0,3,1,0]
 * ]
 * 
 * 进阶:
 * 
 * 
 * 一个直接的解决方案是使用  O(mn) 的额外空间，但这并不是一个好的解决方案。
 * 一个简单的改进方案是使用 O(m + n) 的额外空间，但这仍然不是最好的解决方案。
 * 你能想出一个常数空间的解决方案吗？
 * 
 * 
 */

// @lc code=start
public class Solution {
    public void SetZeroes (int[][] matrix) {
        int row = matrix.Length;
        if (row == 0) return;
        int col = matrix[0].Length;
        bool[, ] states = new bool[row, col];
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                if (states[i, j] == false && matrix[i][j] == 0) {
                    ResetItem (states, i, j, row, col, matrix);
                }
            }
        }
    }

    private void ResetItem (bool[, ] states, int i, int j, int row, int col, int[][] matrix) {
        for (int a = 0; a < row; a++) {
            for (int b = 0; b < col; b++) {
                if (states[a, b] == true || matrix[a][b] == 0) {
                    //过滤
                } else if (a == i && b == j) {
                    //过滤自己
                } else if (a == i) {
                    states[a, b] = true;
                    matrix[a][b] = 0;
                } else if (b == j) {
                    states[a, b] = true;
                    matrix[a][b] = 0;
                }
            }
        }
    }
}
// @lc code=end