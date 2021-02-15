CREATE USER `kaizen`@`localhost` IDENTIFIED BY 'kaizen';

GRANT USAGE ON *.* TO `kaizen`@`localhost`;

CREATE SCHEMA IF NOT EXISTS `kaizen_test`;

GRANT ALL PRIVILEGES ON `kaizen_test`.* TO `kaizen`@`localhost` WITH GRANT OPTION;
