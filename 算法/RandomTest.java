import java.util.HashMap;
import java.util.Map;
import java.util.Random;
import java.util.Scanner;
import java.util.Stack;

public class RandomTest {
    private static final int MAX_X = 14;
    private static final int MAX_Y = 40;
    private static String[][] arr = new String[MAX_X][MAX_Y];//
    // private static int t = 0;//
    private static Map<String, Integer> map = new HashMap<>();

    private static int[] glodNum = new int[4];

    public static void main(String[] args) {
        initArray();
        // printfArr();
        Stack<String> stack = configMap();
        randomLogic(stack);
        printfArr();
    }

    /**
     * random logic
     */
    public static void randomLogic(Stack<String> stack) {
        Random rand = null;
        while (!stack.empty()) {
            String type = stack.peek();
            // System.out.println("type ==== " + type);
            rand = new Random();
            int x = rand.nextInt(MAX_X);
            int y = rand.nextInt(MAX_Y);

            // TODO:
            while (checkPos(x, y, type) == false) {
                rand = new Random();
                x = rand.nextInt(MAX_X);
                y = rand.nextInt(MAX_Y);
            }
            stack.pop();
            int size = map.get(type);
            x -= (size + 1) / 2;
            y -= (size + 1) / 2;
            // System.out.println("size ==== " + size);
            for (int i = x; i < x + size; i++) {
                for (int j = y; j < y + size; j++) {
                    arr[i][j] = type;
                    System.out.println("ij ==== " + i + " - " + j);
                }
            }
        }
    }

    public static boolean checkPos(int x, int y, String type) {
        int size = map.get(type);
        if (x - size < 0 || x + size > MAX_X - 1 || y - size < 0 || y + size > MAX_Y - 1) {
            return false;
        }
        x -= (size + 1) / 2;
        y -= (size + 1) / 2;
        for (int i = x; i < x + size; i++) {
            for (int j = y; j < y + size; j++) {
                if (arr[i][j].equals("0") == false) {
                    return false;
                }
            }
        }
        return true;
    }

    /**
     * glod size and num
     */
    public static Stack configMap() {
        // glod size
        map.put("A", 5);
        map.put("B", 3);
        map.put("C", 1);
        map.put("D", 1);
        int a = glodNum[0] = 2;
        int b = glodNum[1] = 7;
        int c = glodNum[2] = 6;
        int d = glodNum[3] = 12;
        Stack<String> stack = new Stack<String>();
        for (int i = 0; i < a; i++) {
            stack.push("A");
        }
        for (int i = 0; i < b; i++) {
            stack.push("B");
        }
        for (int i = 0; i < c; i++) {
            stack.push("C");
        }
        for (int i = 0; i < d; i++) {
            stack.push("D");
        }
        return stack;
    }

    public static void initArray() {
        for (int i = 0; i < arr.length; i++) {
            String[] temp = arr[i];
            for (int j = 0; j < temp.length; j++) {
                arr[i][j] = "0";
            }
        }
    }

    public static void printfArr() {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < arr.length; i++) {
            String[] temp = arr[i];
            for (int j = 0; j < temp.length; j++) {
                // arr[i][j] = 0;
                sb.append(" " + arr[i][j]+" ");
            }
            sb.append("\n");
            sb.append("\n");
        }
        System.out.println("result-");
        System.out.println(sb.toString());
    }
}