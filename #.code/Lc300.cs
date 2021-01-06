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
                        while (left < right && nums[left] == nums[left + 1])
                        {
                            left++;
                        }
                        left++;
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            right--;
                        }
                        right--;
                    }
                    else if (sum > target)
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

    ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
    {
        if (C >= E || G >= A) return 0;
        if (B >= H || F >= D) return 0;
    }

    private TreeNode List2Tree(ListNode head)
    {
        if (head == null) return null;
        List<int> list = new List<int>();
        ListNode p = head;
        while (p != null)
        {
            list.Add (p);
            p = p.next;
        }
        return Convert2Tree(list);
    }

    private TreeNode Convert2Tree(List<int> list)
    {
        if (list == null || list.Count == 0) return null;
        if (list.Count == 1)
        {
            TreeNode node = new TreeNode(list[0]);
            return node;
        }
        int left = 0;
        int right = list.Count - 1;
        int mid = (left + right) / 2;
        TreeNode root = new TreeNode(list[mid]);
        root.left = 

    }

    private TreeNode GetNode(List<int> list,int index){
        if(index < 0 || index > list.Count-1)return null;
        TreeNode node = new TreeNode(list[index])
    }

    class MapNodePro{
        public Node pre;
        public Node node;
        public MapNodePro(Node p,Node n){
            pre = p;
            node = n;
        }
    }
    //133
    private Node CloneMap(Node node){
        if(node == null)return null;
        Node head = null;
        Node curr = null;
        Queue<MapNodePro> queue = new Queue<MapNodePro>();
        queue.Enqueue(new (null,node));
        while(queue.Count > 0){
            int count = queue.Count;
            for(int i = 0;i<count;i++){
                MapNodePro n = queue.Dequeue();
                if(head == null){
                    head = new Node(n.val);
                    curr = head;
                }else{
                    curr = new Node(n.val);
                }
                foreach(int item in n.neighbors){
                    curr.Add(new Node(item.val))
                }
            }
        }
        return head;
    }

    public int Calculate(string str){
        List<string> list = new List<string>();
        StringBuilder sb = new StringBuilder();
        for(int i = 0;i< str.Length;i++){
            if(str[i] != ' '){
                sb.Append(str[i]);
            }
        }
        str = sb.ToString();
        sb = new StringBuilder();
        for(int i = 0;i<str.Length;i++){
            if(str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/'){
                list.Add(sb.ToString());
                list.Add(str[i].ToString());
                sb = new StringBuilder();
            }else if(i!=str.Length-1){
                sb.Append(str[i]);
            }else{
                list.Add(sb.ToString());
            }
        }
        List<int> tmp = new List<int>();
        //先算乘除
        for(int i = 0;i<list.Count;i++){
            if(list[i] == "*"){
                int a = int.Parse(list[i-1]);
                int b = int.Parse(list[i+1]);
                a = a*b;
            }else if(list[i] == "/"){
                int a = int.Parse(list[i-1]);
                int b = int.Parse(list[i+1]);
                a = a*b;
            }
        }
    }

    //263
    public bool IsUgly(int n){
        if(n == 1 || n == 2|| n == 3 || n == 4 || n == 5 || n == 6 || n == 8 || n == 9)return true;
        if(n == 7 || n == 0)return false;
        if(n%2 == 0){
            return IsUgly(n/2);
        }else if(n%3 == 0){
            return IsUgly(n/3);
        }else if(n%5 == 0){
            return IsUgly(n/5);
        }
        return false;
    }

    public bool IsPowerOfFour(int n) {
        if(n <= 0){
            return false;
        }else if(n < 1){
            return IsPowerOfFour(4*n);
        }else if(n == 1){
            return true;
        }else if(n < 4){
            return false;
        }else if(n == 4){
            return true;
        }else{
            if(n%4==0){
                return IsPowerOfFour(n/4);
            }else{
                return false;
            }
        }
    }

    //345
    public string ReverseVowels(string s) {
        int left = 0;
        int right = s.Length - 1;
        while(left < right){
            while(left < right || CheckAeiou(s[left]) == false){
                left++;
            }
            while(left < right || CheckAeiou(s[right]) == false){
                right--;
            }
            if(left < right){
                char c = s[left];
                s[left] = s[right];
                s[right] = c;
            }
        }
        return s;
    }

    private bool CheckAeiou(char c){
        if(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u'){
            return true;
        }
        return false;
    }

    public bool IsPrefectNum(int num){
        if(num < 0)return false;
        if(num == 1 || num == 0)return true;
        long left = 2;
        long right = num/2;
        long x = 0;
        long g = 0;
        while(left <= right){
            x = left + (right-left)/2;
            g = x * x;
            if(g == num){
                return true;
            }else if(g > num){
                right = x - 1;
            }else{
                left = x + 1;
            }
        }
        return false;
    }

    public int GuessNumber(int n) {
        int left = 1;
        int right = n;
        while(left<=right){
            int mid = left + (right-left)/2;
            if(guess(mid) == 0){
                return mid;
            }else if(guess(mid) > 0){
                left = mid + 1;
            }else{
                right = mid - 1;
            }
        }
        return n;
    }

    public IList<IList<int>> LevelOrder(Node root) {
        List<IList<int>> result = new List<IList<int>>();
        if(root == null)return result;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(root);
        while(queue.Count > 0){
            int count = queue.Count;
            List<int> list = new List<int>();
            for(int i = 0;i<count;i++){
                Node tmp = queue.Dequeue();
                list.Add(tmp.val);
                if(tmp.children!=null){
                    foreach(var n in tmp.children){
                        queue.Enqueue(n);
                    }
                }
            }
            result.Add(list.ToList());
        }   
        return result;
    }

    //[1,3,2,4,3]
    public IList<int> FindDisappearedNumbers(int[] nums) {

    }

    public void Rotate (int[][] matrix) {
        int row = matrix.Length;
        int col = matrix[0].Length;
        Queue<int> queue = new Queue<int>();
        int left = 0;
        int right = col - 1;
        int up = 0;
        int down = row - 1;
        while(left < right && up < down){
            for(int i = left;i<=right;i++){
                queue.Enqueue(matrix[up][i]);
            }
            for(int i = up;i<=down;i++){
                queue.Enqueue(matrix[i][right]);
                matrix[i][right] = queue.Dequeue();
            }
            
            for(int i = right;i>=left;i--){
                queue.Enqueue(matrix[down][i]);
                matrix[down][i] = queue.Dequeue();
            }
            for(int i = down;i>= up;i--){
                queue.Enqueue(matrix[i][left]);
                matrix[i][left] = queue.Dequeue();
            }
            for(int i = left;i<=right;i++){
                matrix[up][i] = queue.Dequeue();
            }
            queue.Clear();
            left++;
            right--;
            up++;
            down--;
        }
    }

    public int CalculateWater(int[] height){
        int len = height.Length;
        if(len == 0)return 0;
        int[] leftMax = new int[len];
        int[] rightMax = new int[len];
        leftMax[0] = height[0];
        for(int i = 1;i<len;i++){
            leftMax[i] = Math.Max(leftMax[i-1],height[i]);
        }
        rightMax[len-1] = height[len-1];
        for(int i = len-1;i>=0;i--){
            rightMax[i] = Math.Max(rightMax[i+1],height[i]);
        }
        int result = 0;
        for(int i = 0;i<len;i++){
            result += Math.Min(rightMax[i],leftMax[i]) - height[i];
        }
        return result;
    }

    public TreeNode BuildTree (int[] preorder, int[] inorder) {
        if(preorder.Length == 0 || inorder.Length == 0)return null;
        TreeNode root = new TreeNode(preorder[0]);
        Stack<TreeNode> stack = new Stack<TreeNode>();
        stack.Push(root);
        int inorderIndex = 0;
        for(int i = 1;i<preorder.Length;i++){
            int val = preorder[i];
            TreeNode node = stack.Peek();
            if(node.val != inorder[inorderIndex]){
                node.left = new TreeNode(val);
                stack.Push(node.left);
            }else{
                while(stack.Count > 0 && stack.Peek().val == inorderIndex[inorderIndex]){
                    inorderIndex++;
                    node = stack.Pop();
                }
                node.right = new TreeNode(val);
                stack.Push(node.right);
            }
        }
        return root;
    }

    public void Solve(char[][] board)
    {
        int row = board.Length;
        if(row == 0)return;
        int col = board[0].Length;
        for(int i = 0;i<col;i++){
            DFS(board,0,i,row,col);
            DFS(board,row-1,i,row,col);
        }
        for(int i = 1;i<row-1;i++){
            DFS(board,i,0,row,col);
            DFS(board,i,col-1,row,col);
        }
        for(int i = 0;i<row;i++){
            for(int j = 0;j<col;j++){
                if(board[i][j] == 'A'){
                    board[i][j] = 'O';
                }else if(board[i][j] == 'O'){
                    board[i][j] = 'X';
                }
            }
        }
    }

    private void DFS(char[][] board,int i,int j,int row,int col){
        if(i<0 || i>=row || j<0 || j >= col || board[i][j] != 'O')return;
        board[i][j] = 'A';
        DFS(board,i,j+1,row,col);
        DFS(board,i+1,j,row,col);
        DFS(board,i,j-1,row,col);
        DFS(board,i-1,j,row,col);
    }

        public int BulbSwitch(int n) {

    }

    public int[][] Merge (int[][] intervals) {
        List<int[]> result = new List<int[]> ();
        if(intervals.Length == 0)return result.ToArray();
        for(int i = 0;i<intervals.Length - 1;i++){
            var a = intervals[i];
            bool has = false;
            for(int j = i+1;j<intervals.Length;j++){
                var b = intervals[j];
                if(CanMerge(a,b)){
                    a = MergeFunc1 (a, b);
                    has = true;
                }
            }
            if(has == false){
                result.Add (a);
            }
        }
        return result.ToArray();
    }

    private bool CanMerge (int[] a, int[] b) {
        if (a == null || b == null) {
            return true;
        } else if (a[1] < b[0]) {
            return false;
        } else if (b[1] < a[0]) {
            return false;
        }
        return true;
    }

    private int[] MergeFunc1 (int[] a, int[] b) {
        int min = Math.Min (a[0], b[0]);
        int max = Math.Max (a[1], b[1]);
        return new int[2] { min, max };
    }

}
