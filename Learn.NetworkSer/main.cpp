#include <stdio.h>
#include <Winsock2.h>
#pragma comment(lib, "ws2_32.lib")

void main()
{
	WORD wVersionRequested;
	WSADATA wsaData;
	int err;

	wVersionRequested = MAKEWORD(1, 1);

	err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) {
		return;
	}

	if (LOBYTE(wsaData.wVersion) != 1 ||
		HIBYTE(wsaData.wVersion) != 1) {
		WSACleanup();
		return;
	}
	SOCKET sockSrv = socket(AF_INET, SOCK_STREAM, 0);

	SOCKADDR_IN addrSrv;
	addrSrv.sin_addr.S_un.S_addr = htonl(INADDR_ANY);
	addrSrv.sin_family = AF_INET;
	addrSrv.sin_port = htons(6000);

	bind(sockSrv, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR));// 绑定端口

	listen(sockSrv, 5);

	SOCKADDR_IN addrClient;// 连接上的客户端ip地址
	int len = sizeof(SOCKADDR);
	while (1)
	{
		SOCKET sockConn = accept(sockSrv, (SOCKADDR*)&addrClient, &len);// 接受客户端连接,获取客户端的ip地址
		char sendBuf[50];
		sprintf(sendBuf, "Welcome %s to here!", inet_ntoa(addrClient.sin_addr));// 组合消息发送出去
		send(sockConn, sendBuf, strlen(sendBuf) + 1, 0);// 发送消息到客户端
		char recvBuf[50];
		recv(sockConn, recvBuf, 50, 0);// 接受客户端消息
		printf("%s\n", recvBuf);
		//closesocket(sockConn);//断开连接
	}
}