var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddEndpointsApiExplorer()
       .AddSwaggerGen();

builder.Services.AddExamples();

//builder.Services.AddSitrep(optionsBuilder => optionsBuilder.UseAspNetCore()
//                                                           .UseInMemoryTicketStore()
//                                                           .UseSignalRNotifications());

builder.Services.AddSitrep(optionsBuilder => optionsBuilder.UseAspNetCore()
                                                           .UseRedisStackTicketStore()
                                                           .UseSignalRNotifications());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExamples();

app.UseSitrep(appBuilder => appBuilder.UseSignalRNotifications());

app.Run();
