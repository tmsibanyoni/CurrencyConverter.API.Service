using CurrencyConverter.Repository.Domain;
using CurrencyConverter.Repository.Interface;
using CurrencyConverter.Repository.MySqlRepository;
using CurrencyConverter.Repository.Options;
using CurrencyConverter.Repository.RedisRepository;
using CurrencyConverter.Repository.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisConnectionString = builder.Configuration.GetSection("RedisConfiguration").GetValue<string>("RedisConnectionString");
builder.Services.AddSingleton<string>(redisConnectionString);

builder.Services.AddHttpClient();
builder.Services.AddLogging();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


builder.Services.Configure<OpenExchangeRatesOptions>(builder.Configuration.GetSection("Service:OpenExchangeService"));

builder.Services.AddTransient<IMySqlRepository, MySqlRepository>();
builder.Services.AddTransient<IDomainManager, DomainManager>();
builder.Services.AddSingleton<IRedisRepository, RedisRepository>();
builder.Services.AddSingleton<IOpenExchangeRatesService, OpenExchangeRatesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
});

app.Run();
