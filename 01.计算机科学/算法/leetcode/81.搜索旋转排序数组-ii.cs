/*
 * @lc app=leetcode.cn id=81 lang=csharp
 *
 * [81] 搜索旋转排序数组 II
 *
 * https://leetcode-cn.com/problems/search-in-rotated-sorted-array-ii/description/
 *
 * algorithms
 * Medium (36.00%)
 * Likes:    234
 * Dislikes: 0
 * Total Accepted:    42.7K
 * Total Submissions: 118.7K
 * Testcase Example:  '[2,5,6,0,0,1,2]\n0'
 *
 * 假设按照升序排序的数组在预先未知的某个点上进行了旋转。
 * 
 * ( 例如，数组 [0,0,1,2,2,5,6] 可能变为 [2,5,6,0,0,1,2] )。
 * 
 * 编写一个函数来判断给定的目标值是否存在于数组中。若存在返回 true，否则返回 false。
 * 
 * 示例 1:
 * 
 * 输入: nums = [2,5,6,0,0,1,2], target = 0
 * 输出: true
 * 
 * 
 * 示例 2:
 * 
 * 输入: nums = [2,5,6,0,0,1,2], target = 3
 * 输出: false
 * 
 * 进阶:
 * 
 * 
 * 这是 搜索旋转排序数组 的延伸题目，本题中的 nums  可能包含重复元素。
 * 这会影响到程序的时间复杂度吗？会有怎样的影响，为什么？
 * 
 * 
 */

// @lc code=start
public class Solution {
    public bool Search (int[] nums, int target) {
        if (nums.Length == 0) return false;
        List<int> l1 = new List<int> ();
        List<int> l2 = new List<int> ();
        l1.Add (nums[0]);
        int step = 0;
        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] >= nums[i - 1]) {
                l1.Add (nums[i]);
                step = i;
            } else {
                break;
            }
        }

        for (int i = step + 1; i < nums.Length; i++) {
            l2.Add (nums[i]);
        }
        int[] arr1 = l1.ToArray ();
        int[] arr2 = l2.ToArray ();
        return ErSearch (arr1, target) || ErSearch (arr2, target);
    }

    private bool ErSearch (int[] nums, int target) {
        if (nums.Length == 0) return false;
        int l = 0;
        int r = nums.Length - 1;
        while (l <= r) {
            int mid = (l + r) / 2;
            if (nums[mid] == target) {
                return true;
            } else if (nums[mid] < target) {
                l = mid + 1;
            } else {
                r = mid - 1;
            }
        }
        return false;
    }
}
// @lc code=end