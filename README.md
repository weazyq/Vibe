[Генерация таблиц БД.txt](https://github.com/weazyq/Vibe/files/14935530/default.txt)# Vibe.Backoffice

Для полной работы:
1) Развернуть базу данных с таблицами [UНазвание Database: vibe
User Id: postgres
Password: 9182kJKjk2hfj2!!jkdf3

-- public.clients определение

CREATE TABLE public.clients (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	phone varchar NOT NULL,
	createdat timestamptz NOT NULL,
	createdby uuid NULL,
	modifiedat timestamptz NULL,
	updatedby uuid NULL,
	isremoved bool DEFAULT false NOT NULL,
	CONSTRAINT clients_unique UNIQUE (id)
);

-- public.employees определение

CREATE TABLE public.employees (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	phone varchar NOT NULL,
	email varchar NOT NULL,
	"role" int4 NOT NULL,
	createdat timestamptz NOT NULL,
	createdby uuid NULL,
	modifiedat timestamptz NULL,
	updatedby uuid NULL,
	isremoved bool DEFAULT false NOT NULL,
	CONSTRAINT employees_unique UNIQUE (id)
);

-- public.phonecodes определение

CREATE TABLE public.phonecodes (
	phone varchar NOT NULL,
	code varchar NOT NULL,
	CONSTRAINT phonecodes_unique UNIQUE (phone)
);

-- public.rents определение

CREATE TABLE public.rents (
	id uuid NOT NULL,
	clientid uuid NOT NULL,
	scooterid uuid NOT NULL,
	price numeric NULL,
	isclosed bool DEFAULT false NOT NULL,
	startedat timestamptz NOT NULL,
	endedat timestamptz NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	CONSTRAINT rents_unique UNIQUE (id)
);

-- public.scooters определение

CREATE TABLE public.scooters (
	id uuid NOT NULL,
	serialnumber varchar NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	latitude numeric NULL,
	longitude numeric NULL,
	charge numeric NULL,
	state int4 NULL,
	CONSTRAINT scooters_unique UNIQUE (id)
);

-- public.users определение

CREATE TABLE public.users (
	id uuid NOT NULL,
	employeeid uuid NULL,
	clientid uuid NULL,
	refreshtoken varchar NULL,
	tokencreated timestamptz NOT NULL,
	tokenexpires timestamptz NOT NULL,
	createdat timestamptz NOT NULL,
	createdby uuid NULL,
	modifiedat timestamptz NULL,
	updatedby uuid NULL,
	isremoved bool DEFAULT false NOT NULL,
	CONSTRAINT users_unique UNIQUE (id)
);ploading Генерация таблиц БД.txt…]()

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
