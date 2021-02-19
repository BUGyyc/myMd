/*
 * @lc app=leetcode.cn id=1736 lang=csharp
 *
 * [1736] 替换隐藏数字得到的最晚时间
 *
 * https://leetcode-cn.com/problems/latest-time-by-replacing-hidden-digits/description/
 *
 * algorithms
 * Easy (40.21%)
 * Likes:    9
 * Dislikes: 0
 * Total Accepted:    5K
 * Total Submissions: 12.5K
 * Testcase Example:  '"2?:?0"'
 *
 * 给你一个字符串 time ，格式为  hh:mm（小时：分钟），其中某几位数字被隐藏（用 ? 表示）。
 * 
 * 有效的时间为 00:00 到 23:59 之间的所有时间，包括 00:00 和 23:59 。
 * 
 * 替换 time 中隐藏的数字，返回你可以得到的最晚有效时间。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：time = "2?:?0"
 * 输出："23:50"
 * 解释：以数字 '2' 开头的最晚一小时是 23 ，以 '0' 结尾的最晚一分钟是 50 。
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：time = "0?:3?"
 * 输出："09:39"
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：time = "1?:22"
 * 输出："19:22"
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * time 的格式为 hh:mm
 * 题目数据保证你可以由输入的字符串生成有效的时间
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string MaximumTime (string time) {
        string[] strs = time.Split (":");
        string a = strs[0];
        StringBuilder sb = new StringBuilder ();
        if (a[0] == '?' && a[1] == '?') {
            sb.Append ('2');
            sb.Append ('3');
        } else {
            for (int i = 0; i < a.Length; i++) {
                if (i == 0 && a[0] == '?') {
                    if (a[1] > '3') {
                        sb.Append ('1');
                    } else {
                        sb.Append ('2');
                    }
                    continue;
                }

                if (i == 1 && a[1] == '?') {
                    if (a[0] > '1') {
                        sb.Append ('3');
                    } else {
                        sb.Append ('9');
                    }
                    continue;
                }
                sb.Append (a[i]);
            }
        }

        sb.Append (':');

        string b = strs[1];
        if (b[0] == '?' && b[1] == '?') {
            sb.Append ('5');
            sb.Append ('9');
        } else {
            for (int i = 0; i < b.Length; i++) {
                if (i == 0 && b[0] == '?') {
                    sb.Append ('5');
                    continue;
                }
                if (i == 1 && b[1] == '?') {
                    sb.Append ('9');
                    continue;
                }
                sb.Append (b[i]);
            }
        }

        return sb.ToString ();
    }
}
// @lc code=end