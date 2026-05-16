-- ============================================================
--  Mobilizacija_Ivan_Markovic  —  Kompletna baza v3
--  Kompatibilna sa C# kodom projekta
-- ============================================================

IF DB_ID('Mobilizacija_Ivan_Markovic') IS NOT NULL
BEGIN
    ALTER DATABASE Mobilizacija_Ivan_Markovic SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Mobilizacija_Ivan_Markovic;
END
GO
CREATE DATABASE Mobilizacija_Ivan_Markovic;
GO
USE Mobilizacija_Ivan_Markovic;
GO

-- ============================================================
--  1. LOOKUP TABELE
-- ============================================================

CREATE TABLE Uloga (
    uloga_id    INT PRIMARY KEY IDENTITY(1,1),
    naziv_uloga NVARCHAR(20) NOT NULL UNIQUE
);

-- Srpski vojni cinovi sa nivoom i kategorijom
CREATE TABLE Cin (
    cin_id     INT PRIMARY KEY IDENTITY(1,1),
    naziv_cina NVARCHAR(50) NOT NULL UNIQUE,
    nivo       INT NOT NULL UNIQUE,
    kategorija NVARCHAR(20) NOT NULL   -- 'Vojnik','Podoficir','Oficir','General'
);

-- Tip jedinice — odredjuje minimalni cin komandanta
CREATE TABLE TipJedinice (
    tip_id             INT PRIMARY KEY IDENTITY(1,1),
    naziv_tipa         NVARCHAR(50) NOT NULL UNIQUE,
    min_cin_komandanta INT NOT NULL    -- minimalni nivo cina
);

-- ============================================================
--  2. HIJERARHIJA  (Divizija -> Puk -> Bataljon)
-- ============================================================

CREATE TABLE Divizija (
    divizija_id    INT PRIMARY KEY IDENTITY(1,1),
    naziv_divizije NVARCHAR(100) NOT NULL UNIQUE,
    lokacija      NVARCHAR(100),
    opis          NVARCHAR(255)
);

CREATE TABLE Puk (
    puk_id     INT PRIMARY KEY IDENTITY(1,1),
    naziv_puka NVARCHAR(100) NOT NULL,
    divizija_id INT NOT NULL FOREIGN KEY REFERENCES Divizija(divizija_id),
    tip_id     INT NOT NULL FOREIGN KEY REFERENCES TipJedinice(tip_id),
    lokacija   NVARCHAR(100)
);

-- ============================================================
--  3. KORISNICI  (mora pre Vojnik i Jedinica)
-- ============================================================

CREATE TABLE Korisnik (
    korisnik_id INT PRIMARY KEY IDENTITY(1,1),
    email       NVARCHAR(100) UNIQUE NOT NULL,
    lozinka     NVARCHAR(100) NOT NULL,
    uloga_id    INT NOT NULL FOREIGN KEY REFERENCES Uloga(uloga_id),
    jedinica_id INT NULL    -- popunjava se naknadno FK-om
);

-- ============================================================
--  4. KOMANDANT  (odvojen entitet, nije obavezno vojnik)
-- ============================================================

CREATE TABLE Komandant (
    komandant_id INT PRIMARY KEY IDENTITY(1,1),
    ime          NVARCHAR(50) NOT NULL,
    prezime      NVARCHAR(50) NOT NULL,
    cin_id       INT NOT NULL FOREIGN KEY REFERENCES Cin(cin_id),
    jmbg         NVARCHAR(13) UNIQUE,
    kontakt      NVARCHAR(100)
);

-- ============================================================
--  5. JEDINICA (Bataljon)
-- ============================================================

CREATE TABLE Jedinica (
    jedinica_id    INT PRIMARY KEY IDENTITY(1,1),
    naziv_jedinice NVARCHAR(100) NOT NULL,
    puk_id         INT NOT NULL FOREIGN KEY REFERENCES Puk(puk_id),
    lokacija       NVARCHAR(100),
    maks_kapacitet INT NOT NULL DEFAULT 100,
    komandant_id   INT NULL FOREIGN KEY REFERENCES Komandant(komandant_id)
);

-- Dodaj FK na Korisnik.jedinica_id sada kad Jedinica postoji
ALTER TABLE Korisnik
    ADD CONSTRAINT FK_Korisnik_Jedinica
    FOREIGN KEY (jedinica_id) REFERENCES Jedinica(jedinica_id);

-- ============================================================
--  6. VOJNIK
-- ============================================================

