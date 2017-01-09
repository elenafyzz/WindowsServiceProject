# WindowsServiceProject
Решение состоит из двух проектов.
В ConsoleApp 3 класса: 
Startup клас для Owin self host; 
Apicontroller "StatusController", в котором метод bool CheckStatus отвечает true, если сервис установлен и запущен;
класс Program, который содержит метод Main.
В WebsitesMonitoringService 4 класса:
WebsitesMonitoringService - класс, где описана логика сервиса. Метод Start запускает три task'а для опроса сайтов 
google.com, Microsoft.com, apple.com на доступность по расписанию.
SiteStatus - класс, в котором описаны методы опроса сайтов.
FileWriter - класс для создания файла, куда будут записываться статусы сайтов. При каждом запуске службы файл пересоздается. 
