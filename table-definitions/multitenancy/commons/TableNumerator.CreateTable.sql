﻿-- <auto-generated>
--   This code was generated by a tool.
--   Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated.
-- </auto-generated>
-- Tpl: SqlServerCreateTable-1.0.tt
-- Src: TableNumerator.tbl
/*
 * TABLE: [Commons].[TableNumerator]
 *
 * */

-- USE <database-name>
-- GO

-- DROP TABLE [Commons].[TableNumerator]
-- GO

CREATE TABLE [Commons].[TableNumerator]
(
      [Id] NVARCHAR(64) NOT NULL
    , [TenantId] INT NOT NULL
    , [LastValue] BIGINT NOT NULL
    , CONSTRAINT [PK_TableNumerator] PRIMARY KEY NONCLUSTERED ([Id])
)
GO
