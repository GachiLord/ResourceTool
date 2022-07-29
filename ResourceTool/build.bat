set dir=%cd%

xcopy "%dir%\bin\Debug\ResourceTool.dll" /Y "%dir%\Build\\ResourceTool"
xcopy "%dir%\core\dockingPort.cfg" /Y "%dir%\Build\ResourceTool"
xcopy "%dir%\core\Localization.cfg" /Y "%dir%\Build\ResourceTool"
xcopy "%dir%\core\resourceList.txt" /Y "%dir%\Build\ResourceTool"