/*
 * @lc app=leetcode.cn id=599 lang=csharp
 *
 * [599] 两个列表的最小索引总和
 *
 * https://leetcode-cn.com/problems/minimum-index-sum-of-two-lists/description/
 *
 * algorithms
 * Easy (51.91%)
 * Likes:    98
 * Dislikes: 0
 * Total Accepted:    20.9K
 * Total Submissions: 40.2K
 * Testcase Example:  '["Shogun","Tapioca Express","Burger King","KFC"]\n' +
  '["Piatti","The Grill at Torrey Pines","Hungry Hunter Steakhouse","Shogun"]'
 *
 * 假设Andy和Doris想在晚餐时选择一家餐厅，并且他们都有一个表示最喜爱餐厅的列表，每个餐厅的名字用字符串表示。
 * 
 * 你需要帮助他们用最少的索引和找出他们共同喜爱的餐厅。 如果答案不止一个，则输出所有答案并且不考虑顺序。 你可以假设总是存在一个答案。
 * 
 * 示例 1:
 * 
 * 输入:
 * ["Shogun", "Tapioca Express", "Burger King", "KFC"]
 * ["Piatti", "The Grill at Torrey Pines", "Hungry Hunter Steakhouse",
 * "Shogun"]
 * 输出: ["Shogun"]
 * 解释: 他们唯一共同喜爱的餐厅是“Shogun”。
 * 
 * 
 * 示例 2:
 * 
 * 输入:
 * ["Shogun", "Tapioca Express", "Burger King", "KFC"]
 * ["KFC", "Shogun", "Burger King"]
 * 输出: ["Shogun"]
 * 解释: 他们共同喜爱且具有最小索引和的餐厅是“Shogun”，它有最小的索引和1(0+1)。
 * 
 * 
 * 提示:
 * 
 * 
 * 两个列表的长度范围都在 [1, 1000]内。
 * 两个列表中的字符串的长度将在[1，30]的范围内。
 * 下标从0开始，到列表的长度减1。
 * 两个列表都没有重复的元素。
 * 
 * 
 */

// @lc code=start
public class Solution {
    public string[] FindRestaurant (string[] list1, string[] list2) {
        Dictionary<string,int> dic = new Dictionary<string,int>();
        for(int i = 0;i<list1.Length;i++){
            dic.Add(list1[i],-1*(i+1));
        }

        for(int i = 0;i<list2.Length;i++){
            if(dic.ContainsKey(list2[i])){
                dic[list2[i]] = -1*(dic[list2[i]]-1) + i;
            }
        }
        
        int min = int.MaxValue;
        string minStr = "";
        List<string> list = new List<string>();
        foreach(var item in dic){
            if(item.Value >= 0){
                if(item.Value < min){
                    minStr = item.Key;
                    min = item.Value;
                }
            }
        }

        foreach(var item in dic){
            if(item.Value == min){
                list.Add(item.Key);
            }
        }

        return list.ToArray();
    }
}
// @lc code=end