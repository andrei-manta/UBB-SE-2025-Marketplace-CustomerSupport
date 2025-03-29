-- Create Ratings table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ratings')
BEGIN
    CREATE TABLE Ratings (
        RatingID INT IDENTITY(1,1) PRIMARY KEY,
        UserID VARCHAR(255) NOT NULL,
        Rating DECIMAL(2,1) NOT NULL,
        Comment NVARCHAR(500) NULL,
        Timestamp DATETIME NOT NULL,
        AppVersion VARCHAR(50) NOT NULL
    );
    PRINT 'Ratings table created successfully.';
END
ELSE
BEGIN
    PRINT 'Ratings table already exists.';
END

-- Create HardwareSurvey table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HardwareSurvey')
BEGIN
    CREATE TABLE HardwareSurvey (
        SurveyID INT IDENTITY(1,1) PRIMARY KEY,
        DeviceID VARCHAR(255) NOT NULL,
        DeviceType VARCHAR(50) NOT NULL,
        OperatingSystem VARCHAR(100) NOT NULL,
        OSVersion VARCHAR(50) NOT NULL,
        BrowserName VARCHAR(50) NULL,
        BrowserVersion VARCHAR(50) NULL,
        ScreenResolution VARCHAR(50) NOT NULL,
        AvailableRAM VARCHAR(50) NOT NULL,
        CPUInformation VARCHAR(255) NULL,
        GPUInformation VARCHAR(255) NULL,
        ConnectionType VARCHAR(50) NOT NULL,
        Timestamp DATETIME NOT NULL,
        AppVersion VARCHAR(50) NOT NULL
    );
    PRINT 'HardwareSurvey table created successfully.';
END
ELSE
BEGIN
    PRINT 'HardwareSurvey table already exists.';
END