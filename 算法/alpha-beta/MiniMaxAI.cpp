#include "MiniMaxAI.h"
void TicTacToe::start()
{
    Role firstPlace = chooseFirstPlace();
    if (firstPlace == Comp)
    { // Choose computer to be the first
        compPlace();
    }
    while (1)
    {
        board.print();
        if (handleGameOver())
            break;
        humanPlace();
        board.print();
        if (handleGameOver())
            break;
        compPlace();
    }
}
Role TicTacToe::chooseFirstPlace()
{
    int choose;
    while (1)
    {
        cout << "who will place first" << endl;
        cout << "0 : human first " << endl;
        cout << "1 : computer first" << endl;
        cin >> choose;
        if (choose == 0 || choose == 1)
        {
            cout << endl;
            break;
        }
        else
            cout << "error,please enter again" << endl;
    }
    return (Role)choose;
}
//
void TicTacToe::humanPlace()
{
    int pos = getPlacePosition();
    board.placeHuman(pos);
    cout << "Your choice:" << endl;
}
//
int TicTacToe::getPlacePosition()
{
    int m, n, pos;
    while (1)
    {
        cout << "It is your turn, please input where you want :" << endl;
        cout << "for example: 2 2 mean you want to add position 2 row,2 col:" << endl;
        cin >> m >> n;
        if (m < 0 || m > 3 || n < 0 || n > 3)
            cout << "error,please input again:" << endl;
        else
        {
            pos = (m - 1) * 3 + n - 1;
            if (board.isEmpty(pos))
                break;
            else
                cout << "error,please input again:" << endl;
        }
    }
    return pos;
}
//
void TicTacToe::compPlace()
{
    int bestMove = getBestMove(); //找当前最优解
    board.placeComp(bestMove);    //执行最优解
    cout << "the computer choice is: " << endl;
}
int TicTacToe::getBestMove()
{
    int bestMove = 0, value = 0;
    findCompMove(bestMove, value, HumanWin, CompWin);
    return bestMove;
}
void TicTacToe::findCompMove(int &bestMove, int &value, int alpha, int beta)
{
    if (board.isFull()) //放满了
        value = Draw;
    else if (board.immediateComWin(bestMove)) //尝试电脑一步获胜
        value = CompWin;                      //电脑赢了
    else
    {
        value = alpha; //最大值
        for (int i = 0; i < GridNuber && value < beta; i++)
        {
            if (board.isEmpty(i)) //当前这格是否为空，如果空的
            {
                board.placeComp(i);          //电脑尝试执行放这格
                int tmp = -1, response = -1; // tmp没有用上
                findHumanMove(tmp, response, value, beta);
                board.unPlace(i); //把刚刚尝试放的那格重置
                if (response > value)
                {
                    value = response;
                    bestMove = i;
                }
            }
        }
    }
}

/*
* 找人的执行
*/
void TicTacToe::findHumanMove(int &bestMove, int &value, int alpha, int beta)
{
    if (board.isFull()) //放满了
        value = Draw;
    else if (board.immediateHumanWin(bestMove)) //是否可以人立即赢
    {
        value = HumanWin;
    }
    else
    {
        value = beta;
        for (int i = 0; i < GridNuber && value > alpha; i++)
        {
            if (board.isEmpty(i))
            {
                board.placeHuman(i);//尝试放这格
                int tmp = -1, response = -1; // tmp 没有用上
                findCompMove(tmp, response, alpha, value);
                board.unPlace(i);//重置回去
                if (response < value)
                {
                    value = response;
                    bestMove = i;
                }
            }
        }
    }
}
//
bool TicTacToe::gameIsOver(bool &draw, Role &win)
{
    if (board.canWin(Comp_Char))
    {
        win = Comp;
        draw = false;
        return true;
    }
    else if (board.canWin(Human_Char))
    {
        win = Human;
        draw = false;
        return true;
    }
    else if (board.isFull())
    {
        draw = true;
        return true;
    }
    else
    {
        return false;
    }
}

bool TicTacToe::handleGameOver()
{
    bool draw = false;
    Role whoWin = Human;
    if (gameIsOver(draw, whoWin))
    {
        if (draw)
        {
            cout << "Draw!" << endl;
        }
        else
        {
            if (whoWin == Comp)
            {
                cout << "You lose!" << endl;
            }
            else if (whoWin == Human)
            {
                cout << "Congratulations! You defeat the computer." << endl;
            }
        }
        return true;
    }
    else
    {
        return false;
    }
}