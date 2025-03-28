    DROP TABLE Conversations;
    DROP TABLE  Messages;

    SELECT * FROM Conversations;
    SELECT * FROM Messages;

    CREATE TABLE Conversations (
        id INT PRIMARY KEY IDENTITY(1,1),
        user1 INT NOT NULL,
        user2 INT NOT NULL
    );

    CREATE TABLE Messages (
        id INT PRIMARY KEY IDENTITY(1,1),
        conversationId INT NOT NULL,
	    creator INT NOT NULL,
	    timestamp BIGINT NOT NULL,
        contentType NVARCHAR(50) NOT NULL,
        content NVARCHAR(MAX) NOT NULL,
    );




         
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