CREATE TABLE Vojnik (
    vojnik_id   INT PRIMARY KEY IDENTITY(1,1),
    ime         NVARCHAR(30),
    prezime     NVARCHAR(30),
    jmbg        NVARCHAR(13) UNIQUE NOT NULL,
    cin_id      INT NOT NULL FOREIGN KEY REFERENCES Cin(cin_id) DEFAULT 1,
    korisnik_id INT NULL FOREIGN KEY REFERENCES Korisnik(korisnik_id)
);

-- ============================================================
--  7. MOBILIZACIJA
-- ============================================================

CREATE TABLE Mobilizacija (
    mobilizacija_id   INT PRIMARY KEY IDENTITY(1,1),
    vojnik_id         INT NOT NULL FOREIGN KEY REFERENCES Vojnik(vojnik_id),
    jedinica_id       INT NOT NULL FOREIGN KEY REFERENCES Jedinica(jedinica_id),
    datum_pocetka     DATE DEFAULT GETDATE(),
    status_aktivnosti NVARCHAR(20) NOT NULL DEFAULT N'Rezerva'
        CHECK (status_aktivnosti IN (N'Aktivan', N'Rezerva', N'Otpusten'))
);
GO

-- ============================================================
--  8. INDEKSI
-- ============================================================

CREATE INDEX IX_Mobilizacija_VojnikId   ON Mobilizacija(vojnik_id);
CREATE INDEX IX_Mobilizacija_JedinicaId ON Mobilizacija(jedinica_id);
CREATE INDEX IX_Vojnik_KorisnikId       ON Vojnik(korisnik_id);
CREATE INDEX IX_Korisnik_UlogaId        ON Korisnik(uloga_id);
CREATE INDEX IX_Korisnik_JedinicaId     ON Korisnik(jedinica_id);
CREATE INDEX IX_Puk_DivizaId           ON Puk(divizija_id);
CREATE INDEX IX_Jedinica_PukId         ON Jedinica(puk_id);
CREATE INDEX IX_Jedinica_KomandantId   ON Jedinica(komandant_id);
GO

-- ============================================================
--  9. POCETNI PODACI
-- ============================================================

INSERT INTO Uloga (naziv_uloga) VALUES ('Admin'), ('Korisnik'), ('Komandir');

INSERT INTO Cin (naziv_cina, nivo, kategorija) VALUES
    (N'Vojnik',              1,  N'Vojnik'),
    (N'Razvodnik',           2,  N'Vojnik'),
    (N'Desetar',             3,  N'Podoficir'),
    (N'Mladi vodnik',        4,  N'Podoficir'),
    (N'Vodnik',              5,  N'Podoficir'),
    (N'Stariji vodnik',      6,  N'Podoficir'),
    (N'Zastavnik',           7,  N'Podoficir'),
    (N'Potporucnik',         8,  N'Oficir'),
    (N'Porucnik',            9,  N'Oficir'),
    (N'Kapetan',             10, N'Oficir'),
    (N'Major',               11, N'Oficir'),
    (N'Potpukovnik',         12, N'Oficir'),
    (N'Pukovnik',            13, N'Oficir'),
    (N'Brigadni general',    14, N'General'),
    (N'General',             15, N'General');

INSERT INTO TipJedinice (naziv_tipa, min_cin_komandanta) VALUES
    (N'Oklopna',       10),
    (N'Mehanizovana',  10),
    (N'Artiljerijska', 10),
    (N'Motorizovana',   9),
    (N'Inzenjerijska',  9),
    (N'Izvidjacka',     9),
    (N'Logisticka',     8);

-- Primer podataka
INSERT INTO Divizija (naziv_divizije, lokacija, opis) VALUES
    (N'1. oklopna divizija',      N'Nis',     N'Glavna oklopna snaga'),
    (N'3. motorizovana divizija', N'Beograd', N'Motorizovana podrska');

INSERT INTO Puk (naziv_puka, divizija_id, tip_id, lokacija) VALUES
    (N'5. mehanizovani puk',    1, 2, N'Nis'),
    (N'12. artiljerijski puk',  1, 3, N'Aleksinac'),
    (N'7. motorizovani puk',    2, 4, N'Beograd');

INSERT INTO Korisnik (email, lozinka, uloga_id, jedinica_id)
VALUES (N'admin@mobilizacija.rs', N'admin123', 1, NULL);

INSERT INTO Komandant (ime, prezime, cin_id, jmbg, kontakt) VALUES
    (N'Marko',  N'Petrovic',  13, N'1234567890123', N'marko@vojska.rs'),
    (N'Nikola', N'Jovanovic', 10, N'9876543210123', N'nikola@vojska.rs');

