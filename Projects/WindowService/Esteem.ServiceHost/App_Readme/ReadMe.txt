WCF Hosting Sample by Hugh Proctor

Open Command Window with Administration Privilages.

To Install:
  <!-- VERY IMPORTANT NOTE - FOR DEBUG WINDOWS SERVICE YOU MUST CHANGE THE VALUE SETTING IN THE WCF.Service.Host.exe.config, they will not get compiled!! -->

Remember to update the Manifest file with the correct name of the (Windows) Service being installed.

cd C:\Windows\Microsoft.NET\Framework\v4.0.30319

installutil /name="WCF.Service.Host.Local" /displayname="ActionServiceLocal" /description="WCF.Service.Host ActionService" "D:\_Development\branches\TravelCompany\_CompileBin\Build 1.0.0.x\WCF.Service.Host.exe"
installutil /name="WCF.Service.Host.Local" /displayname="ActionServiceLocal" /description="WCF.Service.Host ActionService" "D:\_Development\branches\TravelCompany\_CompileBin\Build 2.0.0.x\WCF.Host\WCF.Service.Host.exe"
installutil /name=CalcV2 /displayname="Calc V2" /description="Sample Calculator Service" "c:\CalcV2\WcfSample.Service.Host.exe"
installutil /name="ActionServiceRelease" /displayname="ActionServiceRelease" /description="WCF.Service.Host ActionService" "D:\GitHubRepositories\LayrCake_Framework\WindowsServiceHost\_Release\WCF.Service.Host.exe"
installutil /name="_LayrCake.DataVisualizer" /displayname="_LayrCake.DataVisualizer" /description="WCF.Service.Host _LayrCake.DataVisualizer" "C:\_MacBookRepo\trunk\_ReleaseProjects\LayrCakeDataVisualiser\WCFServiceHost\LayrCake.WCFServiceHost.exe"

To Uninstall:
installutil /u /name="ActionService" "D:\_Development\branches\TravelCompany\2. Hosting Layer\WcfServiceHost\bin\Debug\WCF.Service.Host.exe"
installutil /u /name="WCF.Service.Host.Local" "D:\_Development\branches\TravelCompany\_CompileBin\Build 2.0.0.x\WCF.Host\WCF.Service.Host.exe"
installutil /u /name="_LayrCake.DataVisualizer" "C:\_MacBookRepo\trunk\LayrCake\LayrCake_Framework\DataVisualiser\_WCFRelease\LayrCake.WCFServiceHost.exe"
installutil /u /name=CalcV2 "c:\CalcV2\WcfSample.Service.Host.exe"

To Install Net Local Address
netsh http add urlacl url=http://+:8731/Design_Time_Addresses user=DOMAIN\user
netsh http add urlacl url=http://+:8731/Design_Time_Addresses user=BUILTIN\Administrators
netsh http add urlacl url=http://+:8733/Design_Time_Addresses/ActionService user=WINDOWS-VAI5U86\USER
netsh http add urlacl url=http://+:8735/LayrCake user=NETWORKSERVICE
netsh http add urlacl url=http://192.168.0.142:8736/DataVisualizer user=NETWORKSERVICE
netsh http add urlacl url=http://+:8736/DataVisualizer user=NETWORKSERVICE

To Delete Net Local Address
netsh http delete urlacl url=http://+:8731/Design_Time_Addresses

To Delete a Windows Service manually
sc delete [service name]

Net Start ActionService