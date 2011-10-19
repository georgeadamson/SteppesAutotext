namespace AutoTextDataMigrator
{
    public static class SqlText
    {

        public static string AutoText_CreateTable = @"
CREATE TABLE [dbo].[autotext](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[autotext] [ntext] NOT NULL,
	[company_id] [int] NOT NULL,
	[country_id] [int] NOT NULL,
    [created_at] [datetime] NOT NULL,
    [created_by] [varchar](50) NOT NULL,
    [updated_at] [datetime] NOT NULL,
    [updated_by] [varchar](50) NOT NULL,
	CONSTRAINT [PK_AutoText] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)
) ON [PRIMARY]
";

        public static string AutoText_BuildPlainTextField = @"
-- IF NOT EXISTS(SELECT * from sys.columns WHERE Name = N'PlainText'   
--             AND Object_ID = Object_ID(N'AutoText')) 
-- BEGIN 
	ALTER TABLE AutoText
	ADD PlainText ntext null;
-- END
";

        public static string AutoText_TotalRecords = @"
SELECT COUNT(*) 
FROM AutoText;
";

        public static string AutoText_GetAll = @"
SELECT autotextid, autotextname, autotext 
FROM AutoText 
ORDER BY AutoTextId;
";

        public static string AutoText_UpdatePlainText = @"
UPDATE AutoText 
SET PlainText = @AutoTextPlainText 
WHERE AutoTextId = @AutoTextId;
";

        public static string AutoText_DeleteAll = @"
DELETE FROM autotext;
";

        public static string AutoText_GetAllForMigration = @"
SELECT autotextid, autotextname, plaintext, countryid, companyid 
FROM AutoText 
ORDER BY AutoTextId;
";

        public static string AutoText_SetIdentityInsertOn = @"
SET IDENTITY_INSERT AutoText ON;
";
  
        public static string AutoText_SetIdentityInsertOff = @"
SET IDENTITY_INSERT AutoText OFF;
";

        public static string AutoText_InsertAutotextElement = @"
INSERT INTO [AutoText]
			   ([id]
               ,[company_id]
			   ,[country_id]
			   ,[name]
               ,[autotext]
               ,[created_at]
               ,[created_by]
               ,[updated_at]
               ,[updated_by])
		 VALUES
			   (@id
               ,@company_id
			   ,@country_id
			   ,@name
               ,@autotext
               ,GETDATE()
               ,'AutoText Data Migrator'
               ,GETDATE()
               ,'AutoText Data Migrator');
";

    }
}
