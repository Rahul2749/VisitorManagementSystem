using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Quartz;
using VisitorManagementSystem.Components;
using VisitorManagementSystem.Data;
using VisitorManagementSystem.Jobs;
using VisitorManagementSystem.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


const string CurrentVersion = "VMS.1.0.0";
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = $"auth_token{CurrentVersion}";
        options.LoginPath = "/login";
        //options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.Cookie.MaxAge = TimeSpan.FromDays(1);
        options.AccessDeniedPath = "/accessDenied";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

try
{
    builder.Services.AddDbContext<VMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")), ServiceLifetime.Transient);
}
catch (Exception ex)
{

    throw;
}



try
{
    builder.Services.AddDbContext<NitgenDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("connNitgen")), ServiceLifetime.Transient);
}
catch (Exception ex)
{

    throw;
}



builder.Services.AddScoped<IVDataService, VDataService>();

builder.Services.AddScoped<INitgenService, NitgenService>();

//-----------------------------Scheduler----------------------------

builder.Services.AddScoped<EmailService>();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("DailyEmailJob");
    q.AddJob<DailyEmailJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("EmailJobTrigger")
        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(17, 30)));
}
);
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


//-----------------------------Scheduler----------------------------


builder.Services.AddQuickGridEntityFrameworkAdapter();

//builder.Services.AddMemoryCache();


builder.Services.AddControllers();

//builder.Services.AddHttpClient();

builder.Services.AddHttpClient("MyClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7265/"); // Set your base address here
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<VMSDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Applying database migrations...");

        // Retry logic for database connection
        var maxRetries = 10;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                context.Database.Migrate();
                logger.LogInformation("Database migrations completed successfully.");
                break;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Migration attempt {i + 1} failed: {ex.Message}");
                if (i == maxRetries - 1) throw;
                Thread.Sleep(5000); // Wait 5 seconds before retry
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();





app.Run();
