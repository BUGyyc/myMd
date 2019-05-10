#include <stdio.h>
#include "stdlib.h"
#include "time.h"
#include <ctype.h>
#include <conio.h>
#define MAN 1
#define COM -1
#define MAN_WIN 100
#define COM_WIN -100
#define DRAW 0 //平局
#define NONE 0
#define POSITION 9
#define STEP 9
#define random(x) (rand() % (x)) //产生随机数

int board[10]; //棋盘 初始默认值为0
int win_place_sum;
int angle[4] = {1, 3, 7, 9};
int edge[4] = {2, 4, 6, 8};

//是否赢了
int isWin(int player)
{
    if (board[1] == player && board[1] == board[2] && board[2] == board[3])
        return (player);
    if (board[4] == player && board[4] == board[5] && board[5] == board[6])
        return (player);
    if (board[7] == player && board[7] == board[8] && board[8] == board[9])
        return (player);
    if (board[1] == player && board[1] == board[4] && board[4] == board[7])
        return (player);
    if (board[2] == player && board[2] == board[5] && board[5] == board[8])
        return (player);
    if (board[3] == player && board[3] == board[6] && board[6] == board[9])
        return (player);
    if (board[1] == player && board[1] == board[5] && board[5] == board[9])
        return (player);
    if (board[3] == player && board[3] == board[5] && board[5] == board[7])
        return (player);
    //没人赢返回0
    return NONE;
}
int isAngle(int position)
{
    if (position == 1 || position == 3 || position == 7 || position == 9)
        return position;
    return 0;
}

int isEdge(int position)
{
    if (position == 2 || position == 4 || position == 6 || position == 8)
        return position;
    return 0;
}

//两子成线，返回一个必须要下的位置
int checkWinning(int player)
{
    int i;
    int win_place = 10;
    win_place_sum = 0;

    for (i = 1; i < 10; i++)
    {
        if (board[i] == NONE)
        {
            board[i] = player; //先假设该处有子进行判断
            if (isWin(player) == player)
            {
                board[i] = 0;
                win_place = i;
                win_place_sum++;
            }
            board[i] = 0;
        }
    }
    return win_place; //返回下棋的位置
}

//电脑哥们进行下棋
int com_play(int step, int lastPosition)
{
    int i;

    //随机数
    int ranNumber;

    //设置随机数种子
    srand((unsigned int)time(NULL));

    //第一步电脑走的位置
    int lastComPosition = 0;

    //能赢的地方
    int win_place = 10;

    int max_win_times = 0;
    //如果第一步，那么电脑先占角
    if ((step == 1) && (lastPosition == 0))
    {
        ranNumber = random(4);
        board[angle[ranNumber]] = COM;
        return angle[ranNumber];
    }
    else if (step == 3)
    {

        //第二步玩家走角
        if (isAngle(lastPosition) > 0)
        {
            for (i = 0; i < 4; i++)
            {
                if (board[angle[i]] == COM)
                {
                    lastComPosition = angle[i];
                    // printf("第一步电脑走的是%d", lastComPosition);
                    break;
                }
            }
            //如果走的是靠近边的角,就占对角
            if ((lastPosition + lastComPosition) != 10)
            {
                if (board[10 - lastComPosition] == NONE)
                {
                    board[10 - lastComPosition] = COM;
                    return 10 - lastComPosition;
                }
                else
                {
                    printf("err——1");
                    return 0;
                }
            }
            //如果走的是对角,那么就占其他的角
            else
            {
                while (1) //true
                {
                    ranNumber = random(4);
                    if (board[angle[ranNumber]] == NONE)
                    {
                        board[angle[ranNumber]] = COM;
                        return angle[ranNumber];
                    }
                }
            }
        }

        //第二步玩家走的是边
        else if (isEdge(lastPosition) > 0)
        {
            if (board[5] == NONE)
            {
                board[5] = COM;
                return 5;
            }
            else
            {
                printf("5 be locked ...");
                return 0;
            }
        }
        //如果走的是正中间 ，那么电脑走对角
        else if (lastPosition == 5)
        {
            for (i = 0; i < 4; i++)
            {
                if (board[angle[i]] == COM)
                {
                    lastComPosition = angle[i];
                    // printf("第一步电脑走的是%d", lastComPosition);
                    break;
                }
            }
            if (board[10 - lastComPosition] == NONE)
            {
                board[10 - lastComPosition] = COM;
                return 10 - lastComPosition;
            }
            else
            {
                printf("err——2");
                return 0;
            }
        }
    }

    else if (step == 5)
    {
        win_place = checkWinning(COM); //电脑两子成线
        if (win_place < 10)
        {
            board[win_place] = COM;
            return win_place;
        }

        win_place = checkWinning(MAN); //玩家的棋两子成线
        if (win_place < 10)
        {
            board[win_place] = COM;
            return win_place;
        }
        //如果没有能赢的地方,则占角
        else
        {
            for (i = 0; i < 4; i++)
            {
                if (board[angle[i]] == NONE)
                {
                    board[angle[i]] = COM;

                    if (checkWinning(COM) < 10 && max_win_times < win_place_sum)
                    {
                        max_win_times = win_place_sum; //能赢的次数
                        win_place = angle[i];
                    }

                    board[angle[i]] = NONE;
                }
            }

            board[win_place] = COM;
        }
    }
    else if (step == 2)
    {
        if (board[5] == NONE)
        {
            board[5] = COM;
            return 5;
        }

        else
            while (1) //true
            {
                ranNumber = random(4);
                if (board[angle[ranNumber]] == NONE)
                {
                    board[angle[ranNumber]] = COM;
                    return angle[ranNumber];
                }
            }
    }

    else
    {
        win_place = checkWinning(COM); //电脑两子成线
        if (win_place < 10)
        {
            board[win_place] = COM;
            return win_place;
        }

        win_place = checkWinning(MAN); //玩家的棋两子成线
        if (win_place < 10)
        {
            board[win_place] = COM;
            return win_place;
        }

        for (i = 1; i <= 9; i++)
        {
            if (board[i] == NONE)
            {
                board[i] = COM;
                printf("last step？？");
                return i;
            }
        }
    }
    printf("COM-error");
    return 10;
}

