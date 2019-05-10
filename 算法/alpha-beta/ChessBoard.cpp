#include"ChessBoard.h"
ChessBoard::ChessBoard() {             //默认构造函数
    boardInOneDimens.resize(GridNuber);
    for (int i = 0; i < GridNuber; i++)
        boardInOneDimens[i] = ' ';
    vector<string>tmp = {
        "- - - - - - - - - - - - -",
        "|       |       |       |",
        "|       |       |       |",
        "|       |       |       |",
        "- - - - - - - - - - - - -",
        "|       |       |       |",
        "|       |       |       |",
        "|       |       |       |",
        "- - - - - - - - - - - - -",
        "|       |       |       |",
        "|       |       |       |",
        "|       |       |       |",
        "- - - - - - - - - - - - -"
    };
    board = tmp;
}
//check the posion is empty or not 
bool ChessBoard::isEmpty(int pos) {
    return boardInOneDimens[pos] == ' ';
}
//
bool ChessBoard::isFull() {
    for (int i = 0; i < GridNuber; i++) {
        if (boardInOneDimens[i] == ' ')
            return false;
    }
    return true;
}
//
bool ChessBoard::canWin(char c) {
    //check every row
    for (int i = 0; i <= 6; i+=3) {
        if (boardInOneDimens[i] == c&&boardInOneDimens[i] == boardInOneDimens[i + 1] &&
            boardInOneDimens[i] == boardInOneDimens[i + 2])
            return true;
    }
    //check every col
    for (int i = 0; i < 3; i++) {
        if (boardInOneDimens[i] == c&&boardInOneDimens[i] == boardInOneDimens[i + 3] &&
            boardInOneDimens[i] == boardInOneDimens[i + 6])
            return true;
    }
    //check every diagonals
    if (boardInOneDimens[0] == c&&boardInOneDimens[4] == c&&boardInOneDimens[8] == c)
        return true;
    if (boardInOneDimens[2] == c&&boardInOneDimens[4] == c&&boardInOneDimens[6] == c)
        return true;
    return false;
}
//能否立即让AI赢
bool ChessBoard::immediateComWin(int& bestMove) {
    for (int i = 0; i < GridNuber; i++) {
        if (isEmpty(i)) {
            boardInOneDimens[i] =Comp_Char;
            bool Win = canWin(Comp_Char); 
            boardInOneDimens[i] = Blank_Char;   //backtraceing
            if (Win) {
                bestMove = i;//能立即赢，那这一步肯定是最优解
                return true;
            }
        }
    }
    return false;
}
//
bool ChessBoard::immediateHumanWin(int& bestMove) {
    for (int i = 0; i < GridNuber; i++) {
        if (isEmpty(i)) {
            boardInOneDimens[i] = Human_Char;
            bool Win = canWin(Human_Char);
            boardInOneDimens[i] = Blank_Char;   //backtraceing
            if (Win) {
                bestMove = i;
                return true;
            }
        }
    }
    return false;
}
//
void ChessBoard::placeComp(int pos) {
    boardInOneDimens[pos] = 'X';
}
//
void ChessBoard::placeHuman(int pos) {
    boardInOneDimens[pos] = 'O';
}
//
void ChessBoard::unPlace(int pos) {
    boardInOneDimens[pos] = ' ';
}
//
void ChessBoard::print() {
    int cnt = 0;
    for (int i = 2; i <= 10; i += 4) {
        for (int j = 4; j <= 20; j += 8) {
            board[i][j] = boardInOneDimens[cnt++];
        }
    }
    for (int i = 0; i < ChessBoard_Row; ++i) {
        cout << board[i] << endl;
    }
}