     select
    'Integrated Security=True;TrustServerCertificate=True;data source=' + @@servername +
    ';initial catalog=' + db_name() +
    case type_desc
        when 'WINDOWS_LOGIN' 
            then ';trusted_connection=true'
        else
            ';user id=' + suser_name() + ';password=<<YourPassword>>'
    end
    as ConnectionString
    from sys.server_principals
    where name = suser_name() 

GO;

CREATE TABLE UserGetHelpTickets
(
	TicketID INT PRIMARY KEY IDENTITY(1,1),
	UserID VARCHAR(50),
	UserName VARCHAR(50),
	DateAndTime VARCHAR(50),
	Descript VARCHAR(7000),
	Closed VARCHAR(50)
)

GO;