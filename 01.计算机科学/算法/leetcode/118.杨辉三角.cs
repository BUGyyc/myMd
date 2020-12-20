/*
 * @lc app=leetcode.cn id=118 lang=csharp
 *
 * [118] 杨辉三角
 *
 * https://leetcode-cn.com/problems/pascals-triangle/description/
 *
 * algorithms
 * Easy (67.77%)
 * Likes:    373
 * Dislikes: 0
 * Total Accepted:    114.7K
 * Total Submissions: 169.2K
 * Testcase Example:  '5'
 *
 * 给定一个非负整数 numRows，生成杨辉三角的前 numRows 行。
 * 
 * 
 * 
 * 在杨辉三角中，每个数是它左上方和右上方的数的和。
 * 
 * 示例:
 * 
 * 输入: 5
 * 输出:
 * [
 * ⁠    [1],
 * ⁠   [1,1],
 * ⁠  [1,2,1],
 * ⁠ [1,3,3,1],
 * ⁠[1,4,6,4,1]
 * ]
 * 
 */

// @lc code=start
using System.Collections.Generic;
public class Solution {
    //动态规划
    public IList<IList<int>> Generate (int numRows) {
        int[][] result = new int[numRows][];
        int step = 0;
        while (step < numRows) {
            result[step] = new int[step + 1];
            result[step][0] = result[step][step] = 1;
            int j = 1;
            while (j < step) {
                result[step][j] = result[step - 1][j - 1] + result[step - 1][j];
                j++;
            }
            step++;
        }
        return result;
    }

}
// @lc code=end