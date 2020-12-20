/*
 * @lc app=leetcode.cn id=88 lang=csharp
 *
 * [88] 合并两个有序数组
 *
 * https://leetcode-cn.com/problems/merge-sorted-array/description/
 *
 * algorithms
 * Easy (48.80%)
 * Likes:    668
 * Dislikes: 0
 * Total Accepted:    221.7K
 * Total Submissions: 454.1K
 * Testcase Example:  '[1,2,3,0,0,0]\n3\n[2,5,6]\n3'
 *
 * 给你两个有序整数数组 nums1 和 nums2，请你将 nums2 合并到 nums1 中，使 nums1 成为一个有序数组。
 * 
 * 
 * 
 * 说明：
 * 
 * 
 * 初始化 nums1 和 nums2 的元素数量分别为 m 和 n 。
 * 你可以假设 nums1 有足够的空间（空间大小大于或等于 m + n）来保存 nums2 中的元素。
 * 
 * 
 * 
 * 
 * 示例：
 * 
 * 
 * 输入：
 * nums1 = [1,2,3,0,0,0], m = 3
 * nums2 = [2,5,6],       n = 3
 * 
 * 输出：[1,2,2,3,5,6]
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * -10^9 
 * nums1.length == m + n
 * nums2.length == n
 * 
 * 
 */

// @lc code=start
public class Solution {
    //从后往前比较
    public void Merge (int[] nums1, int m, int[] nums2, int n) {
        int i = m - 1, j = n - 1, pos = nums1.Length - 1;
        while (pos >= 0 && j >= 0) {
            nums1[pos--] = i >= 0 && nums1[i] > nums2[j] ?
                nums1[i--] :
                nums2[j--];
        }
    }
}
// @lc code=end