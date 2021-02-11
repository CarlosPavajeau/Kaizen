CREATE USER `kaizen`@`localhost` IDENTIFIED BY 'kaizen';

GRANT USAGE ON *.* TO `kaizen`@`localhost`;

CREATE SCHEMA IF NOT EXISTS `kaizen_test` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
GRANT ALL PRIVILEGES ON `kaizen_test`.* TO `kaizen`@`localhost` WITH GRANT OPTION;
USE `kaizen_test` ;

-- -----------------------------------------------------
-- Table `kaizen_test`.`__efmigrationshistory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`__efmigrationshistory` (
  `MigrationId` VARCHAR(95) NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20201017212128_StartNewMigration','5.0.3'),('20201031215127_FixTypos','5.0.3'),('20201108181120_RemoveDefaultValues','5.0.3'),('20201110152513_InvoicePaymentDate','5.0.3'),('20201110172503_Statistics','5.0.3'),('20210130173933_Notifications','5.0.3');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetusers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetusers` (
  `Id` VARCHAR(191) NOT NULL,
  `UserName` VARCHAR(15) NULL DEFAULT NULL,
  `NormalizedUserName` VARCHAR(15) NULL DEFAULT NULL,
  `Email` VARCHAR(150) NULL DEFAULT NULL,
  `NormalizedEmail` VARCHAR(150) NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` VARCHAR(191) NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumber` VARCHAR(10) NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_AspNetUsers_Email` (`Email` ASC) VISIBLE,
  UNIQUE INDEX `UserNameIndex` (`NormalizedUserName` ASC) VISIBLE,
  UNIQUE INDEX `IX_AspNetUsers_PhoneNumber` (`PhoneNumber` ASC) VISIBLE,
  INDEX `EmailIndex` (`NormalizedEmail` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`clients`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`clients` (
  `Id` VARCHAR(10) NOT NULL,
  `FirstName` VARCHAR(20) NOT NULL,
  `SecondName` VARCHAR(20) NULL DEFAULT NULL,
  `LastName` VARCHAR(20) NOT NULL,
  `SecondLastName` VARCHAR(20) NULL DEFAULT NULL,
  `UserId` VARCHAR(191) NULL DEFAULT NULL,
  `NIT` VARCHAR(30) NULL DEFAULT NULL,
  `ClientType` VARCHAR(20) NOT NULL,
  `TradeName` VARCHAR(50) NULL DEFAULT NULL,
  `FirstPhoneNumber` VARCHAR(10) NOT NULL,
  `SecondPhoneNumber` VARCHAR(10) NULL DEFAULT NULL,
  `FirstLandLine` VARCHAR(15) NULL DEFAULT NULL,
  `SecondLandLine` VARCHAR(15) NULL DEFAULT NULL,
  `State` INT NOT NULL DEFAULT '0',
  `BusinessName` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_Clients_NIT` (`NIT` ASC) VISIBLE,
  UNIQUE INDEX `IX_Clients_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_Clients_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`activities`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`activities` (
  `Code` INT NOT NULL AUTO_INCREMENT,
  `Date` DATETIME(6) NOT NULL,
  `ClientId` VARCHAR(10) NOT NULL,
  `Periodicity` INT NOT NULL,
  `State` INT NOT NULL DEFAULT '0',
  PRIMARY KEY (`Code`),
  INDEX `IX_Activities_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_Activities_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`employeecharges`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`employeecharges` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Charge` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 8
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;

--
-- Dumping data for table `employeecharges`
--

LOCK TABLES `employeecharges` WRITE;
/*!40000 ALTER TABLE `employeecharges` DISABLE KEYS */;
INSERT INTO `employeecharges` VALUES (1,'Gerente'),(2,'Coordinador de Calidad y Ambiente'),(3,'Contador'),(4,'Lider SST'),(5,'Auxiliar Administrativa'),(6,'Técnico Operativo'),(7,'Aprendiz');
/*!40000 ALTER TABLE `employeecharges` ENABLE KEYS */;
UNLOCK TABLES;

-- -----------------------------------------------------
-- Table `kaizen_test`.`employeecontract`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`employeecontract` (
  `ContractCode` VARCHAR(30) NOT NULL,
  `StartDate` DATETIME(6) NOT NULL,
  `EndDate` DATETIME(6) NOT NULL,
  PRIMARY KEY (`ContractCode`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`employees`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`employees` (
  `Id` VARCHAR(10) NOT NULL,
  `FirstName` VARCHAR(20) NOT NULL,
  `SecondName` VARCHAR(20) NULL DEFAULT NULL,
  `LastName` VARCHAR(20) NOT NULL,
  `SecondLastName` VARCHAR(20) NULL DEFAULT NULL,
  `UserId` VARCHAR(191) NULL DEFAULT NULL,
  `ChargeId` INT NOT NULL,
  `ContractCode` VARCHAR(30) NULL DEFAULT NULL,
  `State` INT NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_Employees_ContractCode` (`ContractCode` ASC) VISIBLE,
  UNIQUE INDEX `IX_Employees_UserId` (`UserId` ASC) VISIBLE,
  INDEX `IX_Employees_ChargeId` (`ChargeId` ASC) VISIBLE,
  CONSTRAINT `FK_Employees_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE RESTRICT,
  CONSTRAINT `FK_Employees_EmployeeCharges_ChargeId`
    FOREIGN KEY (`ChargeId`)
    REFERENCES `kaizen_test`.`employeecharges` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_Employees_EmployeeContract_ContractCode`
    FOREIGN KEY (`ContractCode`)
    REFERENCES `kaizen_test`.`employeecontract` (`ContractCode`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`activitiesemployees`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`activitiesemployees` (
  `ActivityCode` INT NOT NULL,
  `EmployeeId` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`EmployeeId`, `ActivityCode`),
  INDEX `IX_ActivitiesEmployees_ActivityCode` (`ActivityCode` ASC) VISIBLE,
  CONSTRAINT `FK_ActivitiesEmployees_Activities_ActivityCode`
    FOREIGN KEY (`ActivityCode`)
    REFERENCES `kaizen_test`.`activities` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_ActivitiesEmployees_Employees_EmployeeId`
    FOREIGN KEY (`EmployeeId`)
    REFERENCES `kaizen_test`.`employees` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`servicetypes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`servicetypes` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(70) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;

--
-- Dumping data for table `servicetypes`
--

LOCK TABLES `servicetypes` WRITE;
/*!40000 ALTER TABLE `servicetypes` DISABLE KEYS */;
INSERT INTO `servicetypes` VALUES (1,'Control de plagas'),(2,'Desinfección de ambientes y superficies'),(3,'Captura y reubicación de animales'),(4,'Matenimiento de sistemas y equipos'),(5,'Jardinería'),(6,'Suministro, instalación y mantenimiento de equipos');
/*!40000 ALTER TABLE `servicetypes` ENABLE KEYS */;
UNLOCK TABLES;

-- -----------------------------------------------------
-- Table `kaizen_test`.`services`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`services` (
  `Code` VARCHAR(15) NOT NULL,
  `Name` VARCHAR(40) NULL DEFAULT NULL,
  `ServiceTypeId` INT NOT NULL,
  `Cost` DECIMAL(65,30) NOT NULL,
  PRIMARY KEY (`Code`),
  INDEX `IX_Services_ServiceTypeId` (`ServiceTypeId` ASC) VISIBLE,
  CONSTRAINT `FK_Services_ServiceTypes_ServiceTypeId`
    FOREIGN KEY (`ServiceTypeId`)
    REFERENCES `kaizen_test`.`servicetypes` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`activitiesservices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`activitiesservices` (
  `ActivityCode` INT NOT NULL,
  `ServiceCode` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`ActivityCode`, `ServiceCode`),
  INDEX `IX_ActivitiesServices_ServiceCode` (`ServiceCode` ASC) VISIBLE,
  CONSTRAINT `FK_ActivitiesServices_Activities_ActivityCode`
    FOREIGN KEY (`ActivityCode`)
    REFERENCES `kaizen_test`.`activities` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_ActivitiesServices_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetroles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetroles` (
  `Id` VARCHAR(50) NOT NULL,
  `Name` VARCHAR(191) NULL DEFAULT NULL,
  `NormalizedName` VARCHAR(191) NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `RoleNameIndex` (`NormalizedName` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('3bb4b79d-85a4-4a94-b55e-5619c9acf4a2','Administrator','ADMINISTRATOR','1ed77447-fe5c-42c2-9711-3f91cc103255'),('a988a9ea-c7a5-4329-aceb-3da5016c6a43','Client','CLIENT','fba45aab-42d7-4e12-9dc0-44a2f68badf1'),('e6728857-7423-443f-8228-2c8dd22f3aab','TechnicalEmployee','TECHNICALEMPLOYEE','501614ae-a5ad-4ee3-ba6f-17c28ab1cd5d'),('e88f6181-e86a-49e1-a2da-c79c71914624','OfficeEmployee','OFFICEEMPLOYEE','177cda8b-1541-411e-8891-62f58b0e45fa');
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetroleclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetroleclaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `RoleId` VARCHAR(50) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetRoleClaims_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `kaizen_test`.`aspnetroles` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetuserclaims`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetuserclaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(191) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetUserClaims_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetuserlogins`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetuserlogins` (
  `LoginProvider` VARCHAR(128) NOT NULL,
  `ProviderKey` VARCHAR(128) NOT NULL,
  `ProviderDisplayName` LONGTEXT NULL DEFAULT NULL,
  `UserId` VARCHAR(191) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`),
  INDEX `IX_AspNetUserLogins_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetuserroles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetuserroles` (
  `UserId` VARCHAR(191) NOT NULL,
  `RoleId` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `IX_AspNetUserRoles_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `kaizen_test`.`aspnetroles` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`aspnetusertokens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`aspnetusertokens` (
  `UserId` VARCHAR(191) NOT NULL,
  `LoginProvider` VARCHAR(128) NOT NULL,
  `Name` VARCHAR(128) NOT NULL,
  `Value` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`clientaddresses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`clientaddresses` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `City` VARCHAR(40) NULL DEFAULT NULL,
  `Neighborhood` VARCHAR(40) NULL DEFAULT NULL,
  `Street` VARCHAR(40) NULL DEFAULT NULL,
  `ClientId` VARCHAR(10) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_ClientAddresses_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_ClientAddresses_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`contactpeople`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`contactpeople` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NULL DEFAULT NULL,
  `PhoneNumber` VARCHAR(10) NULL DEFAULT NULL,
  `ClientId` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_ContactPeople_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_ContactPeople_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`yearstatistics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`yearstatistics` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `AppliedActivities` INT NOT NULL,
  `ClientsVisited` INT NOT NULL,
  `ClientsRegistered` INT NOT NULL,
  `Profits` DECIMAL(65,30) NOT NULL,
  `Year` INT NOT NULL,
  `Date` DATETIME(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `AK_YearStatistics_Year` (`Year` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`monthstatistics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`monthstatistics` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `AppliedActivities` INT NOT NULL,
  `ClientsVisited` INT NOT NULL,
  `ClientsRegistered` INT NOT NULL,
  `Profits` DECIMAL(65,30) NOT NULL,
  `Date` DATETIME(6) NOT NULL,
  `YearStatisticsId` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_MonthStatistics_YearStatisticsId` (`YearStatisticsId` ASC) VISIBLE,
  CONSTRAINT `FK_MonthStatistics_YearStatistics_YearStatisticsId`
    FOREIGN KEY (`YearStatisticsId`)
    REFERENCES `kaizen_test`.`yearstatistics` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`daystatistics`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`daystatistics` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `AppliedActivities` INT NOT NULL,
  `ClientsVisited` INT NOT NULL,
  `ClientsRegistered` INT NOT NULL,
  `Profits` DECIMAL(65,30) NOT NULL,
  `Date` DATETIME(6) NOT NULL,
  `MonthStatisticsId` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_DayStatistics_MonthStatisticsId` (`MonthStatisticsId` ASC) VISIBLE,
  CONSTRAINT `FK_DayStatistics_MonthStatistics_MonthStatisticsId`
    FOREIGN KEY (`MonthStatisticsId`)
    REFERENCES `kaizen_test`.`monthstatistics` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`employeesservices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`employeesservices` (
  `EmployeeId` VARCHAR(10) NOT NULL,
  `ServiceCode` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`EmployeeId`, `ServiceCode`),
  INDEX `IX_EmployeesServices_ServiceCode` (`ServiceCode` ASC) VISIBLE,
  CONSTRAINT `FK_EmployeesServices_Employees_EmployeeId`
    FOREIGN KEY (`EmployeeId`)
    REFERENCES `kaizen_test`.`employees` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_EmployeesServices_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`equipments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`equipments` (
  `Code` VARCHAR(20) NOT NULL,
  `Name` VARCHAR(50) NULL DEFAULT NULL,
  `MaintenanceDate` DATETIME(6) NOT NULL,
  `Description` VARCHAR(500) NULL DEFAULT NULL,
  `Amount` INT NOT NULL,
  `Price` DECIMAL(65,30) NOT NULL,
  PRIMARY KEY (`Code`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`equipmentsservices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`equipmentsservices` (
  `EquipmentCode` VARCHAR(20) NOT NULL,
  `ServiceCode` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`EquipmentCode`, `ServiceCode`),
  INDEX `IX_EquipmentsServices_ServiceCode` (`ServiceCode` ASC) VISIBLE,
  CONSTRAINT `FK_EquipmentsServices_Equipments_EquipmentCode`
    FOREIGN KEY (`EquipmentCode`)
    REFERENCES `kaizen_test`.`equipments` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_EquipmentsServices_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`notifications`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`notifications` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Title` LONGTEXT NULL DEFAULT NULL,
  `Message` LONGTEXT NULL DEFAULT NULL,
  `Icon` LONGTEXT NULL DEFAULT NULL,
  `State` INT NOT NULL,
  `UserId` VARCHAR(191) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Notifications_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_Notifications_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `kaizen_test`.`aspnetusers` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`productinvoices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`productinvoices` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `State` INT NOT NULL,
  `PaymentMethod` INT NOT NULL,
  `IVA` DECIMAL(65,30) NOT NULL,
  `SubTotal` DECIMAL(65,30) NOT NULL,
  `Total` DECIMAL(65,30) NOT NULL,
  `ClientId` VARCHAR(10) NULL DEFAULT NULL,
  `GenerationDate` DATETIME(6) NOT NULL,
  `PaymentDate` DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  INDEX `IX_ProductInvoices_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_ProductInvoices_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`products` (
  `Code` VARCHAR(15) NOT NULL,
  `Name` VARCHAR(40) NULL DEFAULT NULL,
  `Amount` INT NOT NULL,
  `ApplicationMonths` INT NOT NULL,
  `Description` VARCHAR(350) NULL DEFAULT NULL,
  `Presentation` VARCHAR(50) NULL DEFAULT NULL,
  `Price` DECIMAL(65,30) NOT NULL,
  `HealthRegister` VARCHAR(50) NULL DEFAULT NULL,
  `DataSheet` VARCHAR(50) NULL DEFAULT NULL,
  `SafetySheet` VARCHAR(50) NULL DEFAULT NULL,
  `EmergencyCard` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Code`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`productinvoicedetails`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`productinvoicedetails` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `ProductCode` VARCHAR(15) NULL DEFAULT NULL,
  `ProductName` LONGTEXT NULL DEFAULT NULL,
  `Amount` INT NOT NULL,
  `Total` DECIMAL(65,30) NOT NULL,
  `ProductInvoiceId` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_ProductInvoiceDetails_ProductCode` (`ProductCode` ASC) VISIBLE,
  INDEX `IX_ProductInvoiceDetails_ProductInvoiceId` (`ProductInvoiceId` ASC) VISIBLE,
  CONSTRAINT `FK_ProductInvoiceDetails_ProductInvoices_ProductInvoiceId`
    FOREIGN KEY (`ProductInvoiceId`)
    REFERENCES `kaizen_test`.`productinvoices` (`Id`)
    ON DELETE RESTRICT,
  CONSTRAINT `FK_ProductInvoiceDetails_Products_ProductCode`
    FOREIGN KEY (`ProductCode`)
    REFERENCES `kaizen_test`.`products` (`Code`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`productsservices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`productsservices` (
  `ProductCode` VARCHAR(15) NOT NULL,
  `ServiceCode` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`ServiceCode`, `ProductCode`),
  INDEX `IX_ProductsServices_ProductCode` (`ProductCode` ASC) VISIBLE,
  CONSTRAINT `FK_ProductsServices_Products_ProductCode`
    FOREIGN KEY (`ProductCode`)
    REFERENCES `kaizen_test`.`products` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_ProductsServices_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`sectors`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`sectors` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(40) NOT NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 10
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;

--
-- Dumping data for table `sectors`
--

LOCK TABLES `sectors` WRITE;
/*!40000 ALTER TABLE `sectors` DISABLE KEYS */;
INSERT INTO `sectors` VALUES (1,'Industrial'),(2,'Comercial'),(3,'Alimentos'),(4,'Portuario'),(5,'Hotelero'),(6,'Salud'),(7,'Residencial'),(8,'Educativo'),(9,'Transporte');
/*!40000 ALTER TABLE `sectors` ENABLE KEYS */;
UNLOCK TABLES;

-- -----------------------------------------------------
-- Table `kaizen_test`.`serviceinvoices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`serviceinvoices` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `State` INT NOT NULL,
  `PaymentMethod` INT NOT NULL,
  `IVA` DECIMAL(65,30) NOT NULL,
  `SubTotal` DECIMAL(65,30) NOT NULL,
  `Total` DECIMAL(65,30) NOT NULL,
  `ClientId` VARCHAR(10) NULL DEFAULT NULL,
  `GenerationDate` DATETIME(6) NOT NULL,
  `PaymentDate` DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  INDEX `IX_ServiceInvoices_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_ServiceInvoices_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`serviceinvoicedetails`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`serviceinvoicedetails` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `ServiceCode` VARCHAR(15) NULL DEFAULT NULL,
  `ServiceName` LONGTEXT NULL DEFAULT NULL,
  `Total` DECIMAL(65,30) NOT NULL,
  `ServiceInvoiceId` INT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_ServiceInvoiceDetails_ServiceCode` (`ServiceCode` ASC) VISIBLE,
  INDEX `IX_ServiceInvoiceDetails_ServiceInvoiceId` (`ServiceInvoiceId` ASC) VISIBLE,
  CONSTRAINT `FK_ServiceInvoiceDetails_ServiceInvoices_ServiceInvoiceId`
    FOREIGN KEY (`ServiceInvoiceId`)
    REFERENCES `kaizen_test`.`serviceinvoices` (`Id`)
    ON DELETE RESTRICT,
  CONSTRAINT `FK_ServiceInvoiceDetails_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE RESTRICT)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`servicerequests`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`servicerequests` (
  `Code` INT NOT NULL AUTO_INCREMENT,
  `Date` DATETIME(6) NOT NULL,
  `ClientId` VARCHAR(10) NOT NULL,
  `Periodicity` INT NOT NULL,
  `State` INT NOT NULL DEFAULT '0',
  PRIMARY KEY (`Code`),
  INDEX `IX_ServiceRequests_ClientId` (`ClientId` ASC) VISIBLE,
  CONSTRAINT `FK_ServiceRequests_Clients_ClientId`
    FOREIGN KEY (`ClientId`)
    REFERENCES `kaizen_test`.`clients` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`servicerequestsservices`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`servicerequestsservices` (
  `ServiceRequestCode` INT NOT NULL,
  `ServiceCode` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`ServiceCode`, `ServiceRequestCode`),
  INDEX `IX_ServiceRequestsServices_ServiceRequestCode` (`ServiceRequestCode` ASC) VISIBLE,
  CONSTRAINT `FK_ServiceRequestsServices_ServiceRequests_ServiceRequestCode`
    FOREIGN KEY (`ServiceRequestCode`)
    REFERENCES `kaizen_test`.`servicerequests` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_ServiceRequestsServices_Services_ServiceCode`
    FOREIGN KEY (`ServiceCode`)
    REFERENCES `kaizen_test`.`services` (`Code`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;


-- -----------------------------------------------------
-- Table `kaizen_test`.`workorders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `kaizen_test`.`workorders` (
  `Code` INT NOT NULL AUTO_INCREMENT,
  `WorkOrderState` INT NOT NULL DEFAULT '0',
  `ExecutionDate` DATETIME(6) NOT NULL,
  `ArrivalTime` DATETIME(6) NOT NULL,
  `Validity` DATETIME(6) NOT NULL,
  `Observations` VARCHAR(500) NULL DEFAULT NULL,
  `ClientSignature` TEXT NULL DEFAULT NULL,
  `ActivityCode` INT NOT NULL,
  `EmployeeId` VARCHAR(10) NULL DEFAULT NULL,
  `SectorId` INT NOT NULL,
  `DepartureTime` DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Code`),
  UNIQUE INDEX `AK_WorkOrders_ActivityCode` (`ActivityCode` ASC) VISIBLE,
  INDEX `IX_WorkOrders_EmployeeId` (`EmployeeId` ASC) VISIBLE,
  INDEX `IX_WorkOrders_SectorId` (`SectorId` ASC) VISIBLE,
  CONSTRAINT `FK_WorkOrders_Activities_ActivityCode`
    FOREIGN KEY (`ActivityCode`)
    REFERENCES `kaizen_test`.`activities` (`Code`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_WorkOrders_Employees_EmployeeId`
    FOREIGN KEY (`EmployeeId`)
    REFERENCES `kaizen_test`.`employees` (`Id`)
    ON DELETE RESTRICT,
  CONSTRAINT `FK_WorkOrders_Sectors_SectorId`
    FOREIGN KEY (`SectorId`)
    REFERENCES `kaizen_test`.`sectors` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_unicode_ci;
