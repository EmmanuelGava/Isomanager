-- Crear la base de datos Isomanager
CREATE DATABASE Isomanager;
GO

USE Isomanager;
GO

-- Crear la tabla Contexto
CREATE TABLE Contexto (
    ContextoId INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(500) NOT NULL,
    FechaCreacion DATETIME NOT NULL
);
GO

-- Crear la tabla Norma (relacionada con Contexto)
CREATE TABLE Norma (
    NormaId INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    Version NVARCHAR(50),
    Estado NVARCHAR(50),
    Responsable NVARCHAR(255),
    FechaCreacion DATETIME NOT NULL,
    ContextoId INT NOT NULL,
    FOREIGN KEY (ContextoId) REFERENCES Contexto(ContextoId) ON DELETE CASCADE
);
GO

-- Crear la tabla Foda (relacionada con Contexto)
CREATE TABLE Foda (
    FodaId INT PRIMARY KEY IDENTITY(1,1),
    ContextoId INT NOT NULL,
    Fortalezas NVARCHAR(MAX),
    Oportunidades NVARCHAR(MAX),
    Debilidades NVARCHAR(MAX),
    Amenazas NVARCHAR(MAX),
    FOREIGN KEY (ContextoId) REFERENCES Contexto(ContextoId) ON DELETE CASCADE
);
GO

-- Crear la tabla AlcanceSistemaGestion (relacionada con Contexto)
CREATE TABLE AlcanceSistemaGestion (
    AlcanceId INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(500) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    ContextoId INT NOT NULL,
    FOREIGN KEY (ContextoId) REFERENCES Contexto(ContextoId) ON DELETE CASCADE
);
GO

-- Crear la tabla FactoresExternos (relacionada con Contexto)
CREATE TABLE FactoresExternos (
    FactoresExternosId INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(500) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    ContextoId INT NOT NULL,
    FOREIGN KEY (ContextoId) REFERENCES Contexto(ContextoId) ON DELETE CASCADE
);
GO

-- Crear la tabla Proceso (relacionada con Contexto)
CREATE TABLE Proceso (
    ProcesoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Propietario NVARCHAR(255),
    Objetivo NVARCHAR(500),
    ContextoId INT NOT NULL,
    FOREIGN KEY (ContextoId) REFERENCES Contexto(ContextoId) ON DELETE CASCADE
);
GO

-- Crear la tabla Document (sin relación con Contexto)
CREATE TABLE Document (
    Id NVARCHAR(255) PRIMARY KEY,
    Version NVARCHAR(50),
    Status NVARCHAR(50),
    ResponsiblePerson NVARCHAR(255),
    LastModified DATETIME NOT NULL
);
GO

-- Crear la tabla Mejora (sin relación con Contexto)
CREATE TABLE Mejora (
    Proceso NVARCHAR(255) PRIMARY KEY,
    AreaMejora NVARCHAR(255),
    AccionRecomendada NVARCHAR(500),
    Responsable NVARCHAR(255),
    FechaImplementacion DATETIME NOT NULL
);
GO

-- Crear la tabla Usuario (sin relación con Contexto)
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Correo NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(50) NOT NULL
);
GO
