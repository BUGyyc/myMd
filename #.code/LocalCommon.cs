using System;
using System.Collections.Generic;
public class LocalCommon
{
    private List<IList<int>> CheckNums(int[] nums, int target)
    {
        List<IList<int>> result = new List<IList<int>>();
        if (nums.Length <= 2) return result;
        Array.Sort(nums);
        int pre = 0;
        while (pre < nums.Length - 4)
        {
            for (int start = pre + 1; start < nums.Length - 3; start++)
            {
                int left = start + 1;
                int right = nums.Length - 1;
                while (left < right)
                {
                    if (target > nums[left] + nums[right])
                    {
                        left++;
                    }
                    else if (target < nums[left] + nums[right])
                    {
                        right--;
                    }
                    else
                    {
                        var list = new List<int>();
                        list.Add(nums[pre]);
                        list.Add(nums[start]);
                        list.Add(nums[left]);
                        list.Add(nums[right]);
                        result.Add(list);

                        //去除重复元素
                        while (left < right && nums[left] == list[1])
                            ++left;
                        while (left < right && nums[right] == list[2])
                            --right;
                    }
                }

            }
            //去除重复元素
            int currentStartNumber = nums[pre];
            while (pre < nums.Length - 2 && nums[pre] == currentStartNumber)
                ++pre;
        }
        return result;
    }

    private void ChangeArray(int[] nums)
    {
        int len = nums.Length;
        if (len <= 1) return;
        bool find = false;
        int i = 0;
        int j = 0;
        for (i = len - 1; i >= 0; i--)
        {
            for (j = i - 1; j >= 0; j--)
            {
                if (nums[i] > nums[j])
                {
                    find = true;
                    break;
                }
            }
        }
        if (find)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
        else 
        {
            List<int> list = new List<int> (nums);
            list.Reverse ();
            nums = list.ToArray ();
        }
    }

    public List<int> GetRightArray(TreeNode root){
        Stack<int> stack = new Stack<int> ();
        List<int> list = new List<int>();
        if(root == null){
            return list;
        }
        while(root != null || stack.Count > 0){
            while(root != null){
                
            }
        }
    }

    public TreeNode SortedListToBST(ListNode head) {
        
    }

    public int MinimumTotal(IList<IList<int>> triangle) {
        int row = triangle.Count;
        if(row == 0)return 0;
        int[,] result = new int[row,row];

        result[0,0] = triangle[0][0];

        for(int i = 1;i<row;i++) {
            result[i,0] = result[i-1,0] + triangle[i][0];
            for(int j = 1;j<i;j++){
                result[i,j] = Math.Min(result[i-1,j-1],result[i-1,j]) + triangle[i][j];
            }
            result[i,i] = result[i-1,i-1] + triangle[i][i];
        }
        int min = result[row-1,0];
        for(int i = 0;i<row;i++){
            min = Math.Min(min,result[row-1,i]);
        }
        return min;
    }

    public int CheckArray(int[] nums){
        Dictionary<int,int> result = new Dictionary<int,int>();
        int all = 0;
        int sum;
        for(int i = 0;i<nums.Length;i++){
            all += nums[i];
            if(result.ContainsKey(nums[i]) == false){
                sum += nums[i];
                result.Add(nums[i],nums[i]);
            }
        }
        return (3*sum - all)/2;
    }

    public ListNode ResetListNode(ListNode head){
        if (head == null) return;
        ListNode p1 = head;
        ListNode p2 = ReverseList(head);
        int len = 0;
        ListNode p = head;
        while (p != null)
        {
            p = p.next;
            len++;
        }
        int step = 0;
        ListNode cur = new ListNode(-1);
        cur.next = head;
        while (step < len)
        {
            if (step % 2 == 0)
            {
                cur.next = p1;
                cur = cur.next;
                p1 = p1.next;
            }
            else
            {
                cur.next = p2;
                cur = cur.next;
                p2 = p2.next;
            }
            step++;
        }
    }

    public ListNode ReverseList(ListNode head){
        ListNode cur = head;
        ListNode pre = null;
        while(cur!=null){
            ListNode tmp = cur.next;
            cur.next = pre;
            pre = cur;
            cur = tmp;
        }
        return pre;
    }

    public int GetMaxMul(int[] nums){
        //[3,-1,4]
        int max = int.MinValue;
        int[] result = new int[nums.Length];
        int[] mins = new int[nums.Length];
        result[0] = nums[0];
        for(int i = 1;i< nums.Length; i++){
            result[i] = Math.Max(Math.Max(result[i-1]*nums[i],nums[i]) , Math.Max(mins[i-1]*nums[i],nums[i]));
            mins[i] = Math.Min(Math.Min(result[i-1]*nums[i],nums[i]),Math.Min(mins[i-1]*nums[i],nums[i]));
        }
        for(int i = 0;i<result.Length;i++){
            max = Math.Max(max, result[i]);
        }
        return max;
    }

    int nodeNum = 0;
    public int GetNum(TreeNode root){
        if(root == null)return 0;
        if(root.left==null && root.right==null){
            return 1;
        }

        if(root.left!=null){
            return 1+GetNum(root.left);
        }
        if(root.right!=null){
            return 1+GetNum(root.right);
        }
    }

    private void Func(int[] nums){

    }

