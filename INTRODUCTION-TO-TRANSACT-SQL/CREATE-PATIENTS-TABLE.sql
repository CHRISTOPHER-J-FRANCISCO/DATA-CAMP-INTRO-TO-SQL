USE HospitalEmergencyRoomDB; 
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND type in (N'U'))
    BEGIN
        PRINT 'TABLE Patients HAS BEEN CREATED!';
    END
ELSE
    BEGIN
        CREATE TABLE dbo.Patients (
            -- no duplicated to uniquely identify
            PatientID int PRIMARY KEY,
            -- for long names 
            Name nvarchar(256), 
            -- homosapiens don't live that long yet
            Age  tinyint,
            -- let's be real
            Sex varchar(1)
        )
        PRINT 'TABLE Patients CREATED!';
    END