/*
 * @lc app=leetcode.cn id=500 lang=csharp
 *
 * [500] 键盘行
 *
 * https://leetcode-cn.com/problems/keyboard-row/description/
 *
 * algorithms
 * Easy (69.81%)
 * Likes:    120
 * Dislikes: 0
 * Total Accepted:    23.4K
 * Total Submissions: 33.6K
 * Testcase Example:  '["Hello","Alaska","Dad","Peace"]'
 *
 * 给定一个单词列表，只返回可以使用在键盘同一行的字母打印出来的单词。键盘如下图所示。
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 示例：
 * 
 * 输入: ["Hello", "Alaska", "Dad", "Peace"]
 * 输出: ["Alaska", "Dad"]
 * 
 * 
 * 
 * 
 * 注意：
 * 
 * 
 * 你可以重复使用键盘上同一字符。
 * 你可以假设输入的字符串将只包含字母。
 * 
 */

// @lc code=start
public class Solution {
    public string[] FindWords (string[] words) {
        string a = "qwertyuiop";
        string b = "asdfghjkl";
        string c = "zxcvbnm";
        foreach (var s in words) {
            int index = -1;
            foreach (var item in s) {
                int i = -1;
                int x = a.IndexOf (item);
                if (x != -1) {
                    if (index = -1) {
                        index = 1;
                    } else {

                    }
                } else {
                    int y = b.IndexOf (item);
                    if (y != -1) {
                        index = 2;
                    } else {
                        int z = c.IndexOf (item);
                        if (z != -1) {
                            index = 3;
                        }
                    }
                }

            }
        }
    }
}
// @lc code=end