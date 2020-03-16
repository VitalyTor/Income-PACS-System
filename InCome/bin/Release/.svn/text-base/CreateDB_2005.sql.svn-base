if(select count(*) from master.dbo.syslogins where name='InComeUser')>0 begin exec sp_droplogin 'InComeUser' end
#
IF ((SELECT count(*) FROM sysdatabases WHERE name = N'InCome')>0) BEGIN DROP DATABASE InCome END
#
IF ((SELECT count(*) FROM sysdatabases WHERE name = N'InCome')=0) BEGIN CREATE DATABASE InCome END
#
use InCome 
#
--таблица с основной информацией о клиенте
      CREATE TABLE EmplMain (
		MID int IDENTITY PRIMARY KEY,
		MCODE varchar(25),
		MCODEPRINT varchar(50),
                MLOGIN varchar(20),
		MPASSWORD varchar(20),
		MLOCKED varchar(10),
		MCODEDT DateTime,
		MPASSWORDDT DateTime,
		MUSEPASSW varchar(3),	
                MDEPARTMENT varchar(150),
                MGROUP varchar(100),
		MNAME varchar(30),
		MSECNAME varchar(30),
		MSURNAME varchar(40),
		MBIRTHDAY DateTime,
		MDOCUMENT varchar(200),
		MDOCTYPE varchar(50),
		MMALE varchar(1),
		MEMPDATE DateTime,
		MPHONE varchar(15),
		MMOBPHONE varchar(15),
		MEMAIL varchar(50),
		MCOUNTRY varchar(20),
		MREGION varchar(50),
		MDISTRICT varchar(50),
		MSETTLEMENT varchar(80),
		MADDRESS varchar(100),
		MDESCRIPTION varchar(200),
                MINSDATE DateTime DEFAULT (getdate())

	)
#
	--таблица с информацией о регистрации работников
	CREATE TABLE EmplCurrent (
		ID int IDENTITY PRIMARY KEY,
		MID int,
		INDT DateTime,
		OUTDT  DateTime
	)	
#
        --таблица с описанием отделов
        CREATE TABLE EmplDepart (
		DPID int IDENTITY PRIMARY KEY,
		DPNAME varchar(150) UNIQUE,
                DPTOPDEPART varchar(150),
                DPMANAGER varchar(100),
                DPPHONE varchar(15),
                PDDESCRIPTION varchar(200)
	)
#  
      --таблица с группами пользователей 
        CREATE TABLE EmplGroup( 
		GRID int IDENTITY PRIMARY KEY,
		GRNAME varchar(100) UNIQUE,
                GRDESCRIPTION varchar(200)
	)
#
	--таблица для печати штрих-кодов
        CREATE TABLE CodeToPrint( 
		ID int IDENTITY PRIMARY KEY,
                FIO varchar(120),
		CODE varchar(25),
		CODEPRINT varchar(50),
		DEPARTMENT varchar(150),
                GRUPA varchar(100)
	)
#
	--связываем таблицы
	--текущие зарегестрированные->основная информация	
        ALTER TABLE EmplCurrent WITH NOCHECK ADD CONSTRAINT FK_MID_CURRENT FOREIGN KEY(MID)
		REFERENCES EmplMain (MID) ON UPDATE CASCADE ON DELETE CASCADE
#
	--детальная информация->отделы
        ALTER TABLE EmplMain WITH NOCHECK ADD CONSTRAINT FK_DP_DETAIL FOREIGN KEY(MDEPARTMENT)
		REFERENCES EmplDepart (DPNAME) ON UPDATE CASCADE
#
	--детальная информация->группы
	ALTER TABLE EmplMain WITH NOCHECK ADD CONSTRAINT FK_GR_DETAIL FOREIGN KEY(MGROUP)
		REFERENCES EmplGroup (GRNAME) ON UPDATE CASCADE 
#  
    --промежуточная таблица для отчётов
	CREATE TABLE TempForReport (
		ID int IDENTITY PRIMARY KEY,
		MID int,
		INDT DateTime,
		OUTDT varchar(10)
	)
