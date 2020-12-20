/*
 * @lc app=leetcode.cn id=797 lang=csharp
 *
 * [797] 所有可能的路径
 *
 * https://leetcode-cn.com/problems/all-paths-from-source-to-target/description/
 *
 * algorithms
 * Medium (74.29%)
 * Likes:    83
 * Dislikes: 0
 * Total Accepted:    6.2K
 * Total Submissions: 8.3K
 * Testcase Example:  '[[1,2],[3],[3],[]]'
 *
 * 给一个有 n 个结点的有向无环图，找到所有从 0 到 n-1 的路径并输出（不要求按顺序）
 * 
 * 二维数组的第 i 个数组中的单元都表示有向图中 i 号结点所能到达的下一些结点（译者注：有向图是有方向的，即规定了 a→b 你就不能从 b→a
 * ）空就是没有下一个结点了。
 * 
 * 
 * 
 * 示例 1：
 * 
 * 
 * 
 * 输入：graph = [[1,2],[3],[3],[]]
 * 输出：[[0,1,3],[0,2,3]]
 * 解释：有两条路径 0 -> 1 -> 3 和 0 -> 2 -> 3
 * 
 * 
 * 示例 2：
 * 
 * 
 * 
 * 输入：graph = [[4,3,1],[3,2,4],[3],[4],[]]
 * 输出：[[0,4],[0,3,4],[0,1,3,4],[0,1,2,3,4],[0,1,4]]
 * 
 * 
 * 示例 3：
 * 
 * 输入：graph = [[1],[]]
 * 输出：[[0,1]]
 * 
 * 
 * 示例 4：
 * 
 * 输入：graph = [[1,2,3],[2],[3],[]]
 * 输出：[[0,1,2,3],[0,2,3],[0,3]]
 * 
 * 
 * 示例 5：
 * 
 * 输入：graph = [[1,3],[2],[3],[]]
 * 输出：[[0,1,2,3],[0,3]]
 * 
 * 
 * 
 * 
 * 提示：
 * 
 * 
 * 结点的数量会在范围 [2, 15] 内。
 * 你可以把路径以任意顺序输出，但在路径内的结点的顺序必须保证。
 * 
 * 
 */

// @lc code=start
public class Solution {
    //TODO:
    public IList<IList<int>> AllPathsSourceTarget (int[][] graph) {

    }
}
// @lc code=end