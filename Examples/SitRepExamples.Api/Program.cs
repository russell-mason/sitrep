var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services
       .AddEndpointsApiExplorer()
       .AddSwaggerGen();

builder.Services.AddSitRep();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSitRep();

app.UseExampleEndpointRegistration();

app.Run();
