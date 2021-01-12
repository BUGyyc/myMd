/*
 * @lc app=leetcode.cn id=434 lang=csharp
 *
 * [434] 字符串中的单词数
 *
 * https://leetcode-cn.com/problems/number-of-segments-in-a-string/description/
 *
 * algorithms
 * Easy (36.86%)
 * Likes:    72
 * Dislikes: 0
 * Total Accepted:    28.1K
 * Total Submissions: 76.2K
 * Testcase Example:  '"Hello, my name is John"'
 *
 * 统计字符串中的单词个数，这里的单词指的是连续的不是空格的字符。
 * 
 * 请注意，你可以假定字符串里不包括任何不可打印的字符。
 * 
 * 示例:
 * 
 * 输入: "Hello, my name is John"
 * 输出: 5
 * 解释: 这里的单词是指连续的不是空格的字符，所以 "Hello," 算作 1 个单词。
 * 
 * 
 */

// @lc code=start
public class Solution
{
    public int CountSegments(string s)
    {
        char[] cs = s.ToCharArray();
        s += " 1";
        int i = 0, result = 0, state = 0;
        while (i < cs.Length)
        {
            state = 0;
            while (i < cs.Length)
            {
                if (IsWord(cs[i]) == true)
                {
                    if (state == 0) state = 1;
                    i++;
                    break;
                }
                else
                {
                    i++;
                }

            }
            while (i < cs.Length)
            {
                if (IsWord(cs[i]) == false)
                {
                    if (state == 1) state = 2;
                    i++;
                    break;
                }
                else
                {
                    i++;
                }
            }
            if (state != 0) result++;
        }
        return result;
    }

    private bool IsWord(char c)
    {
        if(c == ' '){
            return false;
        }else{
            return true;
        }
        // if (c >= 'A' && c <= 'Z')
        // {
        //     return true;
        // }
        // else if (c >= 'a' && c <= 'z')
        // {
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }
    }
}
// @lc code=end

