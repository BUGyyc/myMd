/*
 * @lc app=leetcode.cn id=79 lang=csharp
 *
 * [79] 单词搜索
 *
 * https://leetcode-cn.com/problems/word-search/description/
 *
 * algorithms
 * Medium (43.64%)
 * Likes:    653
 * Dislikes: 0
 * Total Accepted:    114.7K
 * Total Submissions: 262.8K
 * Testcase Example:  '[["A","B","C","E"],["S","F","C","S"],["A","D","E","E"]]\n"ABCCED"'
 *
 * 给定一个二维网格和一个单词，找出该单词是否存在于网格中。
 * 
 * 单词必须按照字母顺序，通过相邻的单元格内的字母构成，其中“相邻”单元格是那些水平相邻或垂直相邻的单元格。同一个单元格内的字母不允许被重复使用。
 * 
 * 
 * 
 * 示例:
 * 
 * board =
 * [
 * ⁠ ['A','B','C','E'],
 * ⁠ ['S','F','C','S'],
 * ⁠ ['A','D','E','E']
 * ]
 * 
 * 给定 word = "ABCCED", 返回 true
 * 给定 word = "SEE", 返回 true
 * 给定 word = "ABCB", 返回 false
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * board 和 word 中只包含大写和小写英文字母。
 * 1 <= board.length <= 200
 * 1 <= board[i].length <= 200
 * 1 <= word.length <= 10^3
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
         public bool Exist(char[][] board, string word)
        {
            int row = board.Length;
            if (row == 0) return false;
            int col = board[0].Length;
            if (word.Length > row * col) return false;
            bool[,] flag = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (DFS_Search(board, i, j, word, 0, row, col, flag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DFS_Search(char[][] board, int i, int j, string word, int index, int row, int col, bool[,] flag)
        {
            if (i < 0 || i >= row || j < 0 || j >= col)
            {
                return false;
            }
            if (board[i][j] != word[index] || flag[i, j])
            {
                return false;
            }
            index++;
            flag[i, j] = true;
            if (index >= word.Length) return true;
            int[][] dir = new int[4][];
            dir[0] = new int[2]{ 0, 1 };
            dir[1] = new int[2]{ 0, -1 };
            dir[2] = new int[2]{ 1, 0};
            dir[3] = new int[2]{ -1,0 };
            for(int n = 0;n<4;n++){
                var arr = dir[n];
                int a = i + arr[0];
                int b = j + arr[1];
                if (a >= 0 && a < row && b >= 0 && b < col)
                {
                   if (flag[a, b] == false && DFS_Search(board, a, b, word, index, row, col, flag))
                    {
                        return true;
                    }
                }
            }
            flag[i, j] = false;
            return false;
        }
}
// @lc code=end