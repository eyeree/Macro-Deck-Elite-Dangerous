echo ==== Copy For Testing ====
mkdir "%Appdata%\Macro Deck\plugins\maimedleech.EliteDangerous"
copy .\bin\Debug\netcoreapp3.1\EliteDangerousMacroDeckPlugin.dll  "%Appdata%\Macro Deck\plugins\maimedleech.EliteDangerous"
copy .\bin\Debug\netcoreapp3.1\EliteJournalReader.dll "%Appdata%\Macro Deck\plugins\maimedleech.EliteDangerous"
copy .\Test\Plugin.xml "%Appdata%\Macro Deck\plugins\maimedleech.EliteDangerous"