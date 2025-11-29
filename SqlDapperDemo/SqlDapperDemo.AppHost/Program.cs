var builder = DistributedApplication.CreateBuilder(args);

// App Host
builder.AddAzureContainerAppEnvironment("aca-host");

// SQL Server
var sql = builder.AddSqlServer("sql", port:51000)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("sql-dapper-demo");

var sqlproj = builder.AddSqlProject<Projects.SqlDapperDemo_Database>("sqlproj")
    .WithConfigureDacDeployOptions(options =>
    {
        options.BlockOnPossibleDataLoss = false;
        options.GenerateSmartDefaults = true;
        options.DeployDatabaseInSingleUserMode = true;
        options.AllowTableRecreation = true;
    })
    .WithReference(sql);

// API
builder.AddProject<Projects.SqlDapperDemo_Api>("api")
    .WaitForCompletion(sqlproj)
    .WithReference(sql).WaitFor(sql);

builder.Build().Run();
