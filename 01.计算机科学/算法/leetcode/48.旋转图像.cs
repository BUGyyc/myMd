/*
 * @lc app=leetcode.cn id=48 lang=csharp
 *
 * [48] 旋转图像
 *
 * https://leetcode-cn.com/problems/rotate-image/description/
 *
 * algorithms
 * Medium (69.96%)
 * Likes:    611
 * Dislikes: 0
 * Total Accepted:    105.1K
 * Total Submissions: 150.3K
 * Testcase Example:  '[[1,2,3],[4,5,6],[7,8,9]]'
 *
 * 给定一个 n × n 的二维矩阵表示一个图像。
 * 
 * 将图像顺时针旋转 90 度。
 * 
 * 说明：
 * 
 * 你必须在原地旋转图像，这意味着你需要直接修改输入的二维矩阵。请不要使用另一个矩阵来旋转图像。
 * 
 * 示例 1:
 * 
 * 给定 matrix = 
 * [
 * ⁠ [1,2,3],
 * ⁠ [4,5,6],
 * ⁠ [7,8,9]
 * ],
 * 
 * 原地旋转输入矩阵，使其变为:
 * [
 * ⁠ [7,4,1],
 * ⁠ [8,5,2],
 * ⁠ [9,6,3]
 * ]
 * 
 * 
 * 示例 2:
 * 
 * 给定 matrix =
 * [
 * ⁠ [ 5, 1, 9,11],
 * ⁠ [ 2, 4, 8,10],
 * ⁠ [13, 3, 6, 7],
 * ⁠ [15,14,12,16]
 * ], 
 * 
 * 原地旋转输入矩阵，使其变为:
 * [
 * ⁠ [15,13, 2, 5],
 * ⁠ [14, 3, 4, 1],
 * ⁠ [12, 6, 8, 9],
 * ⁠ [16, 7,10,11]
 * ]
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public void Rotate (int[][] matrix) {
        int row = matrix.Length;
        int col = matrix[0].Length;
        Queue<int> queue = new Queue<int>();
        int left = 0;
        int right = col - 1;
        int up = 0;
        int down = row - 1;
        while(left < right && up < down){
            for(int i = left; i<right;i++){
                queue.Enqueue(matrix[up][i]);
            }
            for(int i = up; i<down;i++){
                queue.Enqueue(matrix[i][right]);
                matrix[i][right] = queue.Dequeue();
            }
            
            for(int i = right;i>left;i--){
                queue.Enqueue(matrix[down][i]);
                matrix[down][i] = queue.Dequeue();
            }
            for(int i = down;i> up;i--){
                queue.Enqueue(matrix[i][left]);
                matrix[i][left] = queue.Dequeue();
            }
            for(int i = left;i<right;i++){
                matrix[up][i] = queue.Dequeue();
            }
            queue.Clear();
            left++;
            right--;
            up++;
            down--;
        }
    }
}
// @lc code=end