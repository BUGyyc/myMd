
/*
 * @Author: delevin.ying 
 * @Date: 2019-03-29 14:53:33 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-05-10 11:06:27
 */
import java.util.Scanner;

/**
 * 极大极小值得剪枝算法
 */
public class MaxMin {
    private static int[] arr = new int[10];
    private static int t = 0;
    private static int INF = Integer.MAX_VALUE;
    private static String ME = "A";
    private static String AI = "B";

    public static void main(String[] args) {
        newGame();
        displayRound();
        Scanner sc = new Scanner(System.in);
        System.out.println("t->");
        t = sc.nextInt();
        // testInput();
        while (t > 0) {
            System.out.println("please choose your step!");
            int i = sc.nextInt();
            if (i < 1 || i > 9) {
                System.out.println("choose error !");
            } else {
                if (arr[i] != 0) {
                    System.out.println("choose error !");
                } else {
                    arr[i] = 1;
                    int choose = dfs(1);
                    System.out.println("dfs----" + choose);
                    arr[choose + 1] = 2;
                    displayRound();
                    t--;
                }
            }
        }
        sc.close();
        return;
    }

    // private static void testInput() {
    // arr[1] = 1;
    // arr[2] = 1;
    // arr[3] = 0;
    // arr[4] = 2;
    // arr[5] = 2;
    // arr[6] = 2;
    // arr[7] = 0;
    // arr[8] = 0;
    // arr[9] = 0;
    // }

    /**
     */
    private static void displayRound() {
        System.out.println("--------------------------------");
        for (int i = 0; i < 3; i++) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 3; j++) {
                String s = (arr[i * 3 + j + 1] == 0) ? "0" : arr[i * 3 + j + 1] == 1 ? ME : AI;
                sb.append(" " + s);
            }
            System.out.println(sb.toString());
        }
        System.out.println("--------------------------------");
    }

    /**
     */
    private static void newGame() {
        System.out.println("new game ------------");
        for (int i = 1; i <= 9; i++) {
            arr[i] = 0;
        }
    }

    /**
     * @param it
     * @return
     */
    private static int dfs(int it) {
        int chec = check();
        if (chec != -1) {//
            return chec;
        }
        int ans = it == 1 ? -INF : INF;
        for (int i = 1; i <= 9; ++i) {
            if (arr[i] > 0)//
                continue;
            if (it == 1) {//
                arr[i] = 1;
                ans = Math.max(ans, dfs(2));//
            } else {
                arr[i] = 2;
                ans = Math.min(ans, dfs(1));
            }
            arr[i] = 0;//
        }
        // System.out.println("ans== " + ans);//
        return ans;
    }

    /**
     * @return
     */
    private static int check() {
        int it = 0;
        for (int i = 1; i <= 3; i++) {
            if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && arr[i] > 0) {
                it = arr[i];
                break;
            }
            int k = 3 * (i - 1) + 1;
            if (arr[k] == arr[k + 1] && arr[k] == arr[k + 2] && arr[k] > 0) {
                it = arr[k];
                break;
            }
        }
        if (it < 0) {
            if (arr[1] == arr[5] && arr[1] == arr[9] && arr[1] > 0)
                it = arr[1];
            else if (arr[3] == arr[5] && arr[5] == arr[7] && arr[5] > 0)
                it = arr[5];
        }
        int cnt = 0;
        for (int i = 1; i <= 9; ++i)
            if (arr[i] == 0)
                cnt++;
        if (it == 0 && cnt == 0)
            return 0;
        if (it == 1)
            return cnt + 1;
        else if (it == 2)
            return -(cnt + 1);
        else
            return -1;
    }
}