CREATE TABLE [dbo].[AppUser]  
      (
            Id integer primary key identity(1,1),
            FirstName nvarchar(100) not null,
            LastName nvarchar(100) not null,
            Age int not null
      )