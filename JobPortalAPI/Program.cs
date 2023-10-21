using JobPortalAPI.Data;
using JobPortalAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("JobPortalDatabase");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSingleton<ApplicationsService>();
builder.Services.AddSingleton<CategoriesService>();
builder.Services.AddSingleton<EmployersJobListingsService>();
builder.Services.AddSingleton<EmployersService>();
builder.Services.AddSingleton<JobCategoryMappingService>();
builder.Services.AddSingleton<JobListingsService>();
builder.Services.AddSingleton<JobSeekerSkillsService>();
builder.Services.AddSingleton<JobSeekersService>();
builder.Services.AddSingleton<MessagesService>();
builder.Services.AddSingleton<NotificationsService>();
builder.Services.AddSingleton<SkillsService>();
builder.Services.AddSingleton<UsersService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Logging.AddDebug();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
