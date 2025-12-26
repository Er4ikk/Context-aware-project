--
-- PostgreSQL database dump
--



-- Dumped from database version 14.20 (Ubuntu 14.20-0ubuntu0.22.04.1)
-- Dumped by pg_dump version 14.20 (Ubuntu 14.20-0ubuntu0.22.04.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: postgis; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS postgis WITH SCHEMA public;


--
-- Name: EXTENSION postgis; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION postgis IS 'PostGIS geometry and geography spatial types and functions';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: ParkingArea; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ParkingArea" (
    "Id" bigint NOT NULL,
    "Area" public.geometry(Polygon,4326),
    "MaxCapacity" bigint,
    "PlacesLeft" bigint
);


ALTER TABLE public."ParkingArea" OWNER TO postgres;

--
-- Name: ParkingArea_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."ParkingArea" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."ParkingArea_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: ParkingEvent; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ParkingEvent" (
    "ParkingAreaId" bigint NOT NULL,
    "Timestamp" timestamp with time zone,
    "EventType" text,
    id bigint NOT NULL,
    "UserId" bigint,
    "ParkingCoordinates" public.geometry
);


ALTER TABLE public."ParkingEvent" OWNER TO postgres;

--
-- Name: ParkingEvent_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."ParkingEvent_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."ParkingEvent_id_seq" OWNER TO postgres;

--
-- Name: ParkingEvent_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."ParkingEvent_id_seq" OWNED BY public."ParkingEvent".id;


--
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    id bigint NOT NULL,
    mail text,
    pwd text
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- Name: User_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."User" ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."User_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: ParkingEvent id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingEvent" ALTER COLUMN id SET DEFAULT nextval('public."ParkingEvent_id_seq"'::regclass);


--
-- Data for Name: ParkingArea; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ParkingArea" ("Id", "Area", "MaxCapacity", "PlacesLeft") FROM stdin;
1	0103000020E61000000100000005000000A00A505ED5B22640FC66D66107404640101EC1A210B32640FC66D66107404640101EC1A210B3264074355B4213404640A00A505ED5B2264074355B4213404640A00A505ED5B22640FC66D66107404640	5	5
3	0103000020E610000001000000050000000079199CF1B62640CCE9C01EBC3F46400079199CF1B62640C48D23668C3F4640001AFBE093B72640C48D23668C3F4640001AFBE093B72640CCE9C01EBC3F46400079199CF1B62640CCE9C01EBC3F4640	100	80
7	0103000020E61000000100000005000000D004447901AC264000F676A529404640D004447901AC2640CC97EFF71540464020CE22293BAC2640CC97EFF71540464020CE22293BAC264000F676A529404640D004447901AC264000F676A529404640	20	4
6	0103000020E61000000100000005000000E094E0326EAE2640A4DDE41E69404640F0CA19BD5AAE26408026CAE74B404640B034FD7A25AF2640C44171603D40464030CF27146EAF264030E163605D404640E094E0326EAE2640A4DDE41E69404640	20	9
\.


--
-- Data for Name: ParkingEvent; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ParkingEvent" ("ParkingAreaId", "Timestamp", "EventType", id, "UserId", "ParkingCoordinates") FROM stdin;
1	2022-10-10 11:30:30+02	Parking	1	1	\N
1	2025-12-19 18:56:26.775+01	Parking	2	1	\N
1	2025-12-16 00:00:00+01	Parking	3	1	\N
3	2025-12-16 00:00:00+01	Parking	5	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	6	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	7	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	8	1	01010000004A76CECBD14333416C7844339C255541
\.


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" (id, mail, pwd) FROM stdin;
1	pipponzio@gmail.com	ahahhah
2	g.pippon@gmail.com	aaaa
4	g.pippon3@gmial.com	ssss
\.


--
-- Data for Name: spatial_ref_sys; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.spatial_ref_sys (srid, auth_name, auth_srid, srtext, proj4text) FROM stdin;
\.


--
-- Name: ParkingArea_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."ParkingArea_Id_seq"', 7, true);


--
-- Name: ParkingEvent_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."ParkingEvent_id_seq"', 8, true);


--
-- Name: User_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_id_seq"', 4, true);


--
-- Name: ParkingArea ParkingArea_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingArea"
    ADD CONSTRAINT "ParkingArea_pkey" PRIMARY KEY ("Id");


--
-- Name: ParkingEvent ParkingEvent_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingEvent"
    ADD CONSTRAINT "ParkingEvent_pkey" PRIMARY KEY (id);


--
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (id);


--
-- Name: fki_ParkingAreaIdForeignKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_ParkingAreaIdForeignKey" ON public."ParkingEvent" USING btree ("ParkingAreaId");


--
-- Name: fki_ParkingEventFOreignKey; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_ParkingEventFOreignKey" ON public."ParkingEvent" USING btree ("ParkingAreaId");


--
-- Name: fki_foreignKeyUserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "fki_foreignKeyUserId" ON public."ParkingEvent" USING btree ("UserId");


--
-- Name: ParkingEvent ParkingAreaIdForeignKey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingEvent"
    ADD CONSTRAINT "ParkingAreaIdForeignKey" FOREIGN KEY ("ParkingAreaId") REFERENCES public."ParkingArea"("Id") ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- Name: ParkingEvent ParkingEventFOreignKey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingEvent"
    ADD CONSTRAINT "ParkingEventFOreignKey" FOREIGN KEY ("ParkingAreaId") REFERENCES public."ParkingEvent"(id) NOT VALID;


--
-- Name: ParkingEvent foreignKeyUserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ParkingEvent"
    ADD CONSTRAINT "foreignKeyUserId" FOREIGN KEY ("UserId") REFERENCES public."User"(id) NOT VALID;


--
-- PostgreSQL database dump complete
--

