/*
 * @lc app=leetcode.cn id=1207 lang=csharp
 *
 * [1207] 独一无二的出现次数
 *
 * https://leetcode-cn.com/problems/unique-number-of-occurrences/description/
 *
 * algorithms
 * Easy (73.22%)
 * Likes:    111
 * Dislikes: 0
 * Total Accepted:    53.2K
 * Total Submissions: 72.7K
 * Testcase Example:  '[1,2,2,1,1,3]'
 *
 * 给你一个整数数组 arr，请你帮忙统计数组中每个数的出现次数。
 * 
 * 如果每个数的出现次数都是独一无二的，就返回 true；否则返回 false。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：arr = [1,2,2,1,1,3]
 * 输出：true
 * 解释：在该数组中，1 出现了 3 次，2 出现了 2 次，3 只出现了 1 次。没有两个数的出现次数相同。
 * 
 * 示例 2：
 * 
 * 输入：arr = [1,2]
 * 输出：false
 * 
 * 
 * 示例 3：
 * 
 * 输入：arr = [-3,0,1,-3,1,1,1,-3,10,0]
 * 输出：true
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 <= arr.length <= 1000
 * -1000 <= arr[i] <= 1000
 * 
 * 
 */

// @lc code=start
public class Solution {
    public bool UniqueOccurrences (int[] arr) {
        Dictionary<int, int> dic = new Dictionary<int, int> ();
        foreach (var item in arr) {
            if (dic.ContainsKey (item)) {
                dic[item]++;
            } else {
                dic.Add (item, 1);
            }
        }
        List<int> list = new List<int> ();
        foreach (var item in dic) {
            if (list.Contains (item.Value)) {
                return false;
            } else {
                list.Add (item.Value);
            }
        }
        return true;
    }
}
// @lc code=end