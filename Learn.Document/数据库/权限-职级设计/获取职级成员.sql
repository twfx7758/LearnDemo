USE [HMCCRM]
GO
/****** Object:  StoredProcedure [dbo].[P_GetPermissionSeniorLeader]    Script Date: 2016/4/18 15:56:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		quwb
-- Create date: 2016-4-14
-- Description:	获取用户的直属上级
-- =============================================
ALTER PROC [dbo].[P_GetPermissionSeniorLeader]
    @ServiceUserID BIGINT ,
    @SystemID INT
AS
    BEGIN
        DECLARE @CurrOrgId HIERARCHYID;
        DECLARE @CrmUserID INT = 0;
        DECLARE @CurrentuserLevel INT;
        SELECT  @CrmUserID = t1.UserID
        FROM    dbo.Permission_UserInfo t1 ( NOLOCK )
                INNER JOIN dbo.Permission_UserRoleReal t2 ( NOLOCK ) ON t1.UserID = t2.UserID
                INNER JOIN dbo.Permission_RoleInfo t3 ( NOLOCK ) ON t2.RoleID = t3.RoleID
                INNER JOIN dbo.Permission_RoleType t4 ( NOLOCK ) ON t3.RoleTypeID = t4.RoleTypeID
        WHERE   OriginUserID = @ServiceUserID
                AND t4.SystemId = @SystemID
                AND t1.IsActive = 1;
		
        SELECT  @CurrOrgId = CrmOrgID
        FROM    dbo.Permission_UserOrganReal(NOLOCK)
        WHERE   CrmUserID = @CrmUserID
                AND IsActive = 1;

        SELECT  *
        INTO    #UserOrgan
        FROM    dbo.Permission_UserOrganReal(NOLOCK)
        WHERE   @CurrOrgId.IsDescendantOf(CrmOrgID) = 1
                AND IsActive = 1;
        SELECT  @CurrentuserLevel = CrmOrgID.GetLevel()
        FROM    #UserOrgan;
        IF @CurrentuserLevel > 0
            BEGIN
                SELECT  *
                FROM    #UserOrgan
                WHERE   CrmOrgID.GetLevel() = @CurrentuserLevel - 1;
            END;
        ELSE
            BEGIN
                SELECT  *
                FROM    #UserOrgan
                WHERE   1 = 2;
            END;
        DROP TABLE #UserOrgan;
    END;


