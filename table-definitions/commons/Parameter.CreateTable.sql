﻿-- <auto-generated>
--   This code was generated by a tool.
--   Changes to this file may cause incorrect behaviour and will be lost if the code is regenerated.
-- </auto-generated>
-- Tpl: SqlServerCreateTable-1.0.tt
-- Src: Parameter.tbl
/*
 * TABLE: [Commons].[Parameter]
 *
 * */

-- USE <database-name>
-- GO

-- DROP TABLE [Commons].[Parameter]
-- GO

CREATE TABLE [Commons].[Parameter]
(
      [Id] VARCHAR(128) NOT NULL
    , [Value] NVARCHAR(4000) NULL
    , CONSTRAINT [PK_Parameter] PRIMARY KEY NONCLUSTERED ([Id])
)
GO
