CREATE DATABASE Colegio 
USE Colegio

BEGIN TRANSACTION
CREATE TABLE Student
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Phone] NVARCHAR(12) NOT NULL,
    [Email] NVARCHAR(80) NOT NULL,
    [Birthdate] DATETIME NOT NULL,
    [Create_Date] DATETIME NOT NULL,
    [Updated_Date] DATETIME NOT NULL,

    CONSTRAINT [PK_Student_Id] PRIMARY KEY ([Id])
);
GO

CREATE INDEX IX_PrimaryKey ON STUDENT ([Id]) 
GO

CREATE TABLE Course
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(50) NOT NULL,
    [Resume] TEXT NULL,
    [Created_Date] DATETIME NOT NULL,
    [Updated_At] DATETIME NOT NULL,
    CONSTRAINT [PK_Course_Id] PRIMARY KEY ([Id])

);
GO

CREATE  INDEX IX_PrimaryKey ON [Course] ([Id])
GO

CREATE TABLE Student_Course(
        [Course_Id] UNIQUEIDENTIFIER NOT NULL,
        [Student_Id] UNIQUEIDENTIFIER NOT NULL,
        [Active] BIT NOT NULL,
        [Created_Date] DATETIME NOT NULL,
        [Updated_At] DATETIME NOT NULL,

        CONSTRAINT [PK_StudentCourse_Id] PRIMARY KEY ([Course_Id], [Student_Id]),
        CONSTRAINT [FK_Student_Student_Id] FOREIGN KEY ([Student_Id]) REFERENCES [Student] ([Id]),
        CONSTRAINT [FK_Student_Course_Id] FOREIGN KEY ([Course_Id]) REFERENCES [Course] ([Id])
);
GO

COMMIT



create table Users(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Phone] nvarchar(100) NOT NULL,
    [Birthdate] DATETIME not null,

    CONSTRAINT [PK_Users_Id] PRIMARY KEY ([Id])

);
GO

-- Create a nonclustered index with or without a unique constraint
-- Or create a clustered index on table '[TableName]' in schema '[dbo]' in database '[DatabaseName]'
CREATE  INDEX IX_Users_Id ON [Users] ([Id])
GO
CREATE  INDEX IX_Users_Email ON [Users] ([Email])
GO
CREATE  INDEX IX_Users_Phone ON [Users] ([Email])
GO

