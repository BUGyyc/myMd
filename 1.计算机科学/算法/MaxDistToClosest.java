
/*
 * @Author: delevin.ying 
 * @Date: 2019-03-29 14:53:33 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2019-05-09 18:10:29
 */
import java.util.Scanner;

public class MaxDistToClosest {
    private static int[] arr = { 1, 0, 0, 0, 1, 0, 1 };

    public static void main(String[] args) {
        int[] newArr = new int[arr.length + 2];
        newArr[0] = 0;
        newArr[newArr.length - 1] = 0;
        for (int i = 0; i < arr.length; i++) {
            newArr[i+1] = arr[i];
        }
        int max = 0;
        int n = 0;
        for (int x : newArr) {
            if (x == 0) {
                n++;
            } else {
                // System.out.println("reset n ----- " + n+" --- max = "+max);
                max = Math.max(max, n);
                n = 0;
                // System.out.println("reset after n ----- " + n+" --- max = "+max);
            }
            System.out.println("reset  index   n    ----- " + n + "   --- max = " + max + "x  ======= " + x);
        }
        System.out.println("for  after n    ----- " + n + "   --- max = " + max);
        if (n > 0 && n >= max) {
            // max = Math.max(max, n);
            System.out.println("n   ----- " + n);
        } else {
            System.out.println("max -------" + ((max + 1) / 2));
        }
        return;
    }

}