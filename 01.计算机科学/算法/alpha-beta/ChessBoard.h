//Author : yqtao
//Date   :2016.10.30
#ifndef CHESS_BOARD_H
#define CHESS_BOARD_H
#include <vector>
#include <iostream>
#include <string>
using namespace std;

const int ChessBoard_Row = 13;
const int ChessBoard_Col = 26;
const int GridNuber = 9;
const char Comp_Char = 'X';
const char Human_Char = 'O';
const char Blank_Char = ' ';

class ChessBoard
{
public:
    ChessBoard();
    bool isEmpty(int pos);
    bool isFull();
    bool canWin(char c);
    bool immediateComWin(int &bestMove);
    bool immediateHumanWin(int &bestMove);
    void placeComp(int pos);
    void placeHuman(int pos);
    void unPlace(int pos);
    void print();

private:
    vector<char> boardInOneDimens;
    vector<string> board;
};
#endif // !CHESS_BOARD_H