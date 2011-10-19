namespace AutoText
{
    public static class SqlText
    {
        public static string Companies_GetAll = @"
SELECT c.id
, c.[name]
FROM companies c
ORDER BY [name];
";

        public static string Companies_GetAllUsed = @"
SELECT DISTINCT c.id
, c.[name]
FROM companies c
INNER JOIN autotext a ON a.company_id = c.id
AND	LEN(LTRIM(a.[name])) > 0
ORDER BY [name];
";

        public static string Countries_GetAll = @"
SELECT c.id
, c.[name]
FROM countries c
ORDER BY [name];
";

        public static string Countries_GetAllUsed = @"
SELECT DISTINCT c.id
, c.[name]
FROM countries c
INNER JOIN autotext a ON a.country_id = c.id
WHERE (@company_id = -1 OR company_id = @company_id)
AND	LEN(LTRIM(a.[name])) > 0
ORDER BY [name];
";

        public static string Autotext_GetFiltered = @"
SELECT id
, [name]
FROM autotext
WHERE (@company_id = -1 OR company_id = @company_id)
AND (@country_id = -1 OR country_id = @country_id)
AND	LEN(LTRIM([name])) > 0
ORDER BY [name];
";

        public static string Autotext_Exists = @"
SELECT COUNT(id)
FROM autotext
WHERE id=@id;
";

        public static string Autotext_Get = @"
SELECT *
FROM autotext
WHERE id=@id;
";

        public static string Autotext_Update = @"
IF @id < 0
BEGIN 
	INSERT INTO [AutoText]
			   ([company_id]
			   ,[country_id]
			   ,[name]
               ,[autotext]
               ,[created_at]
               ,[created_by]
               ,[updated_at]
               ,[updated_by])
		 VALUES
			   (@company_id
			   ,@country_id
			   ,@name
               ,@autotext
               ,GETDATE()
               ,@user
               ,GETDATE()
               ,@user);
	SET @newid = @@Identity;
END
ELSE
BEGIN
	UPDATE [AutoText]
	   SET [company_id] = @company_id
		  ,[country_id] = @country_id
          ,[name] = @name
		  ,[autotext] = @autotext
          ,[updated_at] = GETDATE()
          ,[updated_by] = @user
	 WHERE id=@id;
	 SET @newid = @id;
END
";

        public static string Autotext_UpdateText = @"
UPDATE [AutoText]
SET [autotext] = @autotext
    ,[updated_at] = GETDATE()
    ,[updated_by] = @user
WHERE id=@id;
";

        public static string Autotext_Delete = @"
DELETE FROM AutoText
WHERE id = @id;
";

    }
}
