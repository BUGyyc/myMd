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
        if(row == 0)return;
        int col = board[0].Length;
        for(int i = 0;i<col;i++){
            DFS(board,0,i,row,col);
            DFS(board,row-1,i,row,col);
        }
        for(int i = 1;i<row-1;i++){
            DFS(board,i,0,row,col);
            DFS(board,i,col-1,row,col);
        }
        for(int i = 0;i<row;i++){
            for(int j = 0;j<col;j++){
                if(board[i][j] == 'A'){
                    board[i][j] = 'O';
                }else if(board[i][j] == 'O'){
                    board[i][j] = 'X';
                }
            }
        }
    }

    private void DFS(char[][] board,int i,int j,int row,int col){
        if(i<0 || i>=row || j<0 || j >= col || board[i][j] != 'O')return;
        board[i][j] = 'A';
        DFS(board,i,j+1,row,col);
        DFS(board,i+1,j,row,col);
        DFS(board,i,j-1,row,col);
        DFS(board,i-1,j,row,col);
    }
}
// @lc code=end
