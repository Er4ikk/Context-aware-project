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
7	0103000020E61000000100000005000000D004447901AC264000F676A529404640D004447901AC2640CC97EFF71540464020CE22293BAC2640CC97EFF71540464020CE22293BAC264000F676A529404640D004447901AC264000F676A529404640	20	3
3	0103000020E610000001000000050000000079199CF1B62640CCE9C01EBC3F46400079199CF1B62640C48D23668C3F4640001AFBE093B72640C48D23668C3F4640001AFBE093B72640CCE9C01EBC3F46400079199CF1B62640CCE9C01EBC3F4640	100	82
1	0103000020E61000000100000005000000A00A505ED5B22640FC66D66107404640101EC1A210B32640FC66D66107404640101EC1A210B3264074355B4213404640A00A505ED5B2264074355B4213404640A00A505ED5B22640FC66D66107404640	5	5
6	0103000020E61000000100000005000000E094E0326EAE2640A4DDE41E69404640F0CA19BD5AAE26408026CAE74B404640B034FD7A25AF2640C44171603D40464030CF27146EAF264030E163605D404640E094E0326EAE2640A4DDE41E69404640	20	9
\.


--
-- Data for Name: ParkingEvent; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ParkingEvent" ("ParkingAreaId", "Timestamp", "EventType", id, "UserId", "ParkingCoordinates") FROM stdin;
1	2025-12-16 00:00:00+01	Parking	3	1	\N
3	2025-12-16 00:00:00+01	Parking	5	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	6	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	7	1	\N
3	2025-12-26 11:40:18.654+01	Leaving	8	1	01010000004A76CECBD14333416C7844339C255541
1	2025-12-30 15:35:10.567763+01	Parking	14	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 15:40:08.014454+01	Leaving	15	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 15:43:36.192442+01	Parking	16	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:02:58.656687+01	Leaving	17	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:03:30.031603+01	Parking	18	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:45:34.55346+01	Parking	19	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:45:49.969327+01	Leaving	20	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:47:03.392099+01	Parking	21	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-30 17:47:08.353902+01	Leaving	22	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 10:59:20.329909+01	Leaving	23	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:00:07.618338+01	Parking	24	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:03:23.536941+01	Leaving	25	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:03:51.1207+01	Parking	26	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:05:22.328277+01	Leaving	27	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:06:39.196342+01	Parking	28	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:06:39.196348+01	Parking	29	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:07:36.666712+01	Leaving	30	1	010100000052C0636521B7264097BD7F06AE3F4640
3	2025-12-31 11:08:10.089952+01	Parking	31	1	010100000052C0636521B7264097BD7F06AE3F4640
1	2026-01-02 13:31:01.044601+01	Parking	1	1	0101000000BCD86AC5EDB2264028AAC8320E404640
1	2026-01-02 18:01:19.4848+01	Parking	2	1	0101000000D7ABB51D40BC264097BD7F06AE3F4640
1	2026-01-02 18:57:46.885979+01	Leaving	32	2	0101000000F1E7EE4DE6B226409ECA7C1A0A404640
1	2026-01-02 18:57:46.885977+01	Leaving	33	2	0101000000F1E7EE4DE6B226409ECA7C1A0A404640
1	2026-01-02 18:58:12.983525+01	Parking	34	2	0101000000CB52D83206B326407E15F3860E404640
1	2026-01-02 18:58:25.544288+01	Leaving	35	2	0101000000FB5C6DC5FEB22640561EB77209404640
1	2026-01-02 18:58:39.847351+01	Parking	36	2	01010000004209336DFFB22640E78C28ED0D404640
6	2026-01-02 19:00:57.727312+01	Leaving	37	2	01010000005C877D15F3AE2640A49DAD964E404640
6	2026-01-02 19:01:09.613192+01	Parking	38	2	010100000010406A1327AF2640FFE7305F5E404640
6	2026-01-02 19:01:19.537684+01	Leaving	39	2	0101000000BBD05CA791AE2640EC71CC9F5C404640
6	2026-01-02 19:01:26.534249+01	Parking	40	2	0101000000C971A774B0AE26406F5CD9184E404640
7	2026-01-02 19:03:22.016962+01	Leaving	41	2	0101000000075F984C15AC2640056698ED1D404640
7	2026-01-02 19:03:34.524895+01	Parking	42	2	0101000000D328376416AC2640D23AAA9A20404640
7	2026-01-02 19:03:40.536828+01	Leaving	43	2	01010000001F01C9611DAC26406EDDCD531D404640
7	2026-01-02 19:13:46.702868+01	Leaving	44	2	01010000001F01C9611DAC26406EDDCD531D404640
7	2026-01-02 19:15:21.662642+01	Parking	45	2	01010000007923F3C81FAC2640F5108DEE20404640
\.


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" (id, mail, pwd) FROM stdin;
1	pipponzio@gmail.com	ahahhah
2	g.pippon@gmail.com	aaaa
4	g.pippon3@gmial.com	ssss
5	pippomio@gamil.com	2222
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

SELECT pg_catalog.setval('public."ParkingEvent_id_seq"', 45, true);


--
-- Name: User_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_id_seq"', 5, true);


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


