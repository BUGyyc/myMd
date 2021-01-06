/*
 * @lc app=leetcode.cn id=59 lang=csharp
 *
 * [59] 螺旋矩阵 II
 *
 * https://leetcode-cn.com/problems/spiral-matrix-ii/description/
 *
 * algorithms
 * Medium (78.47%)
 * Likes:    278
 * Dislikes: 0
 * Total Accepted:    55.6K
 * Total Submissions: 70.8K
 * Testcase Example:  '3'
 *
 * 给定一个正整数 n，生成一个包含 1 到 n^2 所有元素，且元素按顺时针顺序螺旋排列的正方形矩阵。
 * 
 * 示例:
 * 
 * 输入: 3
 * 输出:
 * [
 * ⁠[ 1, 2, 3 ],
 * ⁠[ 8, 9, 4 ],
 * ⁠[ 7, 6, 5 ]
 * ]
 * 
 */
// @lc code=start
public class Solution {
    public int[][] GenerateMatrix (int n) {
        int[][] arr = new int[n][];
        for (int i = 0; i < n; i++) {
            arr[i] = new int[n];
        }
        int left = 0;
        int right = n - 1;
        int up = 0;
        int down = n - 1;
        int val = 1;
        while (val <= n * n) {
            for (int i = left; i <= right; i++) {
                arr[up][i] = val;
                val++;
            }
            up++;
            for (int i = up; i <= down; i++) {
                arr[i][right] = val;
                val++;
            }
            right--;
            for (int i = right; i >= left; i--) {
                arr[down][i] = val;
                val++;
            }
            down--;
            for (int i = down; i >= up; i--) {
                arr[i][left] = val;
                val++;
            }
            left++;
        }
        return arr;
    }
}
// @lc code=end