using HowToHideEndpointInSwagger.Conventions;
using HowToHideEndpointInSwagger.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(s =>
s.Conventions.Add(new HideControllerConvention()));
builder.Services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerDocumentFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HowToHideEndpointInSwagger.API", Version = "v1" });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var routeTemplate = apiDesc.RelativePath;
                    if (routeTemplate == "WeatherForecast/GetWeatherForecast")
                        return false;
                    return true;
                });
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    In = ParameterLocation.Header,
                //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    BearerFormat = "JWT",
                //    Scheme = "Bearer"
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id = "Bearer"
                //        }
                //    },
                //        new string[]{}
                //    }
                //});
            });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "HowToHideEndpointInSwagger.API v1"));
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
