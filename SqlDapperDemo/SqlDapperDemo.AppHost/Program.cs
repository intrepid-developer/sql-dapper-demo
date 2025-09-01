var builder = DistributedApplication.CreateBuilder(args);

// App Host
builder.AddAzureContainerAppEnvironment("aca-host");

// SQL Server
var sql = builder.AddSqlServer("sql", port:51000)
    .WithLifetime(ContainerLifetime.Persistent);
var db = sql.AddDatabase("sql-dapper-demo");

var sqlproj = builder.AddSqlProject<Projects.SqlDapperDemo_Database>("sqlproj")
    .WithConfigureDacDeployOptions(options =>
    {
        options.BlockOnPossibleDataLoss = false;
        options.GenerateSmartDefaults = true;
        options.DeployDatabaseInSingleUserMode = true;
        options.AllowTableRecreation = true;
    })
    .WithReference(db).WaitFor(db);

// API
builder.AddProject<Projects.SqlDapperDemo_Api>("api")
    .WaitForCompletion(sqlproj)
    .WithReference(db).WaitFor(db);

builder.Build().Run();
