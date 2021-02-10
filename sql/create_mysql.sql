CREATE
USER `kaizen`@`localhost` IDENTIFIED BY 'kaizen';

GRANT USAGE ON *.* TO `kaizen`@`localhost`;

CREATE
DATABASE /*!32312 IF NOT EXISTS*/`kaizen_test` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

GRANT ALL PRIVILEGES ON `kaizen_test`.* TO `kaizen`@`localhost` WITH GRANT OPTION;

USE
`kaizen_test`;

/*Table structure for table `__efmigrationshistory` */

DROP TABLE IF EXISTS `__efmigrationshistory`;

CREATE TABLE `__efmigrationshistory`
(
    `MigrationId`    varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `__efmigrationshistory` */

insert into `__efmigrationshistory`(`MigrationId`, `ProductVersion`)
values ('20201017212128_StartNewMigration', '5.0.3'),
       ('20201031215127_FixTypos', '5.0.3'),
       ('20201108181120_RemoveDefaultValues', '5.0.3'),
       ('20201110152513_InvoicePaymentDate', '5.0.3'),
       ('20201110172503_Statistics', '5.0.3'),
       ('20210130173933_Notifications', '5.0.3');

/*Table structure for table `activities` */

DROP TABLE IF EXISTS `activities`;

CREATE TABLE `activities`
(
    `Code`        int         NOT NULL AUTO_INCREMENT,
    `Date`        datetime(6) NOT NULL,
    `ClientId`    varchar(10) NOT NULL,
    `Periodicity` int         NOT NULL,
    `State`       int         NOT NULL DEFAULT '0',
    PRIMARY KEY (`Code`),
    KEY           `IX_Activities_ClientId` (`ClientId`),
    CONSTRAINT `FK_Activities_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `activities` */

/*Table structure for table `activitiesemployees` */

DROP TABLE IF EXISTS `activitiesemployees`;

CREATE TABLE `activitiesemployees`
(
    `ActivityCode` int         NOT NULL,
    `EmployeeId`   varchar(10) NOT NULL,
    PRIMARY KEY (`EmployeeId`, `ActivityCode`),
    KEY            `IX_ActivitiesEmployees_ActivityCode` (`ActivityCode`),
    CONSTRAINT `FK_ActivitiesEmployees_Activities_ActivityCode` FOREIGN KEY (`ActivityCode`) REFERENCES `activities` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_ActivitiesEmployees_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `activitiesemployees` */

/*Table structure for table `activitiesservices` */

DROP TABLE IF EXISTS `activitiesservices`;

CREATE TABLE `activitiesservices`
(
    `ActivityCode` int         NOT NULL,
    `ServiceCode`  varchar(15) NOT NULL,
    PRIMARY KEY (`ActivityCode`, `ServiceCode`),
    KEY            `IX_ActivitiesServices_ServiceCode` (`ServiceCode`),
    CONSTRAINT `FK_ActivitiesServices_Activities_ActivityCode` FOREIGN KEY (`ActivityCode`) REFERENCES `activities` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_ActivitiesServices_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `activitiesservices` */

/*Table structure for table `aspnetroleclaims` */

DROP TABLE IF EXISTS `aspnetroleclaims`;

CREATE TABLE `aspnetroleclaims`
(
    `Id`         int         NOT NULL AUTO_INCREMENT,
    `RoleId`     varchar(50) NOT NULL,
    `ClaimType`  longtext,
    `ClaimValue` longtext,
    PRIMARY KEY (`Id`),
    KEY          `IX_AspNetRoleClaims_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetroleclaims` */

/*Table structure for table `aspnetroles` */

DROP TABLE IF EXISTS `aspnetroles`;

CREATE TABLE `aspnetroles`
(
    `Id`               varchar(50) NOT NULL,
    `Name`             varchar(191) DEFAULT NULL,
    `NormalizedName`   varchar(191) DEFAULT NULL,
    `ConcurrencyStamp` longtext,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetroles` */

insert into `aspnetroles`(`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`)
values ('3bb4b79d-85a4-4a94-b55e-5619c9acf4a2', 'Administrator', 'ADMINISTRATOR',
        '1ed77447-fe5c-42c2-9711-3f91cc103255'),
       ('a988a9ea-c7a5-4329-aceb-3da5016c6a43', 'Client', 'CLIENT', 'fba45aab-42d7-4e12-9dc0-44a2f68badf1'),
       ('e6728857-7423-443f-8228-2c8dd22f3aab', 'TechnicalEmployee', 'TECHNICALEMPLOYEE',
        '501614ae-a5ad-4ee3-ba6f-17c28ab1cd5d'),
       ('e88f6181-e86a-49e1-a2da-c79c71914624', 'OfficeEmployee', 'OFFICEEMPLOYEE',
        '177cda8b-1541-411e-8891-62f58b0e45fa');

/*Table structure for table `aspnetuserclaims` */

DROP TABLE IF EXISTS `aspnetuserclaims`;

CREATE TABLE `aspnetuserclaims`
(
    `Id`         int          NOT NULL AUTO_INCREMENT,
    `UserId`     varchar(191) NOT NULL,
    `ClaimType`  longtext,
    `ClaimValue` longtext,
    PRIMARY KEY (`Id`),
    KEY          `IX_AspNetUserClaims_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserclaims` */

/*Table structure for table `aspnetuserlogins` */

DROP TABLE IF EXISTS `aspnetuserlogins`;

CREATE TABLE `aspnetuserlogins`
(
    `LoginProvider`       varchar(128) NOT NULL,
    `ProviderKey`         varchar(128) NOT NULL,
    `ProviderDisplayName` longtext,
    `UserId`              varchar(191) NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    KEY                   `IX_AspNetUserLogins_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserlogins` */

/*Table structure for table `aspnetuserroles` */

DROP TABLE IF EXISTS `aspnetuserroles`;

CREATE TABLE `aspnetuserroles`
(
    `UserId` varchar(191) NOT NULL,
    `RoleId` varchar(50)  NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    KEY      `IX_AspNetUserRoles_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetuserroles` */

/*Table structure for table `aspnetusers` */

DROP TABLE IF EXISTS `aspnetusers`;

CREATE TABLE `aspnetusers`
(
    `Id`                   varchar(191) NOT NULL,
    `UserName`             varchar(15)  DEFAULT NULL,
    `NormalizedUserName`   varchar(15)  DEFAULT NULL,
    `Email`                varchar(150) DEFAULT NULL,
    `NormalizedEmail`      varchar(150) DEFAULT NULL,
    `EmailConfirmed`       tinyint(1) NOT NULL,
    `PasswordHash`         varchar(191) DEFAULT NULL,
    `SecurityStamp`        longtext,
    `ConcurrencyStamp`     longtext,
    `PhoneNumber`          varchar(10)  DEFAULT NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled`     tinyint(1) NOT NULL,
    `LockoutEnd`           datetime(6) DEFAULT NULL,
    `LockoutEnabled`       tinyint(1) NOT NULL,
    `AccessFailedCount`    int          NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_AspNetUsers_Email` (`Email`),
    UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
    UNIQUE KEY `IX_AspNetUsers_PhoneNumber` (`PhoneNumber`),
    KEY                    `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetusers` */

/*Table structure for table `aspnetusertokens` */

DROP TABLE IF EXISTS `aspnetusertokens`;

CREATE TABLE `aspnetusertokens`
(
    `UserId`        varchar(191) NOT NULL,
    `LoginProvider` varchar(128) NOT NULL,
    `Name`          varchar(128) NOT NULL,
    `Value`         longtext,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `aspnetusertokens` */

/*Table structure for table `clientaddresses` */

DROP TABLE IF EXISTS `clientaddresses`;

CREATE TABLE `clientaddresses`
(
    `Id`           int NOT NULL AUTO_INCREMENT,
    `City`         varchar(40) DEFAULT NULL,
    `Neighborhood` varchar(40) DEFAULT NULL,
    `Street`       varchar(40) DEFAULT NULL,
    `ClientId`     varchar(10) DEFAULT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_ClientAddresses_ClientId` (`ClientId`),
    CONSTRAINT `FK_ClientAddresses_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `clientaddresses` */

/*Table structure for table `clients` */

DROP TABLE IF EXISTS `clients`;

CREATE TABLE `clients`
(
    `Id`                varchar(10) NOT NULL,
    `FirstName`         varchar(20) NOT NULL,
    `SecondName`        varchar(20)          DEFAULT NULL,
    `LastName`          varchar(20) NOT NULL,
    `SecondLastName`    varchar(20)          DEFAULT NULL,
    `UserId`            varchar(191)         DEFAULT NULL,
    `NIT`               varchar(30)          DEFAULT NULL,
    `ClientType`        varchar(20) NOT NULL,
    `TradeName`         varchar(50)          DEFAULT NULL,
    `FirstPhoneNumber`  varchar(10) NOT NULL,
    `SecondPhoneNumber` varchar(10)          DEFAULT NULL,
    `FirstLandLine`     varchar(15)          DEFAULT NULL,
    `SecondLandLine`    varchar(15)          DEFAULT NULL,
    `State`             int         NOT NULL DEFAULT '0',
    `BusinessName`      varchar(50)          DEFAULT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_Clients_NIT` (`NIT`),
    UNIQUE KEY `IX_Clients_UserId` (`UserId`),
    CONSTRAINT `FK_Clients_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `clients` */

/*Table structure for table `contactpeople` */

DROP TABLE IF EXISTS `contactpeople`;

CREATE TABLE `contactpeople`
(
    `Id`          int         NOT NULL AUTO_INCREMENT,
    `Name`        varchar(50) DEFAULT NULL,
    `PhoneNumber` varchar(10) DEFAULT NULL,
    `ClientId`    varchar(10) NOT NULL,
    PRIMARY KEY (`Id`),
    KEY           `IX_ContactPeople_ClientId` (`ClientId`),
    CONSTRAINT `FK_ContactPeople_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `contactpeople` */

/*Table structure for table `daystatistics` */

DROP TABLE IF EXISTS `daystatistics`;

CREATE TABLE `daystatistics`
(
    `Id`                int             NOT NULL AUTO_INCREMENT,
    `AppliedActivities` int             NOT NULL,
    `ClientsVisited`    int             NOT NULL,
    `ClientsRegistered` int             NOT NULL,
    `Profits`           decimal(65, 30) NOT NULL,
    `Date`              datetime(6) NOT NULL,
    `MonthStatisticsId` int DEFAULT NULL,
    PRIMARY KEY (`Id`),
    KEY                 `IX_DayStatistics_MonthStatisticsId` (`MonthStatisticsId`),
    CONSTRAINT `FK_DayStatistics_MonthStatistics_MonthStatisticsId` FOREIGN KEY (`MonthStatisticsId`) REFERENCES `monthstatistics` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `daystatistics` */

/*Table structure for table `employeecharges` */

DROP TABLE IF EXISTS `employeecharges`;

CREATE TABLE `employeecharges`
(
    `Id`     int NOT NULL AUTO_INCREMENT,
    `Charge` varchar(50) DEFAULT NULL,
    PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `employeecharges` */

insert into `employeecharges`(`Id`, `Charge`)
values (1, 'Gerente'),
       (2, 'Coordinador de Calidad y Ambiente'),
       (3, 'Contador'),
       (4, 'Lider SST'),
       (5, 'Auxiliar Administrativa'),
       (6, 'Técnico Operativo'),
       (7, 'Aprendiz');

/*Table structure for table `employeecontract` */

DROP TABLE IF EXISTS `employeecontract`;

CREATE TABLE `employeecontract`
(
    `ContractCode` varchar(30) NOT NULL,
    `StartDate`    datetime(6) NOT NULL,
    `EndDate`      datetime(6) NOT NULL,
    PRIMARY KEY (`ContractCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `employeecontract` */

/*Table structure for table `employees` */

DROP TABLE IF EXISTS `employees`;

CREATE TABLE `employees`
(
    `Id`             varchar(10) NOT NULL,
    `FirstName`      varchar(20) NOT NULL,
    `SecondName`     varchar(20)          DEFAULT NULL,
    `LastName`       varchar(20) NOT NULL,
    `SecondLastName` varchar(20)          DEFAULT NULL,
    `UserId`         varchar(191)         DEFAULT NULL,
    `ChargeId`       int         NOT NULL,
    `ContractCode`   varchar(30)          DEFAULT NULL,
    `State`          int         NOT NULL DEFAULT '0',
    PRIMARY KEY (`Id`),
    UNIQUE KEY `IX_Employees_ContractCode` (`ContractCode`),
    UNIQUE KEY `IX_Employees_UserId` (`UserId`),
    KEY              `IX_Employees_ChargeId` (`ChargeId`),
    CONSTRAINT `FK_Employees_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Employees_EmployeeCharges_ChargeId` FOREIGN KEY (`ChargeId`) REFERENCES `employeecharges` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_EmployeeContract_ContractCode` FOREIGN KEY (`ContractCode`) REFERENCES `employeecontract` (`ContractCode`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `employees` */

/*Table structure for table `employeesservices` */

DROP TABLE IF EXISTS `employeesservices`;

CREATE TABLE `employeesservices`
(
    `EmployeeId`  varchar(10) NOT NULL,
    `ServiceCode` varchar(15) NOT NULL,
    PRIMARY KEY (`EmployeeId`, `ServiceCode`),
    KEY           `IX_EmployeesServices_ServiceCode` (`ServiceCode`),
    CONSTRAINT `FK_EmployeesServices_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_EmployeesServices_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `employeesservices` */

/*Table structure for table `equipments` */

DROP TABLE IF EXISTS `equipments`;

CREATE TABLE `equipments`
(
    `Code`            varchar(20)     NOT NULL,
    `Name`            varchar(50)  DEFAULT NULL,
    `MaintenanceDate` datetime(6) NOT NULL,
    `Description`     varchar(500) DEFAULT NULL,
    `Amount`          int             NOT NULL,
    `Price`           decimal(65, 30) NOT NULL,
    PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `equipments` */

/*Table structure for table `equipmentsservices` */

DROP TABLE IF EXISTS `equipmentsservices`;

CREATE TABLE `equipmentsservices`
(
    `EquipmentCode` varchar(20) NOT NULL,
    `ServiceCode`   varchar(15) NOT NULL,
    PRIMARY KEY (`EquipmentCode`, `ServiceCode`),
    KEY             `IX_EquipmentsServices_ServiceCode` (`ServiceCode`),
    CONSTRAINT `FK_EquipmentsServices_Equipments_EquipmentCode` FOREIGN KEY (`EquipmentCode`) REFERENCES `equipments` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_EquipmentsServices_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `equipmentsservices` */

/*Table structure for table `monthstatistics` */

DROP TABLE IF EXISTS `monthstatistics`;

CREATE TABLE `monthstatistics`
(
    `Id`                int             NOT NULL AUTO_INCREMENT,
    `AppliedActivities` int             NOT NULL,
    `ClientsVisited`    int             NOT NULL,
    `ClientsRegistered` int             NOT NULL,
    `Profits`           decimal(65, 30) NOT NULL,
    `Date`              datetime(6) NOT NULL,
    `YearStatisticsId`  int DEFAULT NULL,
    PRIMARY KEY (`Id`),
    KEY                 `IX_MonthStatistics_YearStatisticsId` (`YearStatisticsId`),
    CONSTRAINT `FK_MonthStatistics_YearStatistics_YearStatisticsId` FOREIGN KEY (`YearStatisticsId`) REFERENCES `yearstatistics` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `monthstatistics` */

/*Table structure for table `notifications` */

DROP TABLE IF EXISTS `notifications`;

CREATE TABLE `notifications`
(
    `Id`      int NOT NULL AUTO_INCREMENT,
    `Title`   longtext,
    `Message` longtext,
    `Icon`    longtext,
    `State`   int NOT NULL,
    `UserId`  varchar(191) DEFAULT NULL,
    PRIMARY KEY (`Id`),
    KEY       `IX_Notifications_UserId` (`UserId`),
    CONSTRAINT `FK_Notifications_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `notifications` */

/*Table structure for table `productinvoicedetails` */

DROP TABLE IF EXISTS `productinvoicedetails`;

CREATE TABLE `productinvoicedetails`
(
    `Id`               int             NOT NULL AUTO_INCREMENT,
    `ProductCode`      varchar(15) DEFAULT NULL,
    `ProductName`      longtext,
    `Amount`           int             NOT NULL,
    `Total`            decimal(65, 30) NOT NULL,
    `ProductInvoiceId` int         DEFAULT NULL,
    PRIMARY KEY (`Id`),
    KEY                `IX_ProductInvoiceDetails_ProductCode` (`ProductCode`),
    KEY                `IX_ProductInvoiceDetails_ProductInvoiceId` (`ProductInvoiceId`),
    CONSTRAINT `FK_ProductInvoiceDetails_ProductInvoices_ProductInvoiceId` FOREIGN KEY (`ProductInvoiceId`) REFERENCES `productinvoices` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_ProductInvoiceDetails_Products_ProductCode` FOREIGN KEY (`ProductCode`) REFERENCES `products` (`Code`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `productinvoicedetails` */

/*Table structure for table `productinvoices` */

DROP TABLE IF EXISTS `productinvoices`;

CREATE TABLE `productinvoices`
(
    `Id`             int             NOT NULL AUTO_INCREMENT,
    `State`          int             NOT NULL,
    `PaymentMethod`  int             NOT NULL,
    `IVA`            decimal(65, 30) NOT NULL,
    `SubTotal`       decimal(65, 30) NOT NULL,
    `Total`          decimal(65, 30) NOT NULL,
    `ClientId`       varchar(10) DEFAULT NULL,
    `GenerationDate` datetime(6) NOT NULL,
    `PaymentDate`    datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
    PRIMARY KEY (`Id`),
    KEY              `IX_ProductInvoices_ClientId` (`ClientId`),
    CONSTRAINT `FK_ProductInvoices_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `productinvoices` */

/*Table structure for table `products` */

DROP TABLE IF EXISTS `products`;

CREATE TABLE `products`
(
    `Code`              varchar(15)     NOT NULL,
    `Name`              varchar(40)  DEFAULT NULL,
    `Amount`            int             NOT NULL,
    `ApplicationMonths` int             NOT NULL,
    `Description`       varchar(350) DEFAULT NULL,
    `Presentation`      varchar(50)  DEFAULT NULL,
    `Price`             decimal(65, 30) NOT NULL,
    `HealthRegister`    varchar(50)  DEFAULT NULL,
    `DataSheet`         varchar(50)  DEFAULT NULL,
    `SafetySheet`       varchar(50)  DEFAULT NULL,
    `EmergencyCard`     varchar(50)  DEFAULT NULL,
    PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `products` */

/*Table structure for table `productsservices` */

DROP TABLE IF EXISTS `productsservices`;

CREATE TABLE `productsservices`
(
    `ProductCode` varchar(15) NOT NULL,
    `ServiceCode` varchar(15) NOT NULL,
    PRIMARY KEY (`ServiceCode`, `ProductCode`),
    KEY           `IX_ProductsServices_ProductCode` (`ProductCode`),
    CONSTRAINT `FK_ProductsServices_Products_ProductCode` FOREIGN KEY (`ProductCode`) REFERENCES `products` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductsServices_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `productsservices` */

/*Table structure for table `sectors` */

DROP TABLE IF EXISTS `sectors`;

CREATE TABLE `sectors`
(
    `Id`   int         NOT NULL AUTO_INCREMENT,
    `Name` varchar(40) NOT NULL,
    PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `sectors` */

insert into `sectors`(`Id`, `Name`)
values (1, 'Industrial'),
       (2, 'Comercial'),
       (3, 'Alimentos'),
       (4, 'Portuario'),
       (5, 'Hotelero'),
       (6, 'Salud'),
       (7, 'Residencial'),
       (8, 'Educativo'),
       (9, 'Transporte');

/*Table structure for table `serviceinvoicedetails` */

DROP TABLE IF EXISTS `serviceinvoicedetails`;

CREATE TABLE `serviceinvoicedetails`
(
    `Id`               int             NOT NULL AUTO_INCREMENT,
    `ServiceCode`      varchar(15) DEFAULT NULL,
    `ServiceName`      longtext,
    `Total`            decimal(65, 30) NOT NULL,
    `ServiceInvoiceId` int         DEFAULT NULL,
    PRIMARY KEY (`Id`),
    KEY                `IX_ServiceInvoiceDetails_ServiceCode` (`ServiceCode`),
    KEY                `IX_ServiceInvoiceDetails_ServiceInvoiceId` (`ServiceInvoiceId`),
    CONSTRAINT `FK_ServiceInvoiceDetails_ServiceInvoices_ServiceInvoiceId` FOREIGN KEY (`ServiceInvoiceId`) REFERENCES `serviceinvoices` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_ServiceInvoiceDetails_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `serviceinvoicedetails` */

/*Table structure for table `serviceinvoices` */

DROP TABLE IF EXISTS `serviceinvoices`;

CREATE TABLE `serviceinvoices`
(
    `Id`             int             NOT NULL AUTO_INCREMENT,
    `State`          int             NOT NULL,
    `PaymentMethod`  int             NOT NULL,
    `IVA`            decimal(65, 30) NOT NULL,
    `SubTotal`       decimal(65, 30) NOT NULL,
    `Total`          decimal(65, 30) NOT NULL,
    `ClientId`       varchar(10) DEFAULT NULL,
    `GenerationDate` datetime(6) NOT NULL,
    `PaymentDate`    datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
    PRIMARY KEY (`Id`),
    KEY              `IX_ServiceInvoices_ClientId` (`ClientId`),
    CONSTRAINT `FK_ServiceInvoices_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `serviceinvoices` */

/*Table structure for table `servicerequests` */

DROP TABLE IF EXISTS `servicerequests`;

CREATE TABLE `servicerequests`
(
    `Code`        int         NOT NULL AUTO_INCREMENT,
    `Date`        datetime(6) NOT NULL,
    `ClientId`    varchar(10) NOT NULL,
    `Periodicity` int         NOT NULL,
    `State`       int         NOT NULL DEFAULT '0',
    PRIMARY KEY (`Code`),
    KEY           `IX_ServiceRequests_ClientId` (`ClientId`),
    CONSTRAINT `FK_ServiceRequests_Clients_ClientId` FOREIGN KEY (`ClientId`) REFERENCES `clients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `servicerequests` */

/*Table structure for table `servicerequestsservices` */

DROP TABLE IF EXISTS `servicerequestsservices`;

CREATE TABLE `servicerequestsservices`
(
    `ServiceRequestCode` int         NOT NULL,
    `ServiceCode`        varchar(15) NOT NULL,
    PRIMARY KEY (`ServiceCode`, `ServiceRequestCode`),
    KEY                  `IX_ServiceRequestsServices_ServiceRequestCode` (`ServiceRequestCode`),
    CONSTRAINT `FK_ServiceRequestsServices_ServiceRequests_ServiceRequestCode` FOREIGN KEY (`ServiceRequestCode`) REFERENCES `servicerequests` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_ServiceRequestsServices_Services_ServiceCode` FOREIGN KEY (`ServiceCode`) REFERENCES `services` (`Code`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `servicerequestsservices` */

/*Table structure for table `services` */

DROP TABLE IF EXISTS `services`;

CREATE TABLE `services`
(
    `Code`          varchar(15)     NOT NULL,
    `Name`          varchar(40) DEFAULT NULL,
    `ServiceTypeId` int             NOT NULL,
    `Cost`          decimal(65, 30) NOT NULL,
    PRIMARY KEY (`Code`),
    KEY             `IX_Services_ServiceTypeId` (`ServiceTypeId`),
    CONSTRAINT `FK_Services_ServiceTypes_ServiceTypeId` FOREIGN KEY (`ServiceTypeId`) REFERENCES `servicetypes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `services` */

/*Table structure for table `servicetypes` */

DROP TABLE IF EXISTS `servicetypes`;

CREATE TABLE `servicetypes`
(
    `Id`   int NOT NULL AUTO_INCREMENT,
    `Name` varchar(70) DEFAULT NULL,
    PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `servicetypes` */

insert into `servicetypes`(`Id`, `Name`)
values (1, 'Control de plagas'),
       (2, 'Desinfección de ambientes y superficies'),
       (3, 'Captura y reubicación de animales'),
       (4, 'Matenimiento de sistemas y equipos'),
       (5, 'Jardinería'),
       (6, 'Suministro, instalación y mantenimiento de equipos');

/*Table structure for table `workorders` */

DROP TABLE IF EXISTS `workorders`;

CREATE TABLE `workorders`
(
    `Code`            int NOT NULL AUTO_INCREMENT,
    `WorkOrderState`  int NOT NULL DEFAULT '0',
    `ExecutionDate`   datetime(6) NOT NULL,
    `ArrivalTime`     datetime(6) NOT NULL,
    `Validity`        datetime(6) NOT NULL,
    `Observations`    varchar(500) DEFAULT NULL,
    `ClientSignature` text,
    `ActivityCode`    int NOT NULL,
    `EmployeeId`      varchar(10)  DEFAULT NULL,
    `SectorId`        int NOT NULL,
    `DepartureTime`   datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
    PRIMARY KEY (`Code`),
    UNIQUE KEY `AK_WorkOrders_ActivityCode` (`ActivityCode`),
    KEY               `IX_WorkOrders_EmployeeId` (`EmployeeId`),
    KEY               `IX_WorkOrders_SectorId` (`SectorId`),
    CONSTRAINT `FK_WorkOrders_Activities_ActivityCode` FOREIGN KEY (`ActivityCode`) REFERENCES `activities` (`Code`) ON DELETE CASCADE,
    CONSTRAINT `FK_WorkOrders_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_WorkOrders_Sectors_SectorId` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `workorders` */

/*Table structure for table `yearstatistics` */

DROP TABLE IF EXISTS `yearstatistics`;

CREATE TABLE `yearstatistics`
(
    `Id`                int             NOT NULL AUTO_INCREMENT,
    `AppliedActivities` int             NOT NULL,
    `ClientsVisited`    int             NOT NULL,
    `ClientsRegistered` int             NOT NULL,
    `Profits`           decimal(65, 30) NOT NULL,
    `Year`              int             NOT NULL,
    `Date`              datetime(6) NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `AK_YearStatistics_Year` (`Year`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `yearstatistics` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
