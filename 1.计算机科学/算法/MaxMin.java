
/*
 * @Author: delevin.ying 
 * @Date: 2019-03-29 14:53:33 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-05-10 11:06:27
 */
import java.util.Scanner;

/**
 * 极大极小值
 */
public class MaxMin {
    private static int[] arr = new int[10];
    private static int t = 0;
    private static int INF = Integer.MAX_VALUE;
    private static String ME = "A";
    private static String AI = "B";
    private static int bestMove = 0;
    // private static int value = 0;
    // private static int ManBestMove = 0;
    // private static int ManValue = 0;
    private static int CompWin = 2;
    private static int Draw = 1;
    private static int HumanWin = 0;

    public static void main(String[] args) {
        newGame();
        displayRound();
        Scanner sc = new Scanner(System.in);
        // System.out.println("t->");
        t = 5;// man maxStep num 5
        // testInput();
        while (t > 0) {
            System.out.println("please choose your step!");
            int i = sc.nextInt();
            if (i < 1 || i > 9) {
                System.out.println("choose error !");
            } else {
                if (arr[i] != 0) {//
                    System.out.println("choose error !");
                } else {
                    arr[i] = 1;
                    AIMoveOneStep();
                    displayRound();
                    t--;
                }
            }
        }
        sc.close();
        return;
    }

    /**
     * AI choose one Step
     */
    private static void AIMoveOneStep() {
        int bestStep = getBestStep();
        System.out.println("AI choose Step -- " + bestStep);
        arr[bestStep] = 2;// choose
    }

    /**
     * find all result
     */
    private static int getBestStep() {
        int value = 0;
        findAIStep(value, HumanWin, CompWin);
        return bestMove;// best
    }

    /**
     * deep search
     */
    private static int findAIStep(int value, int alpha, int beta) {
        if (isFull())
            value = Draw;
        else if (checkAINearWin())
            value = CompWin;
        else {
            value = alpha;
            for (int i = 1; i <= 9 && value < beta; i++) {
                if (arr[i] == 0) {
                    arr[i] = 2;
                    int response = -1;
                    response = findManStep(response, alpha, beta);
                    arr[i] = 0;
                    if (response > value) {
                        value = response;
                        bestMove = i;
                    }
                }
            }
        }
        return value;
    }

    private static int findManStep(int value, int alpha, int beta) {
        if (isFull())
            value = Draw;
        else if (checkManNearWin())
            value = HumanWin;
        else {
            value = beta;
            for (int i = 1; i <= 9 && value > alpha; i++) {
                if (arr[i] == 0) {
                    arr[i] = 1;
                    int response = -1;
                    response = findAIStep(response, alpha, beta);
                    arr[i] = 0;
                    if (response < value) {
                        value = response;
                        bestMove = i;
                    }
                }
            }
        }
        return value;
    }

    /**
     * check no step can choose
     * 
     * @return
     */
    private static boolean isFull() {
        for (int i = 1; i <= 9; i++) {
            if (arr[i] == 0) {
                return false;
            }
        }
        return true;
    }

    /**
     * AI will win
     * 
     * @return
     */
    private static boolean checkAINearWin() {
        for (int i = 1; i <= 9; i++) {
            if (arr[i] == 0) {
                arr[i] = 2;
                boolean win = canWin(2);
                arr[i] = 0;
                if (win) {
                    bestMove = i;// best
                    return true;
                }
            }
        }
        return false;
    }

    /**
     * Man will win
     * 
     * @return
     */
    private static boolean checkManNearWin() {
        for (int i = 1; i <= 9; i++) {
            if (arr[i] == 0) {
                arr[i] = 1;
                boolean win = canWin(1);
                arr[i] = 0;
                if (win) {
                    bestMove = i;// best
                    return true;
                }
            }
        }
        return false;
    }

    private static boolean canWin(int type) {
        for (int i = 1; i <= 3; i++) {
            if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && arr[i] == type) {
                return true;
            }
            int k = 3 * (i - 1) + 1;
            if (arr[k] == arr[k + 1] && arr[k] == arr[k + 2] && arr[k] == type) {
                return true;
            }
        }
        if (arr[1] == arr[5] && arr[1] == arr[9] && arr[1] == type)
            return true;
        else if (arr[3] == arr[5] && arr[5] == arr[7] && arr[5] == type)
            return true;
        return false;
    }

    private static void extracted() {
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