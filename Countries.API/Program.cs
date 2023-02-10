using Countries.API;
using Countries.Infrastructure;
using Countries.Infrastructure.Gateway;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration.Get<Config>();
builder.Services.AddCountriesGateway(new CountriesGatewayOptions());
builder.Services.AddCountriesService();
builder.Services.AddDistributedCountriesCache(options =>
{
    options.ConnectionString = config.ConnectionString;
    options.SchemaName = "dbo";
    options.TableName = "CountriesCache";
});
builder.Services.AddInMemoryCountriesCache();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseAuthorization();
app.MapControllers();
app.Run();
