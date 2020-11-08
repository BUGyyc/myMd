/*
 * @lc app=leetcode.cn id=355 lang=csharp
 *
 * [355] 设计推特
 *
 * https://leetcode-cn.com/problems/design-twitter/description/
 *
 * algorithms
 * Medium (41.15%)
 * Likes:    174
 * Dislikes: 0
 * Total Accepted:    18.2K
 * Total Submissions: 44.1K
 * Testcase Example:  '["Twitter","postTweet","getNewsFeed","follow","postTweet","getNewsFeed","unfollow","getNewsFeed"]\n' +
  '[[],[1,5],[1],[1,2],[2,6],[1],[1,2],[1]]'
 *
 * 
 * 设计一个简化版的推特(Twitter)，可以让用户实现发送推文，关注/取消关注其他用户，能够看见关注人（包括自己）的最近十条推文。你的设计需要支持以下的几个功能：
 * 
 * 
 * postTweet(userId, tweetId): 创建一条新的推文
 * getNewsFeed(userId):
 * 检索最近的十条推文。每个推文都必须是由此用户关注的人或者是用户自己发出的。推文必须按照时间顺序由最近的开始排序。
 * follow(followerId, followeeId): 关注一个用户
 * unfollow(followerId, followeeId): 取消关注一个用户
 * 
 * 
 * 示例:
 * 
 * 
 * Twitter twitter = new Twitter();
 * 
 * // 用户1发送了一条新推文 (用户id = 1, 推文id = 5).
 * twitter.postTweet(1, 5);
 * 
 * // 用户1的获取推文应当返回一个列表，其中包含一个id为5的推文.
 * twitter.getNewsFeed(1);
 * 
 * // 用户1关注了用户2.
 * twitter.follow(1, 2);
 * 
 * // 用户2发送了一个新推文 (推文id = 6).
 * twitter.postTweet(2, 6);
 * 
 * // 用户1的获取推文应当返回一个列表，其中包含两个推文，id分别为 -> [6, 5].
 * // 推文id6应当在推文id5之前，因为它是在5之后发送的.
 * twitter.getNewsFeed(1);
 * 
 * // 用户1取消关注了用户2.
 * twitter.unfollow(1, 2);
 * 
 * // 用户1的获取推文应当返回一个列表，其中包含一个id为5的推文.
 * // 因为用户1已经不再关注用户2.
 * twitter.getNewsFeed(1);
 * 
 * 
 */

// @lc code=start
using System.Collections.Generic;
public class Twitter {
    // struct Tweet{
    //     public int id;
    //     public int oth;
    // }
    Dictionary<int, List<int>> allTweetDic;
    List<int> allFollow; //Dictionary<int, List<int>>
    /** Initialize your data structure here. */
    public Twitter () {
        allTweetDic = new Dictionary<int, Tweet> ();
        // allFollowDic = new Dictionary<int, List<int>> ();
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
}

/**
 * Your Twitter object will be instantiated and called as such:
 * Twitter obj = new Twitter();
 * obj.PostTweet(userId,tweetId);
 * IList<int> param_2 = obj.GetNewsFeed(userId);
 * obj.Follow(followerId,followeeId);
 * obj.Unfollow(followerId,followeeId);
 */
// @lc code=end