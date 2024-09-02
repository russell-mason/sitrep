var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddRazorComponents()
       .AddInteractiveServerComponents();

builder.Services.AddExamples();

builder.Services
       .AddSitrep(optionsBuilder => optionsBuilder.UseRedisStackTicketStore()
                                                  .UseCoreApiClient());

builder.Services.AddSingleton<ISignalRTicketHubConnector, SignalRTicketHubConnector>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
