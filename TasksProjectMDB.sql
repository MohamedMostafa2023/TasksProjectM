-- Create the database
CREATE DATABASE TasksProjectM;
GO

-- Use the database
USE TasksProjectM;
GO

-- Create the Users table
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);
GO

-- Create the TaskStatus table
CREATE TABLE TaskStatus (
    TaskStatusId INT PRIMARY KEY IDENTITY(1,1),
    TaskStatusName NVARCHAR(100) NOT NULL,
	StatusColor NVARCHAR(7) NOT NULL DEFAULT '#FFFFFF',
);
GO

-- Create the TaskGroups table
CREATE TABLE TaskGroups (
    TaskGroupId INT PRIMARY KEY IDENTITY(1,1),
    TaskGroupName NVARCHAR(100) NOT NULL
);
GO

-- Create the Tasks table
CREATE TABLE Tasks (
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    TaskGroupId INT FOREIGN KEY REFERENCES TaskGroups(TaskGroupId) ON DELETE SET NULL,
    TaskName NVARCHAR(100) NOT NULL,
    TaskDescription NVARCHAR(MAX),
    TaskStatusId INT FOREIGN KEY REFERENCES TaskStatus(TaskStatusId) ON DELETE SET NULL
);
GO
