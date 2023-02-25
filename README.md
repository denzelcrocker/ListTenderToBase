# ListTenderToBase(for me)
 ## In order to connect the entity framework, you need to install the following NuGet packages:

- Microsoft.EntityFrameworkCore.SqlServer (represents Entity Framework functionality for working with MS SQL Server)

- Microsoft.EntityFrameworkCore.Tools (required for creating classes from the database, i.e. reverse engineering)

- Then in the package installation console run the following command
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