//玩家进行下棋
int person_play()
{
    int row;
    int col;
    int index;

    do
    {
        printf("It's your turn!!!  please input 如  x y  ");
        scanf("%d", &row);
        scanf("%d", &col);
        index = (row - 1) * 3 + col;

        if (board[index] == NONE)
        {
            board[index] = MAN;
            return index;
        }
        printf("input err ,reset  input:");

    } while (1); //true

    printf("MAN-error");
    return 10;
}

//画棋盘
void display()
{
    char board_dis[10] = {""};
    int i;
    for (i = 1; i < 10; i++)
    {
        if (board[i] == COM)
        {
            board_dis[i] = 'X';
        }
        if (board[i] == MAN)
        {
            board_dis[i] = 'O';
        }
    }
    for (i = 0; i < 10; i++)
    {
        printf("-");
    }
    printf("\n|");
    for (i = 1; i <= 3; i++)
    {
        printf("%c |", board_dis[i]);
    }
    printf("\n");
    for (i = 0; i < 10; i++)
    {
        printf("-");
    }
    printf("\n|");
    for (i = 4; i <= 6; i++)
    {
        printf("%c |", board_dis[i]);
    }
    printf("\n");
    for (i = 0; i < 10; i++)
    {
        printf("-");
    }
    printf("\n|");
    for (i = 7; i <= 9; i++)
    {
        printf("%c |", board_dis[i]);
    }
    printf("\n");
    for (i = 0; i < 10; i++)
    {
        printf("-");
    }
    printf("\n");
}

void main()
{
    char c;
    int i;
    int step = 1;
    int lastPosition = 0;

    for (i = 0; i < 30; i++)
        printf("*");
    printf("\n*hello,welcome to this game!!*\n");
    for (i = 0; i < 30; i++)
        printf("*");
    printf("\nDo you want to play first? y -yes  , n-no");
    for (c = getche(); c != 'Y' && c != 'y' && c != 'N' && c != 'n'; c = getche())
        ;
    if (c == 'Y' || c == 'y')
    {
        display();
        for (step = 1; step <= STEP;)
        {
            lastPosition = person_play();
            display();
            if (MAN == isWin(MAN))
            {
                printf("you win ");
                break;
            }
            step++;
            if (step == 10)
            {
                break;
            }
            lastPosition = com_play(step, lastPosition);
            display();

            if (COM == isWin(COM))
            {
                printf("you lose ");
                break;
            }
            step++;
        }
        if (isWin(COM) == NONE && isWin(MAN) == NONE && step == 10)
        {
            printf("nobody win");
        }
    }

    else if (c == 'N' || c == 'n')
    {
        for (step = 1; step <= STEP;)
        {

            lastPosition = com_play(step, lastPosition);
            display();

            if (COM == isWin(COM))
            {
                printf("you lose ");
                break;
            }
            step++;

            if (step == 10)
            {
                break;
            }
            lastPosition = person_play();

            if (MAN == isWin(MAN))
            {
                printf("you win ");
                break;
            }
            step++;
        }
        if (isWin(COM) == NONE && isWin(MAN) == NONE && step == 10)
        {
            printf("nobody win");
        }
    }

    getch();
}