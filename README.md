Steps to get the program running:

----------------------------------------------------------------------------------------
SQL
----------------------------------------------------------------------------------------
Run the following script on MySql:

Create Databae
CREATE SCHEMA `currencyconverterdb` ;

CREATE TABLE `currencyconverterdb`.`currency` (
  `Id` INT NOT NULL,
  `Base` VARCHAR(10) NULL,
  `Target` VARCHAR(10) NULL,
  `Amount` DECIMAL(18,2) NULL,
  `TargetCurrency` DECIMAL(18,2) NULL,
  `Rate` DECIMAL(18,2) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE);
  
----------------------------------------------------------------------------------------
SECRETS
----------------------------------------------------------------------------------------
ProjectName: CurrencyConverter.API.Service
Add Json to Secret File:

{
  "Service": {
    "OpenExchangeService": {
      "app_id": "93459580428e47259261506c20d7adde"
    }
  }
}

Definition:
app_id: Is the ID used for using the OpenExchangeService API

----------------------------------------------------------------------------------------
UNIT TEST
----------------------------------------------------------------------------------------

Once all configurations are compelete. You can run the UnitTests in the ProjectName.
ProjectName: CurrencyConverter.API.Service.Test

----------------------------------------------------------------------------------------
API Service
----------------------------------------------------------------------------------------
The Api Service has been used to test using Swagger.
Please Restore Nuget Packages before running the application and ensure that the project builds.
You may run the application and test the Endpoints

