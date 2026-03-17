using AxiApi.Interfaces;
using AxiApi.Repositories;
using AxiApi.Services;
using StackExchange.Redis;
using ARMCommon.Interface;
using ARMCommon.Services;
using ARMCommon.Helpers;
using AxExtend.Interface;
using AxiApi.Exceptions;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IGrammarService, GrammarService>();
//builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
//{
//    var configuration = builder.Configuration.GetConnectionString("Redis");
//    return ConnectionMultiplexer.Connect(configuration!);
//});





// Add services to the container.
builder.Services.AddScoped<IGrammarBootstrapService, GrammarBootstrapService>();
builder.Services.AddScoped<IUserFavouriteService, UserFavouritesService>();

builder.Services.AddScoped<IAxExtend, AxExtend.AxExtend>();
/* Repositories */
builder.Services.AddScoped<IGrammarRepository, GrammarRepository>();
builder.Services.AddScoped<IUserFavouritesRepository, UserFavouritesRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); 


/* Controller */
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        policy =>
        {
            policy
                //.WithOrigins("https://localhost:44304")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(); 
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
