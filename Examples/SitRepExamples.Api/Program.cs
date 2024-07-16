var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddEndpointsApiExplorer()
       .AddSwaggerGen();

builder.UseSitRep();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseEndpointRegistration();

app.Run();
