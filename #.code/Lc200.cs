using System.Collections.Generic;

namespace LC
{
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

    public class Lc200
    {
        // [["A","B","C","E"],["S","F","E","S"],["A","D","E","E"]]
        // "ABCESEEEFS"
        public bool Exist(char[][] board, string word)
        {
            int row = board.Length;
            if (row == 0) return false;
            int col = board[0].Length;
            if (word.Length > row * col) return false;
            bool[,] flag = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (DFS_Search(board, i, j, word, 0, row, col, flag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DFS_Search(char[][] board, int i, int j, string word, int index, int row, int col, bool[,] flag)
        {
            if (i < 0 || i >= row || j < 0 || j >= col)
            {
                return false;
            }
            if (board[i][j] != word[index] || flag[i, j])
            {
                return false;
            }
            index++;
            flag[i, j] = true;
            if (index >= word.Length) return true;
            int[][] dir = new int[4][];
            dir[0] = new int[2] { 0, 1 };
            dir[1] = new int[2] { 0, -1 };
            dir[2] = new int[2] { 1, 0 };
            dir[3] = new int[2] { -1, 0 };
            for (int n = 0; n < 4; n++)
            {
                var arr = dir[n];
                int a = i + arr[0];
                int b = j + arr[1];
                if (a >= 0 && a < row && b >= 0 && b < col)
                {
                    if (flag[a, b] == false && DFS_Search(board, a, b, word, index, row, col, flag))
                    {
                        return true;
                    }
                }
            }
            flag[i, j] = false;
            return false;
        }

        public int FindNums(int[] nums)
        {
            int len = nums.Length;
            int result = (len - 1 + 1) * (len - 1) / 2;
            foreach (var item in nums)
            {
                result -= item;
            }
            return -result;
        }

        public void GameOfLife(int[][] board) {
            int row = board.Length;
            if(row == 0)return;
            int col = board[0].Length;
            int[,] states = new int[row,col];
            for(int i = 0;i<row;i++){
                for(int j = 0;j<col;j++){
                    CheckGame(board,i,j,row,col);
                }
            }

            for(int i = 0;i<row;i++){
                for(int j = 0;j<col;j++){
                    if(states[i,j] == 1&&board[i][j] == 0){
                        board[i][j] = 1;
                    }else if(states[i,j] == -1&& board[i][j] == 1){
                        board[i][j] = 0;
                    }
                }
            }
        }

        private void CheckGame(int[][] board,int i,int j,int row,int col){
            int a = GetItemResult(board,i-1,j,row,col);
            int b = GetItemResult(board,i-1,j+1,row,col);
            int c = GetItemResult(board,i,j+1,row,col);
            int d = GetItemResult(board,i+1,j+1,row,col);
            int e = GetItemResult(board,i+1,j,row,col);
            int f = GetItemResult(board,i+1,j-1,row,col);
            int g = GetItemResult(board,i,j-1,row,col);
            int h = GetItemResult(board,i-1,j-1,row,col);
            int val = a+b+c+d+e+f+g+h;
            if(board[i][j] == 1){
                if(val < 2){
                    states[i,j] = -1;
                }else if(val>=2 && val <=3){
                    states[i,j] = 1;
                }else{
                    states[i,j] = -1;
                }
            }else{
                states[i,j] = 1;
            }
        }

        private int GetItemResult(int[][] board,int i,int j,int row,int col){
            if(i<0||i>=row||j<0||j>=col)return 0;
            return board[i][j];
        }

        public bool IsIsomorphic(string s, string t) {
            if(s.Length != t.Length)return false;
            if(s.Length == 0 && t.Length == 0)return true;
            Dictionary<char,char> dic = new Dictionary<char,char>();
            int i = 0;
            while(i < s.Length){
                char a = s[i];
                char b = t[i];
                if(dic.ContainsKey(a) == false && dic.ContainsKey(b) == false){
                    dic.Add(a,b);
                    dic.Add(b,a);
                }else if(dic.ContainsKey(a) && dic.ContainsKey(b)){
                    if(dic[dic[a]] != a || dic[dic[b]]!=b){
                        return false;
                    }
                }else{
                    return false;
                }
                i++;
            }
            return true;
        }

        public int CoinChange(int[] coins, int amount) {
            int step = 0;
            if(amount<=0)return step;
            Array.Sort(coins);
            int res = 0;
            CalCoin(coins,amount,step,res);
            return step;
        }

        public void CalCoin(int[] coins,int amount,int step,int res){
            if(amount < 0)return;
            if(amount == 0)return;
            for(int i = coins.Length-1;i>=0;i--){
                if(amount - coins[i] < 0){
                    continue;
                }else if(amount - coins[i] == 0){
                    step++;
                    res = step;
                    return;
                }else{
                    step++;
                    amount-=coins[i];
                    CalCoin(coins,amount,step,res);
                    step--;
                    amount+=coins[i];
                }
            }
        }


        public int[] CheckArr(int[] nums){
            int len = nums.Length;
            for(int i = 0;i<len;i++){
                int val = nums[i];
                if(val < 0)val *=-1;
                if(nums[val - 1]>0){
                    nums[val - 1] *= -1;
                }
            }
            List<int> list = new List<int>();
            for(int i = 0;i<len;i++){
                if(nums[i]>0){
                    list.Add(i+1);
                }
            }
            return list.ToArray();
        }

        public int IslandPerimeter (int[][] grid) {
            int result = 0;
            int row = grid.Length;
            int col = row > 0 ? grid[0].Length : 0;
            int res = 0;
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    if (grid[i][j] == 1) {
                    //判断四周
                    }
                }
            }
        }

        private int GetRes(int[][] grid,int i,int j,int row,int col){
            if(i<0||i>=row||j<0||j>=col)return 0;
        }


        public int PathSum(TreeNode root, int sum) {
            if(root == null)return 0;
            int result = 0;
            SearchPathSum(root,sum,result);
            return result;
        }

        private void SearchPathSum(TreeNode root,int sum,int result){
            if(root == null)return;
            if(sum < 0)return;
            if(sum == 0){
                result++;
                return;
            }
            sum -= root.val;
            if(root.left!=null){
                SearchPathSum(root.left,sum,result);
            }
            if(root.right!=null){
                SearchPathSum(root.right,sum,result);
            }
        }

        public IList<int> Preorder(Node root) {
            List<int> list = new List<int>();
            if(root == null)return list;
            Stack<Node> stack = new Stack<Node>();
            while(stack.Count > 0 || root!=null){
                while(root!=null){

                }
            }
        }

        public TreeNode MergeTrees(TreeNode t1, TreeNode t2) {
            if(t1 == null)return t2;
            if(t2 == null)return t1;
            while(t1 != null || t2 != null){
                
            }
            return t1;
        }

        public int WidthOfBinaryTree(TreeNode root) {
            if(root == null)return 0;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while(queue.Count>0){
                int count = queue.Count;
                for(int i = 0;i<count;i++){
                    TreeNode tmp = queue.Dequeue();
                    if(tmp.left!=null)queue.Enqueue(tmp.left);
                    if(tmp.right!=null)queue.Enqueue(tmp.right);
                }
            }
        }

        public string ReverseVowels(string s) {
        int left = 0;
        int right = s.Length - 1;
        char[] cs = s.ToCharArray();
        while(left < right){
            while(left < right || CheckAeiou(s[left]) == false){
                left++;
            }
            while(left < right || CheckAeiou(s[right]) == false){
                right--;
            }
            if(left < right){
                char c = cs[left];
                cs[left] = cs[right];
                cs[right] = c;
            }
        }
        return new string(cs);
    }

    private bool CheckAeiou(char c){
        if(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u'){
            return true;
        }else if(c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U'){
            return true;
        }
        return false;
    }

    public bool IsSubsequence(string s, string t) {
        int m = s.Length,n=t.Length;

        if(m > n)return false;
        
        int i = 0;
        int j = 0;
        while (i < m && j < n)
        {
            while (s[i] == t[j])
            {
                i++;
            }
            j++;
        }
        return i == m;
    }
    }
}