using Projects;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddProject<SimpleFinance_WebApi>("webapi");


builder.Build().Run();