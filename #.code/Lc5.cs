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

    public int Fib(int n) {
        if(n == 0 || n == 1)return n;
        int[] dp = new int[n+1];
        dp[0] = 0;
        dp[1] = 1;
        for(int i = 2;i<n+1;i++){
            dp[i] = dp[i-1]+dp[i-2];
        }
        return dp[n];
    }

    public int GetMinimumDifference(TreeNode root) {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        bool hasVal = false;
        int pre = 0;
        int min = 0;
        while(stack.Count > 0 || root != null){
            while(root!=null){
                stack.Push(root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop();
            if(hasVal){
                int val = pre - tmp.val;
                val = Math.Abs(val);
                min = Math.Max(min,val);
            }
            hasVal = true;
            pre = tmp.val;
            root = tmp.right;
        }
        return min;
    }

    public string[] FindRelativeRanks (int[] nums) {
        int[] tmp = new int[nums.Length];
        for(int i = 0;i<nums.Length;i++){
            tmp[i] = nums[i];
        }
        Array.Sort(tmp);
        Dictionary<int,string> dic = new Dictionary<int,string>();
        for (int i = 0; i < tmp.Length; i++) {
            if (i == 0) {
                dic.Add (tmp[i],"Gold Medal");
            } else if (i == 1) {
                dic.Add (tmp[i],"Silver Medal");
            } else if (i == 2) {
                dic.Add (tmp[i],"Bronze Medal");
            } else {
                dic.Add (tmp[i],(i + 1).ToString ());
            }
        }
        List<string> list = new List<string>();
        for(int i = 0;i<nums.Length;i++){
            list.Add(dic[nums[i]]);
        }
        return list.ToArray();
    }

    public string[] FindRestaurant (string[] list1, string[] list2) {
        Dictionary<string,int> dic = new Dictionary<string,int>();
        for(int i = 0;i<list1.Length;i++){
            dic.Add(list1[i],-1*i);
        }

        for(int i = 0;i<list2.Length;i++){
            if(dic.ContainsKey(list2[i])){
                dic[list2[i]] = -1*dic[list2[i]] + i;
            }
        }
        
        int min = int.MaxValue;
        string minStr = "";
        foreach(var item in dic){
            if(item.Value >= 0 && item.Value < min){
                minStr = item.Key;
            
            }
        }
        return new string[1]{minStr};
    }

    int minValue = int.MaxValue;

    public int MinDiffInBST (TreeNode root) {
        int minValue = int.MaxValue;
        if (root == null) return 0;
        List<int> list = new List<int>();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        while(stack.Count > 0 || root!=null){
            while(root!=null){
                stack.Push(root);
                root = root.left;
            }
            TreeNode tmp = stack.Pop();
            list.Add(tmp.val);
            root = tmp.right;
        }

        for(int i = 0;i<list.Count-1;i++){
            int v = Math.Abs(list[i] - list[i+1]);
            minValue = Math.Min(v,minValue);
        }
        return minValue;
    }

    private void FindMin(TreeNode root){
        if(root == null)return;
        if(root.left == null && root.right == null){
            return;
        }

        if(root.left!=null){
            int val = Math.Abs(root.val - root.left.val);
            minValue = Math.Min(minValue,val);
            FindMin(root.left);
        }

        if(root.right!=null){
            int val = Math.Abs(root.val - root.right.val);
            minValue = Math.Min(minValue,val);
            FindMin(root.right);
        }
    }

    public bool HasAlternatingBits (int n) {
        if(n == 0 || n == 1)return true;
        if(n == 2)return false;
        
        int pre = n%2;
        n = n/2;
        while(n > 0){   
            int cur = n%2;
            if(cur + pre != 1){
                return false;
            }
            n = n/2;
            pre = cur;
        }
        return true;
    }

    
    public int[] FindErrorNums(int[] nums) {
        int len = nums.Length;
        int sum = (1+len)*len/2;
        List<int> list = new List<int>();
        int target = -1;
        foreach(var item in nums){
            if(list.Contains(item) == false){
                sum -= item;
                list.Add(item);
            }else{
                target = item;
            }
        }
        return new int[2]{target,sum};
    }
}