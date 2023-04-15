USE [master]
GO

CREATE DATABASE [UsersDatabase]
GO

USE [UsersDatabase]
GO

CREATE LOGIN users_admin WITH PASSWORD=N'UsersAdmin123', DEFAULT_DATABASE=[UsersDatabase], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE USER users_admin FOR LOGIN users_admin
GO

EXEC sp_addrolemember 'db_owner', 'users_admin'
GO

