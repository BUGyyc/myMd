/*
 * @lc app=leetcode.cn id=922 lang=csharp
 *
 * [922] 按奇偶排序数组 II
 *
 * https://leetcode-cn.com/problems/sort-array-by-parity-ii/description/
 *
 * algorithms
 * Easy (71.54%)
 * Likes:    189
 * Dislikes: 0
 * Total Accepted:    78.4K
 * Total Submissions: 109.6K
 * Testcase Example:  '[4,2,5,7]'
 *
 * 给定一个非负整数数组 A， A 中一半整数是奇数，一半整数是偶数。
 * 
 * 对数组进行排序，以便当 A[i] 为奇数时，i 也是奇数；当 A[i] 为偶数时， i 也是偶数。
 * 
 * 你可以返回任何满足上述条件的数组作为答案。
 * 
 * 
 * 
 * 示例：
 * 
 * 输入：[4,2,5,7]
 * 输出：[4,5,2,7]
 * 解释：[4,7,2,5]，[2,5,4,7]，[2,7,4,5] 也会被接受。
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 2 <= A.length <= 20000
 * A.length % 2 == 0
 * 0 <= A[i] <= 1000
 * 
 * 
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int[] SortArrayByParityII (int[] A) {
        List<int> list = new List<int> ();
        foreach (var item in A) {
            if (item % 2 == 0) {
                list.Insert (0, item);
            } else {
                list.Add (item);
            }
        }
        int step = 0, i = 0, j = A.Length / 2;
        while (step < A.Length) {
            if (step % 2 == 0) {
                A[step] = list[i++];
            } else {
                A[step] = list[j++];
            }
            step++;
        }
        return A;
    }
}
// @lc code=end