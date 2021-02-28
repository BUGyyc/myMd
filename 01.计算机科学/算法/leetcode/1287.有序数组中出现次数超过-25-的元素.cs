/*
 * @lc app=leetcode.cn id=1287 lang=csharp
 *
 * [1287] 有序数组中出现次数超过25%的元素
 *
 * https://leetcode-cn.com/problems/element-appearing-more-than-25-in-sorted-array/description/
 *
 * algorithms
 * Easy (61.07%)
 * Likes:    40
 * Dislikes: 0
 * Total Accepted:    12.1K
 * Total Submissions: 19.7K
 * Testcase Example:  '[1,2,2,6,6,6,6,7,10]'
 *
 * 给你一个非递减的 有序 整数数组，已知这个数组中恰好有一个整数，它的出现次数超过数组元素总数的 25%。
 * 
 * 请你找到并返回这个整数
 * 
 * 
 * 
 * 示例：
 * 
 * 
 * 输入：arr = [1,2,2,6,6,6,6,7,10]
 * 输出：6
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= arr.length <= 10^4
 * 0 <= arr[i] <= 10^5
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int FindSpecialInteger (int[] arr) {
        int len = arr.Length;
        if (len == 1) return arr[0];
        int i = 0;
        int j = len - 1;
        while (i < j) {
            while (i < j && i < len - 1 && arr[i] != arr[i + 1]) {
                i++;
            }
            while (i < j && j > 0 && arr[j] != arr[j - 1]) {
                j--;
            }
            if (i < j) {
                if (arr[i] == arr[j]) {
                    return arr[i];
                }
                i++;
                j--;
            }
        }
        return -1;
    }
}
// @lc code=end