/*
 * @lc app=leetcode.cn id=744 lang=csharp
 *
 * [744] 寻找比目标字母大的最小字母
 *
 * https://leetcode-cn.com/problems/find-smallest-letter-greater-than-target/description/
 *
 * algorithms
 * Easy (45.58%)
 * Likes:    108
 * Dislikes: 0
 * Total Accepted:    28.4K
 * Total Submissions: 62.3K
 * Testcase Example:  '["c","f","j"]\n"a"'
 *
 * 给你一个排序后的字符列表 letters ，列表中只包含小写英文字母。另给出一个目标字母
 * target，请你寻找在这一有序列表里比目标字母大的最小字母。
 * 
 * 在比较时，字母是依序循环出现的。举个例子：
 * 
 * 
 * 如果目标字母 target = 'z' 并且字符列表为 letters = ['a', 'b']，则答案返回 'a'
 * 
 * 
 * 
 * 
 * 示例：
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "a"
 * 输出: "c"
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "c"
 * 输出: "f"
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "d"
 * 输出: "f"
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "g"
 * 输出: "j"
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "j"
 * 输出: "c"
 * 
 * 输入:
 * letters = ["c", "f", "j"]
 * target = "k"
 * 输出: "c"
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * letters长度范围在[2, 10000]区间内。
 * letters 仅由小写字母组成，最少包含两个不同的字母。
 * 目标字母target 是一个小写字母。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public char NextGreatestLetter (char[] letters, char target) {
        foreach (var c in letters) {
            if(c > target){
                return c;
            }
        }
        return letters[0];
    }
}
// @lc code=end