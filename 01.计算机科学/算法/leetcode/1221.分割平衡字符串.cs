/*
 * @lc app=leetcode.cn id=1221 lang=csharp
 *
 * [1221] 分割平衡字符串
 *
 * https://leetcode-cn.com/problems/split-a-string-in-balanced-strings/description/
 *
 * algorithms
 * Easy (79.02%)
 * Likes:    78
 * Dislikes: 0
 * Total Accepted:    32.2K
 * Total Submissions: 40.7K
 * Testcase Example:  '"RLRRLLRLRL"'
 *
 * 在一个「平衡字符串」中，'L' 和 'R' 字符的数量是相同的。
 * 
 * 给出一个平衡字符串 s，请你将它分割成尽可能多的平衡字符串。
 * 
 * 返回可以通过分割得到的平衡字符串的最大数量。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：s = "RLRRLLRLRL"
 * 输出：4
 * 解释：s 可以分割为 "RL", "RRLL", "RL", "RL", 每个子字符串中都包含相同数量的 'L' 和 'R'。
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：s = "RLLLLRRRLR"
 * 输出：3
 * 解释：s 可以分割为 "RL", "LLLRRR", "LR", 每个子字符串中都包含相同数量的 'L' 和 'R'。
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：s = "LLLLRRRR"
 * 输出：1
 * 解释：s 只能保持原样 "LLLLRRRR".
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 
 * s[i] = 'L' 或 'R'
 * 分割得到的每个字符串都必须是平衡字符串。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public int BalancedStringSplit (string s) {
        char[] cs = s.ToCharArray ();
        if (cs.Length <= 1) return 0;
        Stack<char> stack = new Stack<char> ();
        stack.Push (s[0]);
        for (int i = 1; i < cs.Length; i++) {
            
        }
    }
}
// @lc code=end