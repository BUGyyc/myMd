/*
 * @lc app=leetcode.cn id=66 lang=csharp
 *
 * [66] 加一
 *
 * https://leetcode-cn.com/problems/plus-one/description/
 *
 * algorithms
 * Easy (45.53%)
 * Likes:    560
 * Dislikes: 0
 * Total Accepted:    212.1K
 * Total Submissions: 465.7K
 * Testcase Example:  '[1,2,3]'
 *
 * 给定一个由整数组成的非空数组所表示的非负整数，在该数的基础上加一。
 * 
 * 最高位数字存放在数组的首位， 数组中每个元素只存储单个数字。
 * 
 * 你可以假设除了整数 0 之外，这个整数不会以零开头。
 * 
 * 示例 1:
 * 
 * 输入: [1,2,3]
 * 输出: [1,2,4]
 * 解释: 输入数组表示数字 123。
 * 
 * 
 * 示例 2:
 * 
 * 输入: [4,3,2,1]
 * 输出: [4,3,2,2]
 * 解释: 输入数组表示数字 4321。
 * 
 * 
 */
// @lc code=start
public class Solution {
    //需要单独计一位进位
    public int[] PlusOne (int[] digits) {
        List<int> result = new List<int> ();
        int carry = 1;
        for (int i = digits.Length - 1; i >= 0; i--) {
            int sum = digits[i] + carry;
            int s = sum % 10;
            carry = sum / 10;
            result.Add (s);
        }

        if (carry > 0) {
            result.Add (carry);
        }

        result.Reverse ();
        int[] newArr = new int[result.Count];
        int a = 0;
        foreach (var item in result) {
            newArr[a++] = item;
        }
        return newArr;
    }
}
// @lc code=end