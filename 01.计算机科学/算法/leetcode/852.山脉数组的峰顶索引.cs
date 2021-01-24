/*
 * @lc app=leetcode.cn id=852 lang=csharp
 *
 * [852] 山脉数组的峰顶索引
 *
 * https://leetcode-cn.com/problems/peak-index-in-a-mountain-array/description/
 *
 * algorithms
 * Easy (70.49%)
 * Likes:    127
 * Dislikes: 0
 * Total Accepted:    35.2K
 * Total Submissions: 49.9K
 * Testcase Example:  '[0,1,0]'
 *
 * 符合下列属性的数组 arr 称为 山脉数组 ：
 * 
 * arr.length >= 3
 * 存在 i（0 < i < arr.length - 1）使得：
 * 
 * arr[0] < arr[1] < ... arr[i-1] < arr[i] 
 * arr[i] > arr[i+1] > ... > arr[arr.length - 1]
 * 
 * 
 * 
 * 
 * 给你由整数组成的山脉数组 arr ，返回任何满足 arr[0] < arr[1] < ... arr[i - 1] < arr[i] > arr[i +
 * 1] > ... > arr[arr.length - 1] 的下标 i 。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：arr = [0,1,0]
 * 输出：1
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：arr = [0,2,1,0]
 * 输出：1
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：arr = [0,10,5,2]
 * 输出：1
 * 
 * 
 * 示例 4：
 * 
 * 
 * 输入：arr = [3,4,5,1]
 * 输出：2
 * 
 * 
 * 示例 5：
 * 
 * 
 * 输入：arr = [24,69,100,99,79,78,67,36,26,19]
 * 输出：2
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 3 
 * 0 
 * 题目数据保证 arr 是一个山脉数组
 * 
 * 
 * 
 * 
 * 进阶：很容易想到时间复杂度 O(n) 的解决方案，你可以设计一个 O(log(n)) 的解决方案吗？
 * 
 */

// @lc code=start
public class Solution {
    public int PeakIndexInMountainArray (int[] arr) {
        for (int i = 1; i < arr.Length - 1; i++) {
            if (arr[i] > arr[i - 1] && arr[i] > arr[i + 1]) {
                return i;
            }
        }
        return 0;
    }
}
// @lc code=end