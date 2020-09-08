# -*- coding: utf-8 -*-
import socket
s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
addr = ("115.159.208.238", 49534)
data = "234325435"
s.sendto(data.encode(), addr)
print("udp send over")
s.close()