IF EXISTS (SELECT * FROM sys.databases WHERE name = 'HospitalEmergencyRoomDB')
    BEGIN
        PRINT 'DATABASE HospitalEmergencyRoomDB HAS BEEN CREATED!';
    END
ELSE
    BEGIN
        CREATE DATABASE HospitalEmergencyRoomDB;
        PRINT 'DATABASE HospitalEmergencyRoomDB CREATED!';
    END