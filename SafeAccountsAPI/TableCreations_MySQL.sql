Create database safeaccountsapi_db;
Use safeaccountsapi_db;

/*drop table accounts;
drop table refreshtokens;
drop table folders;
drop table users;*/

Create Table Users
(
	ID int primary key auto_increment,
	First_Name nvarchar(20),
	Last_Name nvarchar(30),
	Email nvarchar(50) unique,
	Password varbinary(200),
	NumAccs int,
	Role nvarchar(25)
);

Create Table Folders
(
	ID int primary key auto_increment,
    UserID int not null,
    ParentID int,
    HasChild bool not null,
    FolderName nvarchar(50),
    Constraint FK_Folders_UserID foreign key (UserID)
    references Users(ID),
    Constraint FK_Folders_ParentID foreign key (ParentID)
    references Folders(ID)
);

Create Table Accounts
(
	ID int primary key auto_increment,
	UserID int not null,
    FolderID int,
	Title nvarchar(50),
	Login nvarchar(50),
	Password varbinary(200),
	Description nvarchar(250),
    CONSTRAINT FK_Accounts_UserID FOREIGN KEY (UserID)
    REFERENCES Users(ID),
    CONSTRAINT FK_Accounts_FolderID FOREIGN KEY (FolderID)
    REFERENCES Folders(ID)
);

Create Table RefreshTokens
(
	ID int primary key auto_increment,
	UserID int not null,
	Token nvarchar(100),
	Expiration nvarchar(150),
    CONSTRAINT FK_RefTokens_UserID FOREIGN KEY (UserID)
    REFERENCES Users(ID)
);