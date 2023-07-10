// 服务器端代码
public class Server
{
    private int currentFrame = 0;
    private Dictionary<int, List<(string input, int frameNumber)>> playerInputBuffer = new Dictionary<int, List<(string, int)>>();

    public void ReceivePlayerInput(int playerId, string input, int frameNumber)
    {
        if (!playerInputBuffer.ContainsKey(playerId))
        {
            playerInputBuffer[playerId] = new List<(string, int)>();
        }
        playerInputBuffer[playerId].Add((input, frameNumber));
    }

    public void Synchronize()
    {
        // 在同步点进行延迟补偿和模拟
        Dictionary<int, string> currentFrameInputs = new Dictionary<int, string>();
        foreach (var entry in playerInputBuffer)
        {
            int playerId = entry.Key;
            var inputs = entry.Value;

            // 找到当前帧的玩家输入
            foreach (var (input, frameNumber) in inputs)
            {
                if (frameNumber == currentFrame)
                {
                    currentFrameInputs[playerId] = input;
                    break;
                }
            }
        }

        // 应用延迟补偿，根据延迟时间获取玩家输入
        int delay = 2; // 假设延迟为2帧
        int compensatedFrame = currentFrame - delay;
        Dictionary<int, string> compensatedInputs = new Dictionary<int, string>();
        foreach (var entry in playerInputBuffer)
        {
            int playerId = entry.Key;
            var inputs = entry.Value;

            foreach (var (input, frameNumber) in inputs)
            {
                if (frameNumber == compensatedFrame)
                {
                    compensatedInputs[playerId] = input;
                    break;
                }
            }
        }

        // 在模拟过程中使用补偿后的输入
        Simulate(compensatedInputs);

        // 清空已处理的输入
        foreach (var entry in playerInputBuffer)
        {
            int playerId = entry.Key;
            var inputs = entry.Value;
            playerInputBuffer[playerId] = inputs.Where(input => input.frameNumber > currentFrame).ToList();
        }

        // 增加当前帧计数
        currentFrame++;
    }

    private void Simulate(Dictionary<int, string> inputs)
    {
        // 使用输入进行模拟
        foreach (var entry in inputs)
        {
            int playerId = entry.Key;
            string input = entry.Value;

            // 在这里根据输入进行游戏模拟
            Console.WriteLine($"Simulating frame {currentFrame} for player {playerId} with input {input}");
        }
    }
}

// 客户端代码
public class Client
{
    private Server server;

    public Client(Server server)
    {
        this.server = server;
    }

    public void SendPlayerInput(string input, int frameNumber)
    {
        int playerId = 1; // 假设只有一个玩家，玩家ID为1
        server.ReceivePlayerInput(playerId, input, frameNumber);
    }
}

// 示例运行
Server server = new Server();
Client client = new Client(server);

// 模拟多个帧
for (int frame = 0; frame < 10; frame++)
{
    string input = "Move forward"; // 假设玩家输入为向前移动
    client.SendPlayerInput(input, frame);
    server.Synchronize();
}
