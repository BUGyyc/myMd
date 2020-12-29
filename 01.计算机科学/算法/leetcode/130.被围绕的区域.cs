/*
 * @lc app=leetcode.cn id=130 lang=csharp
 *
 * [130] 被围绕的区域
 *
 * https://leetcode-cn.com/problems/surrounded-regions/description/
 *
 * algorithms
 * Medium (42.27%)
 * Likes:    413
 * Dislikes: 0
 * Total Accepted:    77.4K
 * Total Submissions: 183.1K
 * Testcase Example:  '[["X","X","X","X"],["X","O","O","X"],["X","X","O","X"],["X","O","X","X"]]'
 *
 * 给定一个二维的矩阵，包含 'X' 和 'O'（字母 O）。
 * 
 * 找到所有被 'X' 围绕的区域，并将这些区域里所有的 'O' 用 'X' 填充。
 * 
 * 示例:
 * 
 * X X X X
 * X O O X
 * X X O X
 * X O X X
 * 
 * 
 * 运行你的函数后，矩阵变为：
 * 
 * X X X X
 * X X X X
 * X X X X
 * X O X X
 * 
 * 
 * 解释:
 * 
 * 被围绕的区间不会存在于边界上，换句话说，任何边界上的 'O' 都不会被填充为 'X'。 任何不在边界上，或不与边界上的 'O' 相连的 'O'
 * 最终都会被填充为 'X'。如果两个元素在水平或垂直方向相邻，则称它们是“相连”的。
 * 
 */
// @lc code=start
public class Solution
{
    //TODO:
    public void Solve(char[][] board)
    {
        int row = board.Length;
        int col = board[0].Length;
        bool[][] visit = new bool[row][col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (visit[i][j] == false && board[i][j] == 'o')
                {
                    visit[i][j] = true;
                    SetFourDir (board, i, j, row, col);
                }
            }
        }
    }

    private void SetFourDir(char[][] arr, int j, int row, int col)
    {
        SetVal(arr, i - 1, j, row, col);
        SetVal(arr, i + 1, j, row, col);
        SetVal(arr, i, j - 1, row, col);
        SetVal(arr, i, j + 1, row, col);
    }

    private void SetVal(char[][] arr, int i, int j, int row, int col)
    {
        if (i < 0 || i >= row) return;
        if (j < 0 || j >= col) return;
        arr[i][j] = 'o';
    }
}
// @lc code=end
