/*
 * @lc app=leetcode.cn id=119 lang=csharp
 *
 * [119] 杨辉三角 II
 *
 * https://leetcode-cn.com/problems/pascals-triangle-ii/description/
 *
 * algorithms
 * Easy (62.07%)
 * Likes:    195
 * Dislikes: 0
 * Total Accepted:    76.2K
 * Total Submissions: 122.7K
 * Testcase Example:  '3'
 *
 * 给定一个非负索引 k，其中 k ≤ 33，返回杨辉三角的第 k 行。
 * 
 * 
 * 
 * 在杨辉三角中，每个数是它左上方和右上方的数的和。
 * 
 * 示例:
 * 
 * 输入: 3
 * 输出: [1,3,3,1]
 * 
 * 
 * 进阶：
 * 
 * 你可以优化你的算法到 O(k) 空间复杂度吗？
 * 
 */

// @lc code=start
public class Solution {
    public IList<int> GetRow (int rowIndex) {
        int[][] result = new int[rowIndex+1][];
        int step = 0;
        while (step < rowIndex+1) {
            result[step] = new int[step + 1];
            result[step][0] = result[step][step] = 1;
            int j = 1;
            while (j < step) {
                result[step][j] = result[step - 1][j - 1] + result[step - 1][j];
                j++;
            }
            step++;
        }
        return result[rowIndex];
    }
}
// @lc code=end