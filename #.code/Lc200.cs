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
    }
}