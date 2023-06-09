using IrisApp.Services.Implementations;
using IrisApp.Services.Interfaces;
using IrisAppRepository.Domain;
using IrisAppRepository.Implementations;
using IrisAppRepository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IClientInformationRepository, DynamoClientInformationRepository>();
builder.Services.AddTransient<IFileService, S3FileService>();
builder.Services.AddTransient<IProcessFileService, ProcessFileService>();

builder.Services.Configure<AwsSettings>(builder.Configuration.GetSection("AwsSettings"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
