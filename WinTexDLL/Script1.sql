--1 Run Visual Studio as an Admin
--2 Open up additional ports that are, by default, blocked by Windows Firewall
--3 Set the project as a Startup Project.
--4 In Visual Studio: go to "SQL Server Object Explorer", right-click on the Instance this is deploying to in the "Debug" tab of "Project Properties", 
--  and select "Allow SQL/CLR Debugging", and click the "Yes" button in the pop-up dialog.
--5 If you want breakpoints to work, you cannot have the "Optimize Code" option checked in the "SQLCLR Build" tab of "Project Properties". 
--  By default it is unchecked for the "Debug" configuration and checked for the "Release" configuration. 
--  The configuration itself doesn't matter, just so long as that option is not checked. 
--  If it was, you need to uncheck it and re-publish and make sure that it actually updates the Assembly (sometimes it might not since there are no other changes to 
--  the Assembly).
--6 In Visual Studio: Open a new query from "SQL Server Object Explorer" and in the drop-down attached to the Execute ">" button, 
--  select "Execute With Debugger" (which is also ALT + F5). 
--  Sometimes breakpoints aren't trapped the first time around, so just execute with debugger again.
--7 Please note: the login that you connect to SQL Server as for debugging needs to be a sysadmin.

USE alderswintex ;
DECLARE @nSonuc INT ;
--declare @nPADMetraj As decimal(10,2) ;
--declare @cKesimPartiNo As char(30) ;
--declare @nKesimIsemriAdet As decimal(10,2) ;

--exec GetPADMetraj 'D65148','D65148_01,D65148_01@01,D65148_01@02,D65148_01@03,D65148_01@04' , 'TMP_999' , @nSonuc

 EXEC STISonMaliyetKompleCLR  @cGercekSiparisFilter = N'and a.kullanicisipno in (select siparisno from ADMINOKANWIN7VMstisonmaliyet10 with (NOLOCK))',   -- nvarchar(max)
    @cPlanSiparisFilter = N'and a.kullanicisipno in (select siparisno from ADMINOKANWIN7VMstisonmaliyet10 with (NOLOCK))',     -- nvarchar(max)
    @cGercekSevkTarihFilter = N'and siparisno in (||HM-583821-1322-HEP||,||HM-588719-1941-STEV||,||HM-588720-1941-MARC||,||HM-613532-1522-DIA||,||HM-613594-1522-DIA||,||HM-631829-1322-BET||,||HM-631831-1322-BET||,||HM-651709-3010-ROSA||,||HM-651710-3010-ROSA||,||HM-655329-1941-HIRO||,||HM-655331-1941-HIRO||,||HM-657542-1543-MARY||,||HM-681895-1322-MELI||,||HM-681898-1322-MELI||,||ZR-1971-162-BLZ-W23||,||ZR-1971-169-PNT-W23||,||ZR-1971-170-BLZ-W23||,||ZR-1971-173-ELB-W23||,||ZR-1971-220-PNT-A-W23-RPT2||,||ZR-1971-222-SRT-W23||,||ZR-1971-223-PNT-W23||,||ZR-1971-225-PNT-W23||) ', -- nvarchar(max)
    @cPlanSevkTarihFilter = N'and siparisno in (||HM-583821-1322-HEP||,||HM-588719-1941-STEV||,||HM-588720-1941-MARC||,||HM-613532-1522-DIA||,||HM-613594-1522-DIA||,||HM-631829-1322-BET||,||HM-631831-1322-BET||,||HM-651709-3010-ROSA||,||HM-651710-3010-ROSA||,||HM-655329-1941-HIRO||,||HM-655331-1941-HIRO||,||HM-657542-1543-MARY||,||HM-681895-1322-MELI||,||HM-681898-1322-MELI||,||ZR-1971-162-BLZ-W23||,||ZR-1971-169-PNT-W23||,||ZR-1971-170-BLZ-W23||,||ZR-1971-173-ELB-W23||,||ZR-1971-220-PNT-A-W23-RPT2||,||ZR-1971-222-SRT-W23||,||ZR-1971-223-PNT-W23||,||ZR-1971-225-PNT-W23||) ',   -- nvarchar(max)
    @cTableNameHeader = N'ADMINOKANWIN7VM',       -- nvarchar(max)
    @nSonuc = @nSonuc OUTPUT       -- int

