/*
 * @Author: delevin.ying 
 * @Date: 2021-01-12 11:16:21 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2021-01-12 11:18:08
 */
using System.Collections.Generic;
using System;
using System.Text;

public class Lc5
{
    public IList<string> GetAllClock(int n)
    {
        // if(n == 0)
        return null;
    }

    public int CountSegments(string s) {
        char[] cs = s.ToCharArray();
        int i = 0,result = 0,state = 0;
        while(i<cs.Count){
            state = 0;
            while(i < cs.Count && IsWord(cs[i]) == false){
                i++;
                if(state == 0)state = 1;
            }
            while(i < cs.Count && IsWord(cs[i]) == true){
                i++;
                if(state == 1)state = 2;
            }
            if(state == 2)result++;
        }
        return result;
    }

    private bool IsWord(char c){
        if(c >= 'A' && c <= 'Z'){
            return true;
        }else if(c >= 'a' && c <= 'z'){
            return true;
        }else{
            return false;
        }
    }

    public int ArrangeCoins(int n) {
        int step = 1;
        while(n > 0){
            n -= step;
            step++;
        }
        return (n < 0)?step-1:step;
    }

    public int HammingDistance(int x, int y) {
        int z = x ^ y;
        int result = 0;
        while(z!=0){
            if(z % 2 == 1){
                result++;
            }
            z >>= 1;
        }
        return result;
    }

    public int FindMaxConsecutiveOnes(int[] nums) {
        int result = 0;
        int count = 0;
        for(int i = 0;i<nums.Length;i++){
            if(nums[i] == 1){
                count++;
            }else{
                result = Math.Max(count,result);
            }
        }
        return result;
    }

    public string[] FindWords(string[] words) {
        List<string> list = new List<string>();
        return list.ToArray();
    }

    public int[] FindMode(TreeNode root) {
        if(root == null)return null;
        Stack<TreeNode> stack = new Stack<TreeNode>();
        Dictionary<int,int> dic = new Dictionary<int,int>();
        int max = 0;
        while(stack.Count > 0 || root != null){
            while(root!=null){
                stack.Push(root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop();
            if(dic.ContainsKey(tmp.val)){
                dic[tmp.val]++;
            }else{
                dic[tmp.val] = 1;
            }
            max = Math.Max(max,dic[tmp.val]);
            root = tmp.right;
        }
        List<int> list = new List<int>();
        foreach (KeyValuePair<int, int> item in dic)
        {
            if(dic.Value == max){
                list.Add(dic.Key);
            }
        }
        return list.ToArray();
    }
}