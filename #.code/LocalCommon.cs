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
            List<int> list = new List<int>(nums);
            list.Reverse();
            nums = list.ToArray();
        }
    }

    public List<int> GetRightArray(TreeNode root)
    {
        Stack<int> stack = new Stack<int>();
        List<int> list = new List<int>();
        if (root == null)
        {
            return list;
        }
        while (root != null || stack.Count > 0)
        {
            while (root != null)
            {

            }
        }
    }

    public TreeNode SortedListToBST(ListNode head)
    {

    }

    public int MinimumTotal(IList<IList<int>> triangle)
    {
        int row = triangle.Count;
        if (row == 0) return 0;
        int[,] result = new int[row, row];

        result[0, 0] = triangle[0][0];

        for (int i = 1; i < row; i++)
        {
            result[i, 0] = result[i - 1, 0] + triangle[i][0];
            for (int j = 1; j < i; j++)
            {
                result[i, j] = Math.Min(result[i - 1, j - 1], result[i - 1, j]) + triangle[i][j];
            }
            result[i, i] = result[i - 1, i - 1] + triangle[i][i];
        }
        int min = result[row - 1, 0];
        for (int i = 0; i < row; i++)
        {
            min = Math.Min(min, result[row - 1, i]);
        }
        return min;
    }

    public int CheckArray(int[] nums)
    {
        Dictionary<int, int> result = new Dictionary<int, int>();
        int all = 0;
        int sum;
        for (int i = 0; i < nums.Length; i++)
        {
            all += nums[i];
            if (result.ContainsKey(nums[i]) == false)
            {
                sum += nums[i];
                result.Add(nums[i], nums[i]);
            }
        }
        return (3 * sum - all) / 2;
    }

    public ListNode ResetListNode(ListNode head)
    {
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

    public ListNode ReverseList(ListNode head)
    {
        ListNode cur = head;
        ListNode pre = null;
        while (cur != null)
        {
            ListNode tmp = cur.next;
            cur.next = pre;
            pre = cur;
            cur = tmp;
        }
        return pre;
    }

    public int GetMaxMul(int[] nums)
    {
        //[3,-1,4]
        int max = int.MinValue;
        int[] result = new int[nums.Length];
        int[] mins = new int[nums.Length];
        result[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = Math.Max(Math.Max(result[i - 1] * nums[i], nums[i]), Math.Max(mins[i - 1] * nums[i], nums[i]));
            mins[i] = Math.Min(Math.Min(result[i - 1] * nums[i], nums[i]), Math.Min(mins[i - 1] * nums[i], nums[i]));
        }
        for (int i = 0; i < result.Length; i++)
        {
            max = Math.Max(max, result[i]);
        }
        return max;
    }

    int nodeNum = 0;
    public int GetNum(TreeNode root)
    {
        if (root == null) return 0;
        if (root.left == null && root.right == null)
        {
            return 1;
        }

        if (root.left != null)
        {
            return 1 + GetNum(root.left);
        }
        if (root.right != null)
        {
            return 1 + GetNum(root.right);
        }
    }

    private void Func(int[] nums)
    {

    }

    private int CheckArray(List<IList<int>> obstacleGrid)
    {
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
        return arr[row - 1, col - 1];
    }

    private void CheckInt(int num)
    {

    }

    public int CheckFlower(int[] flowerbed, int n)
    {
        if (n > flowerbed.Length) return false;
        if (flowerbed.Length < 2)
        {
            if (flowerbed[0] == 0)
            {
                return 1 >= n;
            }
        }
        else if (flowerbed.Length < 3)
        {
            if (flowerbed[0] == 0 && flowerbed[1] == 0)
            {
                return 1 >= n;
            }
            else
            {
                return 0 >= n;
            }
        }
        int count = 0;
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (i == 0)
            {
                if (flowerbed[i] == 0 && flowerbed[i + 1] == 0)
                {
                    flowerbed[i] = 1;
                    count++;
                }
            }
            else if (i == flowerbed.Length - 1)
            {
                if (flowerbed[i] == 0 && flowerbed[i - 1] == 0)
                {
                    flowerbed[i] = 1;
                    count++;
                }
            }
            else
            {
                if (flowerbed[i] == 0 && flowerbed[i + 1] == 0 && flowerbed[i - 1] == 0)
                {
                    flowerbed[i] = 1;
                    count++;
                }
            }
            if (count >= n) return true;
        }
        return count >= n;
    }

    public bool CanJump(int[] nums)
    {
        int max = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (i <= max)
            {
                max = Math.Max(i + nums[i], max);
                if (max >= nums.Length - 1) return true;
            }
        }
        return max >= nums.Length - 1;
    }
    /// <summary>
    /// [1,2,3,4,5]
    // [3,4,5,1,2]
    /// </summary>
    /// <param name="gas"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int start = 0;
        int len = gas.Length;
        while (start < len)
        {
            int step = 0;
            int carry = 0;
            int cur = 0;
            while (step < len)
            {
                cur = gas[(step + start) % len] + carry;
                if (cost[(step + start) % len] > cur)
                {
                    break;
                }
                else
                {
                    carry = cur - cost[(step + start) % len];
                }
                step++;
                if (len == step)
                {
                    return start;
                }
            }
            if (start == len - 1 && step < len)
            {
                return -1;
            }
            start++;
        }
        return start;
    }

    public int WiggleMaxLength(int[] nums)
    {
        int len = nums.Length;
        for (int i = 0; i < len - 1; i++)
        {
            if (nums[i] == nums[i + 1])
            {

            }
            else
            {

            }
        }
    }

    private Dictionary<int, int> dic = new Dictionary<int, int>();
    public TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        int len = inorder.Length;
        for (int i = 0; i < len; i++)
        {
            dic.Add(inorder[i], i);
        }
    }

    private TreeNode MyBuilder(int[] preorder, int[] inorder, int preorder_left, int preorder_right, int inorder_left, int inorder_right)
    {
        if (preorder.left > preorder_right)
        {
            return null;
        }
    }

    public Node Connect(Node root)
    {
        if (root == null) return root;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            List<Node> list = new List<Node>();
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                Node tmp = queue.Dequeue();
                list.Add(tmp);
                if (tmp.left != null)
                {
                    queue.Enqueue(tmp.left);
                }
                if (tmp.right != null)
                {
                    queue.Enqueue(tmp.right);
                }
            }
            for (int i = 0; i < list.Count - 1; i++)
            {
                list[i].next = list[i + 1];
            }
            if (list.Count > 0) list[list.Count - 1].next = null;
        }
        return root;
    }

    public Node CloneGraph(Node node)
    {
        if (node == null) return null;
        Node mapRoot = null;
        Queue<node> queue = new Queue<Node>();
        queue.Enqueue(node);
        while (queue.Count > 0)
        {
            int count = queue.Count;

            for (int i = 0; i < count; i++)
            {
                Node tmp = queue.Dequeue();
                Node l_node = new Node(tmp.val);
                foreach (var n in tmp.neighbors)
                {
                    Node _node = new Node(n.val);
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
    public int GetSumNumber(int s, int[] nums)
    {
        int min = int.MaxValue;
        int start = 0;
        int len = nums.Length;
        if (len == 0) return 0;
        while (start < len)
        {
            int sum = 0;
            for (int i = start; i < len; i++)
            {
                if (sum + nums[i] >= s)
                {
                    min = Math.Min(i - start + 1, min);
                    break;
                }
                else
                {
                    sum += nums[i];
                }
            }
            start++;
        }
        return min;
    }

    /// <summary>
    /// [0,1,0,3,2,3]
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int GetMaxLen(int[] nums)
    {
        int len = nums.Length;
        int start = 0;
        int max = 0;
        while (start < len)
        {
            int num = 0;
            int pre = start;
            for (int i = start + 1; i < len; i++)
            {
                if (nums[i] > nums[pre])
                {
                    pre = i;
                    num = (num == 0) ? 2 : num + 1;
                }
            }
            max = Math.Max(num, max);
            start++;
        }
        return max;
    }

    // public int DealCoin(int[] coins,int amount){
    //     Array.Sort(coins);
    //     while(amount>0){

    //     }
    // }

    public bool IsPowerOfFour(int n)
    {
        if (n <= 0)
        {
            return false;
        }
        else if (n < 1)
        {
            return IsPowerOfFour(4 * n);
        }
        else if (n == 1)
        {
            return true;
        }
        else if (n == 2)
        {
            return true;
        }
        else if (n < 4)
        {
            return false;
        }
        else if (n == 4)
        {
            return true;
        }
        else
        {
            return IsPowerOfFour(n % 4);
        }
    }

    public char FindTheDifference(string s, string t)
    {
        int sum1 = 0;
        int sum2 = 0;
        foreach (var c in s)
        {
            sum1 += c;
        }
        foreach (var c in t)
        {
            sum2 += c;
        }
        return sum2 - sum1;
    }

    public void Order1(TreeNode root)
    {
        List<int> result = new List<int>();
        if (root == null)
        {
            return result;
        }
        Stack<TreeNode> stack = new Stack<TreeNode>();
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                result.Add(root.val);
                stack.Push(root);
                root = root.left;
            }
            root = stack.Pop();
            root = root.right;
        }
    }

    public void Order2(TreeNode root)
    {
        List<int> result = new List<int>();
        if (root == null) return result;
        Stack<TreeNode> stack = new Stack<TreeNode>();
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
            root = stack.Pop();
            result.Add(root.val);
            root = root.right;
        }
    }

    public void Order3(TreeNode root)
    {
        List<int> result = new List<int>();
        if (root == null) return result;
        Stack<TreeNode> stack = new Stack<TreeNode>();
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.right;
            }
            root = stack.Pop();
            root = root.left;
        }
        result.Resverse();
    }

    public List<List<int>> LevelOrder(TreeNode root)
    {
        List<List<int>> result = new List<List<int>>();
        if (root == null) return result;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            List<int> list = new List<int>();
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                TreeNode tmp = queue.Dequeue();
                list.Add(tmp.val);
                if (tmp.left != null) queue.Enqueue(tmp.left);
                if (tmp.right != null) queue.Enqueue(tmp.right);
            }
            result.Add(list);
        }
        return result;
    }

    public void AddNum(int a, int b)
    {
        if (b == 0) return a;
        int a = a ^ b;
        int carry = (a & b) << 1;
        return AddNum(a, carry);
    }

    public void AddNum2(int a, int b)
    {
        int carry = (a & b) << 1;
        int sum = a ^ b;
        while (carry != 0)
        {
            int num1 = sum;
            int num2 = carry;
            carry = (num1 & num2) << 1;
            sum = (num1 ^ num2);
        }
        return sum;
    }

    private string Roma2Int(string str)
    {

    }

    //18
    private List<int> fourList(int[] nums, int target)
    {
        List<int> result = new List<int>();
        int len = nums.Length;
        if (len < 4) return result;
        Array.Sort(nums);
        int step = 0;
        while (step < len)
        {
            int res = target - nums[step];
            if (res <= 0)
            {
                //
            }
            else
            {
                for (int start = step + 1; start < len - 2; start++)
                {
                    if (res - nums[start] <= 0)
                    {
                        //
                    }
                    else
                    {
                        int a = res - nums[start];
                        int left = start + 1;
                        int right = len - 1;
                        while (left < right)
                        {
                            if (a == nums[left] + nums[right])
                            {
                                result.Add(nums[step], nums[start], nums[left], nums[right]);
                            }
                            else if (a < nums[left] + nums[right])
                            {
                                right--;
                            }
                            else
                            {
                                left++;
                            }
                        }
                    }
                }
            }
            step++;
        }
        return result;
    }

    //31
    private void NextNums(int[] nums)
    {
        if (nums == null || nums.Length <= 1) return;
        int maxIndex = -1;
    }

    private void RotationArr(int[][] nums)
    {
        int row = nums.Length;
        int col = nums[0].Length;

        List<int> res1 = new List<int>();
        List<int> res2 = new List<int>();
        int left = 0;
        int right = col - 1;
        int up = 0;
        int down = row - 1;
        int step = 0;
        while (left <= right && up <= down)
        {
            res1.Clear();
            step = 0;
            for (int i = left; i < right; i++)
            {
                res1.Add(nums[up][i]);
                nums[up][i] = res2[step++];
            }
            res2.Clear();
            step = 0;
            for (int i = up; i < down; i++)
            {
                res2.Add(nums[i][right]);
                nums[i][right] = res1[step++];
            }
            res1.Clear();
            step = 0;
            for (int i = right; i >= left; i--)
            {
                res1.Add(nums[down][i]);
                nums[down][i] = res2[step++];
            }
            res2.Clear();
            step = 0;
            for (int i = down; i >= up; i--)
            {
                res2.Add(nums[i][left]);
                nums[i][left] = res1[step++];
            }

            res1.Clear();
            step = 0;
            for (int i = left; i < right; i++)
            {
                res1.Add(nums[up][i]);
                nums[up][i] = res2[step++];
            }
        }
    }

    private List<IList<int>> PrintZ(TreeNode root)
    {
        List<IList<int>> result = new List<IList<int>>();
        if (root == null) return result;
        int level = 0;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            int count = queue.Count;
            List<int> list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                TreeNode tmp = queue.Dequeue();
                list.Add(tmp.val);
                if (tmp.left != null) queue.Enqueue(tmp.left);
                if (tmp.right != null) queue.Enqueue(tmp.right);
            }
            if (level % 2 == 1) list.Reverse();
            result.Add(list);
            level++;
        }
        return result;
    }

    private int GetSum(int a, int b)
    {
        if (b == 0) return a;
        int sum = a ^ b;
        int carry = (a & b) << 1;
        return GetSum(sum, carry);
    }
}
}