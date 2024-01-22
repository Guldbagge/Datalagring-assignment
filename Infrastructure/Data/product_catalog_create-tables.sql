DROP TABLE Products
DROP TABLE Categories

CREATE TABLE Categories 
(
	Id int not null identity primary key,
	CategoryName nvarchar(50) not null unique
)

CREATE TABLE Products
(
	ArticleNumber nvarchar(20) not null primary key,
	Title nvarchar(200) not null,
	Description nvarchar(max) null,
	Price money not null,

	CategoryId int not null references Categories(Id)
)