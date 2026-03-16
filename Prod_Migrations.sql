IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260314052959_InitialCreate'
)
BEGIN
    CREATE TABLE [TransitRoutes] (
        [Id] bigint NOT NULL IDENTITY,
        [RouteName] nvarchar(max) NOT NULL,
        [StartingPoint] nvarchar(max) NOT NULL,
        [Destination] nvarchar(max) NOT NULL,
        [StartingHour] time NOT NULL,
        [EndingHour] time NOT NULL,
        CONSTRAINT [PK_TransitRoutes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260314052959_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260314052959_InitialCreate', N'10.0.5');
END;

COMMIT;
GO

