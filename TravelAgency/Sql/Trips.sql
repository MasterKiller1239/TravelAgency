-- Utwórz bazê danych (jeœli jeszcze nie istnieje)
CREATE DATABASE TravelAgencyDB;
GO

-- U¿yj bazy danych
USE TravelAgencyDB;
GO

-- Utwórz tabelê Trips
CREATE TABLE Trips (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price INT NOT NULL
);
GO

-- Dodaj przyk³adowe dane
INSERT INTO Trips (Name, Description, Price) VALUES
('Italy - Rome', '5 days of sightseeing', 2500),
('Greece - Athens', 'A week of sunny vacation', 3200),
('Japan - Tokyo', '7 days exploring the city', 5200);
GO