select @nSonuc

--EXEC STISonMaliyetKompleCLR  @cGercekSiparisFilter = N'and a.kullanicisipno in (||ZR-1971-021-PANT-S22||)',   -- nvarchar(max)
--                            @cPlanSiparisFilter = N'and a.kullanicisipno in (||ZR-1971-021-PANT-S22||)',     -- nvarchar(max)
--                            @cGercekSevkTarihFilter = N'and siparisno in (||ZR-1971-021-PANT-S22||)', -- nvarchar(max)
--                            @cPlanSevkTarihFilter = N'and siparisno in (||ZR-1971-021-PANT-S22||)',   -- nvarchar(max)
--                            @cTableNameHeader = N'ADMINDENEME',       -- nvarchar(max)
--                            @nSonuc = @nSonuc OUTPUT       -- int--USE istwtx;
--EXEC HizliStokBakimi

--use donsa 
--DECLARE @nSonuc1 INT
--EXEC dbo.MPOzet ' AND  ((a.ilksevktarihi >= ''01.04.2021''  AND  a.ilksevktarihi < ''01.05.2021'') ) ' , 'tmpt_2188' ,  @nSonuc = @nSonuc1 OUTPUT

--use tes 
--exec dbo.FastMTFBuild 'D370364',1

--USE darinda
--DECLARE @nSonuc1 INT;
--EXEC dbo.CorapBakimi ' and b.tasarimno = ''0000000307''',  @nSonuc = @nSonuc1 OUTPUT -- int
--SELECT @nSonuc1

--EXEC dbo.FastMTFBuild  'DEV-20-081', 0

--USE darinda
--SELECT * FROM dbo.ihracattablosu(' and (  (not(a.gbtarihi is null or a.gbtarihi = ||01.01.1950||)  and a.gbtarihi >= ||01.01.2018||  and a.gbtarihi < ||01.01.2019||)  or  ((a.gbtarihi is null or a.gbtarihi = ||01.01.1950||)  and a.ihracatfttarihi >= ||01.01.2018||  and a.ihracatfttarihi < ||01.01.2019||)  ) ')
--SELECT w.dosyano
--FROM (SELECT * FROM dbo.ihracattablosu(' and (  (not(a.gbtarihi is null or a.gbtarihi = ||01.01.1950||)  and a.gbtarihi >= ||01.01.2019||  and a.gbtarihi < ||01.01.2020||)  or  ((a.gbtarihi is null or a.gbtarihi = ||01.01.1950||)  and a.ihracatfttarihi >= ||01.01.2019||  and a.ihracatfttarihi < ||01.01.2020||)  ) ',0) ) w
--ORDER BY w.dosyayil, w.dosyaay, w.dosyano

--USE mothouse ;
--DECLARE @nSonuc1 INT;
--EXEC dbo.MultiOnMaliyetOzetHesaplaCLR @nSonuc = @nSonuc1 OUTPUT -- int
--SELECT @nSonuc1

--USE alderswintex;
--EXEC dbo.MasterTakipHesapla ' and a.ilksevktarihi >= ''01.03.2019''','tmp_9999',0

--USE alderswintex ;
--DECLARE @nSonuc INT;
--EXEC dbo.STISonMaliyetKompleCLR @cGercekSiparisFilter = N'and a.kullanicisipno in (select siparisno from ADMINVIRTUALXP91458stisonmaliyet10 with (NOLOCK))',   -- nvarchar(max)
--                                @cPlanSiparisFilter = N'and a.kullanicisipno in (select siparisno from ADMINVIRTUALXP91458stisonmaliyet10 with (NOLOCK))',     -- nvarchar(max)
--                                @cGercekSevkTarihFilter = N'AND  (SevkTar >= ''01.01.2019''  AND  SevkTar < ''01.01.2020'') ', -- nvarchar(max)
--                                @cPlanSevkTarihFilter = N' AND  (ilksevktar >= ''01.01.2019''  AND  ilksevktar < ''01.01.2020'') ',   -- nvarchar(max)
--                                @cTableNameHeader = N'ADMINMSI',       -- nvarchar(max)
--                                @nSonuc = @nSonuc OUTPUT       -- int
