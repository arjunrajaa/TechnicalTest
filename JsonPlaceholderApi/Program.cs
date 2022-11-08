using Accessor;
using Accessor.ApiCaller;
using Common.Constants;
using Manager;
using Manager.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

builder.Services.AddHttpClient(HttpClientConstant.JsonPlaceHolderHttpClient)
    .ConfigureHttpClient(client =>
    {
        if (apiBaseUrl != null) client.BaseAddress = new Uri(apiBaseUrl);
    });

builder.Services.AddScoped<IAlbumManager, AlbumManager>();
builder.Services.AddScoped<IDataAccessor, JsonPlaceholderAccessor>();
builder.Services.AddScoped<IApiCaller, ApiCaller>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();