/*
 * @lc app=leetcode.cn id=1 lang=java
 *
 * [1] 两数之和
 */

// @lc code=start
class Solution {
    public int[] twoSum(int[] nums, int target) {
        return func1(nums, target);
        // Map<Integer,Integer> map = new HashMap<Integer,Integer>();
        // for(int i = 0;i<nums.length;i++){
        //     if(map.containsKey(target - nums[i])!=null){
        //         return new int[2]{map.get(target-nums[i]),nums[i]};
        //     }else{
        //         map.Add(nums[i],i);
        //     }
        // }
        // return new int[0];
    }

    private int[] func1(int[] nums, int target) {
        Map<Integer, Integer> map = new HashMap<>();
        int[] arr = new int[2];
        for (int i = 0; i < nums.length; i++) {
            int x = nums[i];
            if (map.get(x) != null) {// 直接找 目标差值 （target-x）
                arr[1] = i;
                arr[0] = map.get(nums[i]);
                return arr;
            } else {
                map.put((target - x), i);// 存索引
            }
        }
        return null;
    }
}
// @lc code=end

