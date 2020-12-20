/*
 * @lc app=leetcode.cn id=171 lang=csharp
 *
 * [171] Excel表列序号
 *
 * https://leetcode-cn.com/problems/excel-sheet-column-number/description/
 *
 * algorithms
 * Easy (68.36%)
 * Likes:    180
 * Dislikes: 0
 * Total Accepted:    54.2K
 * Total Submissions: 79.2K
 * Testcase Example:  '"A"'
 *
 * 给定一个Excel表格中的列名称，返回其相应的列序号。
 * 
 * 例如，
 * 
 * ⁠   A -> 1
 * ⁠   B -> 2
 * ⁠   C -> 3
 * ⁠   ...
 * ⁠   Z -> 26
 * ⁠   AA -> 27
 * ⁠   AB -> 28 
 * ⁠   ...
 * 
 * 
 * 示例 1:
 * 
 * 输入: "A"
 * 输出: 1
 * 
 * 
 * 示例 2:
 * 
 * 输入: "AB"
 * 输出: 28
 * 
 * 
 * 示例 3:
 * 
 * 输入: "ZY"
 * 输出: 701
 * 
 * 致谢：
 * 特别感谢 @ts 添加此问题并创建所有测试用例。
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int TitleToNumber (string s) {
        int len = s.Length;
        int i = 0;
        int baseValue = 26;
        int result = 0;
        while (i < len) {
            char c = s[i];
            int value = (c - 'A') + 1;
            result = result * baseValue + value;
            i++;
        }
        return result;
    }
}
// @lc code=end