import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class leetcodePro {
    public static void main(String[] args) {
        System.out.println("leetcodePro   !");
        // int[] arr1 = { 2, 3, 1, 3, 2, 4, 6, 7, 9, 2, 19 };
        // int[] arr2 = { 2, 1, 4, 3, 9, 6 };
        int[][] grid = { { 1, 2, 1, 1, 2, 1, 1 } };
        int cost = orangesRotting994(grid);
        System.out.println("cost ===== " + cost);

    }

    private static int row = 0;
    private static int col = 0;
    private static int[][] Grid;

    public static int orangesRotting994(int[][] grid) {
        int cost = 0;
        row = grid.length;
        int[] arr = grid[0];
        col = arr.length;
        System.out.println("orangesRotting994 " + row + " *  " + col);
        Grid = new int[row][col];
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                Grid[i][j] = grid[i][j];
            }
        }
        return runTime(grid, cost);
    }

    public static int runTime(int[][] grid, int cost) {

        boolean haveBeBad = false;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                if (grid[i][j] == 2) {
                    System.out.println("item value= 2 where " + i + " * " + j + "  =  " + grid[i][j]);
                    boolean temp = setOrangeArr(i, j);
                    haveBeBad = haveBeBad || temp;
                }
            }
        }
        if (haveBeBad == false) {
            boolean haveFresh = false;
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    if (grid[i][j] == 1) {
                        haveFresh = true;
                        break;
                    }
                }
            }
            return (haveFresh) ? -1 : cost;
        }

        // grid = Grid;
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                grid[i][j] = Grid[i][j];
            }
        }

        printAll(grid);
        cost++;
        return runTime(grid, cost);
    }

    public static void printAll(int[][] grid) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                sb.append(" " + grid[i][j] + " ");
            }
            sb.append("\n");
        }
        System.out.println(sb.toString());
        // System.out.println("GRID______________________");
        // sb = new StringBuilder();
        // for (int i = 0; i < row; i++) {
        // for (int j = 0; j < col; j++) {
        // sb.append(" " + Grid[i][j] + " ");
        // }
        // sb.append("\n");
        // }
        // System.out.println(sb.toString());
    }

    public static boolean setOrangeArr(int i, int j) {
        boolean canBeBad1 = false;
        boolean canBeBad2 = false;
        boolean canBeBad3 = false;
        boolean canBeBad4 = false;
        System.out.println("run excute " + i + " *  " + j);
        if (i - 1 >= 0) {
            if (Grid[i - 1][j] == 1) {
                Grid[i - 1][j] = 2;
                canBeBad1 = true;
            }
        }
        if (i + 1 < row) {
            if (Grid[i + 1][j] == 1) {
                Grid[i + 1][j] = 2;
                canBeBad2 = true;
            }
        }

        if (j - 1 >= 0) {
            if (Grid[i][j - 1] == 1) {
                Grid[i][j - 1] = 2;
                canBeBad3 = true;
            }
        }
        if (j + 1 < col) {
            if (Grid[i][j + 1] == 1) {
                Grid[i][j + 1] = 2;
                canBeBad4 = true;
            }
        }
        boolean have = (canBeBad1 || canBeBad2 || canBeBad3 || canBeBad4);
        return have;
    }

    public static int[] relativeSortArray1122(int[] arr1, int[] arr2) {
        int len1 = arr1.length;
        int len2 = arr2.length;
        int[] res = new int[len1];
        int i = 0;
        int j = 0;
        for (int m : arr2) {
            for (int n : arr1) {
                if (n == m) {
                    res[i++] = m;
                }
            }
        }
        int[] temp = new int[len1 - i];
        for (int n : arr1) {
            boolean isEqual = false;
            for (int m : arr2) {
                if (n == m) {
                    isEqual = true;
                    break;
                }
            }
            if (isEqual == false) {
                temp[j++] = n;
            }
        }
        Arrays.sort(temp);
        for (int x : temp) {
            res[i++] = x;
        }
        return res;
    }
}