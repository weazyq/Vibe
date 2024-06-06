CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS public.clients (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	phone varchar NOT NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	isremoved bool DEFAULT false NOT NULL,
	refreshtoken varchar NULL,
	tokencreated timestamptz NOT NULL,
	tokenexpires timestamptz NOT NULL,
	CONSTRAINT clients_unique UNIQUE (id)
);

CREATE TABLE IF NOT EXISTS public.employees (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	phone varchar NOT NULL,
	email varchar NOT NULL,
	createdat timestamptz NOT NULL,
	createdby uuid NULL,
	modifiedat timestamptz NULL,
	updatedby uuid NULL,
	isremoved bool DEFAULT false NOT NULL,
	"password" varchar NOT NULL,
	login varchar NOT NULL,
	CONSTRAINT employees_unique UNIQUE (id)
);

insert into public.employees (id, name, phone, email, createdat, password, login)
values (uuid_generate_v4(), 'Владислав', '+79779221861', 'weazy@internet.ru', '2024-06-06 07:00', '123', 'weazy');

CREATE TABLE IF NOT EXISTS public.phonecodes (
	phone varchar NOT NULL,
	code varchar NOT NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	validityminutes int4 NOT NULL,
	CONSTRAINT phonecodes_unique UNIQUE (phone)
);

CREATE TABLE IF NOT EXISTS public.scooters (
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

CREATE TABLE IF NOT EXISTS public.rents (
	id uuid NOT NULL,
	clientid uuid NOT NULL,
	scooterid uuid NOT NULL,
	price numeric NULL,
	isclosed bool DEFAULT false NOT NULL,
	startedat timestamptz NOT NULL,
	endedat timestamptz NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	CONSTRAINT rents_unique UNIQUE (id),
	CONSTRAINT rents_clients_fk FOREIGN KEY (clientid) REFERENCES public.clients(id),
	CONSTRAINT rents_scooters_fk FOREIGN KEY (scooterid) REFERENCES public.scooters(id)
);


CREATE TABLE IF NOT EXISTS public.sup_supportrequests (
	id uuid NOT NULL,
	title varchar NOT NULL,
	clientid uuid NOT NULL,
	employeeid uuid NULL,
	description varchar NOT NULL,
	createdat timestamptz NOT NULL,
	modifiedat timestamptz NULL,
	isclosed bool NULL,
	isremoved bool DEFAULT false NOT NULL,
	openedat timestamptz NULL,
	CONSTRAINT supportrequests_pk PRIMARY KEY (id),
	CONSTRAINT sup_supportrequests_clients_fk FOREIGN KEY (clientid) REFERENCES public.clients(id),
	CONSTRAINT sup_supportrequests_employees_fk FOREIGN KEY (employeeid) REFERENCES public.employees(id)
);

CREATE TABLE IF NOT EXISTS public.sup_supportmessages (
	id uuid NOT NULL,
	"text" varchar NOT NULL,
	createdat timestamptz NOT NULL,
	createdby uuid NOT NULL,
	modifiedat timestamptz NULL,
	isremoved bool DEFAULT false NOT NULL,
	supportrequestid uuid NOT NULL,
	CONSTRAINT sup_supportmessages_pk PRIMARY KEY (id),
	CONSTRAINT sup_supportmessages_sup_supportrequests_fk FOREIGN KEY (supportrequestid) REFERENCES public.sup_supportrequests(id)
);