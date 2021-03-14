/*
 * @lc app=leetcode.cn id=93 lang=csharp
 *
 * [93] 复原IP地址
 *
 * https://leetcode-cn.com/problems/restore-ip-addresses/description/
 *
 * algorithms
 * Medium (50.39%)
 * Likes:    445
 * Dislikes: 0
 * Total Accepted:    85.6K
 * Total Submissions: 170K
 * Testcase Example:  '"25525511135"'
 *
 * 给定一个只包含数字的字符串，复原它并返回所有可能的 IP 地址格式。
 * 
 * 有效的 IP 地址 正好由四个整数（每个整数位于 0 到 255 之间组成，且不能含有前导 0），整数之间用 '.' 分隔。
 * 
 * 例如："0.1.2.201" 和 "192.168.1.1" 是 有效的 IP 地址，但是
 * "0.011.255.245"、"192.168.1.312" 和 "192.168@1.1" 是 无效的 IP 地址。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：s = "25525511135"
 * 输出：["255.255.11.135","255.255.111.35"]
 * 
 * 
 * 示例 2：
 * 
 * 输入：s = "0000"
 * 输出：["0.0.0.0"]
 * 
 * 
 * 示例 3：
 * 
 * 输入：s = "1111"
 * 输出：["1.1.1.1"]
 * 
 * 
 * 示例 4：
 * 
 * 输入：s = "010010"
 * 输出：["0.10.0.10","0.100.1.0"]
 * 
 * 
 * 示例 5：
 * 
 * 输入：s = "101023"
 * 输出：["1.0.10.23","1.0.102.3","10.1.0.23","10.10.2.3","101.0.2.3"]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 0 <= s.length <= 3000
 * s 仅由数字组成
 * 
 * 
 */

// @lc code=start
public class Solution {
    //回溯
    List<string> list = null;
    public IList<string> RestoreIpAddresses (string s) {
        list = new List<string> ();
        Stack<string> stack = new Stack<string> ();
        BackFunc (s, 1, 0, stack);
        return list;
    }

    private void BackFunc (string origin, int index, int nums, Stack<string> stack) {
        if (index == 5 || nums == origin.Length) {
            if (index == 5 && nums == origin.Length) {
                list.Add (string.Join (".", stack.Reverse ()));
            }
            return;
        }

        for (int i = 1; i < 4; i++) {
            if (i + nums > origin.Length) return;
            string str = origin.Substring (nums, i);
            if (Convert.ToInt32 (str) < 256 && (str == "0" || !str.StartsWith ("0"))) {
                stack.Push (str);
                BackFunc (origin, index + 1, nums + i, stack);
                stack.Pop ();
            }
        }
    }

    // private bool IsValue (string origin, int start, int end) {
    //     if (start > end) return false;

    //     if (origin[start] == 0 && start != end) {
    //         return false;
    //     }

    //     int num = 0;
    //     for (int i = start; i < end; i++) {
    //         if (origin[i] > '9' || origin[i] < '0') {
    //             return false;
    //         }
    //         num = num * 10 + (origin[i] - '0');
    //         if (num > 255) {
    //             return false;
    //         }
    //     }
    //     return true;
    // }
}
// @lc code=end