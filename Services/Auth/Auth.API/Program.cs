using Auth.API;
using Auth.Persistance;
using FastEndpoints;
using FastEndpoints.Swagger;
using Auth.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiOptions(builder.Configuration);

#region Add Services
builder.Services.ConfigureApiServices(builder.Configuration)
    .ConfigureApplicationServices()
    .ConfigurePersistanceServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Use
app.UseHttpsRedirection()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();
#endregion

app.Run();
