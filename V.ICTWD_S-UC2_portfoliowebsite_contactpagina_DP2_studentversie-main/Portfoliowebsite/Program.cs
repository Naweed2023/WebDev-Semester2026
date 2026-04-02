using Portfoliowebsite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// CORS is only needed if another website/domain calls the API via browser fetch.
// A portfolio contact page normally does not need it.

// builder.Services.AddCors(p => p.AddDefaultPolicy(policy =>
//     policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
//


// Commented becuase for DP2:
// Use DebugEmailSender in Development so the app does not crash (FR03/FR04 testable),
// and use SmtpEmailSender in Production (FR02 configurable recipient via appsettings).

// builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
//
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IEmailSender, DebugEmailSender>();
}
else
{
    builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//
// RECOMMENDED:
// HTTPS redirection is good practice and does not conflict with the requirements.
// Old code had this commented out; now we enable it.
// 
app.UseHttpsRedirection();

//
// OPTIONAL (only if you enable AddCors above):
// app.UseCors();
//

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();