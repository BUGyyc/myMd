/*
 * @lc app=leetcode.cn id=17 lang=csharp
 *
 * [17] 电话号码的字母组合
 *
 * https://leetcode-cn.com/problems/letter-combinations-of-a-phone-number/description/
 *
 * algorithms
 * Medium (55.52%)
 * Likes:    961
 * Dislikes: 0
 * Total Accepted:    185.5K
 * Total Submissions: 334.1K
 * Testcase Example:  '"23"'
 *
 * 给定一个仅包含数字 2-9 的字符串，返回所有它能表示的字母组合。
 * 
 * 给出数字到字母的映射如下（与电话按键相同）。注意 1 不对应任何字母。
 * 
 * 
 * 
 * 示例:
 * 
 * 输入："23"
 * 输出：["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
 * 
 * 
 * 说明:
 * 尽管上面的答案是按字典序排列的，但是你可以任意选择答案输出的顺序。
 * 
 */

// @lc code=start
public class Solution {
    public IList<string> LetterCombinations (string digits) {
        List<string> result = new List<string> ();
        if (digits.Length == 0) {
            return result;
        }
        Dictionary<char, string> dic = new Dictionary<char, string> ();
        dic['2'] = "abc";
        dic['3'] = "def";
        dic['4'] = "ghi";
        dic['5'] = "jkl";

        dic['6'] = "mno";
        dic['7'] = "pqrs";
        dic['8'] = "tuv";
        dic['9'] = "wxyz";
        backTrack (0,dic,digits,"",result);
        return result;
    }

    private void backTrack (int index, Dictionary<char, string> dictionary, string digits, string str, List<string> result) {
        if (str.Length == digits.Length) {
            result.Add (str);
        } else {
            string s = dictionary[digits[index]];
            for (int i = 0; i < s.Length; i++) {
                str += s[i];
                backTrack(index+1,dictionary,digits,str,result);
                str = str.Remove(str.Length-1);
            }
        }
    }
}
// @lc code=end