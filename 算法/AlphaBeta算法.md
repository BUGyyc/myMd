九宫格游戏相对简单，因为步数有限，玩家和AI两者的总步数是9
而象棋的模式不一样，象棋的走法多样，步数在设定上是无穷
对于AI上的设定来说，AI未必当前步数需要选择最大收益步数，因为当前最大收益不一定绝对下一步是否最大收益，所以这意味着，向下搜索深度越大，意味着收益的计算才是最准确的

---

AI的行为是在列举有限种情况，在有限种情况中选择当前有利或者是符合长远利益（为什么这么说？长远利益这种情况是存在的，因为当前利益最大化也无法保证长期利益最大化，而当前非最优解，也可能是最符合长远利益的解，这种情况在象棋中就经常会发生）


搜索深度越深，考虑的步子就越远，判断收益的精度将更准确

井字棋是一个很简单的游戏，我们可以直接搜索到最终结果，再从下往上筛选，选择执行步子

```
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
                    if (response > value) {//比较值，进行剪枝
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
                    if (response < value) {//比较值，进行剪枝
                        value = response;
                        bestMove = i;
                    }
                }
            }
        }
        return value;
    }
```
