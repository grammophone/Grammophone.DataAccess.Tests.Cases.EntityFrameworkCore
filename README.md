# Grammophone.DataAccess.Tests.Cases.EntityFrameworkCore

Concrete MSTest project for running the shared data access test cases against Entity Framework Core.

This project derives provider-specific test classes from `Grammophone.DataAccess.Tests.Cases` and supplies an `EFCoreMusicDomainContainerAdapter` backed by `EFCoreMusicDomainContainer`. It uses SQL Server LocalDB and a scratch `database` folder for integration testing.

It validates the EF Core implementation from `Grammophone.DataAccess.EntityFrameworkCore`, including standard LINQ preservation, portable async terminal methods, portable query extensions, EF Core proxy-backed entity creation, and SQL Server exception translation.
