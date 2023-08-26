

# 带延迟补偿的测试代码



# 服务器端代码
class Server:
    def __init__(self):
        self.current_frame = 0
        self.player_input_buffer = {}  # 玩家输入缓冲区，用于存储每个玩家的输入

    def receive_player_input(self, player_id, input_data, frame_number):
        # 将玩家输入存储到输入缓冲区
        if player_id not in self.player_input_buffer:
            self.player_input_buffer[player_id] = []
        self.player_input_buffer[player_id].append((input_data, frame_number))

    def synchronize(self):
        # 在同步点进行延迟补偿和模拟
        current_frame_inputs = {}
        for player_id, inputs in self.player_input_buffer.items():
            # 找到当前帧的玩家输入
            for input_data, frame_number in inputs:
                if frame_number == self.current_frame:
                    current_frame_inputs[player_id] = input_data
                    break

        # 应用延迟补偿，根据延迟时间获取玩家输入
        delay = 2  # 假设延迟为2帧
        compensated_frame = self.current_frame - delay
        compensated_inputs = {}
        for player_id, inputs in self.player_input_buffer.items():
            for input_data, frame_number in inputs:
                if frame_number == compensated_frame:
                    compensated_inputs[player_id] = input_data
                    break

        # 在模拟过程中使用补偿后的输入
        self.simulate(compensated_inputs)

        # 清空已处理的输入
        for player_id, inputs in self.player_input_buffer.items():
            self.player_input_buffer[player_id] = [(input_data, frame_number) for input_data, frame_number in inputs if frame_number > self.current_frame]

        # 增加当前帧计数
        self.current_frame += 1

    def simulate(self, inputs):
        # 使用输入进行模拟
        for player_id, input_data in inputs.items():
            # 在这里根据输入进行游戏模拟
            print(f"Simulating frame {self.current_frame} for player {player_id} with input {input_data}")


# 客户端代码
class Client:
    def __init__(self, server):
        self.server = server

    def send_player_input(self, input_data, frame_number):
        player_id = 1  # 假设只有一个玩家，玩家ID为1
        self.server.receive_player_input(player_id, input_data, frame_number)


# 示例运行
server = Server()
client = Client(server)

# 模拟多个帧
for frame in range(10):
    input_data = "Move forward"  # 假设玩家输入为向前移动
    client.send_player_input(input_data, frame)
    server.synchronize()
