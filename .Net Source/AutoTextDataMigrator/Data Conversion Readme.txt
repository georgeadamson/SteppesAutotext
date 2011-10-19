Steppes AutoText 
Nick Casey <nickoil@hotmail.com> 2011-10-18


Data Conversion
---------------

1) Run AutoTextDataMigrator either as a built exe or from within the deve environment
2) Set the connection strings for the old and new databases. Use a SQlClient string not a OLE or ODBC client and make sure the user has table creation permissions.
3) Convert the RTF text to plain text by clicking the button. This adds a new field to the AutoText table in the old database.
4) Migrate the data. This copies the plain Autotext from the old database to the new, creating the table in the new as required
5) Check the boxes to stop migrating elements without names or text
6) You can convert the data as many times as you like. Data in the new database table will be overwritten.

N.B. Text is copied pretty much as is but I notice that there seem to be carriage returns pasted after the text in the VB version that are not apparent in the .Net version. If these are required they can be added by uncommenting line 67 in ConvertRTF.cs








