var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddEndpointsApiExplorer()
       .AddSwaggerGen();

builder.Services.AddExamples();

//builder.Services.AddSitRep(configureOptions => configureOptions.UseAspNetCore().UseInMemoryTicketStore());
builder.Services.AddSitRep(configureOptions => configureOptions.UseAspNetCore().UseRedisStackTicketStore());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExamples();

app.UseSitRep();

app.Run();
