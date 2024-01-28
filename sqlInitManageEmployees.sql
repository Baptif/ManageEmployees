CREATE DATABASE [ManageEmployees]
GO

USE [ManageEmployees]
GO

-- Cr�ation du d�partement
CREATE TABLE Departments
(
	DepartmentId INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL UNIQUE,
	Address VARCHAR(150),
	Description VARCHAR(300),
)
GO
-- Mieux d'utiliser ALTER TABLE car on peut choisir le nom de la contrainte
ALTER TABLE Departments ADD CONSTRAINT UK_Departments_Name UNIQUE (Name)
GO

-- Cr�ation de l'employ�
CREATE TABLE Employees
(
	EmployeeId INT IDENTITY(1000, 1) PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	BirthdDate DATETIME NOT NULL,
	Email VARCHAR(100) NOT NULL,
	PhoneNumber VARCHAR(13),
	Position VARCHAR(150)
)
GO

ALTER TABLE Employees ADD CONSTRAINT UK_Employees_Email UNIQUE (Email)
GO

-- Cr�ation de l'association Employ� et d�partement
CREATE TABLE EmployeesDepartments
(
	EmployeesDepartmentsId INT IDENTITY PRIMARY KEY,
	DepartmentId INT NOT NULL,
	EmployeeId INT NOT NULL,
)
GO

ALTER TABLE EmployeesDepartments ADD CONSTRAINT FK_EmployeesDepartments_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employees (EmployeeId)
GO
ALTER TABLE EmployeesDepartments ADD CONSTRAINT FK_EmployeesDepartments_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES Departments (DepartmentId)
GO

-- Cr�ation des pr�sences
CREATE TABLE Attendances
(
	AttendanceId INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME,
)
GO

ALTER TABLE Attendances ADD CONSTRAINT FK_Attendances_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employees (EmployeeId)
GO

-- Cr�ation du statut des cong�s
CREATE TABLE LeaveRequestStatus
(
	LeaveRequestStatusId INT IDENTITY PRIMARY KEY,
	Status VARCHAR(50) NOT NULL UNIQUE,
)
GO

INSERT INTO LeaveRequestStatus (Status) VALUES
	('Pending'),
	('Accepted'),
	('Rejected')
GO

-- Cr�ation des cong�s
CREATE TABLE LeaveRequests
(
	LeaveRequestId INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	LeaveRequestStatusId INT NOT NULL,
	RequestDate DATETIME NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME,
)
GO

ALTER TABLE LeaveRequests ADD CONSTRAINT FK_LeaveRequests_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employees (EmployeeId)
GO

ALTER TABLE LeaveRequests ADD CONSTRAINT FK_LeaveRequests_LeaveRequestStatusId FOREIGN KEY (LeaveRequestStatusId) REFERENCES LeaveRequestStatus (LeaveRequestStatusId)
GO