#	
    --вювик для отчётов 
    CREATE VIEW vwReportSource
	AS
	 SELECT TOP 100 PERCENT tfr.MID, MAX(em.MSURNAME + ' ' + ISNULL(em.MNAME, '') + ' ' + ISNULL(em.MSECNAME, '')) AS FIO, MAX(em.MDEPARTMENT) AS MDEPARTMENT,
	 	MAX(em.MGROUP) AS MGROUP, CONVERT(Varchar, tfr.INDT, 104) AS DATE, MIN(CONVERT(Varchar, tfr.INDT, 108)) AS INTIME, MAX(CONVERT(Varchar, tfr.OUTDT, 108)) AS OUTTIME,
 		ROUND(1.0 * SUM(DATEDIFF(mi, tfr.INDT, tfr.OUTDT)) / 60, 2) AS HOURDUR,'' AS DUR FROM dbo.TempForReport AS tfr INNER JOIN dbo.EmplMain AS em ON tfr.MID = em.MID
		GROUP BY tfr.MID, CAST(CAST(tfr.INDT AS INT) AS varchar), CONVERT(Varchar, tfr.INDT, 104)
		ORDER BY tfr.MID, CAST(CAST(tfr.INDT AS INT) AS varchar), CONVERT(Varchar, tfr.INDT, 104)
#
   --процедура для разбивки длинных     
   CREATE PROCEDURE [dbo].[devideDays]
	AS
	BEGIN
	  DECLARE @id int
	  DECLARE @mid int
	  DECLARE @indt DateTime
	  DECLARE @outdt DateTime
	  DECLARE @ind varchar(20)
	  DECLARE @outd varchar(20)
	  DECLARE @d varchar(20)
	  DECLARE @i int
	  DECLARE DIVIDES CURSOR FOR SELECT ID,MID,INDT,OUTDT FROM TempForReport WHERE CONVERT(varchar, INDT, 104)<>CONVERT(varchar, OUTDT, 104)
  
  	OPEN DIVIDES
	FETCH NEXT FROM DIVIDES INTO @id,@mid,@indt,@outdt
	WHILE @@FETCH_STATUS = 0
 	    BEGIN
	        --разбиваем слишком длинный временной интервал на дни
        	SET @i=0 --предохранитель :-)
	        SET @ind=LEFT(CONVERT(varchar, @indt, 20),10)           
	        SET @outd=LEFT(CONVERT(varchar, @outdt, 20),10)           
        	SET @d=@ind         
	        WHILE(@d<>@outd) and (@i<100)
        	  BEGIN
	           IF(@d=@ind)  
			UPDATE TempForReport SET OUTDT=CONVERT(DATETIME,(@d+' 23:59:30'),20)  WHERE ID=@id  
        	   ELSE
                	INSERT INTO TempForReport (MID,INDT,OUTDT) VALUES (@mid,CONVERT(DATETIME,(@d+' 00:00:30'),20),CONVERT(DATETIME,(@d+' 23:59:30'),20))
	           SET @i=@i+1         
           	   SET @d=LEFT(CONVERT(varchar, DATEADD(day,1,CONVERT(DATETIME,(@d+' 00:00:30'),20)), 20),10) 
	          END   
	       IF(@d=@outd)  
	       	INSERT INTO TempForReport (MID,INDT,OUTDT) VALUES (@mid,CONVERT(DATETIME,(@d+' 00:00:30'),20),@outdt)


 	       FETCH NEXT FROM DIVIDES INTO @id,@mid,@indt,@outdt
	   END    
       CLOSE DIVIDES
       DEALLOCATE DIVIDES
       END
