create database DBContact

create table Contact 
( ID int primary key identity not null,
  SSN varchar(32) unique not null,
  FirstName varchar(32) not null,
  LastName varchar(32) not null
 )

insert into Contact
values('19620601-1234', 'Håkan', 'Johansson' ),
	 ('19780805-1234', 'Pontus', 'Wittenmark' ),
	 ('19760809-1234', 'Marilyn', 'Comillas')

select * from Contact
Go

drop table Addresses
create table Address
( ID int primary key identity not null,
  Street varchar(32) not null,
  City varchar(32) not null,
  Zip varchar(32) not null,
  unique(Street, City, Zip))

insert into
	Address
values
	('Borgarfjordsgatan 4', 'Kista', '164 10'),
	('Norgegatan 1', 'Kista', '164 33'),
	('Kungsgatan 58', 'Stockholm', '110 10')

select * from Address
select * from Contact

Go
drop table ContactToAddress
create table ContactToAddress
(
 ID int primary key identity not null,
 ContactID int references Contact(ID) not null,
 AddressID int references Address(ID) not null,
 unique(ContactID, AddressID)
 )

 insert into ContactToAddress
 values(1,1),
 (3,2),
 (2,3)

 select * from ContactToAddress
 
 create table ContactInformation
 (ID int primary key identity not null,
  Info varchar(32) not null unique,
  ContactID int references Contact(ID) null
  )

  insert into ContactInformation
  values('070 464 74 32', 1),
	    ('073 938 44 30', 2),
	    ('072 123 45 67', null)

select * from ContactInformation


GO
create procedure CreateContact @ssn varchar(32), @firstName varchar(32), @lastName varchar(32), @id int output as
begin
	insert into Contact 
	values(@ssn, @firstName, @lastName)
	set @id = SCOPE_IDENTITY()
end
Go

declare @id int
execute CreateContact '19871223-2345', 'Joel', 'Svensson', @id output
Go
create procedure ReadContact @id int as
begin
	select *
	 from Contact
	where ID = @id
end
Go

create procedure UpdateContact @id int,  @ssn varchar(32), @firstName varchar(32), @lastName varchar(32) as
begin
	update Contact Set SSN=@ssn, FirstName=@firstName, LastName=@lastName where ID = @id
end
Go
select * from Contact
Go

create procedure DeleteContact @id int as
begin
	delete from Contact 
	where ID = @id
end
Go
--a procedure that has an output parameter
create procedure CreateAddress @street varchar(32), @city varchar(32), @zip varchar(32), @id int output as
begin
	insert into Address 
	values(@street, @city, @zip)
	set @id = SCOPE_IDENTITY()
end
Go
select * from Address
Go
create procedure ReadAddress @id int as
begin
	select * from Address
	where ID = @id
end
Go

create procedure UpdateAddress @Id int,  @street varchar(32),  @city varchar(32),  @zip varchar(32) as
begin
	update Address set Street = @street, City=@City, Zip=@Zip where ID = @id
end
GO

create procedure DeleteAddress @Id int as
begin
	delete from Address
	where ID=@Id
end
Go

create procedure CreateContactToAddress @contactId int, @addressId int, @id int output as
begin
	insert into ContactToAddress
	values(@contactId, @addressId)
	set @id = SCOPE_IDENTITY()
end
Go

create procedure ReadContactToAddress @id int as
begin
	select * from ContactToAddress
	where ID= @id
end
go


create procedure UpdateContactToAddress @Id int,  @contactId int,  @addressId int as
begin
	update ContactToAddress set ContactID = @contactId, AddressID=@addressId where ID = @id
end
GO

create procedure DeleteContactToAddress @Id int as
begin
	delete from ContactToAddress
	where ID=@Id
end
Go


create procedure CreateContactInformation @info varchar(32), @contactId int, @id int output as
begin
	insert into ContactToAddress
	values(@info, @contactId)
	set @id = SCOPE_IDENTITY()
end
Go

create procedure  ReadContactInformation @id int as
begin
	select * from ContactInformation
	where ID= @id
end
go


create procedure UpdateContactInformation @Id int,  @contactId int,  @addressId int as
begin
	update ContactToAddress set ContactID = @contactId, AddressID=@addressId where ID = @id
end
GO

create procedure DeleteContactInformation @Id int as
begin
	delete from ContactToAddress
	where ID=@Id
end
Go

select * from ContactToAddress
select * from ContactInformation
select * from Contact
select * from Address


