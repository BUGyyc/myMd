/*
 * @lc app=leetcode.cn id=463 lang=csharp
 *
 * [463] 岛屿的周长
 *
 * https://leetcode-cn.com/problems/island-perimeter/description/
 *
 * algorithms
 * Easy (68.43%)
 * Likes:    341
 * Dislikes: 0
 * Total Accepted:    56.1K
 * Total Submissions: 78.4K
 * Testcase Example:  '[[0,1,0,0],[1,1,1,0],[0,1,0,0],[1,1,0,0]]'
 *
 * 给定一个包含 0 和 1 的二维网格地图，其中 1 表示陆地 0 表示水域。
 * 
 * 网格中的格子水平和垂直方向相连（对角线方向不相连）。整个网格被水完全包围，但其中恰好有一个岛屿（或者说，一个或多个表示陆地的格子相连组成的岛屿）。
 * 
 * 岛屿中没有“湖”（“湖” 指水域在岛屿内部且不和岛屿周围的水相连）。格子是边长为 1 的正方形。网格为长方形，且宽度和高度均不超过 100
 * 。计算这个岛屿的周长。
 * 
 * 
 * 
 * 示例 :
 * 
 * 输入:
 * [[0,1,0,0],
 * ⁠[1,1,1,0],
 * ⁠[0,1,0,0],
 * ⁠[1,1,0,0]]
 * 
 * 输出: 16
 * 
 * 解释: 它的周长是下面图片中的 16 个黄色的边：
 * 
 * 
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int IslandPerimeter (int[][] grid) {
        int result = 0;
        int row = grid.Length;
        int col = row > 0 ? grid[0].Length : 0;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                if (grid[i][j] == 1) {
                    //判断四周
                }
            }
        }
    }

    // private int CheckPos (int x, int y, int[][] grid, int row, int col) {
    //     int result = 0;
    //     //上
    //     if (x == 0) {
    //         result++;
    //     } else if (x == row - 1) {
    //         result++;
    //     } else {

    //     }

    //     if ()
    // }
}
// @lc code=end