INSERT INTO Jedinica (naziv_jedinice, puk_id, lokacija, maks_kapacitet, komandant_id) VALUES
    (N'1. bataljon', 1, N'Nis',       120, 1),
    (N'2. bataljon', 1, N'Leskovac',  100, 2),
    (N'1. bataljon', 2, N'Aleksinac',  80, NULL),
    (N'1. bataljon', 3, N'Beograd',   150, NULL);

INSERT INTO Vojnik (ime, prezime, jmbg, cin_id, korisnik_id)
VALUES (N'Petar', N'Nikolic', N'1111111111111', 1, NULL);

INSERT INTO Mobilizacija (vojnik_id, jedinica_id, status_aktivnosti)
VALUES (1, 1, N'Aktivan');
GO

-- ============================================================
--  10. STORED PROCEDURE — Auth
-- ============================================================

CREATE PROCEDURE Provera_Logina
    @email   NVARCHAR(100),
    @lozinka NVARCHAR(100),
    @uloga   NVARCHAR(20) OUTPUT
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    SET @uloga = NULL;

    IF EXISTS(SELECT 1 FROM Korisnik WHERE email = @email AND lozinka = @lozinka)
    BEGIN
        SELECT @uloga = U.naziv_uloga
        FROM   Korisnik K
        JOIN   Uloga U ON K.uloga_id = U.uloga_id
        WHERE  K.email = @email;
        RETURN 0;
    END
    ELSE
        RETURN 1;
END
GO

CREATE PROCEDURE Unos_Korisnika
    @email   NVARCHAR(100),
    @lozinka NVARCHAR(100)
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM Korisnik WHERE email = @email) RETURN 1;
        INSERT INTO Korisnik (email, lozinka, uloga_id, jedinica_id)
        VALUES (@email, @lozinka, 2, NULL);
        RETURN 0;
    END TRY
    BEGIN CATCH RETURN @@ERROR; END CATCH
END
GO

CREATE PROCEDURE Dodaj_Novog_Korisnika
    @email    NVARCHAR(100),
    @lozinka  NVARCHAR(100),
    @uloga_id INT,
    @jed_id   INT = NULL
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM Korisnik WHERE email = @email)
        BEGIN
            RAISERROR(N'Korisnik sa tim emailom vec postoji.', 16, 1);
            RETURN;
        END
        INSERT INTO Korisnik (email, lozinka, uloga_id, jedinica_id)
        VALUES (@email, @lozinka, @uloga_id, @jed_id);
    END TRY
    BEGIN CATCH THROW; END CATCH
END
GO

CREATE PROCEDURE Brisanje_Korisnika @email NVARCHAR(100)
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    BEGIN TRY
        DECLARE @UID INT = (SELECT korisnik_id FROM Korisnik WHERE email = @email);
        IF @UID IS NULL RETURN 0;
        BEGIN TRANSACTION;
            DELETE FROM Mobilizacija
            WHERE vojnik_id IN (SELECT vojnik_id FROM Vojnik WHERE korisnik_id = @UID);
            DELETE FROM Vojnik   WHERE korisnik_id = @UID;
            DELETE FROM Korisnik WHERE korisnik_id = @UID;
        COMMIT TRANSACTION;
        RETURN 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        RETURN @@ERROR;
    END CATCH
END
GO

CREATE PROCEDURE Izmena_Lozinke
    @email        NVARCHAR(100),
    @nova_lozinka NVARCHAR(100)
AS
BEGIN
    UPDATE Korisnik SET lozinka = @nova_lozinka WHERE email = @email;
END
GO

-- ============================================================
--  11. STORED PROCEDURE — Divizija / Puk
-- ============================================================

CREATE PROCEDURE Unos_Divizije
    @naziv    NVARCHAR(100),
    @lokacija NVARCHAR(100),
    @opis     NVARCHAR(255)
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Divizija WHERE naziv_divizije = @naziv) RETURN 1;
    INSERT INTO Divizija (naziv_divizije, lokacija, opis) VALUES (@naziv, @lokacija, @opis);
    RETURN 0;
END
GO

CREATE PROCEDURE Brisanje_Divizije @divizija_id INT
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Puk WHERE divizija_id = @divizija_id) RETURN 0;
    DELETE FROM Divizija WHERE divizija_id = @divizija_id;
    RETURN 1;
END
GO

CREATE PROCEDURE Unos_Puka
    @naziv      NVARCHAR(100),
    @divizija_id INT,
    @tip_id     INT,
    @lokacija   NVARCHAR(100)
AS
BEGIN
    INSERT INTO Puk (naziv_puka, divizija_id, tip_id, lokacija)
    VALUES (@naziv, @divizija_id, @tip_id, @lokacija);
END
GO

