using CleanArchitecture.API;
using CleanArchitecture.API.Infrastructure.Extensions;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]

var builder = WebApplication.CreateBuilder(args);
SerilogExtension.AddConfiguration(builder);
builder.ConfigureService()
       .Configure();
