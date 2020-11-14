/*
 * @lc app=leetcode.cn id=349 lang=csharp
 *
 * [349] 两个数组的交集
 *
 * https://leetcode-cn.com/problems/intersection-of-two-arrays/description/
 *
 * algorithms
 * Easy (71.07%)
 * Likes:    242
 * Dislikes: 0
 * Total Accepted:    101.1K
 * Total Submissions: 142.2K
 * Testcase Example:  '[1,2,2,1]\n[2,2]'
 *
 * 给定两个数组，编写一个函数来计算它们的交集。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：nums1 = [1,2,2,1], nums2 = [2,2]
 * 输出：[2]
 * 
 * 
 * 示例 2：
 * 
 * 输入：nums1 = [4,9,5], nums2 = [9,4,9,8,4]
 * 输出：[9,4]
 * 
 * 
 * 
 * 说明：
 * 
 * 
 * 输出结果中的每个元素一定是唯一的。
 * 我们可以不考虑输出结果的顺序。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int[] Intersection (int[] nums1, int[] nums2) {
        List<int> result = new List<int> ();
        int i = 0;
        int j = 0;
        Array.Sort (nums1);
        Array.Sort (nums2);
        while (i < nums1.Length && j < nums2.Length) {
            if (nums1[i] == nums2[j]) {
               if (result.Count == 0 || nums1[i] != result[result.Count - 1]) {
                    result.Add (nums1[i]);
                }
                i++;
                j++;
            } else if (nums1[i] < nums2[j]) {
                i++;
            } else {
                j++;
            }
        }
        return result.ToArray ();
    }
}
// @lc code=end