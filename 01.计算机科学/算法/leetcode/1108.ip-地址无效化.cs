/*
 * @lc app=leetcode.cn id=1108 lang=csharp
 *
 * [1108] IP 地址无效化
 *
 * https://leetcode-cn.com/problems/defanging-an-ip-address/description/
 *
 * algorithms
 * Easy (83.85%)
 * Likes:    63
 * Dislikes: 0
 * Total Accepted:    54.9K
 * Total Submissions: 65.5K
 * Testcase Example:  '"1.1.1.1"'
 *
 * 给你一个有效的 IPv4 地址 address，返回这个 IP 地址的无效化版本。
 * 
 * 所谓无效化 IP 地址，其实就是用 "[.]" 代替了每个 "."。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入：address = "1.1.1.1"
 * 输出："1[.]1[.]1[.]1"
 * 
 * 
 * 示例 2：
 * 
 * 输入：address = "255.100.50.0"
 * 输出："255[.]100[.]50[.]0"
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 给出的 address 是一个有效的 IPv4 地址
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string DefangIPaddr (string address) {
        StringBuilder sb = new StringBuilder ();
        foreach (var item in address) {
            if (item == '.') {
                sb.Append ('[');
                sb.Append ('.');
                sb.Append (']');
            } else {
                sb.Append (item);
            }
        }
        return sb.ToString ();
    }
}
// @lc code=end