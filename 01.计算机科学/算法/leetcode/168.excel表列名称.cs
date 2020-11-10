/*
 * @lc app=leetcode.cn id=168 lang=csharp
 *
 * [168] Excel表列名称
 *
 * https://leetcode-cn.com/problems/excel-sheet-column-title/description/
 *
 * algorithms
 * Easy (38.57%)
 * Likes:    281
 * Dislikes: 0
 * Total Accepted:    37.7K
 * Total Submissions: 97.7K
 * Testcase Example:  '1'
 *
 * 给定一个正整数，返回它在 Excel 表中相对应的列名称。
 * 
 * 例如，
 * 
 * ⁠   1 -> A
 * ⁠   2 -> B
 * ⁠   3 -> C
 * ⁠   ...
 * ⁠   26 -> Z
 * ⁠   27 -> AA
 * ⁠   28 -> AB 
 * ⁠   ...
 * 
 * 
 * 示例 1:
 * 
 * 输入: 1
 * 输出: "A"
 * 
 * 
 * 示例 2:
 * 
 * 输入: 28
 * 输出: "AB"
 * 
 * 
 * 示例 3:
 * 
 * 输入: 701
 * 输出: "ZY"
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string ConvertToTitle (int n) {
        List<char> list = new List<char> ();
        while (n > 0) {
            n -= 1;
            int x = n % 26;
            char c = (char) (x + 'A');
            list.Add (c);
            n /= 26;
        }
        list.Reverse ();
        StringBuilder sb = new StringBuilder ();
        foreach (var item in list) {
            sb.Append (item);
        }
        return sb.ToString ();
    }
}
// @lc code=end