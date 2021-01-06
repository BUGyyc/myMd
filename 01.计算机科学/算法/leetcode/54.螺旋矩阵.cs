/*
 * @lc app=leetcode.cn id=54 lang=csharp
 *
 * [54] 螺旋矩阵
 *
 * https://leetcode-cn.com/problems/spiral-matrix/description/
 *
 * algorithms
 * Medium (41.34%)
 * Likes:    521
 * Dislikes: 0
 * Total Accepted:    86.4K
 * Total Submissions: 208.9K
 * Testcase Example:  '[[1,2,3],[4,5,6],[7,8,9]]'
 *
 * 给定一个包含 m x n 个元素的矩阵（m 行, n 列），请按照顺时针螺旋顺序，返回矩阵中的所有元素。
 * 
 * 示例 1:
 * 
 * 输入:
 * [
 * ⁠[ 1, 2, 3 ],
 * ⁠[ 4, 5, 6 ],
 * ⁠[ 7, 8, 9 ]
 * ]
 * 输出: [1,2,3,6,9,8,7,4,5]
 * 
 * 
 * 示例 2:
 * 
 * 输入:
 * [
 * ⁠ [1, 2, 3, 4],
 * ⁠ [5, 6, 7, 8],
 * ⁠ [9,10,11,12]
 * ]
 * 输出: [1,2,3,4,8,12,11,10,9,5,6,7]
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public IList<int> SpiralOrder (int[][] matrix) {
        List<int> list = new List<int>();
        int row = matrix.Length;
        int col = matrix[0].Length;//3;
        int level = 0;
        int i = 0;
        int j = 0;
        int max = Math.Min(col,row) -1;
        if(max == 0)max = 1;
        while(level <= max){
            j = level + 0;
            i = level + 0;
            while(j < col - level - 1 || (level!=0 && j <= col - level -1)){
                if(list.Count < col*row)list.Add(matrix[i][j]);
                j++;
            }
            j = col - level - 1;
            while(i < row - level - 1){
                if(list.Count < col*row)list.Add(matrix[i][j]);
                i++;
            }
            i = row - level - 1;
            while(j > 0+level){
                if(list.Count < col*row)list.Add(matrix[i][j]);
                j--;
            }

            j = level + 0;
            while(i > 0 +level){
                if(list.Count < col*row)list.Add(matrix[i][j]);
                i--;
            }
            level++; 
        }
        return list;
    }
}
// @lc code=end