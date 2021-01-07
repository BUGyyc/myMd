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
// "badc"
// "baba"

// "egg"
// "add"
// "paper"
// "title"
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
    }
}