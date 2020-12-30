using System.Collections.Generic;

public struct TreeNode
{
    public int val;

    public TreeNode left;

    public TreeNode right;
}

public struct Node
{
    public int val;

    public Node left;

    public Node right;

    public Node next;
}

public struct ListNode
{
    public int val;

    public ListNode next;
}

public class Lc300
{
    //18
    public List<IList<int>> FourTarget(int[] nums, int target)
    {
        List<IList<int>> result = new List<IList<int>>();
        int len = nums.Length;
        if (len < 4) return result;
        Array.Sort (nums);
        for (int i = 0; i < len - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) continue;
            if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target)
            {
                break;
            }
            if (nums[i] + nums[len - 3] + nums[len - 2] + nums[len - 1] < target
            )
            {
                continue;
            }
            for (int j = i + 1; j < len - 2; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1]) continue;
                if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target)
                {
                    break;
                }
                if (nums[i] + nums[j] + nums[len - 2] + nums[len - 1] < target)
                {
                    continue;
                }
                int left = j + 1;
                int right = len - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[j] + nums[left] + nums[right];
                    if (sum == target)
                    {
                        List<int> list = new List<int>();
                        list.Add(nums[i]);
                        list.Add(nums[j]);
                        list.Add(nums[left]);
                        list.Add(nums[right]);
                        result.Add(list.ToList());
                        while(left < right && nums[left] == nums[left+1]){
                            left++;
                        }
                        left++;
                        while(left < right && nums[right] == nums[right-1]){
                            right--;
                        }
                        right--;
                    }else if(sum > target){
                        right--;
                    }else{
                        left++;
                    }
                }
            }
        }
        return result;
    }

    //59
    public void DynamicArr(int n)
    {
        int[][] arr = new int[n][n];
        int left = 0;
        int right = n - 1;
        int up = 0;
        int down = n - 1;
        int level = 1;
        while (left <= right && up <= down)
        {
            for (int i = left; i < right; i++)
            {
                arr[up][i] = level;
                level++;
            }
            for (int i = up; i < down; i++)
            {
                arr[i][right] = level;
                level++;
            }
            if (left < right && up < down)
            {
                for (int i = right; i >= left; i--)
                {
                    arr[down][i] = level;
                    level++;
                }

                for (int i = down; i >= up; i++)
                {
                    arr[i][left] = level;
                    level++;
                }
            }
        }
    }

    //71
    public string ConvertStr(string path)
    {
        string[] strs = path.Split("/");
        Stack<string> stack = new Stack<string>();
        foreach (var item in strs)
        {
            if (item.Trim().Length() > 0)
            {
                stack.Push (item);
            }
        }

        Stack<string> tmp = new Stack<string>();
        while (stack.Count > 0)
        {
            string s = stack.Pop();
        }
    }

    //117
    public Node GenerateNode(Node root)
    {
        if (root == null) return root;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue (root);
        while (queue.Count > 0)
        {
            int count = queue.Count;
            Node pre = null;
            for (int i = 0; i < count; i++)
            {
                Node tmp = queue.Dequeue();
                if (i == count - 1)
                {
                    tmp.next = null;
                }
                if (pre != null)
                {
                    pre.next = tmp;
                }
                pre = tmp;
                if (tmp.left != null) queue.Enqueue(tmp.left);
                if (tmp.right != null) queue.Enqueue(tmp.right);
            }
        }
        return root;
    }

    //130
    public void CircleArr(char[][] board)
    {
        int row = board.Length;
        int col = board[0].Length;
        bool[][] visit = new bool[row][col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (visit[i][j] == false && board[i][j] == 'o')
                {
                    visit[i][j] = true;
                    SetFourDir (board, i, j, row, col);
                }
            }
        }
    }

    private void SetFourDir(char[][] arr, int j, int row, int col)
    {
        SetVal(arr, i - 1, j, row, col);
        SetVal(arr, i + 1, j, row, col);
        SetVal(arr, i, j - 1, row, col);
        SetVal(arr, i, j + 1, row, col);
    }

    private void SetVal(char[][] arr, int i, int j, int row, int col)
    {
        if (i < 0 || i >= row) return;
        if (j < 0 || j >= col) return;
        arr[i][j] = 'o';
    }

    //213
    //[1,1,1,2]
    //[1,2,3,1]
    private int GetMax(int[] nums)
    {
        int len = nums.Length;
        if (len == 0) return 0;
        int[] dp = new int[len];
        dp[0] = nums[0];
        if (len == 1) return dp[0];
        dp[1] = Math.Max(nums[1], dp[0]);
        if (len == 2) return dp[1];
        if (len == 3)
        {
            if (dp[1] > dp[0])
            {
                return dp[1];
            }
            else
            {
                return dp[0];
            }
        }
        for (int i = 3; i < len; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
        }
        if (dp[2] == dp[0] + nums[2])
        {
            return dp[len - 2];
        }
        else
        {
            return dp[len - 1];
        }

        Array.Copy();
    }

    //220
    private int Check(int[] nums, int k, int t)
    {
        int len = nums.Length;
        for (int i = 0; i < len; i++)
        {
            int step = 1;
            while (i + step < len && step <= k)
            {
                if (Math.Abs(nums[i] - nums[i + step]) <= t)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int
    ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
    {
        if (C >= E || G >= A) return 0;
        if (B >= H || F >= D) return 0;
    }
}
