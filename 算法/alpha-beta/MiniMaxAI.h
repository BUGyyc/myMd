#ifndef TICTACTOE_H
#define TICTACTOE_H
#include "ChessBoard.h"
enum Role { Human, Comp };
static const int CompWin = 2;
static const int Draw = 1;
static const int HumanWin = 0;
class TicTacToe {
public:
    void start();
    bool handleGameOver();
    bool gameIsOver(bool &draw, Role &win);
    Role chooseFirstPlace();
    void compPlace();
    int getBestMove();
    void humanPlace();
    int getPlacePosition();
    void findCompMove(int& bestMove,int& value, int alpha, int beta);
    void findHumanMove(int& bestMove,int& value, int alpha, int beta);
private:
    ChessBoard board;
};
#endif // !TicTacToe