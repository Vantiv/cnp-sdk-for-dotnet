IF "%1"==[] (
	echo "Requires command line argument to be version to zip"
	exit /b
)

copy CnpSdkForNet\CnpSdkForNet\bin\Release\CnpSdkForNet.dll .\
copy CnpSdkForNet\CnpSdkForNet\bin\Release\CnpSdkForNet.dll.config .\
copy CnpSdkForNet\CnpSdkForNet\bin\Release\DiffieHellman.dll .\
copy CnpSdkForNet\CnpSdkForNet\bin\Release\Tamir.SharpSSH.dll .\
copy CnpSdkForNet\CnpSdkForNet\bin\Release\Org.Mentalis.Security.dll .\
"C:\Program Files\7-Zip\7z.exe" a CnpSdkForNet-%1.zip CHANGELOG LICENSE CnpSdkForNet.dll CnpSdkForNet.dll.config README.md DiffieHellman.dll Tamir.SharpSSH.dll Org.Mentalis.Security.dll
del CnpSdkForNet.dll
del CnpSdkForNet.dll.config
del DiffieHellman.dll
del Tamir.SharpSSH.dll
del Org.Mentalis.Security.dll