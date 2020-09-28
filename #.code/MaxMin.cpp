#include <iostream>
using namespace std;

const int INF = 0x3f3f3f3f;
int map[10], T;

int check()
{
    int it = 0;
    for (int i = 1; i <= 3; ++i) { // 找出是否可以结束。
        if (map[i] == map[i + 3] && map[i + 3] == map[i + 6] && map[i]) {
            it = map[i];
            break;
        }
        int k = 3*(i - 1) + 1;
        if (map[k] == map[k + 1] && map[k] == map[k + 2] && map[k]) {
            it = map[k];
            break;
        }
    }
    if (!it) {
        if (map[1] == map[5] && map[1] == map[9] && map[1]) it = map[1];
        else if (map[3] == map[5] && map[5] == map[7] && map[5]) it = map[5];
    }
    int cnt = 0;
    for (int i = 1; i <= 9; ++i)
        if (map[i] == 0) cnt++;
    if (it == 0 && cnt == 0) return 0;
    if (it == 1) return cnt + 1;
    else if (it == 2) return - (cnt + 1);
    else return -1;
}

int dfs(int it)
{
    int chec = check();
    if (chec != -1) return chec;
    int ans = it == 1 ? -INF : INF;
    for (int i = 1; i <= 9; ++i) {
        if (map[i]) continue;
        if (it == 1) {
            map[i] = 1;
            ans = max(ans, dfs(2));
        } else {
            map[i] = 2;
            ans = min(ans, dfs(1));
        }
        map[i] = 0;
    }
    return ans;
}

int main()
{
    cin >> T;
    while (T--) {
        for (int i = 1; i <= 9; ++i) {
            cin >> map[i];
        }
        cout << dfs(1) << endl;;
    }
}