-- Ñîçäàíèå áàçû --

create database ExcelMaster_db
go

-- Êîíåö --

-- Èñïîëüçîâàòü äëÿ áàçû --

use ExcelMaster_db
go

create table ExcelMaster_Users
(
ExcelMaster_Users_id int primary key identity(1,1),
ExcelMaster_Users_Login nvarchar(30),
ExcelMaster_Users_Password nvarchar(30),
)
create table ExcelMaster_MainWindow
(
ExcelMaster_MainWindow_id int primary key identity(1,1),
ExcelMaster_Branch_id int,
ExcelMaster_Educational_Àctivities_id int,
ExcelMaster_Educational_Monitoring_id int,
ExcelMaster_State_Ñontrol_id int,
)
create table ExcelMaster_Branch
(
ExcelMaster_Branch_id int primary key identity(1,1),
ExcelMaster_Branch_Name nvarchar(50),
)
create table ExcelMaster_Educational_Àctivities
(
ExcelMaster_Educational_Àctivities_id int primary key identity(1,1),
ExcelMaster_Educational_Àctivities_Name nvarchar(50),
ExcelMaster_Educational_Àctivities_AP1 int,
ExcelMaster_Educational_Àctivities_AP2 int,
ExcelMaster_Educational_Àctivities_AP3 int,
ExcelMaster_Educational_Àctivities_AP4 int,
ExcelMaster_Educational_Àctivities_Total int,
)
create table ExcelMaster_Educational_Monitoring
(
ExcelMaster_Educational_Monitoring_id int primary key identity(1,1),
ExcelMaster_Educational_Monitoring_Name nvarchar(50),
ExcelMaster_Educational_Monitoring_AP1 int,
ExcelMaster_Educational_Monitoring_AP2 int,
ExcelMaster_Educational_Monitoring_AP3 int,
ExcelMaster_Educational_Monitoring_AP4 int,
ExcelMaster_Educational_Monitoring_Total int,
)
create table ExcelMaster_State_Ñontrol
(
ExcelMaster_State_Ñontrol_id int primary key identity(1,1),
ExcelMaster_State_Ñontrol_Name nvarchar(50),
ExcelMaster_State_Ñontrol_AP1 int,
ExcelMaster_State_Ñontrol_AP2 int,
ExcelMaster_State_Ñontrol_AP3 int,
ExcelMaster_State_Ñontrol_AP4 int,
ExcelMaster_State_Ñontrol_Total int,
)
