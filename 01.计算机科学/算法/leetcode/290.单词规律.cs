/*
 * @lc app=leetcode.cn id=290 lang=csharp
 *
 * [290] 单词规律
 *
 * https://leetcode-cn.com/problems/word-pattern/description/
 *
 * algorithms
 * Easy (43.96%)
 * Likes:    219
 * Dislikes: 0
 * Total Accepted:    36.3K
 * Total Submissions: 82.6K
 * Testcase Example:  '"abba"\n"dog cat cat dog"'
 *
 * 给定一种规律 pattern 和一个字符串 str ，判断 str 是否遵循相同的规律。
 * 
 * 这里的 遵循 指完全匹配，例如， pattern 里的每个字母和字符串 str 中的每个非空单词之间存在着双向连接的对应规律。
 * 
 * 示例1:
 * 
 * 输入: pattern = "abba", str = "dog cat cat dog"
 * 输出: true
 * 
 * 示例 2:
 * 
 * 输入:pattern = "abba", str = "dog cat cat fish"
 * 输出: false
 * 
 * 示例 3:
 * 
 * 输入: pattern = "aaaa", str = "dog cat cat dog"
 * 输出: false
 * 
 * 示例 4:
 * 
 * 输入: pattern = "abba", str = "dog dog dog dog"
 * 输出: false
 * 
 * 说明:
 * 你可以假设 pattern 只包含小写字母， str 包含了由单个空格分隔的小写字母。    
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public bool WordPattern (string pattern, string s) {
        Dictionary<char,string> dic = new Dictionary<char,string>();
        Dictionary<string,char> dic2 = new Dictionary<string,char>();
        char[] cs = pattern.ToCharArray();
        string[] strs = s.Split(" ");
        if(cs.Length != strs.Length)return false;
        dic.Add(cs[0],strs[0]);
        dic2.Add(strs[0],cs[0]);
        for(int i = 1;i<cs.Length;i++){
            if(dic.ContainsKey(cs[i]) && dic[cs[i]].Equals(strs[i]) == false){
                return false;
            }else if(dic.ContainsKey(cs[i]) == false){
                dic.Add(cs[i],strs[i]);
            }

            if(dic2.ContainsKey(strs[i]) && dic2[strs[i]] != cs[i]){
                return false;
            }else if(dic2.ContainsKey(strs[i]) == false){
                dic2.Add(strs[i],cs[i]);
            }   
        }
        return true;
    }
}
// @lc code=end