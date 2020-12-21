using System.Collections.Generic;

public struct ListNode{
    public int val;
    public ListNode next;
}

public struct TreeNode{
    public int val;
    public TreeNode left;
    public TreeNode right;
}


public class LocalRank{


    public bool Find(int target,int[][] array){
        int row = array.Length;
        if(row == 0)return false;
        int col = array[0].Length;
        return searchRect(0,0,col-1,row-1);
    }


    public void JumpFloor2(int n){
        if(n == 0)return 0;
        if(n == 1)return 1;
        return Math.Pow(2,n-1);
    }


    public int MaxTreeDepth(TreeNode root){
        if(root == null)return 0;
        return Math.Max(MaxTreeDepth(root.right),MaxTreeNode(root.left))+1;
    }

    public bool IsBlanceTree(TreeNode root){
        if(root == null)return true;
        int left = getTreeHight(root.left);
        int right = getTreeHight(root.right);
        if(Math.Abs(left-right)>1)return false;
        return IsBlanceTree(root.left) && IsBlanceTree(root.right);
    }

    public ListNode FindFirstCommonNode(ListNode pHead1,ListNode pHead2){
        if(pHead1 == null || pHead2 == null)return null;
        ListNode p1 = pHead1;
        ListNode p2 = pHead2;
        int a = 0;
        int b = 0;
        while(p1!=null){
            p1 = p1.next;
            a++;
        }
        while(p2!=null){
            p2= p2.next;
            b++;
        }
        p1 = (a>b)?pHead1:pHead2;
        p2 = (a>b)?pHead2:pHead1;
        int dis = Math.Abs(a-b);
        while(dis>0){
            p1=p1.next;
            dis--;
        }
        while(p1!=null){
            if(p1 == p2){
                return p1;
            }
            p1=p1.next;
            p2=p2.next;
        }
        return null;
    }

    public int FindFirstOneChar(string s){
        Dictionary<char,int> dic = new Dictionary<char,int>();
        for(int i = 0;i<s.Length;i++){
            if(dic.ContainsKey(s[i])){
                dic[s[i]] = -1;
            }else{
                dic.Add(s[i],i);
            }
        }
        foreach(var item in dic){
            if(item.Value != -1){
                return item.Key;
            }
        }
        return -1;
    }














    private int getTreeHight(TreeNode root){
        if(root == null)return 0;
        return Math.Max(getTreeHight(root.left),getTreeHight(root.right))+1;
    }


















    private bool searchRect(int left,int up,int right,int down,int target,int[][] array){
        if(left > right || up < down)return false;
        if(target < array[up][left])return false;
        if(target > array[down][right])return false;
        int mid = left + (right - left)/2;
        int row = up;
        while(row<=down && array[row][mid] < target){
            if(target == array[row][mid]){
                return true;
            }
            row++;
        }
        return searchRect(left,row,mid-1,down) || searchRect(mid+1,up,right,row-1);
    }
}