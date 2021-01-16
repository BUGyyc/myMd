/*
 * @lc app=leetcode.cn id=506 lang=csharp
 *
 * [506] 相对名次
 *
 * https://leetcode-cn.com/problems/relative-ranks/description/
 *
 * algorithms
 * Easy (55.56%)
 * Likes:    67
 * Dislikes: 0
 * Total Accepted:    14.1K
 * Total Submissions: 25.4K
 * Testcase Example:  '[5,4,3,2,1]'
 *
 * 给出 N 名运动员的成绩，找出他们的相对名次并授予前三名对应的奖牌。前三名运动员将会被分别授予 “金牌”，“银牌” 和“ 铜牌”（"Gold
 * Medal", "Silver Medal", "Bronze Medal"）。
 * 
 * (注：分数越高的选手，排名越靠前。)
 * 
 * 示例 1:
 * 
 * 
 * 输入: [5, 4, 3, 2, 1]
 * 输出: ["Gold Medal", "Silver Medal", "Bronze Medal", "4", "5"]
 * 解释: 前三名运动员的成绩为前三高的，因此将会分别被授予 “金牌”，“银牌”和“铜牌” ("Gold Medal", "Silver Medal"
 * and "Bronze Medal").
 * 余下的两名运动员，我们只需要通过他们的成绩计算将其相对名次即可。
 * 
 * 提示:
 * 
 * 
 * N 是一个正整数并且不会超过 10000。
 * 所有运动员的成绩都不相同。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string[] FindRelativeRanks (int[] nums) {
        List<string> list = new List<string> ();
        for (int i = 0; i < nums.Length; i++) {
            if (i == 0) {
                list.Add ("Gold Medal");
            } else if (i == 1) {
                list.Add ("Silver Medal");
            } else if (i == 2) {
                list.Add ("Bronze Medal");
            } else {
                list.Add ((i + 1).ToString ());
            }
        }
        return list.ToArray ();
    }
}
// @lc code=end