--we create database  
CREATE DATABASE StoreDB;
GO

use StoreDB;

GO

 --we delete tables in case they exist
DROP TABLE IF EXISTS tblProduct;
 

--we create table tblPizza
 CREATE TABLE tblProduct (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ProductName varchar(50),
	Code varchar(50),
	Amount int,
	Price decimal,
	Stored bit
	 
);

 
  
INSERT INTO tblProduct values('Product1','12345',10,1000,1);
INSERT INTO tblProduct values('Product2','45321',20,2000,1);
INSERT INTO tblProduct values('Product3','09876',25,2500,0);
INSERT INTO tblProduct values('Product4','87432',35,3000,0);