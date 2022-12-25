using Dapper.NodaTime;
using DataStorage;
using DataStorage.Queries;
using Services;
using Services.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<PricesService>();
builder.Services.AddScoped<IPricesService>(provider => 
    new PricesServiceDecorator(provider.GetRequiredService<IPricesService>()));
builder.Services.AddScoped<PriceStreamService>();
builder.Services.AddScoped<IPriceStreamService>(provider => 
    new PriceStreamServiceDecorator(provider.GetRequiredService<IPriceStreamService>()));
builder.Services.AddScoped<IPeriodPriceService, PeriodPriceService>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IPeriodPriceRepository, PeriodPriceRepository>();
builder.Services.AddScoped<IPriceStreamRepository, PriceStreamRepository>();
builder.Services.AddScoped<IDuplicatePriceExistsQuery, DuplicatePriceExistsQuery>();
builder.Services.AddScoped<IDuplicatePeriodPriceExistsQuery, DuplicatePeriodPriceExistsQuery>();
builder.Services.AddScoped<IDuplicatePriceStreamExistsQuery, DuplicatePriceStreamExistsQuery>();
builder.Services.AddScoped<IGetPeriodPriceBySymbolQuery, GetPeriodPriceBySymbolQuery>();
builder.Services.AddScoped<IGetPriceStreamBySymbolQuery, GetPriceStreamBySymbolQuery>();
builder.Services.AddScoped<IGetDateTime, GetDateTime>();
builder.Services.AddScoped<IIsMarketHoursFactory, IsMarketHoursFactory>();

DapperNodaTimeSetup.Register();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();