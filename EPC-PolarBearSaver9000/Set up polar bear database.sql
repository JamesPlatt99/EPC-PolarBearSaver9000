If NOT EXISTS(SELECT * FROM master..sysdatabases WHERE [name] = 'EPC_PolarBearSaver9001')
BEGIN
	CREATE DATABASE EPC_PolarBearSaver9001;
END


USE EPC_PolarBearSaver9001
GO
BEGIN TRAN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	-- Address table
	IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
		WHERE TABLE_NAME = 'Address')
	BEGIN
		CREATE TABLE [Address] (ID INTEGER PRIMARY KEY IDENTITY(0,1), PostCode NVARCHAR(8), AddressLine1 NVARCHAR(255), UserID NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id));
	END
	
	-- Friends table
	IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
		WHERE TABLE_NAME = 'Friends')
	BEGIN
		CREATE TABLE Friends (ID INTEGER PRIMARY KEY IDENTITY(0,1) NOT NULL, user1ID NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id), user2ID NVARCHAR(450) NOT NULL FOREIGN KEY REFERENCES AspNetUsers(Id));
	END
ROLLBACK