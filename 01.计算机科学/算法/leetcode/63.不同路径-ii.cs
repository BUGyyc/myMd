/*
 * @lc app=leetcode.cn id=63 lang=csharp
 *
 * [63] 不同路径 II
 *
 * https://leetcode-cn.com/problems/unique-paths-ii/description/
 *
 * algorithms
 * Medium (37.12%)
 * Likes:    450
 * Dislikes: 0
 * Total Accepted:    111.6K
 * Total Submissions: 300.7K
 * Testcase Example:  '[[0,0,0],[0,1,0],[0,0,0]]'
 *
 * 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
 * 
 * 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
 * 
 * 现在考虑网格中有障碍物。那么从左上角到右下角将会有多少条不同的路径？
 * 
 * 
 * 
 * 网格中的障碍物和空位置分别用 1 和 0 来表示。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：obstacleGrid = [[0,0,0],[0,1,0],[0,0,0]]
 * 输出：2
 * 解释：
 * 3x3 网格的正中间有一个障碍物。
 * 从左上角到右下角一共有 2 条不同的路径：
 * 1. 向右 -> 向右 -> 向下 -> 向下
 * 2. 向下 -> 向下 -> 向右 -> 向右
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：obstacleGrid = [[0,1],[0,0]]
 * 输出：1
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * m == obstacleGrid.length
 * n == obstacleGrid[i].length
 * 1 
 * obstacleGrid[i][j] 为 0 或 1
 * 
 * 
 */

// @lc code=start
public class Solution
{
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        int row = obstacleGrid.Length;
        if (row == 0) return 0;
        int col = obstacleGrid[0].Length;
        int[,] arr = new int[row, col];
        arr[0, 0] = 0;
        for (int i = 0; i < col; i++)
        {
            if (obstacleGrid[0][i] == 1)
            {
                break;
            }
            else
            {
                arr[0, i] = 1;
            }
        }
        for (int i = 0; i < row; i++)
        {
            if (obstacleGrid[i][0] == 1)
            {
                break;
            }
            else
            {
                arr[i, 0] = 1;
            }
        }

        for (int i = 1; i < row; i++)
        {
            for (int j = 1; j < col; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    arr[i, j] = 0;
                }
                else
                {
                    arr[i, j] = arr[i - 1, j] + arr[i, j - 1];
                }
            }
        }
        return arr[row-1, col-1];
    }
}
// @lc code=end

