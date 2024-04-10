# Vibe.Backoffice

Для полной работы:
1) Развернуть базу данных с таблицами
2) Запустить Vibe.BackOffice.Server
3) Сделать Publish Vibe.VirtualScooter проекта
4) В компонентах Windows включить IIS службы
5) При необходимости установить модули: [RewriteModule](https://www.iis.net/downloads/microsoft/url-rewrite), [AspNetCoreModuleV2](https://community.chocolatey.org/packages/dotnet-aspnetcoremodule-v2), [CorsModule](https://www.iis.net/downloads/microsoft/iis-cors-module)
6) В папку C:\inetpub\wwwroot\ скопировать содержимое из Publish (bin/release/publish)
7) Настроить pull приложений: Версия CLR .NET = "Без управляемого кода"
8) В "Пулы приложений","Дополнительные параметры"
• "(Общие)" - "Режим запуска" = AlwaysRunning
• "Модель процесса" - "Тайм-аут простоя (в минутах)" = 0
9) Добавить сайт указав папку из C:\inetpub\wwwroot
10) Настроить привязку сайта:
  Имя узла: virtual.scooter{НомерСамоката}.api.ru // (Вместо номера самоката выбираем любой номер)
11) Редактируем конфигурацию сайта:
  Устанавливаем 3 переменных окружения:
  1) SCOOTER_SERIALNUMBER - любой номер для самоката, главное чтобы был такой же как номер самоката из пункта 10
  2) SCOOTER_LATITUDE - любая широта
  3) SCOOTER_LONGITUDE - любая долгота
12) Настройка сайта:
Действия - "Дополнительные параметры"
[preloadEnabled] Предварительная установка включена = true
13) Перезапустить самокат (сайт) в IIS
