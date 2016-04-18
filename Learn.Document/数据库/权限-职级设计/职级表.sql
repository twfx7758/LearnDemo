CREATE TABLE [dbo].[Permission_UserOrganReal](
	[RelaID] [INT] IDENTITY(1,1) NOT NULL,
	[SystemID] [INT] NULL,
	[CrmUserID] [INT] NULL,
	[ServiceUserID] [BIGINT] NULL,
	[CrmOrgID] [HIERARCHYID] NULL,
	[IsActive] [SMALLINT] NULL,
	[CreateTime] [DATETIME] NULL,
	[UpdateTime] [DATETIME] NULL,
 CONSTRAINT [PK_Permission_UserOrganiationInfo] PRIMARY KEY CLUSTERED 
(
	[RelaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


