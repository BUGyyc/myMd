/*
 * @lc app=leetcode.cn id=345 lang=csharp
 *
 * [345] 反转字符串中的元音字母
 *
 * https://leetcode-cn.com/problems/reverse-vowels-of-a-string/description/
 *
 * algorithms
 * Easy (51.15%)
 * Likes:    136
 * Dislikes: 0
 * Total Accepted:    56.8K
 * Total Submissions: 111.2K
 * Testcase Example:  '"hello"'
 *
 * 编写一个函数，以字符串作为输入，反转该字符串中的元音字母。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 输入："hello"
 * 输出："holle"
 * 
 * 
 * 示例 2：
 * 
 * 输入："leetcode"
 * 输出："leotcede"
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 元音字母不包含字母 "y" 。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string ReverseVowels(string s) {
        int left = 0;
        int right = s.Length - 1;
        while(left < right){
            while(left < right || CheckAeiou(s[left]) == false){
                left++;
            }
            while(left < right || CheckAeiou(s[right]) == false){
                right--;
            }
            if(left < right){
                char c = s[left];
                s[left] = s[right];
                s[right] = c;
            }
        }
        return s;
    }

    private bool CheckAeiou(char c){
        if(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u'){
            return true;
        }
        return false;
    }
}
// @lc code=end

