---
typora-root-url: #.res\pic
---

# myMd

梳理所学，永久更新

---

# 计算机科学

## 数据结构

### 链表

#### 翻转链表

    public ListNode reverseList(ListNode head) {
        ListNode prev = null;
        ListNode curr = head;
        while (curr != null) {
            ListNode nextTemp = curr.next;
            curr.next = prev;
            prev = curr;
            curr = nextTemp;
        }
        return prev;
    }

#### 查找链表的倒数第K个节点

双指针查找，第一个指针先走K步，然后第二个指针再走，同时走后保持相同速度，当第一个指针到达尾部后，那么第二个指针就是倒数第K个节点。

#### 查找链表的中间节点

快慢指针，第一个指针每次移动两步，第二个指针每次移动一步，当第一个指针到达尾部后，那么第二个指针就是链表的中间节点。

#### 判断链表是否有环

快慢指针，第一个指针每次移动两步，第二个指针每次移动一步，在到达尾部之前，如果第一个指针会和第二个指针相遇到一个节点，那么就可以判断有环，反之，则没有环。

#### 判断两个链表是否相交

可以先推理一下，如果两个链表相交，那么链表的最后一个节点必定是同一个。若两个链表不相交，那么链表的最后一个节点必定不是同一个。
所以只需要判断两个链表的最后一个节点是否是同一个。

### 二叉树

#### 前序、中序、后序遍历

前序遍历

       class Solution {
            public List<Integer> preorderTraversal(TreeNode root) {
                LinkedList<TreeNode> stack = new LinkedList<>();
                LinkedList<Integer> output = new LinkedList<>();
                if (root == null) {
                    return output;
                }
    
                stack.add(root);
                while (!stack.isEmpty()) {
                    TreeNode node = stack.pollLast();
                    output.add(node.val);
                    if (node.right != null) {
                        stack.add(node.right);
                    }
                    if (node.left != null) {
                        stack.add(node.left);
                    }
                }
                return output;
            }
        }

中序遍历

    class Solution {
        public List < Integer > inorderTraversal(TreeNode root) {
            List < Integer > res = new ArrayList < > ();
            helper(root, res);
            return res;
        }
    
        public void helper(TreeNode root, List < Integer > res) {
            if (root != null) {
                if (root.left != null) {
                    helper(root.left, res);
                }
                res.add(root.val);
                if (root.right != null) {
                    helper(root.right, res);
                }
            }
        }
    }

后序遍历

    class Solution {
        public List<Integer> postorderTraversal(TreeNode root) {
            LinkedList<TreeNode> stack = new LinkedList<>();
            LinkedList<Integer> output = new LinkedList<>();
            if (root == null) {
                return output;
            }
            stack.add(root);
            while (!stack.isEmpty()) {
                TreeNode node = stack.pollLast();
                output.addFirst(node.val);
                if (node.left != null) {
                    stack.add(node.left);
                }
                if (node.right != null) {
                    stack.add(node.right);
                }
            }
            return output;
        }
    }

#### 层序遍历

    void bfs(TreeNode root) {
        Queue<TreeNode> queue = new ArrayDeque<>();
        queue.add(root);
        while (!queue.isEmpty()) {
            TreeNode node = queue.poll(); // Java 的 pop 写作 poll()
            if (node.left != null) {
                queue.add(node.left);
            }
            if (node.right != null) {
                queue.add(node.right);
            }
        }
    }

#### 二叉树的最大深度

### 堆栈

#### 堆栈实现队列

### 队列

#### 队列实现堆栈

### 图

#### 图的遍历

#### 图的搜索最短路径

## 算法

### 十大排序算法

![1598754122780](/1598754122780.png)

#### 冒泡排序

两两比较

#### 选择排序

每次从未排序的列表中找出最大或最小的一项

#### 插入排序

将第一待排序序列第一个元素看做一个有序序列，把第二个元素到最后一个元素当成是未排序序列。从头到尾依次扫描未排序序列，将扫描到的每个元素插入有序序列的适当位置。（如果待插入的元素与有序序列中的某个元素相等，则将待插入元素插入到相等元素的后面。）

#### 快速排序

从数列中挑出一个元素，称为 “基准”（pivot）;重新排序数列，所有元素比基准值小的摆放在基准前面，所有元素比基准值大的摆在基准的后面（相同的数可以到任一边）。在这个分区退出之后，该基准就处于数列的中间位置。这个称为分区（partition）操作；递归地（recursive）把小于基准值元素的子数列和大于基准值元素的子数列排序；

#### 归并排序

#### 堆排序

#### 桶排序

#### 希尔排序

#### 计数排序

#### 基数排序

### 四大常用算法

#### 分治算法

#### 贪心算法

#### 动态规划

#### 回溯算法

### 其他类算法

#### A* 算法

#### KMP模式匹配

## 计算机组成原理

## 操作系统

## 编译原理

# 编程语言

## Java

## C#

## JavaScript

## TypeScript

# 软件开发

## Windows桌面应用

## AndroidAPP

## 小程序

# 计算机数学

## 矩阵

# 游戏编程

## 游戏开发技巧

# 游戏引擎

## 物理引擎

# 计算机图形学

## 渲染管线

### 应用阶段

#### 加载数据到显存

#### 设置渲染状态

#### 调用DrawCall

### 几何阶段

#### 顶点着色器

#### 曲面细分着色器

#### 几何着色器

#### 裁剪

#### 屏幕映射

### 光栅化阶段

#### 三角形设置

#### 三角形遍历

#### 片元着色器

#### 逐片元操作

#### 混合测试

# 游戏音效

# 游戏物理与动画

# 人工智能

# 网络

## 七层结构

### 应用层

### 会话层

### 表示层

### 传输层

### 网络层

### 数据链路层

### 物理层

## 网络安全

# 硬件

# 日常开发问题

---