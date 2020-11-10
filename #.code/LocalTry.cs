using System.Collections.Generic;

public class LocalTry
{
    public void Dived(int dividend, int divisor)
    {
        bool sign = (dividend ^ divisor) >> 31;
        if (dividend > 0) dividend = -dividend;
        if (divisor > 0) divisor = -divisor;
        long lDividend = (long)dividend;
        long lDivisor = (long)divisor;
        long res = 0;
        while (lDividend >= lDivisor)
        {
            long temp = lDivisor;
            long i = 1;
            while (lDividend >= temp)
            {
                lDividend -= temp;
                res += i;
                i <<= 1;
                temp <<= 1;
            }
        }
        if (sign == -1) res = -res;
        if (res > int.MaxValue)
        {
            return int.MaxValue;
        }
        else if (res < int.MinValue)
        {
            return int.MinValue;
        }
        else
        {
            return res;
        }
    }

    /// <summary>
    /// 144
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PreorderTraversal(TreeNode root)
    {
        List<int> result = new List<int>();
        PreorderTraversalFunc1(ref result, root);
    }

    private void PreorderTraversalFunc1(ref List<int> result, TreeNode root)
    {
        if (root == null) return;
        result.Add(root.val);
        if (root.left != null) PreorderTraversalFunc1(ref result, root.left);
        if (root.right != null) PreorderTraversalFunc1(ref result, root.right);
    }

    private void PreorderTraversalFunc2(ref List<int> result,TreeNode root){
        Stack<TreeNode> stack = new Stack<TreeNode>();
        if(root!=null){
            stack.Push(root);
        }
        while(stack.Count>0){
            TreeNode node = stack.Pop();
            result.Add(node.val);
            if(node.left!=null){
                stack.Push(node.left);
            }
            if(node.right!=null){
                stack.Push(node.right);
            }
        }
    }
}

public class Twitter {
    Dictionary<int, List<int>> allTweetDic;
    List<int> allFollow;
    /** Initialize your data structure here. */
    public Twitter () {
        allTweetDic = new Dictionary<int, Tweet> ();
        allFollow = new List<int> ();
    }

    /** Compose a new tweet. */
    public void PostTweet (int userId, int tweetId) {
        if (allTweetDic.ContainsKey (userId)) {
            var list = allTweetDic[userId];
            list.Insert (0,tweetId);
            allTweetDic[userId] = list;
        } else {
            List<int> list = new List<int> ();
            list.Insert (0,tweetId);
            allTweetDic.Add (userId, list);
        }
    }

    /** Retrieve the 10 most recent tweet ids in the user's news feed. Each item in the news feed must be posted by users who the user followed or by the user herself. Tweets must be ordered from most recent to least recent. */
    public IList<int> GetNewsFeed (int userId) {
        if (allFollow == null || allFollow.Count < 1 || allFollow.Contains (userId) == false) {
            return new List<int> ();
        } else 
        if(allTweetDic.ContainsKey(userId)){
            return allTweetDic[userId];
        }else{
            return new List<int>();
        }
    }

    /** Follower follows a followee. If the operation is invalid, it should be a no-op. */
    public void Follow (int followerId, int followeeId) {

    }

    /** Follower unfollows a followee. If the operation is invalid, it should be a no-op. */
    public void Unfollow (int followerId, int followeeId) {

    }

    /// <summary>
    /// 80
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    private int FilterArray(int[] nums){
        if(nums.Length == 0)return 0;
        int j = 0;
        int count = 1;
        int i = 1;
        while(i++<nums.Length){
            if(nums[i-1] == nums[i]){
                count++;
            }else{
                count= 1;
            }
            if(count<=2){
                nums[j++] = nums[i];
            }
        }
        return j;
    }
    /// <summary>
    /// 输入: nums = [2,5,6,7,0,1,2], target = 0
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool FindTarget(int[] nums,int target){
        if(nums.Length == 0)return false;
        List<int> l1 = new List<int>();
        List<int> l2 = new List<int>();
        l1.Add(nums[0]);
        int step = 1;
        for(int i = 1;i<nums.Length;i++){
            if(nums[i]>= nums[i-1]){
                l1.Add(nums[i]);
                step = i;
            }
        }

        for(int i = step;i<nums.Length;i++){
            l2.Add(nums[i]);
        }
        int[] arr1 = l1.ToList();
        int[] arr2 = l2.ToList();
        return ErSearch(arr1,target) || ErSearch(arr2,target);
    }

    private bool ErSearch(int[] nums,int target){
        if(nums.Length == 0)return false;
        int l = 0;
        int r = nums.Length-1;
        while(l<r){
            int mid = (l+r)/2;
            if(nums[mid] == target){
                return true;
            }else if(nums[mid] < target){
                l = mid+1;
            }else{
                r = mid-1;
            }
        }
        return false;
    }
}

public struct TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
}

public struct ListNode
{
    public int val;
    public ListNode next;
}