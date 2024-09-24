-- Create Employees table without foreign key constraint
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    EmployeeCode NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    DepartmentId INT NOT NULL, 
    HireDate DATE NOT NULL,
    EmployeeImagePath NVARCHAR(255), 
    Age INT, 
    Gender NVARCHAR(10), 
    DateOfBirth DATE, 
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    INDEX idx_EmployeeCode (EmployeeCode),
    INDEX idx_Email (Email)
);

-- Create Departments table without foreign key constraint
CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    DepartmentLead INT, -- Will add FK constraint later
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    INDEX idx_DepartmentName (DepartmentName)
);

-- Add foreign key constraint from Employees to Departments
ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
REFERENCES Departments(DepartmentId);

-- Add foreign key constraint from Departments to Employees (for DepartmentLead)
ALTER TABLE Departments
ADD CONSTRAINT FK_Departments_Employees FOREIGN KEY (DepartmentLead)
REFERENCES Employees(EmployeeId);

--select * from Departments


-- Updated Users table with DateOfBirth column
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    DateOfBirth DATE, 
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    INDEX idx_Username (Username),
    INDEX idx_Email (Email)
);



-------------------Insert Query For Employee & Department Table-----------------------------

INSERT INTO Departments (DepartmentName, Description, DepartmentLead, CreatedAt, IsActive)
VALUES 
    ('Human Resources', 'Handles employee-related functions', NULL, GETDATE(), 1),
    ('Engineering', 'Develops and maintains products', NULL, GETDATE(), 1),
    ('Marketing', 'Responsible for marketing and outreach', NULL, GETDATE(), 1);
	
INSERT INTO Employees (EmployeeCode, Name, Email, DepartmentId, HireDate, EmployeeImagePath, Age, Gender, DateOfBirth, CreatedAt, IsActive)
VALUES 
    ('EMP001', 'Jay Chan', 'Jay@gmail.com', 1, '2022-01-15', 'Jay.jpg', 30, 'Male', '1992-05-14', GETDATE(), 1),
    ('EMP002', 'Smith', 'smith@gmail.com', 2, '2021-03-10', 'smith.jpg', 28, 'Male', '1994-08-22', GETDATE(), 1),
    ('EMP003', 'Bala', 'bala@gmail.com', 2, '2023-02-20', 'bala.jpg', 35, 'Male', '1989-12-05', GETDATE(), 1),
    ('EMP004', 'sathish', 'sathish@gmail.com', 3, '2020-07-25', 'sathish.jpg', 32, 'Male', '1990-10-11', GETDATE(), 1);

INSERT INTO Users (Username, Email, PasswordHash, DateOfBirth, CreatedAt, IsActive)
VALUES 
    ('Vikram', 'Vikram@gmail.com', 'hashed_password_3', '1988-12-05', GETDATE(), 1),
    ('Priyanka', 'Priyanka@example.com', 'hashed_password_4', '1992-02-18', GETDATE(), 1);



--select * from Employees
--select * from Departments
--select * from Users