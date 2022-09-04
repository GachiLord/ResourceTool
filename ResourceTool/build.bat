set dir=%cd%
set gdir=D:\Games\SteamLibrary\steamapps\common\Kerbal Space Program


xcopy "%dir%\bin\Debug\ResourceTool.dll" /Y "%dir%\Build\\ResourceTool"
xcopy "%dir%\core\dockingPort.cfg" /Y "%dir%\Build\ResourceTool"
xcopy "%dir%\core\Localization.cfg" /Y "%dir%\Build\ResourceTool"
xcopy "%dir%\core\resourceList.txt" /Y "%dir%\Build\ResourceTool"


xcopy "%dir%\bin\Debug\ResourceTool.dll" /Y "%gdir%\GameData\ResourceTool"
xcopy "%dir%\core\dockingPort.cfg" /Y "%gdir%\GameData\ResourceTool"
xcopy "%dir%\core\Localization.cfg" /Y "%gdir%\GameData\ResourceTool"
xcopy "%dir%\core\resourceList.txt" /Y "%gdir%\GameData\ResourceTool"