#
   --процедура для выборки данных для отчётов
    CREATE PROCEDURE [dbo].[formReportTable] @df DateTime,@dt DateTime,@midlist as varchar(5000)
	AS
	BEGIN 
	  DECLARE @sql varchar(5000) 
	  
        --удаление временной таблицы отчёта
	IF OBJECT_ID('TempForReport', 'U') IS NOT NULL
	  	DROP TABLE TempForReport
	--создание временной таблицы отчёта
	CREATE TABLE TempForReport(
		ID int IDENTITY PRIMARY KEY ,
		MID int,
		INDT datetime,
		OUTDT datetime)

	IF(@midlist<>'')
	  BEGIN
		--добавляем записи, попавшие в диапазон
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,INDT,OUTDT FROM EmplCurrent WHERE (INDT>=@df) and (OUTDT<=@dt) and (CHARINDEX('['+CAST(MID as VARCHAR)+']',@midlist)>0) 
		--добавляем остальные записи, которые частично попадают в диапазон
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,@df,@dt FROM EmplCurrent WHERE (INDT<@df) and ((OUTDT>@dt) OR (OUTDT IS NULL))  and (CHARINDEX('['+CAST(MID as VARCHAR)+']',@midlist)>0)
    		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,@df,(CASE WHEN (OUTDT>@dt) OR (OUTDT IS NULL) THEN @dt ELSE OUTDT END) FROM EmplCurrent WHERE (INDT<@df) and ((OUTDT>@df) OR (OUTDT IS NULL))  and (CHARINDEX('['+CAST(MID as VARCHAR)+']',@midlist)>0)
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,INDT,@dt FROM EmplCurrent WHERE (INDT<@dt) and (INDT>@df) and ((OUTDT>@dt) OR (OUTDT IS NULL))  and (CHARINDEX('['+CAST(MID as VARCHAR)+']',@midlist)>0)   
          END
    	ELSE
          BEGIN
		--добавляем записи, попавшие в диапазон
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,INDT,OUTDT FROM EmplCurrent WHERE (INDT>=@df) and (OUTDT<=@dt) 
	        --добавляем остальные записи, которые частично попадают в диапазон
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,@df,@dt FROM EmplCurrent WHERE (INDT<@df) and ((OUTDT>@dt) OR (OUTDT IS NULL)) 
    		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,@df,(CASE WHEN (OUTDT>@dt) OR (OUTDT IS NULL) THEN @dt ELSE OUTDT END) FROM EmplCurrent WHERE (INDT<@df) and ((OUTDT>@df) OR (OUTDT IS NULL))  
		INSERT INTO TempForReport (MID,INDT,OUTDT) SELECT MID,INDT,@dt FROM EmplCurrent WHERE (INDT<@dt) and (INDT>@df) and ((OUTDT>@dt) OR (OUTDT IS NULL))  
	  END
        --вставляем фиктивные точки входа/выхода для того, чтобы рабочий день не растягивался на несколько дней          
        EXEC dbo.devideDays 
        END
#
    --процедура для показа и удаления некорректных регистраций сотрудников
    CREATE PROCEDURE [dbo].[correctTime] @mode as int,@cnt as int output 
	AS 
	BEGIN
	 SET @cnt=0
	 IF(@mode=0)
	   SELECT @cnt=COUNT(*) FROM  EmplCurrent WHERE ((OUTDT IS NULL) OR (INDT IS NULL) OR (OUTDT < INDT)) AND (ID NOT IN (SELECT MAX(ID) AS MAXID FROM EmplCurrent AS ec GROUP BY MID))
	 ELSE 
	   DELETE FROM EmplCurrent WHERE ((OUTDT IS NULL) OR (INDT IS NULL) OR (OUTDT < INDT)) AND (ID NOT IN (SELECT MAX(ID) AS MAXID FROM EmplCurrent AS ec GROUP BY MID)) 
	END 
#
    --процедура для регистрации прихода/ухода сотрудников
    CREATE PROCEDURE [dbo].[LogInOut] @code varchar(20), @res varchar(3)	out
	AS
	BEGIN
	 DECLARE @outdt DateTime
	 DECLARE @id int 
	 DECLARE @mid int
 
    	SET @res='NON'
        SELECT TOP 1 @mid=MID FROM EmplMain WHERE MCODE=@code

        IF(@mid is not null)
	    BEGIN 
		SELECT TOP 1 @id=ec.ID,@outdt=ec.OUTDT FROM EmplCurrent ec INNER JOIN EmplMain em on ec.MID=em.MID WHERE (em.MCODE=@code) ORDER BY ec.INDT DESC
		--смотрим заходит или выходит
		IF(@outdt is null) and (@id is not null) --выход
			BEGIN
				UPDATE EmplCurrent SET OUTDT=getDate() WHERE id=@id
				SET @res='OUT'
			END 
		ELSE
			BEGIN   
				INSERT INTO EmplCurrent (MID,INDT) Values (@mid,getDate())
				SET @res='IN'
			END 
	   END
    END
#
use master;     exec sp_addlogin 'InComeUser','7471740','InCome','Russian'; exec sp_addsrvrolemember  'InComeUser', 'sysadmin'