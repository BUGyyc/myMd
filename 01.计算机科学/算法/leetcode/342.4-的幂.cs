/*
 * @lc app=leetcode.cn id=342 lang=csharp
 *
 * [342] 4的幂
 *
 * https://leetcode-cn.com/problems/power-of-four/description/
 *
 * algorithms
 * Easy (49.60%)
 * Likes:    151
 * Dislikes: 0
 * Total Accepted:    36.6K
 * Total Submissions: 73.7K
 * Testcase Example:  '16'
 *
 * 给定一个整数，写一个函数来判断它是否是 4 的幂次方。如果是，返回 true ；否则，返回 false 。
 * 
 * 整数 n 是 4 的幂次方需满足：存在整数 x 使得 n == 4^x
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：n = 16
 * 输出：true
 * 
 * 
 * 示例 2：
 * 
 * 
 * 输入：n = 5
 * 输出：false
 * 
 * 
 * 示例 3：
 * 
 * 
 * 输入：n = 1
 * 输出：true
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * -2^31 
 * 
 * 
 * 
 * 
 * 进阶：
 * 
 * 
 * 你能不使用循环或者递归来完成本题吗？
 * 
 * 
 */

// @lc code=start
public class Solution {
    public bool IsPowerOfFour(int n) {
        if(n <= 0){
            return false;
        }else if(n < 1){
            return IsPowerOfFour(4*n);
        }else if(n == 1){
            return true;
        }else if(n < 4){
            return false;
        }else if(n == 4){
            return true;
        }else{
            if(n%4==0){
                return IsPowerOfFour(n/4);
            }else{
                return false;
            }
        }
    }
}
// @lc code=end