CREATE PROCEDURE Brisanje_Puka @puk_id INT
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Jedinica WHERE puk_id = @puk_id) RETURN 0;
    DELETE FROM Puk WHERE puk_id = @puk_id;
    RETURN 1;
END
GO

-- ============================================================
--  12. STORED PROCEDURE — Jedinica (Bataljon)
-- ============================================================

CREATE PROCEDURE Unos_Jedinice
    @naziv     NVARCHAR(100),
    @puk_id    INT,
    @lokacija  NVARCHAR(100),
    @kapacitet INT
AS
BEGIN
    INSERT INTO Jedinica (naziv_jedinice, puk_id, lokacija, maks_kapacitet)
    VALUES (@naziv, @puk_id, @lokacija, @kapacitet);
END
GO

CREATE PROCEDURE Izmena_Jedinice
    @jedinica_id    INT,
    @naziv          NVARCHAR(100),
    @lokacija       NVARCHAR(100),
    @novi_kapacitet INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS(SELECT 1 FROM Jedinica WHERE jedinica_id = @jedinica_id) RETURN 0;
        UPDATE Jedinica
        SET naziv_jedinice = @naziv,
            lokacija       = @lokacija,
            maks_kapacitet = @novi_kapacitet
        WHERE jedinica_id = @jedinica_id;
        RETURN 1;
    END TRY
    BEGIN CATCH RETURN @@ERROR; END CATCH
END
GO

CREATE PROCEDURE Brisanje_Jedinice @jedinica_id INT
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Mobilizacija WHERE jedinica_id = @jedinica_id) RETURN 0;
    UPDATE Korisnik SET jedinica_id = NULL WHERE jedinica_id = @jedinica_id;
    DELETE FROM Jedinica WHERE jedinica_id = @jedinica_id;
    RETURN 1;
END
GO

-- ============================================================
--  13. STORED PROCEDURE — Komandant
-- ============================================================

CREATE PROCEDURE Unos_Komandanta
    @ime     NVARCHAR(50),
    @prezime NVARCHAR(50),
    @cin_id  INT,
    @jmbg    NVARCHAR(13),
    @kontakt NVARCHAR(100)
AS
BEGIN
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM Komandant WHERE jmbg = @jmbg) RETURN 1;
        INSERT INTO Komandant (ime, prezime, cin_id, jmbg, kontakt)
        VALUES (@ime, @prezime, @cin_id, @jmbg, @kontakt);
        RETURN 0;
    END TRY
    BEGIN CATCH RETURN @@ERROR; END CATCH
END
GO

CREATE PROCEDURE Brisanje_Komandanta @komandant_id INT
AS
BEGIN
    UPDATE Jedinica SET komandant_id = NULL WHERE komandant_id = @komandant_id;
    DELETE FROM Komandant WHERE komandant_id = @komandant_id;
END
GO

-- Vraca: 0=ok, 1=jedinica ne postoji, 2=cin prenizak, 3=komandant ne postoji
CREATE PROCEDURE Dodeli_Komandanta
    @jedinica_id  INT,
    @komandant_id INT
AS
BEGIN
    DECLARE @min_nivo INT;
    DECLARE @cin_nivo INT;

    SELECT @min_nivo = T.min_cin_komandanta
    FROM   Jedinica J
    JOIN   Puk P ON J.puk_id = P.puk_id
    JOIN   TipJedinice T ON P.tip_id = T.tip_id
    WHERE  J.jedinica_id = @jedinica_id;

    IF @min_nivo IS NULL RETURN 1;

    SELECT @cin_nivo = C.nivo
    FROM   Komandant K
    JOIN   Cin C ON K.cin_id = C.cin_id
    WHERE  K.komandant_id = @komandant_id;

    IF @cin_nivo IS NULL RETURN 3;
    IF @cin_nivo < @min_nivo RETURN 2;

    UPDATE Jedinica SET komandant_id = @komandant_id WHERE jedinica_id = @jedinica_id;
    RETURN 0;
END
GO

CREATE PROCEDURE Ukloni_Komandanta @jedinica_id INT
AS
BEGIN
    UPDATE Jedinica SET komandant_id = NULL WHERE jedinica_id = @jedinica_id;
END
GO

-- ============================================================
--  14. STORED PROCEDURE — Vojnik
-- ============================================================

CREATE PROCEDURE Unos_Vojnika
    @ime         NVARCHAR(30),
    @prezime     NVARCHAR(30),
    @jmbg        NVARCHAR(13),
    @cin_id      INT = 1,
    @korisnik_id INT = NULL
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    BEGIN TRY
        IF EXISTS(SELECT 1 FROM Vojnik WHERE jmbg = @jmbg) RETURN 1;
        INSERT INTO Vojnik (ime, prezime, jmbg, cin_id, korisnik_id)
        VALUES (@ime, @prezime, @jmbg, @cin_id, @korisnik_id);
        RETURN 0;
    END TRY
    BEGIN CATCH RETURN @@ERROR; END CATCH
