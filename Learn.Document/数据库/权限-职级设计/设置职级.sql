USE [HMCCRM]
GO
/****** Object:  StoredProcedure [dbo].[P_SetPermissionUserOrganReal]    Script Date: 2016/4/18 15:55:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<quwb/chengqiang>
-- Create date: <2016-04-13>
-- Description:	<返回结果，0、成功，-1、没有此系统下的UserId对应关系，-2、未生成HIERARCHYID>
-- =============================================
ALTER PROCEDURE [dbo].[P_SetPermissionUserOrganReal]
    (
      @ManagServiceUserID BIGINT ,
      @SystemID INT ,
      @ServiceUserID BIGINT ,
      @Result INT = 0 OUTPUT 
    )
AS
    BEGIN
        DECLARE @CrmOrgID HIERARCHYID ,
            @Descendant HIERARCHYID;

		--获取@ServiceUserID在CRM里对应的权限用户表的UserID
        DECLARE @CrmUserID INT = 0;
        SELECT  @CrmUserID = t1.UserID
        FROM    dbo.Permission_UserInfo t1 ( NOLOCK )
                INNER JOIN dbo.Permission_UserRoleReal t2 ( NOLOCK ) ON t1.UserID = t2.UserID
                INNER JOIN dbo.Permission_RoleInfo t3 ( NOLOCK ) ON t2.RoleID = t3.RoleID
                INNER JOIN dbo.Permission_RoleType t4 ( NOLOCK ) ON t3.RoleTypeID = t4.RoleTypeID
        WHERE   OriginUserID = @ServiceUserID
                AND t4.SystemId = @SystemID
                AND t1.IsActive = 1;

		--获取@ManagServiceUserID在CRM里对应的权限用户表的UserID
        DECLARE @CrmManagerUserID INT = 0;
        SELECT  @CrmManagerUserID = t1.UserID
        FROM    dbo.Permission_UserInfo t1 ( NOLOCK )
                INNER JOIN dbo.Permission_UserRoleReal t2 ( NOLOCK ) ON t1.UserID = t2.UserID
                INNER JOIN dbo.Permission_RoleInfo t3 ( NOLOCK ) ON t2.RoleID = t3.RoleID
                INNER JOIN dbo.Permission_RoleType t4 ( NOLOCK ) ON t3.RoleTypeID = t4.RoleTypeID
        WHERE   OriginUserID = @ManagServiceUserID
                AND t4.SystemId = @SystemID
                AND t1.IsActive = 1;

        IF @CrmUserID <= 0 OR @CrmManagerUserID <= 0
            BEGIN
                SET @Result = -1;
                RETURN;
            END;

		--更新已存在职级
        IF EXISTS ( SELECT  *
                    FROM    dbo.Permission_UserOrganReal(NOLOCK)
                    WHERE   CrmUserID = @CrmUserID
                            AND IsActive = 1 )
            BEGIN
				--判断是否有子代
                DECLARE @CurrOrgId HIERARCHYID;
                SELECT  @CurrOrgId = CrmOrgID
                FROM    dbo.Permission_UserOrganReal(NOLOCK)
                WHERE   CrmUserID = @CrmUserID
                        AND IsActive = 1;
				
                IF @CurrOrgId IS NOT NULL
                    BEGIN
                        IF EXISTS ( SELECT  *
                                    FROM    dbo.Permission_UserOrganReal(NOLOCK)
                                    WHERE   CrmOrgID.IsDescendantOf(@CurrOrgId) = 1
                                            AND CrmUserID != @CrmUserID
                                            AND IsActive = 1
											AND CrmOrgID<>@CurrOrgId )
                            BEGIN
                                SET @Result = -3;
                                RETURN;
                            END;
                    END;

                UPDATE  dbo.Permission_UserOrganReal
                SET     IsActive = 0
                WHERE   CrmUserID = @CrmUserID;  
            END;

        --获取此CCUser的对应的职级ID
        SELECT  @CrmOrgID = CrmOrgID
        FROM    dbo.Permission_UserOrganReal (NOLOCK)
        WHERE   CrmUserID = @CrmManagerUserID
                AND SystemID = @SystemID
                AND IsActive = 1;

		--获取用户的职级
        IF @CrmOrgID IS NULL
            BEGIN
                SET @Descendant = HierarchyId::GetRoot();
            END;
        ELSE
            BEGIN
                DECLARE @lc HIERARCHYID;
                SELECT  @lc = MAX(CrmOrgID)
                FROM    Permission_UserOrganReal (NOLOCK)
                WHERE   CrmOrgID.GetAncestor(1) = @CrmOrgID
                        AND SystemID = @SystemID
                        AND IsActive = 1;
                SET @Descendant = @CrmOrgID.GetDescendant(@lc, NULL);
            END;
		
        IF @Descendant IS NULL
            BEGIN
                SET @Result = -2;
                RETURN;
            END;

        INSERT  INTO Permission_UserOrganReal
                ( SystemID ,
                  CrmUserID ,
                  ServiceUserID ,
                  CrmOrgID ,
                  IsActive ,
                  CreateTime ,
                  UpdateTime
                )
        VALUES  ( @SystemID ,
                  @CrmUserID ,
                  @ServiceUserID ,
                  @Descendant ,
                  1 ,
                  GETDATE() ,
                  GETDATE()
                );
		
		SET @Result = 0;
        RETURN;
    END;

