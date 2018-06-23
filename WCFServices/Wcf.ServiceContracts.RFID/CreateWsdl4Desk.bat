cd %~dp0
del ..\WSDL\* /q /s
:: generate wsdl. Run this when there's a breaking change in the interface. You still need to be careful about interface versioning.
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\svcutil.exe" ^
bin\Debug\Wcf.ServiceContracts.RFID.dll  /directory:..\WSDL
pause