# CookieAuth

Create simple table with next structure to work with this application

![image](https://user-images.githubusercontent.com/67163370/132915804-0219b22a-5f4f-435a-89ef-83d5f307e421.png)


For DB first you can use next command in PM:

Scaffold-DbContext -Connection "Server=YOUR_SERVER_NAME; Database=YOUR_DB_NAME; Trusted_Connection=True; MultipleActiveResultSets=true;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models" -ContextDir "Data" -Context "ApplicationDbContext" –NoOnConfiguring
