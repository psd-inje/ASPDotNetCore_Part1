CREATE TABLE [dbo].[Blogs] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

