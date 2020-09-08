public class leetcode840 {
    public static void main(String[] args) {
        System.out.println("leetcode840   !");
        int[][] test = {
             { 4, 3, 8, 4 }, 
             { 9, 5, 1, 9 }, 
             { 2, 7, 6, 2 } };
        int n = numMagicSquaresInside(test);
        System.out.println("n ===== " + n);
    }

    public static int numMagicSquaresInside(int[][] grid) {
        int row = grid.length;
        int col = grid[0].length;
        if (row < 3 || col < 3)
            return 0;
        int startX = 0;
        int startY = 0;
        int sum = 0;
        for (int i = startX; i < row - 2; i++) {
            for (int j = startY; j < col - 2; j++) {
                if (checkResult2(i, j, grid)) {
                    sum++;
                }
            }
        }
        return sum;
    }

    public static boolean checkResult(int x, int y, int[][] grid) {
        int compareSum = grid[x][y] + grid[x][y + 1] + grid[x][y + 2];
        for (int i = x; i < x + 3; i++) {
            int compareNum = 0;
            for (int j = y; j < y + 3; j++) {
                if (i == 0) {
                    System.out.println("item ===== " + grid[0][j]);
                }
                compareNum += grid[x][y];
            }
            if (compareNum != compareSum) {
                System.out.println(
                        "~~~~~~~~~~~~~~compareNum ===== return false " + compareNum + "  compareSum " + compareSum);
                return false;
            }
        }

        for (

                int j = y; j < y + 3; j++) {
            int compareNum = 0;
            for (int i = x; i < x + 3; i++) {
                compareNum += grid[x][y];
            }
            if (compareNum != compareSum) {
                System.out.println("=================compareNum ===== return false " + compareNum);
                return false;
            }
        }

        int a = grid[x][y] + grid[x + 1][y + 1] + grid[x + 2][y + 2];
        int b = grid[x][y + 2] + grid[x + 1][y + 1] + grid[x + 2][y];
        if (a != compareSum || b != compareSum) {
            System.out.println(
                    "********************compareNum ===== return false " + compareSum + "   " + a + " +  " + b);
            return false;
        }
        return true;
    }

    public static boolean checkResult2(int x, int y, int[][] grid) {
        int row = grid.length;
        int col = grid[0].length;
        if (x + 2 > row - 1)
            return false;
        if (y + 2 > col - 1)
            return false;
        int compareSum = grid[x][y] + grid[x][y + 1] + grid[x][y + 2];
        //
        for (int i = x; i < x + 3; i++) {
            int compareNum = 0;
            for (int j = y; j < y + 3; j++) {
                compareNum += grid[x][y];
            }
            if (compareNum != compareSum)
                return false;

        }
        //
        for (int j = y; j < y + 3; j++) {
            int compareNum = 0;
            for (int i = x; i < x + 3; i++) {
                compareNum += grid[x][y];
            }
            if (compareNum != compareSum)
                return false;
        }   
        //
        int a = grid[x][y] + grid[x + 1][y + 1] + grid[x + 2][y + 2];
        int b = grid[x][y + 2] + grid[x + 1][y + 1] + grid[x + 2][y];
        if (a != compareSum || b != compareSum)
            return false;

        return true;
    }
}