@echo off
call "c:\Program Files\Microsoft Visual Studio .NET 2003\Vc7\bin\vcvars32.bat"
rem cl /LD /MD gpc.c
csc /out:"GpcTest.exe" /target:winexe GpcWrapper.cs GpcTest.cs /r:System.dll,System.Windows.Forms.dll,System.Drawing.dll
Pause