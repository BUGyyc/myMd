/*
 * @lc app=leetcode.cn id=322 lang=csharp
 *
 * [322] 零钱兑换
 *
 * https://leetcode-cn.com/problems/coin-change/description/
 *
 * algorithms
 * Medium (41.72%)
 * Likes:    892
 * Dislikes: 0
 * Total Accepted:    148.3K
 * Total Submissions: 355.3K
 * Testcase Example:  '[1,2,5]\n11'
 *
 * 给定不同面额的硬币 coins 和一个总金额
 * amount。编写一个函数来计算可以凑成总金额所需的最少的硬币个数。如果没有任何一种硬币组合能组成总金额，返回 -1。
 * 
 * 你可以认为每种硬币的数量是无限的。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 输入：coins = [1, 2, 5], amount = 11
 * 输出：3 
 * 解释：11 = 5 + 5 + 1
 * 
 * 示例 2：
 * 
 * 
 * 输入：coins = [2], amount = 3
 * 输出：-1
 * 
 * 示例 3：
 * 
 * 
 * 输入：coins = [1], amount = 0
 * 输出：0
 * 
 * 
 * 示例 4：
 * 
 * 
 * 输入：coins = [1], amount = 1
 * 输出：1
 * 
 * 
 * 示例 5：
 * 
 * 
 * 输入：coins = [1], amount = 2
 * 输出：2
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 1 
 * 1 
 * 0 
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public int CoinChange(int[] coins, int amount) {
            int step = 0;
            if(amount<=0)return step;
            Array.Sort(coins);
            int res = 0;
            CalCoin(coins,amount,step,res);
            return res;
        }

        public void CalCoin(int[] coins,int amount,int step,int res){
            if(amount < 0)return;
            if(amount == 0)return;
            for(int i = coins.Length-1;i>=0;i--){
                if(amount - coins[i] < 0){
                    continue;
                }else if(amount - coins[i] == 0){
                    step++;
                    res = step;
                    return;
                }else{
                    step++;
                    amount-=coins[i];
                    CalCoin(coins,amount,step,res);
                    step--;
                    amount+=coins[i];
                }
            }
        }
}
// @lc code=end

