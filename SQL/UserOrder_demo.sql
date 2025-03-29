DROP TABLE Orders;


CREATE TABLE Orders (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    cost FLOAT NOT NULL,
    created BIGINT NOT NULL,
    sellerId INT NOT NULL,
    buyerId INT NOT NULL,
    orderStatus NVARCHAR(50) NOT NULL
);

INSERT INTO Orders(name,description,cost,created,sellerId,buyerId,orderStatus) VALUES (
	'book',
	'a cool book',
	10.5,
	1743210476533,
	1,
	0,
	'shipped'
)

INSERT INTO Orders(name,description,cost,created,sellerId,buyerId,orderStatus) VALUES (
	'car',
	'best car',
	50000.0,
	1743210478533,
	0,
	1,
	'shipping'
)

INSERT INTO Orders(name,description,cost,created,sellerId,buyerId,orderStatus) VALUES (
	'iphone 15 pro max',
	'very good condition iphone',
	800.0,
	1743210486533,
	1,
	0,
	'shipping'
)

# Not "completed" orders


INSERT INTO Orders(name,description,cost,created,sellerId,buyerId,orderStatus) VALUES (
	'bmw m4',
	'50k km , good condition',
	60000.0,
	1743210486533,
	1,
	-1,
	'waiting buyer'
)


INSERT INTO Orders(name,description,cost,created,sellerId,buyerId,orderStatus) VALUES (
	'jordan',
	'limited edition 1/100',
	1500.0,
	1743210496533,
	0,
	-1,
	'waiting buyer'
)



SELECT * FROM Orders;