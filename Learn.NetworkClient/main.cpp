#include <stdio.h>
#include <WinSock2.h>
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

	SOCKET sockClient = socket(AF_INET, SOCK_STREAM, 0);
	//��ʼ��������˿ں�
	SOCKADDR_IN addrSrv;
	addrSrv.sin_addr.S_un.S_addr = inet_addr("192.168.84.107");//������ַ���������ڱ�������
	addrSrv.sin_family = AF_INET;
	addrSrv.sin_port = htons(6000);// ���ö˿ں�

	int result = connect(sockClient, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR));//���ӷ�����
	char recvBuf[50];
	recv(sockClient, recvBuf, 50, 0);//��������
	printf("%s\n", recvBuf);
	send(sockClient, "hello", strlen("hello") + 1, 0);//��������
	closesocket(sockClient);//�ر�����
	WSACleanup();
}