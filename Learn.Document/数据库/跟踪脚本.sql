/****************************************************/
/* Created by: SQL Server 2008 Profiler             */
/* Date: 2014/12/18  15:21:51         */
/****************************************************/

IF OBJECT_ID('') IS NOT NULL
    DROP PROC dbo.sp_perfworkload_trace_start;
GO

CREATE PROC dbo.sp_perfworkload_trace_start
    @dbid AS INT ,
    @tracefile AS NVARCHAR(254) ,
    @TraceID AS INT OUTPUT
AS 
	--创建一个队列
    DECLARE @rc INT
    DECLARE @maxfilesize BIGINT
    SET @maxfilesize = 5 

    EXEC @rc = sp_trace_create @TraceID OUTPUT, 0, N'InsertFileNameHere',
        @maxfilesize, NULL 
    IF ( @rc != 0 )
        GOTO error

	--设置要跟踪的事件
    DECLARE @on BIT
    SET @on = 1
    EXEC sp_trace_setevent @TraceID, 10, 15, @on
    EXEC sp_trace_setevent @TraceID, 10, 16, @on
    EXEC sp_trace_setevent @TraceID, 10, 1, @on
    EXEC sp_trace_setevent @TraceID, 10, 9, @on
    EXEC sp_trace_setevent @TraceID, 10, 17, @on
    EXEC sp_trace_setevent @TraceID, 10, 2, @on
    EXEC sp_trace_setevent @TraceID, 10, 18, @on
    EXEC sp_trace_setevent @TraceID, 10, 11, @on
    EXEC sp_trace_setevent @TraceID, 10, 12, @on
    EXEC sp_trace_setevent @TraceID, 10, 13, @on
    EXEC sp_trace_setevent @TraceID, 10, 14, @on
    EXEC sp_trace_setevent @TraceID, 45, 16, @on
    EXEC sp_trace_setevent @TraceID, 45, 1, @on
    EXEC sp_trace_setevent @TraceID, 45, 9, @on
    EXEC sp_trace_setevent @TraceID, 45, 17, @on
    EXEC sp_trace_setevent @TraceID, 45, 18, @on
    EXEC sp_trace_setevent @TraceID, 45, 11, @on
    EXEC sp_trace_setevent @TraceID, 45, 12, @on
    EXEC sp_trace_setevent @TraceID, 45, 13, @on
    EXEC sp_trace_setevent @TraceID, 45, 14, @on
    EXEC sp_trace_setevent @TraceID, 45, 15, @on
    EXEC sp_trace_setevent @TraceID, 41, 15, @on
    EXEC sp_trace_setevent @TraceID, 41, 16, @on
    EXEC sp_trace_setevent @TraceID, 41, 1, @on
    EXEC sp_trace_setevent @TraceID, 41, 9, @on
    EXEC sp_trace_setevent @TraceID, 41, 17, @on
    EXEC sp_trace_setevent @TraceID, 41, 18, @on
    EXEC sp_trace_setevent @TraceID, 41, 11, @on
    EXEC sp_trace_setevent @TraceID, 41, 12, @on
    EXEC sp_trace_setevent @TraceID, 41, 13, @on
    EXEC sp_trace_setevent @TraceID, 41, 14, @on


	--设置筛选器
    DECLARE @intfilter INT
    DECLARE @bigintfilter BIGINT

	--应用程序名称筛选器
    EXEC sys.sp_trace_setfilter @TraceID, 10, 0, 7, N'SQL Server Profiler%';

	--数据库ID筛选器
    EXEC sys.sp_trace_setfilter @TraceID, 3, 0, 0, @dbid;

	--启动跟踪
    EXEC sp_trace_setstatus @TraceID, 1

	--打印跟踪ID和文件名供以后引用
    PRINT 'Trce ID:' + CAST(@TraceID AS VARCHAR(10)) + ', Trace File:'''
        + @tracefile + '''';
    GOTO finish

    error: 
    PRINT 'Error Code:' + CAST(@rc AS VARCHAR(10));

    finish: 
GO
