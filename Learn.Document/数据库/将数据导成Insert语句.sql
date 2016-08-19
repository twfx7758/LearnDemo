CREATE PROCEDURE   [dbo].[outputdata]   
  @tableName Varchar(100)--表名
AS
    DECLARE @IsIdentity INT
    DECLARE @columnName VARCHAR(100)--列名
    DECLARE @TypeName VARCHAR(100)--数据类型
    DECLARE @columns VARCHAR(8000)--
    DECLARE @columnsAndhead VARCHAR(8000)--
     
    SET @columnsAndhead = 'INSERT INTO '+@tableName+'(['
    SET @columns = ''
     
    --获取表的所有字段名称
    DECLARE a CURSOR FOR 
        SELECT COLUMNPROPERTY( a.id,a.name,'IsIdentity') AS IsIdentity, a.[NAME] AS ColumnName ,b.[NAME] AS TypeName 
        FROM syscolumns a INNER JOIN systypes b ON a.xtype=b.xtype AND b.xtype=b.xusertype 
        WHERE a.[id]=(SELECT [id] FROM sysobjects WHERE [NAME]=@tableName)
     
    OPEN a

    FETCH NEXT FROM a INTO @IsIdentity, @columnName ,@TypeName
        WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @IsIdentity =0
                    BEGIN
                        IF @TypeName IN ('bigint','bit','decimal','float','int','money','numeric','real','smallint','smallmoney','tinyint')
                            BEGIN
                                SET @columns = @columns + 'ISNULL(CAST(['+@columnName +'] AS VARCHAR),''NULL'')+'',''+'
                            END
                        ELSE
                            BEGIN
                                SET @columns = @columns+ '''''''''+ ISNULL(CAST(['+@columnName +'] AS VARCHAR(50)),''NULL'')+ ''''''''+'',''+'
                            END
                        SET @columnsAndhead = @columnsAndhead + ''+ @columnName +'],[' 
                    END
                FETCH NEXT FROM a INTO @IsIdentity, @columnName ,@TypeName
             END


    SELECT @columnsAndhead = left(@columnsAndhead,len(@columnsAndhead)-2) +') VALUES('''
    SELECT @columns = left(@columns,len(@columns)-5)

    CLOSE a
    DEALLOCATE a 

    exec('select '''+@columnsAndhead+'+'+@columns +'+'')'' as InsertSQL from '+@tableName)
     
--END