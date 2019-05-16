import java.util.ArrayList;
import java.util.Scanner;

public class ClearUp {
    private static int[][] arr = new int[9][9];

    public static void main(String[] args) {
        newGame();
        displayRound();
        checkAll();
        return;
    }

    private static void checkAll() {
        ArrayList<Integer> boomList = new ArrayList<>();
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                if (isBoom(arr[i][j], i, j, Directions.TOP) || isBoom(arr[i][j], i, j, Directions.BOTTOM)
                        || isBoom(arr[i][j], i, j, Directions.LEFT) || isBoom(arr[i][j], i, j, Directions.RIGHT)) {
                    System.out.println("point ---- i " + i + " j " + j + "  will boom");
                    if (!boomList.contains(i * 10 + j)) {
                        boomList.add(i * 10 + j); // save
                    }
                }
            }
        }
        System.out.println("check over~~~~~~~~~~~~~~~~~~");
    }

    private static boolean isBoom(int type, int x, int y, Directions d) {
        if (d == Directions.TOP) {
            if (x + 2 <= 8) {
                if (type == arr[x + 1][y] && type == arr[x + 2][y]) {
                    return true;
                }
            } else { // 超过索引
                return false;
            }
        } else if (d == Directions.BOTTOM) {
            if (x - 2 >= 0) {
                if (type == arr[x - 1][y] && type == arr[x - 2][y]) {
                    return true;
                }
            } else {
                return false;
            }
        } else if (d == Directions.LEFT) {
            if (y - 2 >= 0) {
                if (type == arr[x][y - 1] && type == arr[x][y - 2]) {
                    return true;
                }
            } else {
                return false;
            }
        } else if (d == Directions.LEFT) {
            if (y + 2 <= 8) {
                if (type == arr[x][y + 1] && type == arr[x][y + 2]) {
                    return true;
                }
            } else {
                return false;
            }
        }
        return false;
    }

    /**
    */
    private static void displayRound() {
        System.out.println("--------------------------------");
        for (int i = 0; i < 9; i++) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 9; j++) {
                String s = "" + arr[i][j];
                sb.append("" + s);
            }
            System.out.println(sb.toString());
        }
        System.out.println("--------------------------------");
    }

    /**
    */
    private static void newGame() {
        System.out.println("new game ------------");
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                arr[i][j] = (int) (Math.random() * 8) + 1;
            }

        }
    }

    /**
     * 枚举四个方向
     */
    enum Directions {
        TOP, BOTTOM, LEFT, RIGHT
    }
}
