
CREATE TABLE Customers 

( 
Cust_ID INT NOT NULL PRIMARY KEY IDENTITY,
First_Name VARCHAR(50) NOT NULL , 
Last_Name VARCHAR(50) NOT NULL ,
Email VARCHAR(50) NOT NULL ,
Address VARCHAR(100) NOT NULL ,
Active BIT NOT NULL,
Created_Date VARCHAR(50) NOT NULL default convert(varchar, getdate(), 3),
Last_Update VARCHAR(50) NOT NULL default convert(varchar, getdate(), 3),
 );

INSERT INTO Customers (First_Name, Last_Name, Email, Address, Active)
VALUES
('Bill', 'Gates', 'billgates@microsoft.com', 'New York, USA', 1),
('Jefferey', 'Epstein', 'epstein@chomo.com', 'Florida, USA', 0),
('Albert', 'Wesker', 'wesker@umbrella.com', 'Spencer Mansion, Arklay Mountains, USA', 1),
('Oda', 'Nobunaga', 'betrayedandroasted@honnoji.com', '522 Shimohonnojimaecho, Nakagyo Ward, Kyoto, 604-8091, Japan', 1);