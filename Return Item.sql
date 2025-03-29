IF OBJECT ID('Return_Requests', 'U') IS NOT NULL
	drop table Retuern_Requests

CREATE TABLE Return_Requests(
	id int primary key,
	product varchar(50),
	price varchar(100),
	reason varchar(200),
	approach varchar(50)
)


