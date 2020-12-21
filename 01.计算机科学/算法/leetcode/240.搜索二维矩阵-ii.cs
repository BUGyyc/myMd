/*
 * @lc app=leetcode.cn id=240 lang=csharp
 *
 * [240] 搜索二维矩阵 II
 *
 * https://leetcode-cn.com/problems/search-a-2d-matrix-ii/description/
 *
 * algorithms
 * Medium (42.38%)
 * Likes:    464
 * Dislikes: 0
 * Total Accepted:    85.6K
 * Total Submissions: 202.1K
 * Testcase Example:  '[[1,4,7,11,15],[2,5,8,12,19],[3,6,9,16,22],[10,13,14,17,24],[18,21,23,26,30]]\n' +
  '5'
 *
 * 编写一个高效的算法来搜索 m x n 矩阵 matrix 中的一个目标值 target。该矩阵具有以下特性：
 * 
 * 
 * 每行的元素从左到右升序排列。
 * 每列的元素从上到下升序排列。
 * 
 * 
 * 示例:
 * 
 * 现有矩阵 matrix 如下：
 * 
 * [
 * ⁠ [1,   4,  7, 11, 15],
 * ⁠ [2,   5,  8, 12, 19],
 * ⁠ [3,   6,  9, 16, 22],
 * ⁠ [10, 13, 14, 17, 24],
 * ⁠ [18, 21, 23, 26, 30]
 * ]
 * 
 * 
 * 给定 target = 5，返回 true。
 * 
 * 给定 target = 20，返回 false。
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public bool SearchMatrix (int[, ] matrix, int target) {
        int row = matrix.Length;
        if(row == 0)return false;
        int col = matrix[0].Length;
        return searchRect(0,0,col-1,row-1,target,matrix);
    }

    private bool searchRect(int left,int up,int right,int down,int target,int[,] array){
        if(left > right || up > down)return false;
        if(target < array[up,left])return false;
        if(target > array[down,right])return false;
        int mid = left + (right - left)/2;
        int row = up;
        while(row <= down && array[row,mid] <= target){
            if(target == array[row,mid]){
                return true;
            }
            row++;
        }
        return searchRect(left,row,mid-1,down,target,array) || searchRect(mid+1,up,right,row-1,target,array);
    }
}
// @lc code=end