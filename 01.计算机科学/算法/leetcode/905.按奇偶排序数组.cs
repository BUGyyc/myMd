/*
 * @lc app=leetcode.cn id=905 lang=csharp
 *
 * [905] 按奇偶排序数组
 *
 * https://leetcode-cn.com/problems/sort-array-by-parity/description/
 *
 * algorithms
 * Easy (69.75%)
 * Likes:    187
 * Dislikes: 0
 * Total Accepted:    47.5K
 * Total Submissions: 68.1K
 * Testcase Example:  '[3,1,2,4]'
 *
 * 给定一个非负整数数组 A，返回一个数组，在该数组中， A 的所有偶数元素之后跟着所有奇数元素。
 * 
 * 你可以返回满足此条件的任何数组作为答案。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：[3,1,2,4]
 * 输出：[2,4,3,1]
 * 输出 [4,2,3,1]，[2,4,1,3] 和 [4,2,1,3] 也会被接受。
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= A.length <= 5000
 * 0 <= A[i] <= 5000
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int[] SortArrayByParity (int[] A) {
        if (A.Length == 1) return A;
        int i = 0;
        int j = A.Length - 1;
        while (i < j) {
            while (i < j && A[i] % 2 == 0) {
                i++;
            }
            while (i < j && A[j] % 2 == 1) {
                j--;
            }
            if (i < j && A[i] % 2 == 1 && A[j] % 2 == 0) {
                int tmp = A[i];
                A[i] = A[j];
                A[j] = tmp;
                i++;
                j--;
            }
        }
        return A;
    }
}
// @lc code=end