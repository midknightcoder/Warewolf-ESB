@echo off
@echo Assembly Name eg: Dev2.Activities.Specs.dll:
set /P assembly=
cd %CD%\..\..\Win2k8
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\MSTest.exe" /testcontainer:"..\..\..\..\TestBinaries\%assembly%" /testSettings:"SpecFlow.testsettings"
pause