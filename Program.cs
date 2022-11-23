using BeFriendr.Network;
using BeFriendr.Network.Authentication;
using BeFriendr.Network.Authentication.Extensions;
using BeFriendr.Network.Consumers;
using BeFriendr.Network.Middleware;
using BeFriendr.Network.UserProfiles.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddProfileServices(builder.Configuration);
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddDbContext<DbContext, NetworkDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMassTransit(x =>
                {
                    x.AddConsumer<AccountCreatedConsumer, AccountCreatedConsumerDefinition>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("rabbitmq://guest:guest@localhost:5672");
                        cfg.ReceiveEndpoint(e =>
                        {
                            e.ConfigureConsumer<AccountCreatedConsumer>(context);
                        });
                    });                                        
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
