using Projects;

var builder = DistributedApplication.CreateBuilder(args);
var webApi = builder.AddProject<SimpleFinance_WebApi>("webapi");

var webApp = builder.AddProject<SimpleFinance_WebApp>("webapp")
    .WithReference(webApi)
    .WaitFor(webApi);


builder.Build().Run();