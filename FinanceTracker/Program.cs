using FinanceTracker.Adapters;
using FinanceTracker.Repositories;
using FinanceTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositories — singleton so in-memory data persists across requests
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
builder.Services.AddSingleton<IBudgetRepository, InMemoryBudgetRepository>();

// External adapter — swap this implementation for a real notification service without
// touching business logic (Boundaries / Dependency Inversion)
builder.Services.AddSingleton<INotificationAdapter, ConsoleNotificationAdapter>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<ReportService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
