using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistance;

var builder = WebApplication.CreateBuilder(args);

#region AddServices
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region UseServices
app.UseSwagger()
    .UseSwaggerUI()
    .UseRouting();

app.MapAllEndpoints();

if (app.Environment.IsDevelopment())
{
    
}

#endregion
app.Run();
