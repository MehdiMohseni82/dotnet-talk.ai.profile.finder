using DotNetTalk.AI.Profile.Finder.Gateways.Pg;
using DotNetTalk.AI.Profile.Finder.ML;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmbeddingServiceConfig>(builder.Configuration.GetSection("EmbeddingService"));

// Add services to the container.
builder.Services.AddTransient<IEmbeddingService, EmbeddingService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AiProfileFinderDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("ProfileFinderDb"), o => o.UseVector())
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AiProfileFinderDbContext>();
    db.Database.Migrate();
}

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
