/*
 * @Author: delevin.ying 
 * @Date: 2019-03-29 14:53:33 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-03-29 15:02:44
 */
import java.util.Scanner;

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
                    System.out.println("dfs----" + dfs(1));
                    displayRound();
                    t--;
                }
            }
        }
        sc.close();
        return;
    }

    /**
     * 展示当局结果
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
     * 初始化
     */
    private static void newGame() {
        System.out.println("new game ------------");
        for (int i = 1; i <= 9; i++) {
            arr[i] = 0;
        }
    }

    /**
     * 深度遍历
     * @param it
     * @return
     */
    private static int dfs(int it) {
        int chec = check();
        if (chec != -1) {//如果等于-1说明打完了，出结果了
            // System.out.println("chec== " + chec);
            return chec;
        }
        //下面是没打完去尝试下棋
        int ans = it == 1 ? -INF : INF;
        for (int i = 1; i <= 9; ++i) {
            if (arr[i] > 0)//这一步已经下了
                continue;
            if (it == 1) {//如果是类型1
                arr[i] = 1;
                ans = Math.max(ans, dfs(2));//取最大
            } else {
                arr[i] = 2;
                ans = Math.min(ans, dfs(1));
            }
            arr[i] = 0;//最后重置回去
        }
        System.out.println("ans== " + ans);//最终取得值
        return ans;
    }

    /**
     * 检测输赢
     * @return
     */
    private static int check() {
        int it = 0;
        for (int i = 1; i <= 3; i++) {
            if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && arr[i] > 0) {//有同列
                it = arr[i];
                break;
            }
            int k = 3 * (i - 1) + 1;
            if (arr[k] == arr[k + 1] && arr[k] == arr[k + 2] && arr[k] > 0) {//有同行
                it = arr[k];
                break;
            }
        }
        if (it < 0) {
            if (arr[1] == arr[5] && arr[1] == arr[9] && arr[1] > 0)//斜
                it = arr[1];
            else if (arr[3] == arr[5] && arr[5] == arr[7] && arr[5] > 0)//斜
                it = arr[5];
        }
        //it如果不等于0 ，说明有人赢了
        int cnt = 0;//cnt统计剩下可以走的步数
        for (int i = 1; i <= 9; ++i)
            if (arr[i] == 0)
                cnt++;
        if (it == 0 && cnt == 0)//如果没有步子可以走而且没人赢，说明和棋了
            return 0;
        if (it == 1)//如果类型1赢了，
            return cnt + 1;
        else if (it == 2)//如果类型2赢了
            return -(cnt + 1);
        else
            return -1;
    }
}