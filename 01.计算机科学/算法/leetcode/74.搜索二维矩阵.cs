/*
 * @lc app=leetcode.cn id=74 lang=csharp
 *
 * [74] 搜索二维矩阵
 *
 * https://leetcode-cn.com/problems/search-a-2d-matrix/description/
 *
 * algorithms
 * Medium (39.13%)
 * Likes:    261
 * Dislikes: 0
 * Total Accepted:    66.5K
 * Total Submissions: 170K
 * Testcase Example:  '[[1,3,5,7],[10,11,16,20],[23,30,34,50]]\n3'
 *
 * 编写一个高效的算法来判断 m x n 矩阵中，是否存在一个目标值。该矩阵具有如下特性：
 * 
 * 
 * 每行中的整数从左到右按升序排列。
 * 每行的第一个整数大于前一行的最后一个整数。
 * 
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,50]], target = 3
 * 输出：true
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,50]], target = 13
 * 输出：false
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：matrix = [], target = 0
 * 输出：false
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * m == matrix.length
 * n == matrix[i].length
 * 0 
 * -10^4 
 * 
 * 
 */

// @lc code=start
public class Solution {
    //二分法、分治
    public bool SearchMatrix (int[][] matrix, int target) {
        int row = matrix.Length;
        if (row == 0) return false;
        int col = matrix[0].Length;
        int l = 0;
        int r = row * col - 1;
        while (l <= r) {
            int mid = (l + r) / 2;
            int n = matrix[mid / col][mid % col];
            if (n > target) {
                r = mid - 1;
            } else if (n < target) {
                l = mid + 1;
            } else {
                return true;
            }
        }
        return false;
    }

}
// @lc code=end