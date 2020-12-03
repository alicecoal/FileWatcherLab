# FileWatcherLab

ParserXml, ParserJson - расширяемые обработчики (парсер) XML и JSON файлов.
***

Все видимые исключительные ситуации обработаны и залогированы с помощью Logger.
***

Реализован менеджер опций - OptionsManager. При запуске сервиса этот класс загружает файлы конфигурации appsettings.json и config.xml. 

Если менеджер опций находит JSON-конфигурацию, то загружается она, иначе загружается XML-конфигурация, в худшем случае загружается стандартная конфигурация. 

Содержимому конфигурационных файлов предоставлена соответствующая модель на стороне .NET, именование модели ETL[XML\JSON]Options. 

Помимо всего прочего, реализована возможность доставать конфигурацию не только целиком из всего файла, а кусками (секциями) с помощью метода Options FindOption<T>(ETLOptions options).
  
Для каждого логически отдельного блока конфигураций создана соответствующая модель на стороне .NET - ArchiveOptions, FolderOptions, CrypterOptions, LogOptions.

Т.е. созданы классы:

* class FolderOptions - содержит опции SourceDir -  и TargetDir. Наследуется от общего класса Options.
* class ArchiveOptions - содержит опцию CompressionLevel - степень сжатия и ArchiveDir - путь к папке, хранящей архивы. Наследуется от общего класса Options.
* class CrypterOptions - содержит опцию NeedToEncrypt. Наследуется от общего класса Options.
* class LogOptions - содержит опцию LogFile - путь к файлу, где происходит сохранение логов для службы, и опцию NeedToLog. Наследуется от общего класса Options.

Менеджер конфигурации получает на вход путь к файлу с использованием AppDomain.