    private int CheckArray(List<IList<int>> obstacleGrid){
        int row = obstacleGrid.Length;
        if (row == 0) return 0;
        int col = obstacleGrid[0].Length;
        int[,] arr = new int[row, col];
        arr[0, 0] = 0;
        for (int i = 0; i < col; i++)
        {
            if (obstacleGrid[0][i] == 1)
            {
                break;
            }
            else
            {
                arr[0, i] = 1;
            }
        }
        for (int i = 0; i < row; i++)
        {
            if (obstacleGrid[i][0] == 1)
            {
                break;
            }
            else
            {
                arr[i, 0] = 1;
            }
        }

        for (int i = 1; i < row; i++)
        {
            for (int j = 1; j < col; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    arr[i, j] = 0;
                }
                else
                {
                    arr[i, j] = arr[i - 1, j] + arr[i, j - 1];
                }
            }
        }
        return arr[row-1, col-1];
    }

    private void CheckInt(int num){
        
    }

    public int CheckFlower(int[] flowerbed, int n){
        if(n>flowerbed.Length)return false;
        if(flowerbed.Length < 2){
            if(flowerbed[0] == 0){
                return 1>=n;
            }
        }else if(flowerbed.Length < 3){
            if(flowerbed[0] == 0 && flowerbed[1] == 0){
                return 1>=n;
            }else{
                return 0>=n;
            }
        }
        int count = 0;
        for(int i = 0;i<flowerbed.Length;i++){
            if(i == 0){
                if(flowerbed[i] == 0 && flowerbed[i + 1] == 0){
                    flowerbed[i] = 1;
                    count++;
                }
            }else if(i == flowerbed.Length - 1){
                if(flowerbed[i] == 0 && flowerbed[i - 1] == 0){
                    flowerbed[i] = 1;
                    count++;
                }
            }else{
                if(flowerbed[i] == 0 && flowerbed[i + 1] == 0 && flowerbed[i - 1] == 0){
                    flowerbed[i] = 1;
                    count++;
                }
            }
            if(count>=n)return true;
        }
        return count>=n;
    }

    public bool CanJump(int[] nums){
        int max = 0;
        for(int i = 0;i<nums.Length;i++){
            if(i<=max){
                max = Math.Max(i+nums[i],max);
                if(max>=nums.Length-1)return true;
            }
        }
        return max>=nums.Length-1;
    }
    /// <summary>
    /// [1,2,3,4,5]
    // [3,4,5,1,2]
    /// </summary>
    /// <param name="gas"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public int CanCompleteCircuit(int[] gas, int[] cost) {
        int start = 0;
        int len = gas.Length;
        while(start < len){
            int step = 0;
            int carry = 0;
            int cur = 0;
            while(step < len){
                cur = gas[(step+start)%len] + carry;
                if(cost[(step+start)%len] > cur){
                    break;
                }else{
                    carry = cur - cost[(step+start)%len];
                }
                step++;
                if(len == step){
                    return start;
                }
            }
            if(start == len-1 && step<len){
                return -1;
            }
            start++;
        }
        return start;
    }

    public int WiggleMaxLength(int[] nums) {
        int len = nums.Length;
        for(int i = 0;i<len-1;i++){
            if(nums[i] == nums[i+1]){
                
            }else{

            }
        }
    }

    private Dictionary<int,int> dic = new Dictionary<int,int>();
    public TreeNode BuildTree (int[] preorder, int[] inorder) {
        int len = inorder.Length;
        for(int i = 0;i<len;i++){
            dic.Add(inorder[i],i);
        }
    }

    private TreeNode MyBuilder(int[] preorder, int[] inorder,int preorder_left,int preorder_right,int inorder_left,int inorder_right) {
        if(preorder.left > preorder_right){
            return null;
        }
    }

    public Node Connect(Node root) {
        if(root == null)return root;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);
        while(queue.Count >0 ){
            List<Node> list = new List<Node>();
            int count = queue.Count;
            for(int i = 0;i<count;i++){
                Node tmp = queue.Dequeue();
                list.Add(tmp);
                if(tmp.left!=null){
                    queue.Enqueue(tmp.left);
                }
                if(tmp.right!=null){
                    queue.Enqueue(tmp.right);
                }
            }
            for(int i = 0;i<list.Count-1;i++){
                list[i].next = list[i+1];
            }
            if(list.Count>0)list[list.Count-1].next = null;
        }
        return root;
    }

    public Node CloneGraph(Node node) {
        if(node == null)return null;
        Node mapRoot = null;
        Queue<node> queue = new Queue<Node>();
        queue.Enqueue(node);
        while(queue.Count > 0){
            int count = queue.Count;

            for(int i = 0;i<count;i++){
                Node tmp = queue.Dequeue ();
                Node l_node = new Node(tmp.val);
                foreach(var n in tmp.neighbors) {
                    Node _node = new Node (n.val);
                }
                // l_node.
            }
        }
        
    }
    /// <summary>
    /// 11
// [1,2,3,4,5]
    /// </summary>
    /// <param name="s"></param>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int GetSumNumber(int s,int[] nums){
        int min = int.MaxValue;
        int start = 0;
        int len = nums.Length;
        if(len == 0)return 0;
        while(start<len){
            int sum = 0;
            for(int i = start;i<len;i++){
                if(sum + nums[i]>s){
                    break;
                }else if(sum + nums[i] == s){
                    min = Math.Min(i-start+1,min);
                }else{
                    sum += nums[i];
                }
            }
            start++;
        }
        return min;
    }

}