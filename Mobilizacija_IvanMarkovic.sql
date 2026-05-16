IF OBJECT_ID('Mobilizacija', 'U') IS NOT NULL DROP TABLE Mobilizacija;
IF OBJECT_ID('Vojnik', 'U') IS NOT NULL DROP TABLE Vojnik;
IF OBJECT_ID('Korisnik', 'U') IS NOT NULL DROP TABLE Korisnik;
IF OBJECT_ID('Uloga', 'U') IS NOT NULL DROP TABLE Uloga;
IF OBJECT_ID('Jedinica', 'U') IS NOT NULL DROP TABLE Jedinica;
GO

CREATE TABLE Uloga (
    uloga_id INT PRIMARY KEY IDENTITY(1,1),
    naziv_uloga NVARCHAR(20) NOT NULL
);

CREATE TABLE Jedinica (
    jedinica_id INT PRIMARY KEY IDENTITY(1,1),
    naziv_jedinice NVARCHAR(50) NOT NULL,
    lokacija NVARCHAR(50),
    maks_kapacitet INT
);

CREATE TABLE Korisnik (
    korisnik_id INT PRIMARY KEY IDENTITY(1,1),
    email NVARCHAR(100) UNIQUE NOT NULL,
    lozinka NVARCHAR(100) NOT NULL,
    uloga_id INT FOREIGN KEY REFERENCES Uloga(uloga_id),
    jedinica_id INT NULL FOREIGN KEY REFERENCES Jedinica(jedinica_id)
);

CREATE TABLE Vojnik (
    vojnik_id INT PRIMARY KEY IDENTITY(1,1),
    ime NVARCHAR(30),
    prezime NVARCHAR(30),
    jmbg NVARCHAR(13) UNIQUE,
    korisnik_id INT NULL FOREIGN KEY REFERENCES Korisnik(korisnik_id)
);

CREATE TABLE Mobilizacija (
    mobilizacija_id INT PRIMARY KEY IDENTITY(1,1),
    vojnik_id INT FOREIGN KEY REFERENCES Vojnik(vojnik_id),
    jedinica_id INT FOREIGN KEY REFERENCES Jedinica(jedinica_id),
    datum_pocetka DATE DEFAULT GETDATE(),
    status_aktivnosti NVARCHAR(20) 
);
GO

CREATE INDEX IX_Mobilizacija_VojnikId ON Mobilizacija(vojnik_id);
CREATE INDEX IX_Mobilizacija_JedinicaId ON Mobilizacija(jedinica_id);
CREATE INDEX IX_Vojnik_KorisnikId ON Vojnik(korisnik_id);
GO

INSERT INTO Uloga (naziv_uloga) VALUES ('Admin'), ('Korisnik'), ('Komandir');
INSERT INTO Jedinica (naziv_jedinice, lokacija, maks_kapacitet) 
VALUES ('Alfa Squad', 'Beograd', 10), ('Beta Squad', 'Novi Sad', 15);
GO

CREATE PROCEDURE Dodaj_Novog_Korisnika 
    @email NVARCHAR(100), 
    @lozinka NVARCHAR(100), 
    @uloga_id INT,
    @jed_id INT = NULL
AS
BEGIN
    INSERT INTO Korisnik (email, lozinka, uloga_id, jedinica_id)
    VALUES (@email, @lozinka, @uloga_id, @jed_id);
END
GO

CREATE PROCEDURE Registracija_Vojnika @ime NVARCHAR(30), @prezime NVARCHAR(30), @jmbg NVARCHAR(13), @email NVARCHAR(100), @lozinka NVARCHAR(100)
AS
BEGIN
    BEGIN TRANSACTION
    BEGIN TRY
        INSERT INTO Korisnik (email, lozinka, uloga_id) VALUES (@email, @lozinka, 2);
        DECLARE @kid INT = SCOPE_IDENTITY();
        INSERT INTO Vojnik (ime, prezime, jmbg, korisnik_id) VALUES (@ime, @prezime, @jmbg, @kid);
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        RETURN @@ERROR;
    END CATCH
END
GO

CREATE PROCEDURE Rasporedi_Vojnika @vojnik_id INT, @jedinica_id INT, @status NVARCHAR(20)
AS
BEGIN
    DECLARE @trenutno INT = (SELECT COUNT(*) FROM Mobilizacija WHERE jedinica_id = @jedinica_id AND status_aktivnosti = 'Aktivan');
    DECLARE @maks INT = (SELECT maks_kapacitet FROM Jedinica WHERE jedinica_id = @jedinica_id);
    IF (@trenutno < @maks)
    BEGIN
        INSERT INTO Mobilizacija (vojnik_id, jedinica_id, status_aktivnosti) VALUES (@vojnik_id, @jedinica_id, @status);
        RETURN 1;
    END
    ELSE RETURN 0;
END
GO

CREATE PROCEDURE Izmena_Statusa_Vojnika @vojnik_id INT, @novi_status NVARCHAR(20)
AS
BEGIN
    UPDATE Mobilizacija SET status_aktivnosti = @novi_status WHERE vojnik_id = @vojnik_id;
END
GO

CREATE PROCEDURE Spisak_Vojnika_Po_Jedinici @jedinica_id INT
AS
BEGIN
    SELECT V.vojnik_id, V.ime, V.prezime, M.status_aktivnosti, M.datum_pocetka, J.naziv_jedinice
    FROM Vojnik V
    JOIN Mobilizacija M ON V.vojnik_id = M.vojnik_id
    JOIN Jedinica J ON M.jedinica_id = J.jedinica_id
    WHERE M.jedinica_id = @jedinica_id;
END
GO

CREATE PROCEDURE Proveri_Pristup @korisnik_id INT, @target_jedinica_id INT
AS
BEGIN
    DECLARE @uloga_id INT;
    DECLARE @u_jedinica INT;
    SELECT @uloga_id = uloga_id, @u_jedinica = jedinica_id FROM Korisnik WHERE korisnik_id = @korisnik_id;
    IF (@uloga_id = 1) SELECT 'WRITE' AS Pristup;
    ELSE IF (@uloga_id = 3 AND @u_jedinica = @target_jedinica_id) SELECT 'WRITE' AS Pristup;
    ELSE SELECT 'READ' AS Pristup;
END
GO

CREATE PROCEDURE Statistika_Jedinice @jedinica_id INT
AS
BEGIN
    SELECT status_aktivnosti, COUNT(*) AS broj_vojnika
    FROM Mobilizacija
    WHERE jedinica_id = @jedinica_id
    GROUP BY status_aktivnosti;
END
GO