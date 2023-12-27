
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/26/2023 08:42:30
-- Generated from EDMX file: C:\Users\ShenJun\Desktop\New_ASPMVC\bookstore\bookstore\Models\bookstoreModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bookstore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Applications];
GO
IF OBJECT_ID(N'[dbo].[BigSales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BigSales];
GO
IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[Categorys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categorys];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Featureds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Featureds];
GO
IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[Members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Members];
GO
IF OBJECT_ID(N'[dbo].[MemberTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemberTypes];
GO
IF OBJECT_ID(N'[dbo].[Modules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modules];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[ProgramTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramTypes];
GO
IF OBJECT_ID(N'[dbo].[Publishers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Publishers];
GO
IF OBJECT_ID(N'[dbo].[Securitys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Securitys];
GO
IF OBJECT_ID(N'[dbo].[Titles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Titles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [department_no] nvarchar(50)  NULL,
    [department_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [language_no] nvarchar(50)  NULL,
    [language_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'MemberTypes'
CREATE TABLE [dbo].[MemberTypes] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [type_no] nvarchar(50)  NULL,
    [type_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Modules'
CREATE TABLE [dbo].[Modules] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_enabled] bit  NOT NULL,
    [module_no] nvarchar(50)  NULL,
    [module_name] nvarchar(50)  NULL,
    [icon_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_enabled] bit  NOT NULL,
    [module_no] nvarchar(50)  NULL,
    [program_no] nvarchar(50)  NULL,
    [program_name] nvarchar(50)  NULL,
    [program_type_no] nvarchar(50)  NULL,
    [area_name] nvarchar(50)  NULL,
    [controller_name] nvarchar(50)  NULL,
    [action_name] nvarchar(50)  NULL,
    [parameter_value] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'ProgramTypes'
CREATE TABLE [dbo].[ProgramTypes] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [type_no] nvarchar(50)  NULL,
    [type_name] nvarchar(50)  NULL,
    [icon_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Titles'
CREATE TABLE [dbo].[Titles] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [title_no] nvarchar(50)  NULL,
    [title_name] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_validate] bit  NOT NULL,
    [user_no] nvarchar(50)  NULL,
    [user_name] nvarchar(50)  NULL,
    [user_password] nvarchar(50)  NULL,
    [department_no] nvarchar(50)  NOT NULL,
    [title_no] nvarchar(50)  NULL,
    [in_date] datetime  NOT NULL,
    [contact_email] nvarchar(50)  NULL,
    [contact_phone] nvarchar(50)  NULL,
    [contact_address] nvarchar(50)  NULL,
    [validate_code] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_validate] bit  NULL,
    [member_no] nvarchar(50)  NULL,
    [member_name] nvarchar(50)  NULL,
    [member_type_no] nvarchar(50)  NULL,
    [member_password] nvarchar(50)  NULL,
    [gender_code] nvarchar(50)  NULL,
    [birth_date] datetime  NULL,
    [contact_email] nvarchar(50)  NULL,
    [contact_phone] nvarchar(50)  NULL,
    [contact_zip] nvarchar(50)  NULL,
    [contact_address] nvarchar(50)  NULL,
    [validate_code] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_validate] bit  NOT NULL,
    [admin_no] nvarchar(50)  NULL,
    [admin_name] nvarchar(50)  NULL,
    [admin_password] nvarchar(50)  NULL,
    [contact_email] nvarchar(250)  NULL,
    [validate_code] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Securitys'
CREATE TABLE [dbo].[Securitys] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [account_no] nvarchar(50)  NULL,
    [module_no] nvarchar(50)  NULL,
    [program_no] nvarchar(50)  NULL,
    [is_add] bit  NOT NULL,
    [is_edit] bit  NOT NULL,
    [is_delete] bit  NOT NULL,
    [is_print] bit  NOT NULL,
    [is_upload] bit  NOT NULL,
    [is_download] bit  NOT NULL,
    [is_confirm] bit  NOT NULL,
    [is_undo] bit  NOT NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Applications'
CREATE TABLE [dbo].[Applications] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [shop_name] nvarchar(50)  NULL,
    [banner_header] nvarchar(250)  NULL,
    [banner_description] nvarchar(max)  NULL,
    [shipping_description] nvarchar(max)  NULL,
    [return_description] nvarchar(max)  NULL,
    [support_description] nvarchar(max)  NULL,
    [contact_address] nvarchar(250)  NULL,
    [contact_tel] nvarchar(250)  NULL,
    [contact_email] nvarchar(250)  NULL,
    [shop_about] nvarchar(max)  NULL
);
GO

-- Creating table 'BigSales'
CREATE TABLE [dbo].[BigSales] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [sort_no] nvarchar(50)  NULL,
    [product_no] nvarchar(50)  NULL,
    [start_time] datetime  NULL,
    [end_time] datetime  NULL,
    [sale_price] int  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Categorys'
CREATE TABLE [dbo].[Categorys] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_enabled] bit  NOT NULL,
    [parentno] nvarchar(50)  NULL,
    [mno] nvarchar(50)  NULL,
    [mname] nvarchar(50)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Featureds'
CREATE TABLE [dbo].[Featureds] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [is_enabled] bit  NOT NULL,
    [sort_no] nvarchar(50)  NULL,
    [product_no] nvarchar(50)  NULL
);
GO

-- Creating table 'Publishers'
CREATE TABLE [dbo].[Publishers] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [publisher_no] nvarchar(50)  NULL,
    [publisher_name] nvarchar(50)  NULL,
    [boss_name] nvarchar(50)  NULL,
    [contact_name] nvarchar(50)  NULL,
    [company_tel] nvarchar(50)  NULL,
    [contact_tel] nvarchar(50)  NULL,
    [company_addr] nvarchar(250)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [rowid] int IDENTITY(1,1) NOT NULL,
    [book_no] nvarchar(50)  NULL,
    [isbn_no] nvarchar(50)  NULL,
    [book_name] nvarchar(250)  NULL,
    [author_name] nvarchar(50)  NULL,
    [publish_date] datetime  NOT NULL,
    [publisher_no] nvarchar(50)  NULL,
    [language_no] nvarchar(50)  NULL,
    [category_no] nvarchar(50)  NULL,
    [sale_price] int  NOT NULL,
    [content_text] nvarchar(max)  NULL,
    [detail_text] nvarchar(max)  NULL,
    [remark] nvarchar(250)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [rowid] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'MemberTypes'
ALTER TABLE [dbo].[MemberTypes]
ADD CONSTRAINT [PK_MemberTypes]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Modules'
ALTER TABLE [dbo].[Modules]
ADD CONSTRAINT [PK_Modules]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'ProgramTypes'
ALTER TABLE [dbo].[ProgramTypes]
ADD CONSTRAINT [PK_ProgramTypes]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Titles'
ALTER TABLE [dbo].[Titles]
ADD CONSTRAINT [PK_Titles]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Securitys'
ALTER TABLE [dbo].[Securitys]
ADD CONSTRAINT [PK_Securitys]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [PK_Applications]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'BigSales'
ALTER TABLE [dbo].[BigSales]
ADD CONSTRAINT [PK_BigSales]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Categorys'
ALTER TABLE [dbo].[Categorys]
ADD CONSTRAINT [PK_Categorys]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Featureds'
ALTER TABLE [dbo].[Featureds]
ADD CONSTRAINT [PK_Featureds]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Publishers'
ALTER TABLE [dbo].[Publishers]
ADD CONSTRAINT [PK_Publishers]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- Creating primary key on [rowid] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([rowid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------