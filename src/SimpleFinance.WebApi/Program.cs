using SimpleFinance.Database.Configuration;
using SimpleFinance.Repository.Configuration;
using SimpleFinance.Services.Configuration;
using SimpleFinance.WebApi.Configuration;
using SimpleFinance.WebApi.Endpoints.Institutions;
using SimpleFinance.WebApi.Endpoints.AccountTypes;
using SimpleFinance.WebApi.Endpoints.Accounts;
using SimpleFinance.WebApi.Endpoints.ExpenseCategories;
using SimpleFinance.WebApi.Endpoints.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointHandlers();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddRepository();
builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapInstitutionEndpoints();
app.MapAccountTypeEndpoints();
app.MapAccountEndpoints();
app.MapExpenseCategoryEndpoints();
app.MapTransactionEndpoints();



app.Run();

