// 处理Unhandled Exception的回调函数
//
// #include "CrashGuarder.h"

#include <Windows.h>
#include <DbgHelp.h>
#include <iostream>
#include <vector>

#pragma comment(lib, "dbghelp.lib")
 
using namespace std;

class CrashTest
{
public:
	void Test() 
	{ 
		Crash(); 
	}
 
private:
	void Crash() 
	{ 
		// 除零，人为的使程序崩溃
		//
		int i = 13;
		int j = 0;
		int m = i / j;
	}
};

// 创建Dump文件
// 
void CreateDumpFile(LPCWSTR lpstrDumpFilePathName, EXCEPTION_POINTERS* pException)
{
	// 创建Dump文件
	//
	HANDLE hDumpFile = CreateFile(lpstrDumpFilePathName, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

	// Dump信息
	//
	MINIDUMP_EXCEPTION_INFORMATION dumpInfo;
	dumpInfo.ExceptionPointers = pException;
	dumpInfo.ThreadId = GetCurrentThreadId();
	dumpInfo.ClientPointers = TRUE;

	// 写入Dump文件内容
	//
	MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(), hDumpFile, MiniDumpNormal, &dumpInfo, NULL, NULL);

	CloseHandle(hDumpFile);
};

LPCWSTR string2LPCWSTR(std::string str)
{
	size_t size = str.length();
	int wLen = ::MultiByteToWideChar(CP_UTF8,
		0,
		str.c_str(),
		-1,
		NULL,
		0);
	wchar_t* buffer = new wchar_t[wLen + 1];
	memset(buffer, 0, (wLen + 1) * sizeof(wchar_t));
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), size, (LPWSTR)buffer, wLen);
	return buffer;
};


LONG ApplicationCrashHandler(EXCEPTION_POINTERS* pException)
{
	// 这里弹出一个错误对话框并退出程序
	// FatalAppExit(-1,  _T("*** Unhandled Exception! ***"));
	std::cout << "CrashGuarder catch error -------------------------->\n";

	CreateDumpFile(string2LPCWSTR("C:\\Test.dmp"), pException);

	return EXCEPTION_EXECUTE_HANDLER;
};


int main()
{
	SetUnhandledExceptionFilter((LPTOP_LEVEL_EXCEPTION_FILTER)ApplicationCrashHandler);
	std::cout << "Hello World!\n";
	CrashTest test;
	test.Test();

	int a = 10;
	int b = 0;

	int c = a / b;

	std::cout << "CrashGuard Over!\n";
	//return 0;
}