END
GO

CREATE PROCEDURE Izmena_Vojnika
    @vojnik_id INT,
    @ime       NVARCHAR(30),
    @prezime   NVARCHAR(30),
    @cin_id    INT
AS
BEGIN
    UPDATE Vojnik
    SET ime = @ime, prezime = @prezime, cin_id = @cin_id
    WHERE vojnik_id = @vojnik_id;
END
GO

CREATE PROCEDURE Brisanje_Vojnika @vojnik_id INT
AS
BEGIN
    SET LOCK_TIMEOUT 3000;
    BEGIN TRY
        BEGIN TRANSACTION;
            DELETE FROM Mobilizacija WHERE vojnik_id = @vojnik_id;
            DELETE FROM Vojnik       WHERE vojnik_id = @vojnik_id;
        COMMIT TRANSACTION;
        RETURN 1;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        RETURN @@ERROR;
    END CATCH
END
GO

-- ============================================================
--  15. STORED PROCEDURE — Mobilizacija
-- ============================================================

-- Vraca: 1=uspeh, 0=puno, 2=vec mobilizovan
CREATE PROCEDURE Rasporedi_Vojnika
    @vojnik_id   INT,
    @jedinica_id INT,
    @status      NVARCHAR(20)
AS
BEGIN
    IF EXISTS(
        SELECT 1 FROM Mobilizacija
        WHERE  vojnik_id = @vojnik_id
          AND  status_aktivnosti <> N'Otpusten'
    ) RETURN 2;

    DECLARE @trenutno INT = (
        SELECT COUNT(*) FROM Mobilizacija
        WHERE jedinica_id = @jedinica_id AND status_aktivnosti = N'Aktivan');
    DECLARE @maks INT = (
        SELECT maks_kapacitet FROM Jedinica WHERE jedinica_id = @jedinica_id);

    IF @trenutno >= @maks RETURN 0;

    INSERT INTO Mobilizacija (vojnik_id, jedinica_id, status_aktivnosti)
    VALUES (@vojnik_id, @jedinica_id, @status);
    RETURN 1;
END
GO

CREATE PROCEDURE Izmena_Statusa_Vojnika
    @vojnik_id   INT,
    @novi_status NVARCHAR(20)
AS
BEGIN
    UPDATE Mobilizacija SET status_aktivnosti = @novi_status
    WHERE vojnik_id = @vojnik_id AND status_aktivnosti <> N'Otpusten';
END
GO

-- ============================================================
--  16. STORED PROCEDURE — Statistika / Pristup
-- ============================================================

CREATE PROCEDURE Statistika_Jedinice @jedinica_id INT = 0
AS
BEGIN
    SELECT
        J.naziv_jedinice,
        J.lokacija,
        J.maks_kapacitet,
        COUNT(CASE WHEN M.status_aktivnosti = N'Aktivan' THEN 1 END) AS trenutno_vojnika,
        J.maks_kapacitet - COUNT(CASE WHEN M.status_aktivnosti = N'Aktivan' THEN 1 END) AS slobodna_mesta,
        ISNULL(KOM.ime + N' ' + KOM.prezime, N'—') AS komandant
    FROM Jedinica J
    LEFT JOIN Mobilizacija M ON J.jedinica_id = M.jedinica_id
    LEFT JOIN Komandant KOM ON J.komandant_id = KOM.komandant_id
    WHERE @jedinica_id = 0 OR J.jedinica_id = @jedinica_id
    GROUP BY J.jedinica_id, J.naziv_jedinice, J.lokacija, J.maks_kapacitet,
             KOM.ime, KOM.prezime;
END
GO

CREATE PROCEDURE Proveri_Pristup
    @korisnik_id       INT,
    @target_jedinica_id INT
AS
BEGIN
    DECLARE @uloga_id   INT = (SELECT uloga_id   FROM Korisnik WHERE korisnik_id = @korisnik_id);
    DECLARE @jedinica_id INT = (SELECT jedinica_id FROM Korisnik WHERE korisnik_id = @korisnik_id);

    IF @uloga_id = 1
        SELECT N'WRITE' AS pristup;
    ELSE IF @uloga_id = 3 AND @jedinica_id = @target_jedinica_id
        SELECT N'WRITE' AS pristup;
    ELSE
        SELECT N'READ' AS pristup;
END
GO
