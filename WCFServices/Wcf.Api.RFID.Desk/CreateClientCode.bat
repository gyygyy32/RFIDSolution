cd %~dp0
rem generate client proxy classes with the wsdl
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\svcutil.exe" ..\WSDL\*.wsdl ..\WSDL\*.xsd /language:C# /enableDataBinding /noConfig ^
/n:http://www.hulisoft.com/RFIDService,RFIDService.Clients ^
/n:http://www.hulisoft.com/RFIDServiceData,RFIDService.ClientData ^
/o:RFIDService.cs 
:: /config:app.config
pause