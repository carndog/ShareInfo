using Dapper.NodaTime;
using DataStorage;
using DataStorage.Queries;
using Services;
using Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.jso.SerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

DapperNodaTimeSetup.Register();

builder.Services.AddScoped<IPricesService, PricesServiceDecorator>();
builder.Services.AddScoped<IPricesService, PricesService>();
builder.Services.AddScoped<IPriceStreamService, PriceStreamServiceDecorator>();
builder.Services.AddScoped<IPriceStreamService, PriceStreamService>();
builder.Services.AddScoped<IPeriodPriceService, PeriodPriceService>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IPeriodPriceRepository, PeriodPriceRepository>();
builder.Services.AddScoped<IPriceStreamRepository, PriceStreamRepository>();
builder.Services.AddScoped<IDuplicatePriceExistsQuery, DuplicatePriceExistsQuery>();
builder.Services.AddScoped<IDuplicatePeriodPriceExistsQuery, DuplicatePeriodPriceExistsQuery>();
builder.Services.AddScoped<IDuplicatePriceStreamExistsQuery, DuplicatePriceStreamExistsQuery>();
builder.Services.AddScoped<IGetPeriodPriceBySymbolQuery, GetPeriodPriceBySymbolQuery>();
builder.Services.AddScoped<IGetPriceStreamBySymbolQuery, IGetPriceStreamBySymbolQuery>();
builder.Services.AddScoped<IGetDateTime, GetDateTime>();
builder.Services.AddScoped<IIsMarketHoursFactory, IsMarketHoursFactory>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();