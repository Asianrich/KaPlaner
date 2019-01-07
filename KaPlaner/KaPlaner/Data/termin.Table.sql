CREATE TABLE [termin].[Table]
(
	[Start] DATETIME NOT NULL, 
    [End] DATETIME2 NOT NULL, 
    [Event] CHAR(10) NOT NULL, 
    [Place] CHAR(10) NOT NULL, 
    [Memberslist] INT NOT NULL, 
    [UserID] INT NOT NULL, 
    [Reccurance] CHAR(10) NOT NULL, 
    [Allday] CHAR(10) NOT NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([UserID]) 
)
