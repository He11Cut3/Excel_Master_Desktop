-- �������� ���� --

create database ExcelMaster_db
go

-- ����� --

-- ������������ ��� ���� --

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
ExcelMaster_Educational_�ctivities_id int,
ExcelMaster_Educational_Monitoring_id int,
ExcelMaster_State_�ontrol_id int,
)
create table ExcelMaster_Branch
(
ExcelMaster_Branch_id int primary key identity(1,1),
ExcelMaster_Branch_Name nvarchar(50),
)
create table ExcelMaster_Educational_�ctivities
(
ExcelMaster_Educational_�ctivities_id int primary key identity(1,1),
ExcelMaster_Educational_�ctivities_Name nvarchar(50),
ExcelMaster_Educational_�ctivities_AP1 int,
ExcelMaster_Educational_�ctivities_AP2 int,
ExcelMaster_Educational_�ctivities_AP3 int,
ExcelMaster_Educational_�ctivities_AP4 int,
ExcelMaster_Educational_�ctivities_Total int,
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
create table ExcelMaster_State_�ontrol
(
ExcelMaster_State_�ontrol_id int primary key identity(1,1),
ExcelMaster_State_�ontrol_Name nvarchar(50),
ExcelMaster_State_�ontrol_AP1 int,
ExcelMaster_State_�ontrol_AP2 int,
ExcelMaster_State_�ontrol_AP3 int,
ExcelMaster_State_�ontrol_AP4 int,
ExcelMaster_State_�ontrol_Total int,
)
