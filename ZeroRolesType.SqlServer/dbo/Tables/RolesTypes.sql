CREATE TABLE [dbo].[RolesTypes] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Name]   NVARCHAR (MAX) NULL,
    [Active] BIT            NOT NULL,
);

