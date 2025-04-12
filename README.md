# Notes Applicaton task Full-Stack

asp.NET + Vue Js

## Back-End

BASE_URL

```bash
  http://localhost:5000/api
```

Generate Table in SQL Server

```bash
  USE TestingDb;
  GO
  
  -- Create the Users table
  CREATE TABLE [dbo].[Users] (
      [Id] INT IDENTITY(1,1) PRIMARY KEY,
      [Username] NVARCHAR(255) NOT NULL,
      [Password] NVARCHAR(255) NOT NULL
  );
  GO

  -- Create the Notes table
  CREATE TABLE [dbo].[Notes] (
      [Id] INT IDENTITY(1,1) PRIMARY KEY,
      [Title] NVARCHAR(255) NOT NULL,
      [Content] NVARCHAR(MAX) NULL,    
      [Created_At] DATETIME NOT NULL DEFAULT GETDATE(),  
      [Updated_At] DATETIME NULL,       
      [UserId] INT NULL,
      FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
  );
  GO
```


## Front-End

URL

```bash
  http://localhost:5173/sigin
```

Install dependencies

```bash
  pnpm install
```

Start the web

```bash
  pnpm run dev
```

## Authors

- [@seanghor](https://github.com/Seanghor)
